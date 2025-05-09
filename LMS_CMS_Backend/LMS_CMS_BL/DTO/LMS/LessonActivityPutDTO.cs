﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class LessonActivityPutDTO
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "English Title is required")]
        [StringLength(100, ErrorMessage = "English Title cannot be longer than 100 characters.")]
        public string EnglishTitle { get; set; }
        [Required(ErrorMessage = "Arabic Title is required")]
        [StringLength(100, ErrorMessage = "Arabic Title cannot be longer than 100 characters.")]
        public string ArabicTitle { get; set; }
        public string? AttachmentLink { get; set; }
        public IFormFile? AttachmentFile { get; set; }
        public int Order { get; set; }
        public string? Details { get; set; }
        public long LessonID { get; set; }
        public long LessonActivityTypeID { get; set; }
    }
}
