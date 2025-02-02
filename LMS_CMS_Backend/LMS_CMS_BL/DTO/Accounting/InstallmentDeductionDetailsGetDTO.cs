using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class InstallmentDeductionDetailsGetDTO
    {
        public long ID { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
        public long InstallmentDeductionMasterID { get; set; }
        public string? InstallmentDeductionMasterName { get; set; }
        public long FeeTypeID { get; set; }
        public string? FeeTypeName { get; set; }

    }
}
