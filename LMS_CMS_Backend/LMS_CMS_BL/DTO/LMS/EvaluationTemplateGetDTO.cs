using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationTemplateGetDTO
    {
        public long ID { get; set; }
        public string EnglishTitle { get; set; }
        public string ArabicTitle { get; set; }
        public int Weight { get; set; }
        public int AfterCount { get; set; }
        public ICollection<EvaluationTemplateGroupDTO> EvaluationTemplateGroups { get; set; } = new HashSet<EvaluationTemplateGroupDTO>();
        public long? InsertedByUserId { get; set; }

    }
}
