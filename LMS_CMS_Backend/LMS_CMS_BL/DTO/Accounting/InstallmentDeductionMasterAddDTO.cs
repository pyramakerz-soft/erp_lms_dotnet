using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class InstallmentDeductionMasterAddDTO
    {
        public int? DocNumber { get; set; }
        public string Date { get; set; }
        public string? Notes { get; set; }
        public long EmployeeID { get; set; }
        public long StudentID { get; set; }
    }
}
