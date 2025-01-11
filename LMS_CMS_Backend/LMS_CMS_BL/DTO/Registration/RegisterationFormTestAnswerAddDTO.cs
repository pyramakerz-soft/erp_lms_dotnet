using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormTestAnswerAddDTO
    {
        public string? EssayAnswer { get; set; }
        public long QuestionID { get; set; }
        public long RegisterationFormParentID { get; set; }
        public long? AnswerID { get; set; }

    }
}
