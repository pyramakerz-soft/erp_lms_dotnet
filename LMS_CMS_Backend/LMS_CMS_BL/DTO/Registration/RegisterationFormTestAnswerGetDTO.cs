using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormTestAnswerGetDTO
    {
        public long ID { get; set; }
        public string? EssayAnswer { get; set; }
        public long QuestionID { get; set; }
        public string StudentName { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }

        public long? CorrectAnswerID { get; set; }
        public string? CorrectAnswerName { get; set; }
        public long QuestionTypeID { get; set; }
        public string QuestionTypeName { get; set; }

        public long? AnswerID { get; set; }
        public string? AnswerName { get; set; }



    }
}
