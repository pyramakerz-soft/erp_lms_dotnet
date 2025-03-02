using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class FollowUpDrug : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("FollowUp")]
        public long FollowUpId { get; set; }
        public FollowUp? FollowUp { get; set; }

        [ForeignKey("Drug")]
        public long DrugId { get; set; }
        public Drug? Drug { get; set; }


        [ForeignKey("Dose")]
        public long DoseId { get; set; }
        public Dose? Dose { get; set; }
    }
}
