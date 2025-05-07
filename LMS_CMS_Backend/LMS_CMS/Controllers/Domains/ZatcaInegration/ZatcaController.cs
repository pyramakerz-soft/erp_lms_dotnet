using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_DAL.Models.Domains.Zatca;
using LMS_CMS_PL.Services;
using LMS_CMS_PL.Services.Invoice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IConfiguration _configuration;

        public ZatcaController(ICsrGenerator csrGenerator, DbContextFactoryService dbContextFactory, IConfiguration configuration)
        {
            _csrGenerator = csrGenerator;
            _dbContextFactory = dbContextFactory;
            _configuration = configuration;
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
            string csr = Path.Combine(invoices, $"PC-{schoolPc.ID}-{schoolPc.School.ID}");
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

            string csid = await InvoicingServices.GenerateCSID(csrPath, otp, version, _configuration);

            dynamic csidJson = JsonConvert.DeserializeObject(csid);
            string formattedCsid = JsonConvert.SerializeObject(csidJson, Formatting.Indented);
            await System.IO.File.WriteAllTextAsync(csidPath, formattedCsid);

            string user = csidJson.binarySecurityToken;
            string secret = csidJson.secret;

            string token = $"{user}:{secret}";
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            string tokenBase64 = Convert.ToBase64String(tokenBytes);

            string requestId = csidJson.requestID.ToString();

            string pcsid = await InvoicingServices.GeneratePCSID(tokenBase64, version, requestId, _configuration);

            string formattedPcsid = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(pcsid), Formatting.Indented);

            await System.IO.File.WriteAllTextAsync(pcsidPath, formattedPcsid);

            string certificateDate = InvoicingServices.GetCertificateDate(pcsid);

            schoolPc.CertificateDate = DateOnly.Parse(certificateDate);

            Unit_Of_Work.schoolPCs_Repository.Update(schoolPc);
            Unit_Of_Work.SaveChanges();

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

            string csid = await InvoicingServices.GenerateCSID(csrPath, otp, version, _configuration);

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

            string pcsid = await InvoicingServices.UpdatePCSID(tokenBase64, csrContentEncoded, version, otp.ToString(), _configuration);

            string formattedPcsid = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(pcsid), Newtonsoft.Json.Formatting.Indented);

            await System.IO.File.WriteAllTextAsync(pcsidPath, formattedPcsid);

            return Ok(formattedPcsid);
        }
        #endregion

        #region Report Invoice
        [HttpPost("ReportInvoice")]
        //#region Report Invoice
        //[HttpPost("ReportInvoice")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> ReportInvoice(long masterId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            InventoryMaster master = await Unit_Of_Work.inventoryMaster_Repository.FindByIncludesAsync(
                d => d.ID == masterId && 
                (d.FlagId == 11 || d.FlagId == 12) && 
                d.IsDeleted != true,
                query => query.Include(s => s.School)
            );

            if (master is null)
                return NotFound("Invoice not found.");

            DateTime invDate = DateTime.Parse(master.Date);
            string date = invDate.ToString("yyyy-MM-dd");
            string time = invDate.ToString("HH:mm:ss").Replace(":", "");

            string xmlPath = string.Empty;
            if (master.FlagId == 11)
                xmlPath = Path.Combine(Directory.GetCurrentDirectory(), $"Invoices/XMLInvoices/{master.School.CRN}_{date.Replace("-", "")}T{time}_{date}-{master.StoreID}_{master.FlagId}_{master.ID}.xml");

            if (master.FlagId == 12)
                xmlPath = Path.Combine(Directory.GetCurrentDirectory(), $"Invoices/XMLCredits/{master.School.CRN}_{date.Replace("-", "")}T{time}_{date}-{master.StoreID}_{master.FlagId}_{master.ID}.xml");

            if (master.IsValid == 0 || master.IsValid == null)
            {
                HttpResponseMessage response = await InvoicingServices.InvoiceReporting(xmlPath, master.InvoiceHash, master.uuid, (long)master.SchoolPCId, (long)master.SchoolId, _configuration);

                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic responseJson = JsonConvert.DeserializeObject(responseContent);
                master.Status = responseJson.reportingStatus;

                if (response.IsSuccessStatusCode)
                {
                    master.IsValid = 1;
                }
                else
                {
                    master.IsValid = 0;
                }

                Unit_Of_Work.inventoryMaster_Repository.Update(master);
                Unit_Of_Work.SaveChanges();
            }

            return master.IsValid == 1 ? Ok(master.Status) : BadRequest(master.Status);
        }
        #endregion

        #region Report Invoices
        [HttpPost("ReportInvoices")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> ReportInvoices(long schoolId, long schoolPcId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<InventoryMaster> masters = await Unit_Of_Work.inventoryMaster_Repository.Select_All_With_IncludesById<List<InventoryMaster>>(
                d => d.SchoolId == schoolId && 
                d.SchoolPCId == schoolPcId && 
                (d.FlagId == 11 || d.FlagId == 12) && 
                d.IsDeleted != true,
                query => query.Include(s => s.School)
            );

            if (masters is null || masters.Count == 0)
                return NotFound("No invoices found.");

            foreach (var master in masters)
            {
                if (master.IsValid == 0 || master.IsValid == null)
                {
                    try
                    {
                        DateTime invDate = DateTime.Parse(master.Date);
                        string date = invDate.ToString("yyyy-MM-dd");
                        string time = invDate.ToString("HH:mm:ss").Replace(":", "");

                        string csr = Path.Combine(Directory.GetCurrentDirectory(), $"Invoices/CSR/PC-{master.SchoolPCId}-{master.SchoolId}");
                        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), $"Invoices/XML/{master.School.CRN}_{date.Replace("-", "")}T{time}_{date}-{master.ID}.xml");

                        HttpResponseMessage response = await InvoicingServices.InvoiceReporting(xmlPath, master.InvoiceHash, master.uuid, (long)master.SchoolPCId, (long)master.SchoolId, _configuration);

                        string responseContent = await response.Content.ReadAsStringAsync();
                        dynamic responseJson = JsonConvert.DeserializeObject(responseContent);
                        master.Status = responseJson.reportingStatus;

                        if (response.IsSuccessStatusCode)
                        {
                            master.IsValid = 1;
                        }
                        else
                        {
                            master.IsValid = 0;
                        }
                        Unit_Of_Work.inventoryMaster_Repository.Update(master);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"Error reporting invoice {master.ID}: {ex.Message}");
                    }
                }
            }
            Unit_Of_Work.SaveChanges();

            return Ok();
        }
        #endregion

        #region Filter by School and Date
        [HttpGet("FilterBySchoolAndDate")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> FilterBySchoolAndDate(long schoolId, string startDate, string endDate, int pageNumber, int pageSize)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            DateTime start = DateTime.Parse(startDate).Date;
            DateTime end = DateTime.Parse(endDate).Date;

            //List<InventoryMaster> masters = await Unit_Of_Work.inventoryMaster_Repository.Select_All_With_IncludesById_Pagination<InventoryMaster>(
            //    d => d.SchoolId == schoolId && DateTime.Parse(d.Date).Date == start && DateTime.Parse(d.Date) <= end && d.IsDeleted != true,
            //    query => query.Include(s => s.School)
            //)
            //    .Skip((pageNumber - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();

            List<InventoryMaster> mastersBySchool = await Unit_Of_Work.inventoryMaster_Repository.Select_All_With_IncludesById_Pagination<InventoryMaster>(
                d => d.SchoolId == schoolId && d.IsDeleted != true).ToListAsync();

            if (mastersBySchool is null || mastersBySchool.Count == 0)
                return NotFound("No invoices found.");

            List<InventoryMaster> mastersByDate = mastersBySchool
                .Where(d => DateTime.Parse(d.Date).Date >= start && DateTime.Parse(d.Date).Date <= end)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(mastersByDate);
        }
        #endregion
    }
}