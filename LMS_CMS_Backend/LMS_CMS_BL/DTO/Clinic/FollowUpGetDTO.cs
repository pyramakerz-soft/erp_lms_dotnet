﻿using LMS_CMS_DAL.Models.Domains.ClinicModule;
using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class FollowUpGetDTO
    {
        public long ID { get; set; }
        public long SchoolId { get; set; }
        public string School { get; set; }
        public long GradeId { get; set; }
        public string Grade { get; set; }
        public long ClassroomId { get; set; }
        public string Classroom { get; set; }
        public long StudentId { get; set; }
        public string Student { get; set; }
        public string? Complains { get; set; }
        public long DiagnosisId { get; set; }
        public string? Diagnosis { get; set; }

        public ICollection<FollowUpDrugGetDTO> FollowUpDrugs { get; set; } = new HashSet<FollowUpDrugGetDTO>();

        public string? Recommendation { get; set; }
        public bool SendSMSToParent { get; set; }
        public DateTime InsertedAt { get; set; }
    }
}
