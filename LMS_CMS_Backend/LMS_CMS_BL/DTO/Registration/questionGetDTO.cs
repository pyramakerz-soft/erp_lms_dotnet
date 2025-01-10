using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class questionGetDTO
    {
        [Key]
        public long ID { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public long? CorrectAnswerID { get; set; }
        public long QuestionTypeID { get; set; }
        public long TestID { get; set; }
        public string? CorrectAnswerName { get; set; }
        public string QuestionTypeName { get; set; }
        public string TestName { get; set; }
        public long? InsertedByUserId { get; set; }
        public List<MCQQuestionOptionGetDto> options { get; set; }

    }
}
