using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_PL.Services.Invoice;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Xml;
using Zatca.EInvoice.SDK.Contracts;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace LMS_CMS_PL.Controllers.Domains.ZatcaInegration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class ZatcaController : ControllerBase
    {
        private readonly ICsrGenerator _csrGenerator;
        private readonly IEInvoiceSigner _invoiceSigner;
        private readonly IEInvoiceHashGenerator _hashGenerator;
        private readonly IEInvoiceQRGenerator _qrGenerator;
        private readonly UOW _unit_Of_Work;

        public ZatcaController(ICsrGenerator csrGenerator, IEInvoiceSigner invoiceSigner, IEInvoiceHashGenerator hashGenerator, UOW unit_of_work, IEInvoiceQRGenerator qrGenerator)
        {
            _csrGenerator = csrGenerator;
            _invoiceSigner = invoiceSigner;
            _hashGenerator = hashGenerator;
            _unit_Of_Work = unit_of_work;
            _qrGenerator = qrGenerator;
        }

        //#region Generate Private Key
        //[HttpPost("GeneratePrivateKey")]
        ////[Authorize_Endpoint_(
        ////    allowedTypes: new[] { "octa", "employee" },
        ////    pages: new[] { "" }
        ////)]
        //public async Task<string> GeneratePrivateKey()
        //{
        //    string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");

        //    if (!Directory.Exists(invoices))
        //    {
        //        Directory.CreateDirectory(invoices);
        //    }

        //    string privateKeyPath = Path.Combine(invoices, "PrivateKey.pem");

        //    return await InvoicingServices.GeneratePrivateKey(privateKeyPath);
        //}
        //#endregion

        #region Generate Public Key
        [HttpPost("GeneratePublicKey")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<string> GeneratePublicKey()
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
            string publicKeyPath = Path.Combine(invoices, "PublicKey.pem");
            string privateKeyPath = Path.Combine(invoices, "PrivateKey.pem");

            return await InvoicingServices.GeneratePublicKey(publicKeyPath, privateKeyPath);
        }
        #endregion

        #region Generate CSR
        [HttpPost("GenerateCSRandPrivateKey")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public IActionResult GenerateCSRandPrivateKey(CsrGenerationDto csrGeneration)
        //public async Task<IActionResult>GenerateCSR()
        {
            CsrResult csr = _csrGenerator.GenerateCsr(csrGeneration, EnvironmentType.NonProduction, true);

            string certificates = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
            string config = Path.Combine(Directory.GetCurrentDirectory(), "Services/Invoice");
            string configPath = Path.Combine(config, "Config.cnf");
            string privateKeyPath = Path.Combine(certificates, "PrivateKey.pem");
            string csrPath = Path.Combine(certificates, "CSR.csr");

            if (!Directory.Exists(certificates))
            {
                Directory.CreateDirectory(certificates);
            }

            csr.SavePrivateKeyToFile(privateKeyPath);
            csr.SaveCsrToFile(csrPath);

            return Ok("CSR an Private Key created successfully.");
           // return Ok(await InvoicingServices.GenerateCSR(csrPath, privateKeyPath, configPath));
        }
        #endregion

        #region Generate CSID
        [HttpPost("GenerateCSID")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> GenerateCSID(long otp, string version)
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
            string csrPath = Path.Combine(invoices, "CSR.csr");

            if (!System.IO.File.Exists(csrPath))
                throw new FileNotFoundException();

            string csid = await InvoicingServices.GenerateCSID(csrPath, otp, version);

            string csidPath = Path.Combine(invoices, "CSID.cer");

            await System.IO.File.WriteAllTextAsync(csidPath, csid);

            return Ok(csid);
        }
        #endregion

        #region Hash Invoice 
        [HttpPost("HashInvoice")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public string HashInvoice()
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Services/Invoice");
            string invoice = Path.Combine(invoices, "temp.xml");

            if (!System.IO.File.Exists(invoice))
                throw new FileNotFoundException();

            XmlDocument doc = new XmlDocument();
            doc.Load(invoice);

            //var ublExt = doc.GetElementsByTagName("ext:UBLExtensions");
            //if (ublExt.Count > 0)
            //{
            //    foreach (XmlNode node in ublExt)
            //    {
            //        node.ParentNode.RemoveChild(node);
            //    }
            //}

            //var c14nTransform = new XmlDsigC14NTransform();
            //c14nTransform.LoadInput(doc);

            //using (var stream = (Stream)c14nTransform.GetOutput(typeof(Stream)))
            //using (var sha256 = SHA256.Create())
            //{
            //    byte[] hashBytes = sha256.ComputeHash(stream);
            //    return Convert.ToBase64String(hashBytes); // OR return BitConverter.ToString(hashBytes).Replace("-", "").ToLower()
            //}


            //var hash = InvocingServices.HashInvoice(doc);

            //if (!hash.IsValid)
            //{
            //    return hash.ErrorMessage;
            //}

            var hash = _hashGenerator.GenerateEInvoiceHashing(doc);

            return hash.Hash;
        }
        #endregion

        #region Invoice Compliance 
        [HttpPost("InvoiceCompliance")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> InvoiceCompliance(string version)
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
            string csidPath = Path.Combine(invoices, "CSID.json");

            if (!System.IO.File.Exists(csidPath))
                throw new FileNotFoundException();

            //var hash = InvoicingServices.HashInvoice(doc);

            //var result = await InvoicingServices.InvoiceCompliance(csidPath, hash, version);

            //return Ok(csid);
            return Ok("");
        }
        #endregion

        #region Generate XML
        [HttpPost("GenerateXML")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public IActionResult GenerateXML()
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/Data");

            if (!Directory.Exists(invoices))
            {
                Directory.CreateDirectory(invoices);
            }

            //InventoryMaster master = await _unit_Of_Work.inventoryMaster_Repository.FindByIncludesAsync(
            //    x => x.ID == id,
            //    query => query.Include(m => m.InventoryDetails),
            //    query => query.Include(m => m.Student));

            //if (master == null)
            //    return NotFound();

            InventoryMaster master = new();

            //string invoiceXmlPath = Path.Combine(invoices, $"INV-001.xml");

            InvoicingServices.GenerateXML(master);

            return Ok("Invoice XML created successfully.");

        }
        #endregion

        //#region Generat Request
        //[HttpPost("GenerateRequest")]
        ////[Authorize_Endpoint_(
        ////    allowedTypes: new[] { "octa", "employee" },
        ////    pages: new[] { "" }
        ////)]
        //public string GenerateRequest()
        //{
        //    string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/Data");
        //    string xmlPath = Path.Combine(invoices, "INV001.xml");

        //    if (!System.IO.File.Exists(xmlPath))
        //        throw new FileNotFoundException();

        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(xmlPath);

        //    QRResult hash = _qrGenerator.GenerateEInvoiceQRCode(xmlDoc);

        //    return hash.QR;
        //}
        //#endregion

        #region Generate XML With UBL
        [HttpPost("GenerateXMLWithUBL")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public IActionResult GenerateXMLWithUBL(long id)
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/Data");
            string invoice = Path.Combine(invoices, "INV001.xml");

            //string csr = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR/TaxPayer.csr");
            //string certificateContent = System.IO.File.ReadAllText(csr);

            //string privateKey = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR/PrivateKey.pem");
            //string privateKeyContent = System.IO.File.ReadAllText(privateKey);
            //privateKey.Replace("-----BEGIN EC PRIVATE KEY-----", "");
            //privateKey.Replace("-----END EC PRIVATE KEY-----", "");

            //if (!Directory.Exists(invoices))
            //{
            //    Directory.CreateDirectory(invoices);
            //}

            //InventoryMaster master = await _unit_Of_Work.inventoryMaster_Repository.FindByIncludesAsync(
            //    x => x.ID == id,
            //    query => query.Include(m => m.InventoryDetails),
            //    query => query.Include(m => m.Student));

            //if (master == null)
            //    return NotFound();

            InventoryMaster master = new();

            //string invoiceXmlPath = Path.Combine(invoices, $"INV-001.xml");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(invoice);

            InvoicingServices.GenerateXMLWithUBL(xmlDoc, invoice);

            //var result = _invoiceSigner.SignDocument(xmlDoc, certificateContent, privateKeyContent);

            return Ok();

        }
        #endregion

        //#region Sign XML
        //[HttpPost("SignXML")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        //public IActionResult SignXML()
        //{
        //    string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
        //    string csrPath = Path.Combine(invoices, "textbuyer.csr");
        //    string privateKeyPath = Path.Combine(invoices, "privateKey.pem");
        //    string xmlPath = "\"E:\\هيئة الزكاة والدخل\\zatca-einvoicing-sdk-DotNet-238-R3.3.9\\Data\\Samples\\Simplified\\Invoice\\Simplified_Invoice.xml\"";

        //    string certificateContent = System.IO.File.ReadAllText(csrPath);
        //    string privateKeyContent = System.IO.File.ReadAllText(privateKeyPath);

        //    try
        //    {
        //        XmlDocument xml = new XmlDocument();

        //        xml.Load(xmlPath);

        //        SignResult xx = _invoiceSigner.SignDocument(
        //            xml,
        //            certificateContent,
        //            privateKeyContent);

        //        string jsonString = JsonConvert.SerializeXmlNode(xx.SignedEInvoice, Newtonsoft.Json.Formatting.Indented);

        //        return Ok(xx.IsValid ? jsonString : xx.ErrorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //}
        //#endregion
    }
}

//#region Generate Private Key
//[HttpPost("GeneratePrivateKey")]
////[Authorize_Endpoint_(
////    allowedTypes: new[] { "octa", "employee" },
////    pages: new[] { "" }
////)]
//public IActionResult GeneratePrivateKey()
//{
//    string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
//    string privateKeyPath = Path.Combine(invoices, "PrivateKey.pem");

//    return Ok(CSRServices.GeneratePrivateKey(privateKeyPath));
//}
//#endregion

//#region Generate Public Key
//[HttpPost("GeneratePublicKey")]
////[Authorize_Endpoint_(
////    allowedTypes: new[] { "octa", "employee" },
////    pages: new[] { "" }
////)]
//public IActionResult GeneratePublicKey()
//{
//    string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
//    string publicKeyPath = Path.Combine(invoices, "PublicKey.pem");
//    string privateKeyPath = Path.Combine(invoices, "PrivateKey.pem");

//    if (!Directory.Exists(invoices))
//    {
//        Directory.CreateDirectory(invoices);
//    }

//    return Ok(CSRServices.GeneratePublicKey(publicKeyPath, privateKeyPath));
//}
//#endregion
