using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class InstallmentDeductionDetails : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
        [ForeignKey("InstallmentDeductionMaster")]
        public long InstallmentDeductionMasterID { get; set; }
        [ForeignKey("TuitionFeesType")]
        public long FeeTypeID { get; set; }

        public InstallmentDeductionMaster InstallmentDeductionMaster { get; set; }
        public TuitionFeesType TuitionFeesType { get; set; }
    }
}
