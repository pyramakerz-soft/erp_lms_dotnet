﻿using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class DiagnosisAddDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
