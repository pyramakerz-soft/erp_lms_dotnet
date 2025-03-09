using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class StockingGetDto
    {
        public long? ID { get; set; }
        public string Date { get; set; }
        public int InvoiceNumber { get; set; }
    }
}
