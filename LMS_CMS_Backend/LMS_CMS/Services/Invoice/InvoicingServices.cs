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
using LMS_CMS_DAL.Models.Domains.LMS;

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

        public static async Task<string> GenerateCSID(string csrPath, long OTP, string version)
        {
            string csrContent = await File.ReadAllTextAsync(csrPath);

            string payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(csrContent));
            var payloadObj = new { csr = payload };
            string jsonPayload = JsonConvert.SerializeObject(payloadObj);

            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                    "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance");

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

        public static async Task<string> GeneratePCSID(string securityToken, string version, string requestId)
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/production/csids");

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

        public static async Task<string> InvoiceCompliance(string csidPath, string EncodedInvoice, string invoiceHash, string uuid, string version)
        {
            try
            {
                string csidContent = await File.ReadAllTextAsync(csidPath);

                JObject obj = JObject.Parse(csidContent);
                string token = (string)obj["binarySecurityToken"];
                string secret = (string)obj["secret"];

                string authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(token + ":" + secret));

                HttpClient client = new HttpClient();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance/invoices");

                request.Headers.Add("accept", "application/json");
                request.Headers.Add("Accept-Language", "en");
                request.Headers.Add("Accept-Version", version);
                request.Headers.Add("Authorization", $"Basic {authorization}");

                request.Content = new StringContent("");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<bool> GenerateXML(InventoryMaster master)
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/XML");
            string examplePath = Path.Combine(Directory.GetCurrentDirectory(), "Services/Invoice");
            string csr = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");

            if (!Directory.Exists(invoices))
            {
                Directory.CreateDirectory(invoices);
            }

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            //string newXmlPath = Path.Combine(invoices, $"{school.CRN}_{date.Replace("-", "")}T{time.Replace(":","")}_{date}-{master.ID}.xml");
            string newXmlPath = Path.Combine(invoices, $"INV001.xml");
            string tempXmlPath = Path.Combine(examplePath, "INV001.xml");
            string certPath = Path.Combine(csr, "PCSID.json");
            string privateKeyPath = Path.Combine(csr, "PrivateKey.pem");

            File.Copy(tempXmlPath, newXmlPath, true);

            XmlDocument inv = new XmlDocument();
            inv.PreserveWhitespace = true;
            inv.Load(newXmlPath);

            string uuid = Guid.NewGuid().ToString();

            XmlNamespaceManager nsMgr = RegisterAllNamespaces(inv);

            AddValue(inv, "//cbc:UUID", uuid, nsMgr);
            AddValue(inv, "//cbc:IssueDate", date, nsMgr); // edit in master
            AddValue(inv, "//cbc:IssueTime", time, nsMgr); // edit in master
            AddValue(inv, "//cac:AdditionalDocumentReference[cbc:ID='PIH']/cac:Attachment/cbc:EmbeddedDocumentBinaryObject", Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("0"))), nsMgr);

            SaveFormatted(inv, newXmlPath);

            //AddValue(inv, "//cbc:ID[text()='SME00001']", $"INV{master.ID.ToString()}", nsMgr);
            //AddValue(inv, "//cbc:UUID", uuid, nsMgr);
            //AddValue(inv, "//cac:AdditionalDocumentReference/cbc:UUID", master.ID.ToString(), nsMgr);
            //AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification/cbc:ID", school.CRN, nsMgr);
            //AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:StreetName", school.PostalAddress, nsMgr);
            //AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:BuildingNumber", school.BuildingNumber, nsMgr);
            //AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID", school.VatNumber, nsMgr);
            //AddValue(inv, "//cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName", school.Name, nsMgr);
            //AddValue(inv, "//cac:TaxTotal/cbc:TaxAmount", master.VatAmount.ToString(), nsMgr);
            //AddValue(inv, "(//cac:TaxTotal)[2]/cbc:TaxAmount", master.VatAmount.ToString(), nsMgr);
            //AddValue(inv, "//cac:TaxSubtotal/cbc:TaxableAmount", master.Total.ToString(), nsMgr);
            //AddValue(inv, "//cac:TaxSubtotal/cbc:TaxAmount", master.VatAmount.ToString(), nsMgr);
            //AddValue(inv, "//cac:TaxSubtotal/cbc:TaxCategory/cbc:Percent", master.VatPercent.ToString(), nsMgr);
            //AddValue(inv, "//cac:LegalMonetaryTotal/cbc:LineExtensionAmount", master.Total.ToString(), nsMgr);
            //AddValue(inv, "//cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount", master.Total.ToString(), nsMgr);
            //AddValue(inv, "//cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount", master.TotalWithVat.ToString(), nsMgr);
            ////AddValue(inv, "//cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount", "master.AllowanceTotalAmount", nsMgr);
            ////AddValue(inv, "//cac:LegalMonetaryTotal/cbc:PrepaidAmount", "master.PrepaidAmount", nsMgr);
            //AddValue(inv, "//cac:LegalMonetaryTotal/cbc:PayableAmount", master.TotalWithVat.ToString(), nsMgr);

            XmlElement root = inv.DocumentElement;

            for (int i = 1; i <= 3; i++)
            {
                XmlElement invoiceLine = inv.CreateElement("cac", "InvoiceLine", nsMgr.LookupNamespace("cac"));

                // ID
                AppendElementWithText(inv, invoiceLine, "cbc", "ID", i.ToString(), nsMgr);

                // InvoicedQuantity
                XmlElement quantity = AppendElementWithText(inv, invoiceLine, "cbc", "InvoicedQuantity", "33.000000", nsMgr);
                quantity.SetAttribute("unitCode", "PCE");

                // LineExtensionAmount
                XmlElement lineAmount = AppendElementWithText(inv, invoiceLine, "cbc", "LineExtensionAmount", "99.00", nsMgr);
                lineAmount.SetAttribute("currencyID", "SAR");

                // TaxTotal
                XmlElement taxTotal = inv.CreateElement("cac", "TaxTotal", nsMgr.LookupNamespace("cac"));
                XmlElement taxAmount = AppendElementWithText(inv, taxTotal, "cbc", "TaxAmount", "14.85", nsMgr);
                taxAmount.SetAttribute("currencyID", "SAR");
                XmlElement roundingAmount = AppendElementWithText(inv, taxTotal, "cbc", "RoundingAmount", "113.85", nsMgr);
                roundingAmount.SetAttribute("currencyID", "SAR");
                invoiceLine.AppendChild(taxTotal);

                // Item
                XmlElement item = inv.CreateElement("cac", "Item", nsMgr.LookupNamespace("cac"));
                AppendElementWithText(inv, item, "cbc", "Name", "كتاب", nsMgr);

                XmlElement classifiedTaxCategory = inv.CreateElement("cac", "ClassifiedTaxCategory", nsMgr.LookupNamespace("cac"));
                AppendElementWithText(inv, classifiedTaxCategory, "cbc", "ID", "S", nsMgr);
                AppendElementWithText(inv, classifiedTaxCategory, "cbc", "Percent", "15.00", nsMgr);

                XmlElement taxScheme = inv.CreateElement("cac", "TaxScheme", nsMgr.LookupNamespace("cac"));
                AppendElementWithText(inv, taxScheme, "cbc", "ID", "VAT", nsMgr);
                classifiedTaxCategory.AppendChild(taxScheme);

                item.AppendChild(classifiedTaxCategory);
                invoiceLine.AppendChild(item);

                // Price
                XmlElement price = inv.CreateElement("cac", "Price", nsMgr.LookupNamespace("cac"));
                XmlElement priceAmount = AppendElementWithText(inv, price, "cbc", "PriceAmount", "3.00", nsMgr);
                priceAmount.SetAttribute("currencyID", "SAR");

                XmlElement allowanceCharge = inv.CreateElement("cac", "AllowanceCharge", nsMgr.LookupNamespace("cac"));
                AppendElementWithText(inv, allowanceCharge, "cbc", "ChargeIndicator", "true", nsMgr);
                AppendElementWithText(inv, allowanceCharge, "cbc", "AllowanceChargeReason", "discount", nsMgr);
                XmlElement amount = AppendElementWithText(inv, allowanceCharge, "cbc", "Amount", "0.00", nsMgr);
                amount.SetAttribute("currencyID", "SAR");

                price.AppendChild(allowanceCharge);
                invoiceLine.AppendChild(price);

                //XmlElementToString(invoiceLine);

                root.AppendChild(invoiceLine);
                SaveFormatted(inv, newXmlPath);
            }

            SignResult signer = InvoiceSigning(newXmlPath, certPath, privateKeyPath);

            if (!signer.IsValid)
                return false;

            string invoiceHash = signer.Steps.FirstOrDefault(x => x.StepName == "Generate EInvoice Hash").ResultedValue;
            string reporting = await InvoiceReporting(newXmlPath, invoiceHash, uuid);

            if (reporting != "OK")
                return false;

            return true;
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

        private static async Task<string> InvoiceReporting(string xmlPath, string invoiceHash, string uuid)
        {
            string csr = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");
            string certPath = Path.Combine(csr, "PCSID.json");

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(xmlPath);

            string csidContent = await File.ReadAllTextAsync(certPath);

            JObject obj = JObject.Parse(csidContent);
            string token = (string)obj["binarySecurityToken"];
            string secret = (string)obj["secret"];

            string authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(token + ":" + secret));

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/invoices/reporting/single");

            request.Headers.Add("accept", "application/json");
            request.Headers.Add("accept-language", "en");
            request.Headers.Add("Clearance-Status", "0");
            request.Headers.Add("Accept-Version", "V2");
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
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseBody);
            return response.ReasonPhrase;
        }

        private static void SaveFormatted(XmlDocument doc, string path, bool omitXmlDeclaration = true)
        {
            doc.PreserveWhitespace = true;
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Indent = true,
                IndentChars = "  ", // use two spaces
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace,
                Encoding = Encoding.UTF8
            };

            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = XmlWriter.Create(fs, settings))
            {
                doc.Save(writer);
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
                throw new Exception("SignatureValue node not found.");
            }
        }

        private static XmlElement AppendElementWithText(XmlDocument doc, XmlElement parent, string prefix, string localName, string innerText, XmlNamespaceManager nsMgr)
        {
            XmlElement elem = doc.CreateElement(prefix, localName, nsMgr.LookupNamespace(prefix));
            elem.InnerText = innerText;
            parent.AppendChild(elem);
            return elem;
        }
    }
}
