using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using LMS_CMS_DAL.Models.Domains.Inventory;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.OpenSsl;
using System.Globalization;
using System.Numerics;

namespace LMS_CMS_PL.Services.Invoice
{
    public class InvoicingServices
    {
        public static async Task<string> GeneratePublicKey(string publicKeyPath, string privateKeyPath)
        {
            try
            {
                string opensslCmd = @"C:\Program Files\OpenSSL-Win64\bin\openssl.exe";

                if (!File.Exists(privateKeyPath))
                {
                    return "You must generate a private key first!";
                }

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

                    if (process.ExitCode != 0)
                    {
                        return error;
                    }

                    if (File.Exists(publicKeyPath))
                    {
                        return await File.ReadAllTextAsync(publicKeyPath);
                    }
                    else
                    {
                        return "Error: Public key file not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<string> GenerateCSID(string csrPath, long OTP, string version)
        {
            try
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

        public static async Task GenerateXML(InventoryMaster master)
        {
            string invoices = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/Data");
            string examplePath = Path.Combine(Directory.GetCurrentDirectory(), "Services/Invoice");
            string csr = Path.Combine(Directory.GetCurrentDirectory(), "Invoices/CSR");

            if (!Directory.Exists(invoices))
            {
                Directory.CreateDirectory(invoices);
            }

            string newInvoice = Path.Combine(invoices, $"INV001.xml");
            string exampleXml = Path.Combine(examplePath, "example.xml");
            string tempXml = Path.Combine(examplePath, "temp.xml");
            string privateKeyPath = Path.Combine(csr, "PrivateKey.pem");
            string csrPath = Path.Combine(csr, "CSR.csr");
            string cerPath = Path.Combine(csr, "CSID.cer");

            File.Copy(exampleXml, newInvoice, true);
            File.Copy(exampleXml, tempXml, true);

            XmlDocument doc = new XmlDocument();
            doc.Load(newInvoice);

            XmlDocument tempDoc = new XmlDocument();
            tempDoc.Load(tempXml);

            XmlNamespaceManager nsMgr = RegisterAllNamespaces(doc);

            RemoveUnneededTags(tempDoc, nsMgr);

            string uuid = Guid.NewGuid().ToString();
            AddValue(tempDoc, "//cbc:UUID", uuid, nsMgr);

            SaveFormatted(tempDoc, tempXml, true);

            string xmlHashBytes = HashInvoice(tempDoc);
            string invoiceHash64 = Convert.ToBase64String(Convert.FromHexString(xmlHashBytes));

            string digitalSignature = GenerateDigitalSignature(Convert.FromBase64String(xmlHashBytes), privateKeyPath);

            byte[] binaryData = GenerateCertificateHash(cerPath);
            byte[] hashBytes = SHA256.HashData(binaryData);
            string hashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            string certHashBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(hashHex));

            X509Certificate2 cert = new X509Certificate2(binaryData);

            string csrContent = File.ReadAllText(csrPath);
            csrContent = csrContent.Replace("-----BEGIN CERTIFICATE REQUEST-----", "")
                .Replace("-----END CERTIFICATE REQUEST-----", "")
                .Replace("\r", "").Replace("\n", "").Trim();

            XmlElement root = doc.DocumentElement;

            AddValue(doc, "//ds:Reference[@Id='invoiceSignedData']/ds:DigestValue", invoiceHash64, nsMgr);
            AddValue(doc, "//ds:SignatureValue", digitalSignature, nsMgr);
            AddValue(doc, "//xades:CertDigest/ds:DigestValue", certHashBase64, nsMgr);
            AddValue(doc, "//cbc:IssueDate", DateTime.Now.ToString("yyyy-MM-dd"), nsMgr); // edit in master
            AddValue(doc, "//cbc:IssueTime", DateTime.Now.ToString("HH:mm:ss"), nsMgr); // edit in master
            AddValue(doc, "//ds:X509Certificate", csrContent, nsMgr);
            AddValue(doc, "//xades:SignedSignatureProperties/xades:SigningTime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), nsMgr);
            AddValue(doc, "//xades:IssuerSerial/ds:X509IssuerName", cert.Issuer, nsMgr);
            AddValue(doc, "//xades:IssuerSerial/ds:X509SerialNumber", BigInteger.Parse("00" + cert.SerialNumber, NumberStyles.HexNumber).ToString(), nsMgr);
            AddValue(doc, "//cbc:ProfileID", "reporting:1.0", nsMgr);
            AddValue(doc, "//cbc:UUID", uuid, nsMgr);
            //AddValue(doc, "//cbc:ID[text()='SME00010']", "INV + master.ID.ToString()", nsMgr);
            //AddValue(doc, "//cbc:UUID", Guid.NewGuid().ToString(), nsMgr);
            //AddValue(doc, "//cac:AdditionalDocumentReference/cbc:UUID", "master.ID.ToString()", nsMgr);
            //AddValue(doc, "//cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification/cbc:ID", "master.CRN", nsMgr);
            //AddValue(doc, "//cac:AccountingSupplierParty/cac:Party/cac:PostalAddress", "Supplier.Address", nsMgr);
            //AddValue(doc, "//cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID", "Supplier.VatNumber", nsMgr);
            //AddValue(doc, "//cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName", "Supplier.Name", nsMgr);
            //AddValue(doc, "//cac:TaxTotal/cbc:TaxAmount", "master.VatAmount", nsMgr);
            //AddValue(doc, "(//cac:TaxTotal)[2]/cbc:TaxAmount", "master.VatAmount", nsMgr);
            //AddValue(doc, "//cac:TaxSubtotal/cbc:TaxableAmount", "master.Total", nsMgr);
            //AddValue(doc, "//cac:TaxSubtotal/cbc:TaxAmount", "master.VatAmount", nsMgr);
            //AddValue(doc, "//cac:TaxSubtotal/cbc:TaxCategory/cbc:Percent", "master.Vat", nsMgr);
            //AddValue(doc, "//cac:LegalMonetaryTotal/cbc:LineExtensionAmount", "master.Total", nsMgr);
            //AddValue(doc, "//cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount", "master.Total", nsMgr);
            //AddValue(doc, "//cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount", "master.TotalWithVat", nsMgr);
            //AddValue(doc, "//cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount", "master.AllowanceTotalAmount", nsMgr);
            //AddValue(doc, "//cac:LegalMonetaryTotal/cbc:PrepaidAmount", "master.PrepaidAmount", nsMgr);
            //AddValue(doc, "//cac:LegalMonetaryTotal/cbc:PayableAmount", "master.TotalWithVat", nsMgr);

            //for (int i = 1; i <= 3; i++)
            //{
            //    XmlElement invoiceLine = doc.CreateElement("cac", "InvoiceLine", nsMgr.LookupNamespace("cac"));

            //    // ID
            //    AppendElementWithText(doc, invoiceLine, "cbc", "ID", i.ToString(), nsMgr);

            //    // InvoicedQuantity
            //    XmlElement quantity = AppendElementWithText(doc, invoiceLine, "cbc", "InvoicedQuantity", "33.000000", nsMgr);
            //    quantity.SetAttribute("unitCode", "PCE");

            //    // LineExtensionAmount
            //    XmlElement lineAmount = AppendElementWithText(doc, invoiceLine, "cbc", "LineExtensionAmount", "99.00", nsMgr);
            //    lineAmount.SetAttribute("currencyID", "SAR");

            //    // TaxTotal
            //    XmlElement taxTotal = doc.CreateElement("cac", "TaxTotal", nsMgr.LookupNamespace("cac"));
            //    XmlElement taxAmount = AppendElementWithText(doc, taxTotal, "cbc", "TaxAmount", "14.85", nsMgr);
            //    taxAmount.SetAttribute("currencyID", "SAR");
            //    XmlElement roundingAmount = AppendElementWithText(doc, taxTotal, "cbc", "RoundingAmount", "113.85", nsMgr);
            //    roundingAmount.SetAttribute("currencyID", "SAR");
            //    invoiceLine.AppendChild(taxTotal);

            //    // Item
            //    XmlElement item = doc.CreateElement("cac", "Item", nsMgr.LookupNamespace("cac"));
            //    AppendElementWithText(doc, item, "cbc", "Name", "كتاب", nsMgr);

            //    XmlElement classifiedTaxCategory = doc.CreateElement("cac", "ClassifiedTaxCategory", nsMgr.LookupNamespace("cac"));
            //    AppendElementWithText(doc, classifiedTaxCategory, "cbc", "ID", "S", nsMgr);
            //    AppendElementWithText(doc, classifiedTaxCategory, "cbc", "Percent", "15.00", nsMgr);

            //    XmlElement taxScheme = doc.CreateElement("cac", "TaxScheme", nsMgr.LookupNamespace("cac"));
            //    AppendElementWithText(doc, taxScheme, "cbc", "ID", "VAT", nsMgr);
            //    classifiedTaxCategory.AppendChild(taxScheme);

            //    item.AppendChild(classifiedTaxCategory);
            //    invoiceLine.AppendChild(item);

            //    // Price
            //    XmlElement price = doc.CreateElement("cac", "Price", nsMgr.LookupNamespace("cac"));
            //    XmlElement priceAmount = AppendElementWithText(doc, price, "cbc", "PriceAmount", "3.00", nsMgr);
            //    priceAmount.SetAttribute("currencyID", "SAR");

            //    XmlElement allowanceCharge = doc.CreateElement("cac", "AllowanceCharge", nsMgr.LookupNamespace("cac"));
            //    AppendElementWithText(doc, allowanceCharge, "cbc", "ChargeIndicator", "true", nsMgr);
            //    AppendElementWithText(doc, allowanceCharge, "cbc", "AllowanceChargeReason", "discount", nsMgr);
            //    XmlElement amount = AppendElementWithText(doc, allowanceCharge, "cbc", "Amount", "0.00", nsMgr);
            //    amount.SetAttribute("currencyID", "SAR");

            //    price.AppendChild(allowanceCharge);
            //    invoiceLine.AppendChild(price);

            //    XmlElementToString(invoiceLine);

            //    // Finally, append the completed InvoiceLine
            //    root.AppendChild(invoiceLine);
            //}
            SaveFormatted(tempDoc, tempXml, true);
            SaveFormatted(doc, newInvoice, true);

            XmlNode propertiesNode = doc.SelectSingleNode("//xades:QualifyingProperties/xades:SignedProperties", nsMgr);

            string signed64EncodedHash = string.Empty;
            if (propertiesNode != null)
            {
                // Step 3: Linearize the XML block and remove spaces
                string linearizedProperties = LinearizeXml(propertiesNode);

                // Step 4: Hash the property tag using SHA-256
                byte[] hashBytesss = ComputeSHA256(linearizedProperties);

                // Step 5: Encode the hashed property tag to Base64
                signed64EncodedHash = ConvertToBase64(hashBytesss);
            }

            AddValue(doc, "//ds:Reference[@URI='#xadesSignedProperties']/ds:DigestValue", signed64EncodedHash, nsMgr);

            SaveFormatted(doc, newInvoice, true);

            string sellerNameValue = doc.SelectSingleNode("//cac:PartyLegalEntity/cbc:RegistrationName", nsMgr).InnerText;
            string vatNumberValue = doc.SelectSingleNode("//cac:PartyTaxScheme/cbc:CompanyID", nsMgr).InnerText;
            string invoiceDateValue = doc.SelectSingleNode("//cbc:IssueDate", nsMgr).InnerText;
            string invoiceTimeValue = doc.SelectSingleNode("//cbc:IssueTime", nsMgr).InnerText;
            string invoiceTotalValue = doc.SelectSingleNode("//cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount", nsMgr).InnerText;
            string invoiceVatValue = doc.SelectSingleNode("//cac:TaxTotal/cbc:TaxAmount", nsMgr).InnerText;
            string invoiceXmlHash = doc.SelectSingleNode("//ds:Reference/ds:DigestValue", nsMgr).InnerText;
            string signatureValue = doc.SelectSingleNode("//sac:SignatureInformation/ds:Signature /ds:SignatureValue", nsMgr).InnerText;
            string PublicKeyValue = doc.SelectSingleNode("//ds:KeyInfo /ds:X509Data/ds:X509Certificate", nsMgr).InnerText;
            string certificateValue = doc.SelectSingleNode("//xades:CertDigest/ds:DigestValue", nsMgr).InnerText; ;


            var sellerName = GetTLVForValue(1, sellerNameValue);
            var vatNumber = GetTLVForValue(2, vatNumberValue);
            var invoiceTimestamp = GetTLVForValue(3, invoiceDateValue + "T" + invoiceTimeValue);
            var invoiceTotal = GetTLVForValue(4, invoiceTotalValue);
            var invoiceVat = GetTLVForValue(5, invoiceVatValue);
            var invoiceXml = GetTLVForValue(6, invoiceXmlHash);
            var ecdsaSignature = GetTLVForValue(7, signatureValue);
            var ecdsaPublicKey = GetTLVForValue(8, PublicKeyValue);
            var certificate = GetTLVForValue(9, certificateValue);

            byte[][] tagsBytesArray = [sellerName, vatNumber, invoiceTimestamp, invoiceTotal, invoiceVat, invoiceXml, ecdsaSignature, ecdsaPublicKey, certificate];

            byte[] qrCodeBytes = tagsBytesArray.SelectMany(b => b).ToArray();

            string qrCodeBase64 = Convert.ToBase64String(qrCodeBytes);

            AddValue(doc, "//cac:AdditionalDocumentReference[cbc:ID='QR']/cac:Attachment/cbc:EmbeddedDocumentBinaryObject", qrCodeBase64, nsMgr);
            AddValue(doc, "//cac:AdditionalDocumentReference[cbc:ID='PIH']/cac:Attachment/cbc:EmbeddedDocumentBinaryObject", Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("0"))), nsMgr);
            SaveFormatted(doc, newInvoice, true);

            string invoicEncoded = await File.ReadAllTextAsync(newInvoice);

            //string test = await InvoiceCompliance(cerPath, ConvertToBase64(Encoding.UTF8.GetBytes(invoicEncoded)), invoiceHash64, uuid, "V2");

            SaveFormatted(doc, newInvoice, true);
        }

        private static string HashInvoice(XmlDocument doc)
        {
            byte[] invoiceCanonicalize = CanonicalizeInvoice(doc); 
            byte[] xmlHash = SHA256.HashData(invoiceCanonicalize);
            string invoiceHashHex = BitConverter.ToString(xmlHash).Replace("-", "").ToLowerInvariant();
            return invoiceHashHex; 
        }

        private static byte[] CanonicalizeInvoice(XmlDocument doc)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    Indent = false,
                    NewLineChars = string.Empty
                };

                XmlDsigC14NTransform transform = new XmlDsigC14NTransform();
                transform.LoadInput(doc);

                using (Stream stream = (Stream)transform.GetOutput(typeof(Stream)))
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    stream.CopyTo(ms);
                return ms.ToArray();
                //}
            }
        }

        private static string GenerateDigitalSignature(byte[] hash, string privateKeyPath)
        {
            using var reader = File.OpenText(privateKeyPath);
            var pemReader = new PemReader(reader);
            var keyPair = pemReader.ReadObject() as AsymmetricCipherKeyPair;

            if (keyPair == null || !(keyPair.Private is ECPrivateKeyParameters privateKey))
                throw new Exception("Invalid ECDSA private key in PEM");

            ISigner signer = SignerUtilities.GetSigner("SHA-256withECDSA");
            signer.Init(true, privateKey);
            signer.BlockUpdate(hash, 0, hash.Length);
            byte[] derSignature = signer.GenerateSignature();

            var decoder = new Org.BouncyCastle.Asn1.Asn1InputStream(derSignature);
            var seq = (Org.BouncyCastle.Asn1.Asn1Sequence)decoder.ReadObject();
            var r = ((Org.BouncyCastle.Asn1.DerInteger)seq[0]).Value;
            var s = ((Org.BouncyCastle.Asn1.DerInteger)seq[1]).Value;

            // Ensure 32-byte values
            byte[] rBytes = r.ToByteArrayUnsigned();
            byte[] sBytes = s.ToByteArrayUnsigned();

            rBytes = PadTo32Bytes(rBytes);
            sBytes = PadTo32Bytes(sBytes);

            byte[] rawSignature = rBytes.Concat(sBytes).ToArray();

            return Convert.ToBase64String(rawSignature);
        }

        private static byte[] GenerateCertificateHash(string cerPath)
        {
            string jsonCer = File.ReadAllText(cerPath);
            dynamic cerObject = JsonConvert.DeserializeObject(jsonCer);
            string securityToken = cerObject.binarySecurityToken;

            byte[] binaryData = Convert.FromBase64String(securityToken);
            return binaryData;
        }

        private static string LinearizeXml(XmlNode node)
        {
            string xmlContent = node.InnerXml;

            StringBuilder linearizedXml = new StringBuilder();
            foreach (char c in xmlContent)
            {
                if (!Char.IsWhiteSpace(c))
                {
                    linearizedXml.Append(c);
                }
            }
            return linearizedXml.ToString();
        }

        private static byte[] GetTLVForValue(byte tagNum, string tagValue)
        {
            // Convert tag number to byte array
            byte[] tagByte = new byte[] { tagNum };

            // Convert tag value length to byte array
            byte[] tagValueLenByte = new byte[] { (byte)tagValue.Length };

            // Convert tag value to byte array
            byte[] tagValueByte = Encoding.UTF8.GetBytes(tagValue);

            // Combine all byte arrays into one (Tag, Length, Value)
            byte[] result = new byte[tagByte.Length + tagValueLenByte.Length + tagValueByte.Length];
            Buffer.BlockCopy(tagByte, 0, result, 0, tagByte.Length);
            Buffer.BlockCopy(tagValueLenByte, 0, result, tagByte.Length, tagValueLenByte.Length);
            Buffer.BlockCopy(tagValueByte, 0, result, tagByte.Length + tagValueLenByte.Length, tagValueByte.Length);

            return result;
        }

        private static byte[] ComputeSHA256(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                return sha256Hash.ComputeHash(bytes);
            }
        }

        private static string ConvertToBase64(byte[] hashBytes)
        {
            return Convert.ToBase64String(hashBytes);
        }

        private static byte[] PadTo32Bytes(byte[] input)
        {
            if (input.Length == 32)
                return input;

            var padded = new byte[32];
            Buffer.BlockCopy(input, 0, padded, 32 - input.Length, input.Length);
            return padded;
        }

        private static void SaveFormatted(XmlDocument doc, string path, bool omitXmlDeclaration = true)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Indent = true,
                IndentChars = "", // use two spaces
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Entitize,
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
        }

        private static void RemoveUnneededTags(XmlDocument doc, XmlNamespaceManager nsMgr)
        {
            XmlNode ublExtensions = doc.SelectSingleNode("//ext:UBLExtensions", nsMgr);
            if (ublExtensions != null) ublExtensions.ParentNode.RemoveChild(ublExtensions);

            XmlNode signature = doc.SelectSingleNode("//cac:Signature", nsMgr);
            if (signature != null) signature.ParentNode.RemoveChild(signature);

            XmlNode qrNode = doc.SelectSingleNode("//cac:AdditionalDocumentReference[cbc:ID='QR']", nsMgr);
            if (qrNode != null) qrNode.ParentNode.RemoveChild(qrNode);
        }

        public static void GenerateXMLWithUBL(XmlDocument doc, string xmlPath)
        {
            XmlElement ublExtensions = doc.CreateElement("ext", "UBLExtensions", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            XmlElement ublExtension = doc.CreateElement("ext", "UBLExtension", ublExtensions.NamespaceURI);
            XmlElement extensionURI = doc.CreateElement("ext", "ExtensionURI", ublExtensions.NamespaceURI);
            extensionURI.InnerText = "urn:oasis:names:specification:ubl:dsig:enveloped:xades";

            XmlElement extensionContent = doc.CreateElement("ext", "ExtensionContent", ublExtensions.NamespaceURI);
            XmlElement ublDocumentSignatures = doc.CreateElement("sig", "UBLDocumentSignatures", "urn:oasis:names:specification:ubl:schema:xsd:CommonSignatureComponents-2");
            ublDocumentSignatures.SetAttribute("xmlns:sac", "urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2");
            ublDocumentSignatures.SetAttribute("xmlns:sbc", "urn:oasis:names:specification:ubl:schema:xsd:SignatureBasicComponents-2");

            XmlElement signatureInfo = doc.CreateElement("sac", "SignatureInformation", "urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2");
            
            signatureInfo.InnerXml = $@"
            <cbc:ID>urn:oasis:names:specification:ubl:signature:1</cbc:ID>
            <sbc:ReferencedSignatureID>urn:oasis:names:specification:ubl:signature:Invoice</sbc:ReferencedSignatureID>
            <ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"" Id=""signature"">
                <ds:SignedInfo>
                    <ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2006/12/xml-c14n11""/>
                    <ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256""/>
                    <ds:Reference Id=""invoiceSignedData"" URI="""">
                        <ds:Transforms>
                            <ds:Transform Algorithm=""http://www.w3.org/TR/1999/REC-xpath-19991116"">
                                <ds:XPath>not(//ancestor-or-self::ext:UBLExtensions)</ds:XPath>
                            </ds:Transform>
                            <ds:Transform Algorithm=""http://www.w3.org/TR/1999/REC-xpath-19991116"">
                                <ds:XPath>not(//ancestor-or-self::cac:Signature)</ds:XPath>
                            </ds:Transform>
                            <ds:Transform Algorithm=""http://www.w3.org/TR/1999/REC-xpath-19991116"">
                                <ds:XPath>not(//ancestor-or-self::cac:AdditionalDocumentReference[cbc:ID='QR'])</ds:XPath>
                            </ds:Transform>
                            <ds:Transform Algorithm=""http://www.w3.org/2006/12/xml-c14n11""/>
                        </ds:Transforms>
                        <ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256""/>
                        <ds:DigestValue></ds:DigestValue>
                    </ds:Reference>
                </ds:SignedInfo>
                <ds:SignatureValue></ds:SignatureValue>
                <ds:KeyInfo>
                    <ds:X509Data>
                        <ds:X509Certificate></ds:X509Certificate>
                    </ds:X509Data>
                </ds:KeyInfo>
                <ds:Object>
                    <xades:QualifyingProperties xmlns:xades=""http://uri.etsi.org/01903/v1.3.2#"" Target=""#signature"">
                        <xades:SignedProperties Id=""xadesSignedProperties"">
                            <xades:SignedSignatureProperties>
                                <xades:SigningTime></xades:SigningTime>
                                <xades:SigningCertificate>
                                    <xades:Cert>
                                        <xades:CertDigest>
                                            <ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" />
                                            <ds:DigestValue></ds:DigestValue>
                                        </xades:CertDigest>
                                        <xades:IssuerSerial>
                                            <ds:X509IssuerName></ds:X509IssuerName>
                                            <ds:X509SerialNumber></ds:X509SerialNumber>
                                        </xades:IssuerSerial>
                                    </xades:Cert>
                                </xades:SigningCertificate>
                            </xades:SignedSignatureProperties>
                        </xades:SignedProperties>
                    </xades:QualifyingProperties>
                </ds:Object>
            </ds:Signature>";

            ublDocumentSignatures.AppendChild(signatureInfo);
            extensionContent.AppendChild(ublDocumentSignatures);
            ublExtension.AppendChild(extensionURI);
            ublExtension.AppendChild(extensionContent);
            ublExtensions.AppendChild(ublExtension);

            XmlElement root = doc.DocumentElement;

            root.PrependChild(ublExtensions);

            SaveFormatted(doc, xmlPath);
        }
    }
}
