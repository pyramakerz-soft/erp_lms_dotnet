﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class MCQQuestionOptionGetDto
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
