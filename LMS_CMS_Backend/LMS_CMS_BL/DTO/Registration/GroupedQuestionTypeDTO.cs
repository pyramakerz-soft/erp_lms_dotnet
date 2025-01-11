using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class GroupedQuestionTypeDTO
    {
        public long QuestionTypeID { get; set; }
        public string QuestionTypeName { get; set; }
        public List<RegisterationFormTestAnswerGetDTO> Questions { get; set; }
    }
}
