using AutoMapper;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LMS_CMS_PL.Services
{
    public class InVoiceNumberCreate
    {


        //public InVoiceNumberCreate(LMS_CMS_Context db)
        //{
        //    this.db = db;
        //}
        public async Task<int> GetNextInvoiceNumber(LMS_CMS_Context db , long storeId, long flagId)
        {
            int lastInvoice = await db.InventoryMaster
                .Where(x => x.StoreID == storeId && x.FlagId == flagId)
                .OrderByDescending(x => x.InvoiceNumber)
                .Select(x => x.InvoiceNumber)
                .FirstOrDefaultAsync();

            string invoiceStr = lastInvoice.ToString();

            string trimmedInvoice = invoiceStr.Length > 2 ? invoiceStr.Substring(2) : "0";
            int cleanedInvoice = int.Parse(trimmedInvoice);

            int nextNumber = cleanedInvoice != 0 ? cleanedInvoice + 1 : 1;

            return int.Parse($"{storeId}{flagId}{nextNumber}"); 
        }
    }
}
