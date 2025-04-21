using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_PL.Services.Invoice;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
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
        private readonly UOW _unit_Of_Work;
        private readonly IEInvoiceQRGenerator _qRGenerator;
        private readonly IEInvoiceSigner _signer;

        public ZatcaController(ICsrGenerator csrGenerator, UOW unit_of_work, IEInvoiceQRGenerator qRGenerator, IEInvoiceSigner signer)
        {
            _csrGenerator = csrGenerator;
            _unit_Of_Work = unit_of_work;
            _qRGenerator = qRGenerator;
            this._signer = signer;
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

            InvoicingServices invoicingServices = new InvoicingServices();

            return await invoicingServices.GeneratePublicKey(publicKeyPath, privateKeyPath);
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
            CsrResult csr = _csrGenerator.GenerateCsr(csrGeneration, EnvironmentType.Production, true);

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

            InvoicingServices invoicingServices = new InvoicingServices();

            string csid = await invoicingServices.GenerateCSID(csrPath, otp, version);

            // Optional: Prettify the JSON for readability
            dynamic parsedJson = JsonConvert.DeserializeObject(csid);
            string formattedJson = JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);

            // Save as .json instead of .cer
            string csidPath = Path.Combine(invoices, "CSID.json");
            await System.IO.File.WriteAllTextAsync(csidPath, formattedJson);

            return Ok(formattedJson);  // Return the pretty JSON as response too
        }
        #endregion

        #region Generate PCSID
        [HttpPost("GeneratePCSID")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> GeneratePCSID(string version)
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
            string csidPath = Path.Combine(invoices, "CSID.json");

            if (!System.IO.File.Exists(csidPath))
                throw new FileNotFoundException();

            string jsonContent = System.IO.File.ReadAllText(csidPath);
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonContent);

            string user = jsonObject.binarySecurityToken;
            string secret = jsonObject.secret;

            string token = $"{user}:{secret}";
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            string tokenBase64 = Convert.ToBase64String(tokenBytes);

            string requestId = jsonObject.requestID;

            InvoicingServices invoicingServices = new InvoicingServices();

            string pcsid = await invoicingServices.GeneratePCSID(tokenBase64, version, requestId);

            // Optional: Prettify the JSON for readability
            string formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(pcsid), Newtonsoft.Json.Formatting.Indented);


            // Save as .json instead of .cer
            string pcsidPath = Path.Combine(invoices, "PCSID.json");
            await System.IO.File.WriteAllTextAsync(pcsidPath, formattedJson);

            return Ok(formattedJson);  // Return the pretty JSON as response too
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

            //var hash = _hashGenerator.GenerateEInvoiceHashing(doc);

            return "";
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

            //InvoicingServices.GenerateXML(master);

            return Ok("Invoice XML created successfully.");

        }
        #endregion

        #region Generate QR Code 
        [HttpPost("GenerateQRCode")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> GenerateQRCode()
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/Data");
            string xml = Path.Combine(invoices, "INV001.xml");

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(xml);

            if (!System.IO.File.Exists(xml))
                throw new FileNotFoundException();

            var qr = _qRGenerator.GenerateEInvoiceQRCode(doc);



            //return Ok(csid);
            return Ok(qr.QR);
        }
        #endregion

        #region Invoice Signing 
        [HttpPost("InvoiceSigning")]
        //[Authorize_Endpoint_(
        //    allowedTypes: new[] { "octa", "employee" },
        //    pages: new[] { "" }
        //)]
        public async Task<IActionResult> InvoiceSigning()
        {
            //string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Services/Invoice");
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/Data");
            string xml = Path.Combine(invoices, "INV001.xml");

            string csr = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
            string cerPath = Path.Combine(csr, "CSID.json");
            string csrPath = Path.Combine(csr, "CSR.csr");
            string privateKeyPath = Path.Combine(csr, "PrivateKey.pem");

            XmlDocument doc = new XmlDocument();
            doc.Load(xml);

            if (!System.IO.File.Exists(xml))
                throw new FileNotFoundException();

            string csrContent = await System.IO.File.ReadAllTextAsync(csrPath);
            csrContent = csrContent
                .Replace("-----BEGIN CERTIFICATE REQUEST-----", "")
                .Replace("-----END CERTIFICATE REQUEST-----", "")
                .Replace("\n", "")
                .Replace("\r", "");

            string jsonContent = System.IO.File.ReadAllText(cerPath);
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonContent);
            string base64Cert = jsonObject.binarySecurityToken;

            byte[] certBytes = Convert.FromBase64String(base64Cert);
            string certDecoded = Encoding.UTF8.GetString(certBytes);

            string privateKeyContent = System.IO.File.ReadAllText(privateKeyPath);
            privateKeyContent = privateKeyContent
                .Replace("-----BEGIN EC PRIVATE KEY-----", "")
                .Replace("-----END EC PRIVATE KEY-----", "")
                .Replace("\n", "")
                .Replace("\r", "");
            string cert = "MIIB9TCCAZugAwIBAgIGAZY95e+sMAoGCCqGSM49BAMCMBUxEzARBgNVBAMMCmVJbnZvaWNpbmcwHhcNMjUwNDE2MDkxOTU2WhcNMzAwNDE1MjEwMDAwWjBdMQswCQYDVQQGEwJTQTEUMBIGA1UECwwLQWJkZWxyYWhtYW4xFjAUBgNVBAoMDUFiZGVscmFobWFuIEMxIDAeBgNVBAMMF0FiZGVscmFobWFuIEFwcGxpY2F0aW9uMFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEWkIQoB+6exGhWM3iVNzsYdXGWs4cc88ZMu8bSs38NIg34CzO/OuLS5p2nNnHBxHDC9PmkMwotFcjyTJUAjblmKOBkTCBjjAMBgNVHRMBAf8EAjAAMH4GA1UdEQR3MHWkczBxMSAwHgYDVQQEDBcxLURldmljZXwyLTU1NXwzLTk5OTk5OTEfMB0GCgmSJomT8ixkAQEMDzM3NjQ5MjEzNDI1Njc4MzENMAsGA1UEDAwEMDEwMDEOMAwGA1UEGgwFTWFra2ExDTALBgNVBA8MBFRlY2gwCgYIKoZIzj0EAwIDSAAwRQIgfvbpdWHYYfDb5j9ZVuWnMN9lZfQuk4v82mjXaPXptagCIQC+6g9taXZ+2BChZ+LtdLUQ3/yjHzs2ol7MvTci3c75Jw==";
            var signed = _signer.SignDocument(doc, cert, privateKeyContent);
            signed.SaveSignedEInvoice(xml);


            //return Ok(csid);
            return Ok(signed.IsValid);
        }
        #endregion
    }
}