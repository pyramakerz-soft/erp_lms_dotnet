using LMS_CMS_DAL.Models.Domains.Inventory;
using System.Xml;
using System.Xml.Linq;

namespace LMS_CMS_PL.Services.Invoice
{
    public static class XMLInvoice
    {
        //public static XElement GenerateXML(InventoryMaster master, string filePath)
        //{
            //if (master == null)
            //    throw new ArgumentNullException(nameof(master));

            //if (string.IsNullOrWhiteSpace(filePath))
            //    throw new ArgumentNullException(nameof(filePath));

            //if (!File.Exists(filePath))
            //    throw new FileNotFoundException();

            //// Define UBL Namespace
            //XNamespace ns = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            //XNamespace cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            //XNamespace cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
            //XNamespace ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";

            //// Create Invoice XML
            //XElement invoiceXml = new XElement(ns + "Invoice",
            //    new XAttribute(XNamespace.Xmlns + "cac", cac),
            //    new XAttribute(XNamespace.Xmlns + "cbc", cbc),
            //    new XAttribute(XNamespace.Xmlns + "ext", ext),

            //    new XElement(cbc + "ProfileID", "reporting:1.0"),
            //    new XElement(cbc + "ID", master.InvoiceNumber),
            //    new XElement(cbc + "UUID", Guid.NewGuid()),
            //    new XElement(cbc + "IssueDate", master.Date),
            //    new XElement(cbc + "IssueTime", master.Date),
            //    new XElement(cbc + "InvoiceTypeCode", new XAttribute("name", "0200000"), "388"),
            //    new XElement(cbc + "Note", new XAttribute("languageID", "ar"), "ABC"),
            //    new XElement(cbc + "DocumentCurrencyCode", "SAR"),
            //    new XElement(cbc + "TaxCurrencyCode", "SAR"),

            //    new XElement(cac + "AdditionalDocumentReference",
            //        new XElement(cbc + "ID", "ICV"),
            //        new XElement(cbc + "UUID", "10")
            //    ),

            //    new XElement(cac + "AccountingSupplierParty",
            //        new XElement(cac + "Party",
            //            new XElement(cac + "PartyIdentification",
            //                new XElement(cbc + "ID", new XAttribute("schemeID", "CRN"), master.CRN)
            //            ),
            //            new XElement(cac + "PostalAddress",
            //                new XElement(cbc + master.Student.Street),
            //                new XElement(cbc + master.Student.BuildingNumber),
            //                new XElement(cbc + master.Student.CitySubdivision),
            //                new XElement(cbc + master.Student.City),
            //                new XElement(cbc + master.Student.PostalCode),
            //                new XElement(cac + master.Student.Country,
            //                    new XElement(cbc + "IdentificationCode", "SA")
            //                )
            //            ),
            //            new XElement(cac + "PartyTaxScheme",
            //                new XElement(cbc + "CompanyID", master.TIN),
            //                new XElement(cac + "TaxScheme",
            //                    new XElement(cbc + "ID", "VAT")
            //                )
            //            ),
            //            new XElement(cac + "PartyLegalEntity",
            //                new XElement(cbc + master.OrganizationName)
            //            )
            //        )
            //    ),

                //new XElement(cac + "AccountingCustomerParty",
                //    new XElement(cac + "Party",
                //        new XElement(cac + "PostalAddress",
                //            new XElement(cbc + "StreetName", "صلاح الدين | Salah Al-Din"),
                //            new XElement(cbc + "BuildingNumber", "1111"),
                //            new XElement(cbc + "CitySubdivisionName", "المروج | Al-Murooj"),
                //            new XElement(cbc + "CityName", "الرياض | Riyadh"),
                //            new XElement(cbc + "PostalZone", "12222"),
                //            new XElement(cac + "Country",
                //                new XElement(cbc + "IdentificationCode", "SA")
                //            )
                //        ),
                //        new XElement(cac + "PartyTaxScheme",
                //            new XElement(cbc + "CompanyID", "399999999800003"),
                //            new XElement(cac + "TaxScheme",
                //                new XElement(cbc + "ID", "VAT")
                //            )
                //        ),
                //        new XElement(cac + "PartyLegalEntity",
                //            new XElement(cbc + "RegistrationName", "شركة نماذج فاتورة المحدودة | Fatoora Samples LTD")
                //        )
                //    )
                //),

            //    new XElement(cac + "PaymentMeans",
            //        new XElement(cbc + "PaymentMeansCode", "10")
            //    ),

            //    new XElement(cac + "TaxTotal",
            //        new XElement(cbc + "TaxAmount", new XAttribute("currencyID", "SAR"), master.VatAmount)
            //    ),

            //    new XElement(cac + "LegalMonetaryTotal",
            //        new XElement(cbc + "LineExtensionAmount", new XAttribute("currencyID", "SAR"), master.Total),
            //        new XElement(cbc + "TaxExclusiveAmount", new XAttribute("currencyID", "SAR"), master.Total),
            //        new XElement(cbc + "TaxInclusiveAmount", new XAttribute("currencyID", "SAR"), master.TotalWithVat),
            //        new XElement(cbc + "AllowanceTotalAmount", new XAttribute("currencyID", "SAR"), "0.00"), //ask
            //        new XElement(cbc + "PrepaidAmount", new XAttribute("currencyID", "SAR"), "0.00"),
            //        new XElement(cbc + "PayableAmount", new XAttribute("currencyID", "SAR"), master.TotalWithVat)
            //    ),

            //    master.InventoryDetails.Select(item =>
            //        new XElement(cac + "InvoiceLine",
            //            new XElement(cbc + "ID", item.ID),
            //            new XElement(cbc + "InvoicedQuantity", new XAttribute("unitCode", "PCE"), item.Quantity),
            //            new XElement(cbc + "LineExtensionAmount", new XAttribute("currencyID", "SAR"), item.Price),
            //            new XElement(cac + "TaxTotal",
            //                new XElement(cbc + "TaxAmount", new XAttribute("currencyID", "SAR"), item.ShopItem.VATForForeign),
            //                new XElement(cbc + "RoundingAmount", new XAttribute("currencyID", "SAR"), item.TotalPrice) //Ask
            //        ),
            //        new XElement(cac + "Item",
            //            new XElement(cbc + "Name", item.ShopItem.ArName),
            //            new XElement(cac + "ClassifiedTaxCategory",
            //                new XElement(cbc + "ID", "S"),
            //                new XElement(cbc + "Percent", "15.00"),
            //                new XElement(cac + "TaxScheme",
            //                    new XElement(cbc + "ID", "VAT")
            //                )
            //            )
            //        ),
            //        new XElement(cac + "Price",
            //            new XElement(cbc + "PriceAmount", new XAttribute("currencyID", "SAR"), item.ShopItem.SalesPrice),
            //            new XElement(cac + "AllowanceCharge",
            //                new XElement(cbc + "ChargeIndicator", "true"),
            //                new XElement(cbc + "AllowanceChargeReason", "discount"),
            //                new XElement(cbc + "Amount", new XAttribute("currencyID", "SAR"), "0.00")
            //            )
            //        )
            //    )
            //    )
            //);

            //invoiceXml.Save(filePath);

            //return invoiceXml;
        //}

        public static void SignXML(InventoryMaster master, string filePath)
        {
            if (master == null)
                throw new ArgumentNullException(nameof(master));
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException();
            // Load the XML file
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            // Create the Signature element
            var signatureElement = CreateSignatureElement();
            // Append the Signature element to the XML document
            xmlDoc.DocumentElement.AppendChild(signatureElement);
            // Save the XML document
            xmlDoc.Save(filePath);
        }

        private static XmlElement CreateSignatureElement()
        {
            var xmlDoc = new XmlDocument();

            // Define namespaces
            var nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
            nsManager.AddNamespace("sig", "urn:oasis:names:specification:ubl:schema:xsd:CommonSignatureComponents-2");
            nsManager.AddNamespace("sac", "urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2");
            nsManager.AddNamespace("sbc", "urn:oasis:names:specification:ubl:schema:xsd:SignatureBasicComponents-2");
            nsManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            nsManager.AddNamespace("xades", "http://uri.etsi.org/01903/v1.3.2#");

            // Create UBLDocumentSignatures element
            var ublDocSignatures = xmlDoc.CreateElement("sig", "UBLDocumentSignatures", nsManager.LookupNamespace("sig"));
            ublDocSignatures = xmlDoc.CreateElement("sac", "UBLDocumentSignatures", nsManager.LookupNamespace("sac"));
            ublDocSignatures = xmlDoc.CreateElement("sbc", "UBLDocumentSignatures", nsManager.LookupNamespace("sbc"));

            // Create SignatureInformation element
            var signatureInfo = xmlDoc.CreateElement("sac", "SignatureInformation", nsManager.LookupNamespace("sac"));

            // Create ID element
            var idElement = xmlDoc.CreateElement("cbc", "ID", nsManager.LookupNamespace("cbc"));
            idElement.InnerText = "urn:oasis:names:specification:ubl:signature:1";
            signatureInfo.AppendChild(idElement);

            // Create ReferencedSignatureID element
            var refSigIdElement = xmlDoc.CreateElement("sbc", "ReferencedSignatureID", nsManager.LookupNamespace("sbc"));
            refSigIdElement.InnerText = "urn:oasis:names:specification:ubl:signature:Invoice";
            signatureInfo.AppendChild(refSigIdElement);

            // Create Signature element
            var signatureElement = xmlDoc.CreateElement("ds", "Signature", nsManager.LookupNamespace("ds"));
            signatureElement.SetAttribute("Id", "signature");

            // Populate SignedInfo, SignatureValue, KeyInfo, and Object elements as per your requirements
            // This includes setting up CanonicalizationMethod, SignatureMethod, References, DigestValues, etc.

            // Append Signature to SignatureInformation
            signatureInfo.AppendChild(signatureElement);

            // Append SignatureInformation to UBLDocumentSignatures
            ublDocSignatures.AppendChild(signatureInfo);

            // Import the constructed element into the XmlDocument
            var importedElement = xmlDoc.ImportNode(ublDocSignatures, true) as XmlElement;

            return importedElement;
        }
    }
}
