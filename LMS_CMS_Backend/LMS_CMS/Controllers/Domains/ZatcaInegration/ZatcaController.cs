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

        //#region Generate XML
        //[HttpPost("GenerateXML")]
        ////[Authorize_Endpoint_(
        ////    allowedTypes: new[] { "octa", "employee" },
        ////    pages: new[] { "" }
        ////)]
        //public async Task<IActionResult> GenerateXML(long masterId)
        //{
        //    string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/XML");

        //    if (!Directory.Exists(invoices))
        //    {
        //        Directory.CreateDirectory(invoices);
        //    }

        //    InventoryMaster master = await _unit_Of_Work.inventoryMaster_Repository.FindByIncludesAsync(
        //        x => x.ID == masterId,
        //        query => query.Include(m => m.InventoryDetails),
        //        query => query.Include(m => m.Student));

        //    if (master == null)
        //        return NotFound();

        //    //InventoryMaster master = new();

        //    //string invoiceXmlPath = Path.Combine(invoices, $"INV-001.xml");

        //    bool result = await InvoicingServices.GenerateXML(master);

        //    if (!result)
        //        return BadRequest();

        //    return Ok("Invoice XML created successfully.");
        //}
        //#endregion

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

        //#region Invoice Signing 
        //[HttpPost("InvoiceSigning")]
        ////[Authorize_Endpoint_(
        ////    allowedTypes: new[] { "octa", "employee" },
        ////    pages: new[] { "" }
        ////)]
        //public async Task<IActionResult> InvoiceSigning()
        //{
        //    //string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Services/Invoice");
        //    string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/XML");
        //    string xml = Path.Combine(invoices, "INV001.xml");

        //    string csr = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
        //    string cerPath = Path.Combine(csr, "PCSID.json");
        //    string privateKeyPath = Path.Combine(csr, "PrivateKey.pem");

        //    XmlDocument doc = new XmlDocument();
        //    doc.PreserveWhitespace = true;
        //    doc.Load(xml);

        //    if (!System.IO.File.Exists(xml))
        //        throw new FileNotFoundException();

        //    XmlNamespaceManager nsMgr = new XmlNamespaceManager(doc.NameTable);
        //    nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

        //    string jsonContent = System.IO.File.ReadAllText(cerPath);
        //    dynamic jsonObject = JsonConvert.DeserializeObject(jsonContent);
        //    string base64Cert = jsonObject.binarySecurityToken;

        //    byte[] certBytes = Convert.FromBase64String(base64Cert);
        //    string certDecoded = Encoding.UTF8.GetString(certBytes);

        //    SignResult result = InvoicingServices.InvoiceSigning(xml, cerPath, privateKeyPath);

        //    string invoiceHash = result.Steps.FirstOrDefault(x => x.StepName == "Generate EInvoice Hash").ResultedValue;

        //    string uuid = "afdce130-3dc5-44fb-b494-0c4a9e22343a";
        //    string invoiceEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(doc.InnerXml));

        //    string reporting = await InvoicingServices.InvoiceReporting(invoiceHash, uuid, invoiceEncoded);

        //    return Ok(reporting);
        //}
        //#endregion
    }
}