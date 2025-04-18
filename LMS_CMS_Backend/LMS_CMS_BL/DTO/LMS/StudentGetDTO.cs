﻿using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class StudentGetDTO
    {
        public long ID { get; set; }
        public string User_Name { get; set; }
        public string en_name { get; set; }
        public string ar_name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public long? Nationality { get; set; }
        public string? NationalityEnName { get; set; }
        public string? NationalityArName { get; set; }
        public string? NationalIDExpiredDate { get; set; }
        public long? AccountNumberID { get; set; }
        public string? AccountNumberName { get; set; }
        public long GenderId { get; set; }
        public string GenderName { get; set; }

        public long? Parent_Id { get; set; }
        public string DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string PassportNo { get; set; }
        public string? PassportExpiredDate { get; set; }
        public string Religion { get; set; }
        public string MotherName { get; set; }
        public string MotherPassportNo { get; set; }
        public string? MotherPassportExpireDate { get; set; }
        public string MotherNationalID { get; set; }
        public string? MotherNationalIDExpiredDate { get; set; }
        public string MotherQualification { get; set; }
        public string MotherWorkPlace { get; set; }
        public string PreviousSchool { get; set; }
        public long? RegistrationFormParentID { get; set; }
        public long? StartAcademicYearID { get; set; }
        public string? StartAcademicYearName { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactRelation { get; set; }
        public string? EmergencyContactMobile { get; set; }
        public string? PickUpContactName { get; set; }
        public string? PickUpContactRelation { get; set; }
        public string? PickUpContactMobile { get; set; }
        public bool? IsRegisteredInNoor { get; set; } 
        public string? MotherExperiences { get; set; }
        public string? MotherProfession { get; set; }
        public string? MotherMobile { get; set; }
        public string? MotherEmail { get; set; }
        public string? GuardianRelation { get; set; }
        public long? InsertedByUserId { get; set; }
        public DateTime? InsertedAt { get; set; }

        public string GuardianName { get; set; }
        public string GuardianPassportNo { get; set; }
        public string? GuardianPassportExpireDate { get; set; }
        public string GuardianNationalID { get; set; }
        public string? GuardianNationalIDExpiredDate { get; set; }
        public string GuardianQualification { get; set; }
        public string GuardianWorkPlace { get; set; }
        public string GuardianEmail { get; set; }
        public string GuardianProfession { get; set; }
        public string? AdmissionDate { get; set; }

        public string IsRegisteredToBus { get; set;}

        public string CurrentGradeName { get; set;}
        public string CurrentAcademicYear { get; set;}
    }
}
