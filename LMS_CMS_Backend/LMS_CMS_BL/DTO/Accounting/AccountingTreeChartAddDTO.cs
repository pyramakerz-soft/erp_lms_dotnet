using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class AccountingTreeChartAddDTO
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public long? MainAccountNumberID { get; set; }
        public long SubTypeID { get; set; }
        public long? EndTypeID { get; set; }
        public long? LinkFileID { get; set; }
        public long? MotionTypeID { get; set; }
    }
}
