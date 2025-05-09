﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationTemplateGroupDTO
    {
        public long ID { get; set; }
        public string EnglishTitle { get; set; }
        public string ArabicTitle { get; set; }
        public long EvaluationTemplateID { get; set; }
        public List<EvaluationTemplateGroupQuestionGetDTO> EvaluationTemplateGroupQuestions { get; set; }
        public long? InsertedByUserId { get; set; }

    }
}
