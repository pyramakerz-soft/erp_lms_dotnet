﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class QuestionAddDTO
    {
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public IFormFile? ImageFile { get; set; }
        public IFormFile? VideoFile { get; set; }

        public string? CorrectAnswerName { get; set; }
        public long QuestionTypeID { get; set; }
        public long TestID { get; set; }
        public List<string>? options { get; set; }
    }
}
