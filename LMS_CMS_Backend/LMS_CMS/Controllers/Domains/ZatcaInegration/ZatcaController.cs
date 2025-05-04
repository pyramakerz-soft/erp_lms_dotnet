using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_DAL.Models.Domains.Zatca;
using LMS_CMS_PL.Services;
using LMS_CMS_PL.Services.Invoice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Text;
using Zatca.EInvoice.SDK.Contracts;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace LMS_CMS_PL.Controllers.Domains.ZatcaInegration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class ZatcaController : ControllerBase
    {
        private readonly ICsrGenerator _csrGenerator;
        private readonly DbContextFactoryService _dbContextFactory;

        public ZatcaController(ICsrGenerator csrGenerator, DbContextFactoryService dbContextFactory)
        {
            _csrGenerator = csrGenerator;
            _dbContextFactory = dbContextFactory;
        }

        #region Generate PCSID
        [HttpPost("GeneratePCSID")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> GeneratePCSID(long otp, long schoolPcId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            SchoolPCs schoolPc = await Unit_Of_Work.schoolPCs_Repository.FindByIncludesAsync(
                d => d.ID == schoolPcId && d.IsDeleted != true,
                query => query.Include(s => s.School)
            );

            if (schoolPc is null)
                return NotFound("School PC not found.");

            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSRs");
            string csr = Path.Combine(invoices, $"PC-{schoolPcId}");
            string privateKeyPath = Path.Combine(csr, "PrivateKey.pem");
            string csrPath = Path.Combine(csr, "CSR.csr");
            string publicKeyPath = Path.Combine(csr, "PublicKey.pem");
            string csidPath = Path.Combine(csr, "CSID.json");
            string pcsidPath = Path.Combine(csr, "PCSID.json");

            if (!Directory.Exists(csr))
            {
                Directory.CreateDirectory(csr);
            }

            string commonName = schoolPc.School.Name;
            string serialNumber = $"1-{schoolPcId}|2-{schoolPc.PCName}|3-{schoolPc.SerialNumber}";
            string organizationIdentifier = schoolPc.School.VatNumber;
            string organizationUnitName = schoolPc.School.Name;
            string organizationName = schoolPc.School.Name;
            string countryName = "SA";
            string invoiceType = "0100";
            string locationAddress = schoolPc.School.City;
            string industryBusinessCategory = "Learning";

            CsrGenerationDto csrGeneration = new(
                commonName, 
                serialNumber, 
                organizationIdentifier, 
                organizationUnitName, 
                organizationName, 
                countryName, 
                invoiceType, 
                locationAddress, 
                industryBusinessCategory
            );

            InvoicingServices.GenerateCSRandPrivateKey(csrGeneration, privateKeyPath, csrPath);

            await InvoicingServices.GeneratePublicKey(publicKeyPath, privateKeyPath);

            string version = "V2";

            string csid = await InvoicingServices.GenerateCSID(csrPath, otp, version);

            dynamic csidJson = JsonConvert.DeserializeObject(csid);
            string formattedCsid = JsonConvert.SerializeObject(csidJson, Newtonsoft.Json.Formatting.Indented);
            await System.IO.File.WriteAllTextAsync(csidPath, formattedCsid);

            string user = csidJson.binarySecurityToken;
            string secret = csidJson.secret;

            string token = $"{user}:{secret}";
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            string tokenBase64 = Convert.ToBase64String(tokenBytes);

            string requestId = csidJson.requestID.ToString();

            string pcsid = await InvoicingServices.GeneratePCSID(tokenBase64, version, requestId);

            string formattedPcsid = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(pcsid), Newtonsoft.Json.Formatting.Indented);

            await System.IO.File.WriteAllTextAsync(pcsidPath, formattedPcsid);

            return Ok(formattedPcsid); 
        }
        #endregion

        #region Update PCSID
        [HttpPost("UpdatePCSID")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> UpdatePCSID(string version, long otp)
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
            string csrPath = Path.Combine(invoices, "CSR.csr");
            string csidPath = Path.Combine(invoices, "CSID.json");
            string pcsidPath = Path.Combine(invoices, "PCSID.json");

            string csid = await InvoicingServices.GenerateCSID(csrPath, otp, version);

            dynamic csidJson = JsonConvert.DeserializeObject(csid);
            string formattedCsid = JsonConvert.SerializeObject(csidJson, Newtonsoft.Json.Formatting.Indented);
            await System.IO.File.WriteAllTextAsync(csidPath, formattedCsid);

            string oldPcsidContent = await System.IO.File.ReadAllTextAsync(pcsidPath);
            dynamic oldPcsidJson = JsonConvert.DeserializeObject(oldPcsidContent);
            string formattedOldPcsid = JsonConvert.SerializeObject(oldPcsidJson, Newtonsoft.Json.Formatting.Indented);

            string user = oldPcsidJson.binarySecurityToken;
            string secret = oldPcsidJson.secret;

            string token = $"{user}:{secret}";
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            string tokenBase64 = Convert.ToBase64String(tokenBytes);

            string csrContent = await System.IO.File.ReadAllTextAsync(csrPath);

            string csrContentEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(csrContent));

            string pcsid = await InvoicingServices.UpdatePCSID(tokenBase64, csrContentEncoded, version, otp.ToString());

            string formattedPcsid = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(pcsid), Newtonsoft.Json.Formatting.Indented);

            await System.IO.File.WriteAllTextAsync(pcsidPath, formattedPcsid);

            return Ok(formattedPcsid);
        }
        #endregion

        #region Report Invoice 
        //[HttpPost("ReportInvoice")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        //public async Task<IActionResult> ReportInvoice()
        //{
            
        //} 
        #endregion 
    }
}