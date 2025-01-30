using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class LinkFile
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public string ArName { get; set; }
        public string? TableName { get; set; }
        [ForeignKey("MotionType")]
        public long? MotionTypeID { get; set; }

        public MotionType MotionType { get; set; }
        public ICollection<AccountingTreeChart> AccountingTreeCharts { get; set; } = new HashSet<AccountingTreeChart>();
        public ICollection<ReceivableMaster> ReceivableMasters { get; set; } = new HashSet<ReceivableMaster>();
        public ICollection<ReceivableDetails> ReceivableDetails { get; set; } = new HashSet<ReceivableDetails>();
        public ICollection<PayableMaster> PayableMaster { get; set; } = new HashSet<PayableMaster>();
        public ICollection<PayableDetails> PayableDetails { get; set; } = new HashSet<PayableDetails>();
    }
}
