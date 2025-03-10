using LMS_CMS_DAL.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Services
{
    public class CalculateCurrentStock
    {
        public async Task<int> GetCurrentStock(LMS_CMS_Context db, long storeId, long shopItemId, string date)
        {
            var totalStock = await (from details in db.InventoryDetails
                                    join master in db.InventoryMaster on details.InventoryMasterId equals master.ID
                                    join flag in db.InventoryFlags on master.FlagId equals flag.ID
                                    where master.StoreID == storeId
                                          && details.ShopItemID == shopItemId
                                          && master.Date.CompareTo(date) <= 0  
                                          && master.IsDeleted != true
                                          && details.IsDeleted != true
                                    select details.Quantity * flag.ItemInOut
                                   ).SumAsync();

            return totalStock;
        }

        //////
        public async Task<int> GetCurrentStockInTransformedStore(LMS_CMS_Context db, long storeId, long shopItemId, string date)
        {
            var totalStock = await (from details in db.InventoryDetails
                                    join master in db.InventoryMaster on details.InventoryMasterId equals master.ID
                                    join flag in db.InventoryFlags on master.FlagId equals flag.ID
                                    where master.StoreToTransformId == storeId
                                          && details.ShopItemID == shopItemId
                                          && master.Date.CompareTo(date) <= 0
                                          && master.IsDeleted != true
                                          && details.IsDeleted != true
                                    select details.Quantity * 1
                                   ).SumAsync();

            return totalStock;
        }

    }
}
