using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class QuestionEditDTO
    {
        [Key]
        public long ID { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? CorrectAnswer { get; set; }
        public long QuestionTypeID { get; set; }
        public long TestID { get; set; }
        public List<string> options { get; set; }

    }
}
