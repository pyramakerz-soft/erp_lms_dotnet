﻿using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Zatca
{
    public class SchoolPCsPutDTO
    {
        public long ID { get; set; }
        [Required]
        public string PCName { get; set; }
        public long? SchoolId { get; set; }
        public DateOnly? CertificateDate { get; set; }
    }
}
