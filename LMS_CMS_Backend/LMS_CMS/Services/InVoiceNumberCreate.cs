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
        public async Task<string> GetNextInvoiceNumber(LMS_CMS_Context db, long storeId, long flagId)
        {
            string prefix = storeId.ToString() + flagId.ToString();

            var invoiceNumbers = await db.InventoryMaster
                .Where(x => x.StoreID == storeId && x.FlagId == flagId && x.InvoiceNumber.StartsWith(prefix))
                .Select(x => x.InvoiceNumber)
                .ToListAsync();

            long maxNumber = 0;

            if (invoiceNumbers.Any())
            {
                maxNumber = invoiceNumbers
                    .Select(inv =>
                    {
                        var suffix = inv.Length > prefix.Length ? inv.Substring(prefix.Length) : "0";
                        return long.TryParse(suffix, out var num) ? num : 0;
                    })
                    .Max();
            }

            long nextNumber = maxNumber + 1;

            return $"{prefix}{nextNumber}";
        }

    }
}
