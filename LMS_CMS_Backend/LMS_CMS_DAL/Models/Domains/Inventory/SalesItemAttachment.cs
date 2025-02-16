using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class SalesItemAttachment : AuditableEntity
    {
        [Key]
        public long Id { get; set; }
        public string Link { get; set; }

        [ForeignKey("SalesItem")]
        public long SalesItemID { get; set; }

        public SalesItem SalesItem { get; set; }
    }
}
