using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class StoreCategoriesEditDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<long> categoriesIds { get; set; }
    }
}
