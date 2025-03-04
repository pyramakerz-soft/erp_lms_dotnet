using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.ECommerce
{
    public class OrderGetDTO
    {
        public long ID { get; set; }
        public int TotalPrice { get; set; }
        public long OrderStateID { get; set; }
        public string OrderStateName { get; set; }
        public long StudentID { get; set; }
        public long CartID { get; set; }
        public DateTime? InsertedAt { get; set; }
        public string MainImage { get; set; }
    }
}
