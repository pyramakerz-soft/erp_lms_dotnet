using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class InstallmentDeductionMasterGetDTO
    {
        [Key]
        public long ID { get; set; }
        public int? DocNumber { get; set; }
        public string Date { get; set; }
        public string? Notes { get; set; }
        public long EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public long StudentID { get; set; }
        public string? StudentName { get; set; }
        public long InsertedByUserId { get; set; }
    }
}
