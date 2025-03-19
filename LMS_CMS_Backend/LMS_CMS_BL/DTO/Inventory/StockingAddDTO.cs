using LMS_CMS_DAL.Models.Domains.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class StockingAddDTO
    {
        public string Date { get; set; }
        public long StoreID { get; set; }

        public List<StockingDetailsAddDto>? StockingDetails { get; set; }
    }
}
