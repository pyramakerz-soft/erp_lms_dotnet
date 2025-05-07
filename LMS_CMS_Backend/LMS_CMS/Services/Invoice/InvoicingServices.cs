using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using LMS_CMS_DAL.Models.Domains.Inventory;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Zatca.EInvoice.SDK.Contracts.Models;
using Zatca.EInvoice.SDK;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using ZXing.Windows.Compatibility;

namespace LMS_CMS_PL.Services.Invoice
{
    public static class InvoicingServices
    {
        public static void GenerateCSRandPrivateKey(CsrGenerationDto csrGeneration, string privateKeyPath, string csrPath)
        {
            CsrGenerator csrGene = new();
            CsrResult csr = csrGene.GenerateCsr(csrGeneration, EnvironmentType.Production, true);
            csr.SavePrivateKeyToFile(privateKeyPath);
            csr.SaveCsrToFile(csrPath);
        }

        public static async Task GeneratePublicKey(string publicKeyPath, string privateKeyPath)
        {
            string opensslCmd = @"C:\Program Files\OpenSSL-Win64\bin\openssl.exe";

            if (File.Exists(privateKeyPath))
            {
                string arguments = "ec -in \"" + privateKeyPath + "\" -pubout -out \"" + publicKeyPath + "\"";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = opensslCmd,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    await process.WaitForExitAsync();
                }
            }
            
        }

        public static async Task<string> GenerateCSID(string csrPath, long OTP, string version, IConfiguration configuration)
        {
            string csrContent = await File.ReadAllTextAsync(csrPath);

            string payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(csrContent));
            var payloadObj = new { csr = payload };
            string jsonPayload = JsonConvert.SerializeObject(payloadObj);

            using (HttpClient client = new HttpClient())
            {
                bool isProduction = configuration.GetValue<bool>("IsProduction");
                HttpRequestMessage request;

                if (isProduction)
                {
                    request = new HttpRequestMessage(HttpMethod.Post,
                        "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/compliance");
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Post,
                        "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance");
                }

                request.Headers.Add("accept", "application/json");
                request.Headers.Add("OTP", OTP.ToString());
                request.Headers.Add("Accept-Version", version);

                request.Content = new StringContent(jsonPayload);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent; 
            }
        }

        public static async Task<string> GeneratePCSID(string securityToken, string version, string requestId, IConfiguration configuration)
        {
            try
            {
                HttpClient client = new HttpClient();

                bool isProduction = configuration.GetValue<bool>("IsProduction");
                HttpRequestMessage request;
                if (isProduction)
                {
                    request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/production/CSIDs");
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/production/csids");
                }

                request.Headers.Add("accept", "application/json");
                request.Headers.Add("Accept-Version", version);
                request.Headers.Add("Authorization", $"Basic {securityToken}");

                request.Content = new StringContent("{\n  \"compliance_request_id\": \"" + requestId + "\"\n}");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody; 
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<HttpResponseMessage> InvoiceCompliance(string xmlPath, string invoiceHash, string uuid, long pcId, long schoolId, IConfiguration configuration)
        {
            string csr = Path.Combine(Directory.GetCurrentDirectory(), $"Invoices/CSRs/PC-{pcId}-{schoolId}");
            string certPath = Path.Combine(csr, "CSID.json");
            string csidContent = await File.ReadAllTextAsync(certPath);

            JObject obj = JObject.Parse(csidContent);
            string token = (string)obj["binarySecurityToken"];
            string secret = (string)obj["secret"];

            string authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(token + ":" + secret));

            HttpClient client = new HttpClient();

            bool isProduction = configuration.GetValue<bool>("IsProduction");
            HttpRequestMessage request;
            if (isProduction)
            {
                request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/compliance/invoices");
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance/invoices");
            }

            string version = "V2";

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(xmlPath);

            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Accept-Language", "en");
            request.Headers.Add("Accept-Version", version);
            request.Headers.Add("Authorization", $"Basic {authorization}");

            string invoiceEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(doc.InnerXml));

            string jsonPayload = $@"
            {{
              ""invoiceHash"": ""{invoiceHash}"",
              ""uuid"": ""{uuid}"",
              ""invoice"": ""{invoiceEncoded}""
            }}";

            request.Content = new StringContent(jsonPayload);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public static bool GenerateInvoiceXML(InventoryMaster master, string lastInvoiceHash)
        {
            string invoices = string.Empty;

            if (master.FlagId == 11)
                invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/XMLInvoices");

            if (master.FlagId == 12)
                invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/XMLCredits");

            string examplePath = Path.Combine(Directory.GetCurrentDirectory(), "Services/Invoice");
            string csr = Path.Combine(Directory.GetCurrentDirectory(), $"Invoices/CSRs/PC-{master.SchoolPCId}-{master.SchoolId}");

            if (!Directory.Exists(invoices))
            {
                Directory.CreateDirectory(invoices);
            }

            DateTime invDate = DateTime.Parse(master.Date);
            
            string date = invDate.ToString("yyyy-MM-dd");
            string time = invDate.ToString("HH:mm:ss");

            string newXmlPath = Path.Combine(invoices, $"{master.School.CRN}_{date.Replace("-", "")}T{time.Replace(":", "")}_{date}-{master.StoreID}_{master.FlagId}_{master.ID}.xml");
            string certPath = Path.Combine(csr, "PCSID.json");
            string privateKeyPath = Path.Combine(csr, "PrivateKey.pem");

            string tempXmlPath = string.Empty;
            if (master.FlagId == 11)
                tempXmlPath = Path.Combine(examplePath, "INV001.xml");

            if (master.FlagId == 12)
                tempXmlPath = Path.Combine(examplePath, "Credit.xml");

            File.Copy(tempXmlPath, newXmlPath, true);

            XmlDocument temp = new XmlDocument();
            temp.PreserveWhitespace = true;
            temp.Load(tempXmlPath);

            XmlDocument inv = new XmlDocument();
            inv.PreserveWhitespace = true;
            inv.Load(newXmlPath);

            XmlNamespaceManager nsMgr = RegisterAllNamespaces(inv);

            AddValue(inv, "//cbc:UUID", master.uuid, nsMgr);
            AddValue(inv, "//cbc:IssueDate", date, nsMgr); 
            AddValue(inv, "//cbc:IssueTime", time, nsMgr); 

            if (lastInvoiceHash.IsNullOrEmpty())
            {
                AddValue(inv, "//cac:AdditionalDocumentReference[cbc:ID='PIH']/cac:Attachment/cbc:EmbeddedDocumentBinaryObject", Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("0"))), nsMgr);
            }
            else
            {
                AddValue(inv, "//cac:AdditionalDocumentReference[cbc:ID='PIH']/cac:Attachment/cbc:EmbeddedDocumentBinaryObject", lastInvoiceHash, nsMgr);
            }

            if (master.IsCash == false && master.IsVisa == false)
                AddValue(inv, "//cac:PaymentMeans/cbc:PaymentMeansCode", "10", nsMgr);

            if (master.IsCash == true)
                AddValue(inv, "//cac:PaymentMeans/cbc:PaymentMeansCode", "10", nsMgr);

            if (master.IsVisa == true)
                AddValue(inv, "//cac:PaymentMeans/cbc:PaymentMeansCode", "48", nsMgr);

            if (master.FlagId == 11)
            {
                AddValue(inv, "//cbc:ID[text()='SME00001']", master.InvoiceNumber, nsMgr);
                AddValue(inv, "//cac:AllowanceCharge/cac:TaxCategory[1]/cbc:Percent", (master.VatPercent * 100).ToString(), nsMgr);
                AddValue(inv, "//cac:AllowanceCharge/cac:TaxCategory[2]/cbc:Percent", (master.VatPercent * 100).ToString(), nsMgr);
            }

            if (master.FlagId == 12)
            {
                AddValue(inv, "//cbc:ID[text()='123456']", master.InvoiceNumber, nsMgr);
                AddValue(inv, "//cac:BillingReference/cac:InvoiceDocumentReference/cbc:ID", master.InvoiceNumber, nsMgr);
                AddValue(inv, "//cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:Percent", (master.VatPercent * 100).ToString(), nsMgr);
                AddValue(inv, "//cac:PaymentMeans/cbc:InstructionNote[1]", "Return", nsMgr);
                AddValue(inv, "//cac:PaymentMeans/cbc:InstructionNote[2]", "مرتجع", nsMgr);
            }

            AddValue(inv, "//cac:AdditionalDocumentReference/cbc:UUID", master.InvoiceNumber, nsMgr);
            AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification/cbc:ID", master.School.CRN, nsMgr);
            AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:StreetName", master.School.StreetName, nsMgr);
            AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:BuildingNumber", "00"+master.School.BuildingNumber, nsMgr);
            AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CitySubdivisionName", master.School.CitySubdivision, nsMgr);
            AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName", master.School.City, nsMgr);
            AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:PostalZone", master.School.PostalZone, nsMgr);
            AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID", master.School.VatNumber, nsMgr);
            AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName", master.School.Name, nsMgr);
            AddValue(inv, "//cac:TaxTotal/cbc:TaxAmount", master.VatAmount.ToString(), nsMgr);
            AddValue(inv, "(//cac:TaxTotal)[2]/cbc:TaxAmount", master.VatAmount.ToString(), nsMgr);
            AddValue(inv, "//cac:TaxSubtotal/cbc:TaxableAmount", master.Total.ToString(), nsMgr);
            AddValue(inv, "//cac:TaxSubtotal/cbc:TaxAmount", master.VatAmount.ToString(), nsMgr);
            
            AddValue(inv, "//cac:LegalMonetaryTotal/cbc:LineExtensionAmount", master.Total.ToString(), nsMgr);
            AddValue(inv, "//cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount", master.Total.ToString(), nsMgr);
            AddValue(inv, "//cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount", master.TotalWithVat.ToString(), nsMgr);
            AddValue(inv, "//cac:LegalMonetaryTotal/cbc:PayableAmount", master.TotalWithVat.ToString(), nsMgr);

            XmlElement root = inv.DocumentElement;

            int counter = 0;

            foreach (InventoryDetails itemDetail in master.InventoryDetails)
            {
                decimal itemTotalPriceWithVat = itemDetail.TotalPrice + (decimal)(itemDetail.TotalPrice * master.VatPercent);
                XmlElement invoiceLine = inv.CreateElement("cac", "InvoiceLine", nsMgr.LookupNamespace("cac"));

                // ID
                AppendElementWithText(inv, invoiceLine, "cbc", "ID", (++counter).ToString(), nsMgr);

                // InvoicedQuantity
                XmlElement quantity = AppendElementWithText(inv, invoiceLine, "cbc", "InvoicedQuantity", itemDetail.Quantity.ToString(), nsMgr);
                quantity.SetAttribute("unitCode", "PCE");

                // LineExtensionAmount
                XmlElement lineAmount = AppendElementWithText(inv, invoiceLine, "cbc", "LineExtensionAmount", itemDetail.TotalPrice.ToString(), nsMgr);
                lineAmount.SetAttribute("currencyID", "SAR");

                // TaxTotal
                XmlElement taxTotal = inv.CreateElement("cac", "TaxTotal", nsMgr.LookupNamespace("cac"));
                XmlElement taxAmount = AppendElementWithText(inv, taxTotal, "cbc", "TaxAmount", (itemDetail.TotalPrice * master.VatPercent).ToString(), nsMgr);
                taxAmount.SetAttribute("currencyID", "SAR");
                XmlElement roundingAmount = AppendElementWithText(inv, taxTotal, "cbc", "RoundingAmount", itemTotalPriceWithVat.ToString(), nsMgr);
                roundingAmount.SetAttribute("currencyID", "SAR");
                invoiceLine.AppendChild(taxTotal);

                // Item
                XmlElement item = inv.CreateElement("cac", "Item", nsMgr.LookupNamespace("cac"));
                AppendElementWithText(inv, item, "cbc", "Name", itemDetail.ShopItem.EnName + " | " + itemDetail.ShopItem.ArName, nsMgr);

                XmlElement classifiedTaxCategory = inv.CreateElement("cac", "ClassifiedTaxCategory", nsMgr.LookupNamespace("cac"));
                AppendElementWithText(inv, classifiedTaxCategory, "cbc", "ID", "S", nsMgr);
                AppendElementWithText(inv, classifiedTaxCategory, "cbc", "Percent", (master.VatPercent * 100).ToString(), nsMgr);

                XmlElement taxScheme = inv.CreateElement("cac", "TaxScheme", nsMgr.LookupNamespace("cac"));
                AppendElementWithText(inv, taxScheme, "cbc", "ID", "VAT", nsMgr);
                classifiedTaxCategory.AppendChild(taxScheme);

                item.AppendChild(classifiedTaxCategory);
                invoiceLine.AppendChild(item);

                // Price
                XmlElement price = inv.CreateElement("cac", "Price", nsMgr.LookupNamespace("cac"));
                XmlElement priceAmount = AppendElementWithText(inv, price, "cbc", "PriceAmount", itemDetail.Price.ToString(), nsMgr);
                priceAmount.SetAttribute("currencyID", "SAR");

                XmlElement allowanceCharge = inv.CreateElement("cac", "AllowanceCharge", nsMgr.LookupNamespace("cac"));
                AppendElementWithText(inv, allowanceCharge, "cbc", "ChargeIndicator", "true", nsMgr);
                AppendElementWithText(inv, allowanceCharge, "cbc", "AllowanceChargeReason", "discount", nsMgr);
                XmlElement amount = AppendElementWithText(inv, allowanceCharge, "cbc", "Amount", "0.00", nsMgr);
                amount.SetAttribute("currencyID", "SAR");

                price.AppendChild(allowanceCharge);
                invoiceLine.AppendChild(price);

                root.AppendChild(invoiceLine);
            }

            XmlNodeList invoiceLines = inv.SelectNodes("//cac:InvoiceLine", nsMgr);

            foreach (var line in invoiceLines)
            {
                InsertWhitespace((XmlElement)line);
            }

            XmlNodeList taxTotalNode = inv.SelectNodes("//cac:InvoiceLine/cac:TaxTotal", nsMgr);

            foreach (var tax in taxTotalNode)
            {
                InsertWhitespace((XmlElement)tax);
            }

            XmlNodeList itemNode = inv.SelectNodes("//cac:InvoiceLine/cac:Item", nsMgr);

            foreach (var item in itemNode)
            {
                InsertWhitespace((XmlElement)item);
            }

            XmlNodeList classTax = inv.SelectNodes("//cac:ClassifiedTaxCategory", nsMgr);

            foreach (var clt in classTax)
            {
                InsertWhitespace((XmlElement)clt);
            }

            XmlNodeList priceNode = inv.SelectNodes("//cac:InvoiceLine/cac:Price", nsMgr);

            foreach (var pn in priceNode)
            {
                InsertWhitespace((XmlElement)pn);
            }

            XmlNodeList allowance = inv.SelectNodes("//cac:AllowanceCharge", nsMgr);

            foreach (var allCha in allowance)
            {
                InsertWhitespace((XmlElement)allCha);
            }

            SaveFormatted(inv, newXmlPath);

            SignResult signer = InvoiceSigning(newXmlPath, certPath, privateKeyPath);

            if (!signer.IsValid)
                return false;

            return true;
        }

        public static async Task<string> UpdatePCSID(string securityToken, string csrContent, string version, string otp, IConfiguration configuration)
        {
            HttpClient client = new HttpClient();

            bool isProduction = configuration.GetValue<bool>("IsProduction");
            HttpRequestMessage request;
            if (isProduction)
            {
                request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/production/CSIDs");
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/production/csids");
            }

            request.Headers.Add("accept", "application/json");
            request.Headers.Add("OTP", otp);
            request.Headers.Add("accept-language", "en");
            request.Headers.Add("Accept-Version", version);
            request.Headers.Add("Authorization", $"Basic {securityToken}");

            string jsonPayload = $@"
            {{
              ""csr"": ""{csrContent}""
            }}";

            request.Content = new StringContent(jsonPayload);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public static string GetInvoiceHash(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(xmlPath);
            XmlNamespaceManager nsMgr = RegisterAllNamespaces(doc);
            string invoiceHash = doc.SelectSingleNode("//ds:DigestValue", nsMgr).InnerText;
            return invoiceHash;
        }

        public static string GetQRCode(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(xmlPath);
            XmlNamespaceManager nsMgr = RegisterAllNamespaces(doc);
            string qrCode = doc.SelectSingleNode("//cac:AdditionalDocumentReference[cbc:ID='QR']/cac:Attachment/cbc:EmbeddedDocumentBinaryObject", nsMgr).InnerText;
            return qrCode;
        }

        public static string GetUUID(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(xmlPath);
            XmlNamespaceManager nsMgr = RegisterAllNamespaces(doc);
            string uuid = doc.SelectSingleNode("//cbc:UUID", nsMgr).InnerText;
            return uuid;
        }

        public static string GetCertificateDate(string pcsidContent)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(pcsidContent);
            string base64Cert = jsonObject.binarySecurityToken;

            byte[] certBytes = Convert.FromBase64String(base64Cert);
            var cert = new X509Certificate2(certBytes);

            string expires = cert.NotAfter.ToString("yyyy-MM-dd");

            return expires;
        }

        private static SignResult InvoiceSigning(string xmlPath, string certPath, string privateKeyPath)
        {
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException();

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(xmlPath);

            string jsonContent = File.ReadAllText(certPath);
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonContent);
            string base64Cert = jsonObject.binarySecurityToken;

            byte[] certBytes = Convert.FromBase64String(base64Cert);
            string certDecoded = Encoding.UTF8.GetString(certBytes);

            string privateKeyContent = File.ReadAllText(privateKeyPath);
            privateKeyContent = privateKeyContent
                .Replace("-----BEGIN EC PRIVATE KEY-----", "")
                .Replace("-----END EC PRIVATE KEY-----", "")
                .Replace("\n", "")
                .Replace("\r", "");

            EInvoiceSigner signer = new EInvoiceSigner();

            var signed = signer.SignDocument(doc, certDecoded, privateKeyContent);
            signed.SignedEInvoice.Normalize();
            signed.SaveSignedEInvoice(xmlPath);

            XmlDsigC14NTransform transform = new XmlDsigC14NTransform();
            doc.Load(xmlPath);
            doc.PreserveWhitespace = false;
            transform.LoadInput(doc);

            var settings = new XmlWriterSettings
            {
                Indent = false, 
                NewLineHandling = NewLineHandling.None,
                NewLineChars = "",
                OmitXmlDeclaration = false, 
                Encoding = Encoding.UTF8
            };

            using (var writer = XmlWriter.Create(xmlPath, settings))
            {
                doc.Save(writer);
            }

            return signed;
        }

        public static async Task<HttpResponseMessage> InvoiceReporting(string xmlPath, string invoiceHash, string uuid, long pcId, long schoolId, IConfiguration configuration)
        {
            string csr = Path.Combine(Directory.GetCurrentDirectory(), $"Invoices/CSRs/PC-{pcId}-{schoolId}");
            string certPath = Path.Combine(csr, "PCSID.json");

            string version = "V2";

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(xmlPath);

            string csidContent = await File.ReadAllTextAsync(certPath);

            JObject obj = JObject.Parse(csidContent);
            string token = (string)obj["binarySecurityToken"];
            string secret = (string)obj["secret"];

            string authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(token + ":" + secret));

            HttpClient client = new HttpClient();

            bool isProduction = configuration.GetValue<bool>("IsProduction");
            HttpRequestMessage request;

            if (isProduction)
            {
                request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/core/invoices/reporting/single");
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/invoices/reporting/single");
            }
            

            request.Headers.Add("accept", "application/json");
            request.Headers.Add("accept-language", "en");
            request.Headers.Add("Clearance-Status", "0");
            request.Headers.Add("Accept-Version", version);
            request.Headers.Add("Authorization", $"Basic {authorization}");

            string invoiceEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(doc.InnerXml));

            string jsonPayload = $@"
            {{
              ""invoiceHash"": ""{invoiceHash}"",
              ""uuid"": ""{uuid}"",
              ""invoice"": ""{invoiceEncoded}""
            }}";

            request.Content = new StringContent(jsonPayload);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private static void SaveFormatted(XmlDocument doc, string filePath, bool omitXmlDeclaration = true)
        {
            doc.PreserveWhitespace = false;
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Indent = true,
                IndentChars = "  ", // use two spaces
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace,
                Encoding = Encoding.UTF8
            };

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (var writer = XmlWriter.Create(fs, settings))
            {
                doc.Save(writer);
            }
        }

        private static void InsertWhitespace(XmlElement parentElement)
        {
            XmlDocument doc = parentElement.OwnerDocument;

            // Add leading whitespace before first child
            if (parentElement.HasChildNodes)
            {
                parentElement.InsertBefore(doc.CreateWhitespace("\r\n\t"), parentElement.FirstChild);
            }

            // Insert whitespace between each child node
            for (int i = parentElement.ChildNodes.Count - 1; i > 0; i--)
            {
                XmlNode prev = parentElement.ChildNodes[i - 1];
                XmlNode current = parentElement.ChildNodes[i];

                if (!(prev is XmlWhitespace)) // avoid duplicating whitespaces
                {
                    XmlNode whitespace = doc.CreateWhitespace("\r\n\t");
                    parentElement.InsertBefore(whitespace, current);
                }
            }

            // Add trailing newline after last child
            if (parentElement.HasChildNodes)
            {
                parentElement.AppendChild(doc.CreateWhitespace("\r\n\t"));
            }
        }

        private static XmlNamespaceManager RegisterAllNamespaces(XmlDocument xmlDoc)
        {
            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            var namespaceSet = new HashSet<string>();

            // Select all nodes with a namespace declaration
            var nodesWithNamespaces = xmlDoc.SelectNodes("//*");

            foreach (XmlNode node in nodesWithNamespaces)
            {
                if (node.Attributes == null) continue;

                foreach (XmlAttribute attr in node.Attributes)
                {
                    if ((attr.Prefix == "xmlns" || attr.Name == "xmlns") && !string.IsNullOrEmpty(attr.Value))
                    {
                        string prefix = attr.Prefix == "xmlns" ? attr.LocalName : string.Empty;
                        if (!namespaceSet.Contains(attr.Value))
                        {
                            nsmgr.AddNamespace(prefix, attr.Value);
                            namespaceSet.Add(attr.Value);
                        }
                    }
                }
            }

            return nsmgr;
        }

        private static void AddValue(XmlDocument doc, string element, string innerText, XmlNamespaceManager nsMgr)
        {
            XmlNode node = doc.SelectSingleNode(element, nsMgr);
            if (node != null)
            {
                node.InnerText = innerText;
            }
            else
            {
                throw new Exception("XML node not found.");
            }
        }

        private static XmlElement AppendElementWithText(XmlDocument doc, XmlElement parent, string prefix, string localName, string innerText, XmlNamespaceManager nsMgr)
        {
            XmlElement elem = doc.CreateElement(prefix, localName, nsMgr.LookupNamespace(prefix));
            elem.InnerText = innerText;
            parent.AppendChild(elem);
            return elem;
        }

        public static byte[] GenerateQrImage(string qrCode)
        {
            var writer = new BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 100,
                    Height = 100,
                    Margin = 1
                }
            };

            using Bitmap qrBitmap = writer.Write(qrCode);
            using MemoryStream ms = new MemoryStream();
            qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
