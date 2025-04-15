using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Services
{
    public class CreateStudentService
    {
        public async Task<StudentGetDTO> CreateStudentDtoObj(UOW Unit_Of_Work, long registrationFormParentID)
        {
            var registerationFormParent = Unit_Of_Work.registerationFormParent_Repository
                .First_Or_Default(r => r.ID == registrationFormParentID && r.IsDeleted != true); 
             
            var submittionAName = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 2);
            var submittionEName = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 1);
            var submittionGender = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 3);
            var submittionNationality = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 5);
            var submittionBirthDate = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 4);
            var submittionPassport = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 12);
            var submittionReligion = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 6);
            var submittionMotherName = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 22);
            var submittionMotherPassport = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 23);
            var submittionMotherIDNo = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 24);
            var submittionMotherQualification = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 25);
            var submittionMotherWork = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 26);
            var submittionMotherMobile = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 27);
            var submittionMotherEmail = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 28);
            var submittionPrevSchool = GetSubmissionByCategory(Unit_Of_Work, registrationFormParentID, 13);

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerationFormParent.StudentName);

            long parentId = 0;
            if (registerationFormParent.ParentID != null)
            {
                parentId = Convert.ToInt64(registerationFormParent.ParentID);
            }

            var studentDto = new StudentGetDTO
            {
                en_name = submittionEName?.TextAnswer,
                ar_name = submittionAName?.TextAnswer,
                User_Name = registerationFormParent.StudentName,
                Nationality = Convert.ToInt64(submittionNationality?.TextAnswer),
                GenderId = Convert.ToInt64(submittionGender?.TextAnswer),
                DateOfBirth = submittionBirthDate?.TextAnswer,
                Password = hashedPassword,
                PassportNo = submittionPassport?.TextAnswer,
                Religion = submittionReligion?.TextAnswer,
                MotherName = submittionMotherName?.TextAnswer,
                MotherPassportNo = submittionMotherPassport?.TextAnswer,
                MotherNationalID = submittionMotherIDNo?.TextAnswer,
                MotherQualification = submittionMotherQualification?.TextAnswer,
                MotherWorkPlace = submittionMotherWork?.TextAnswer,
                MotherEmail = submittionMotherEmail?.TextAnswer,
                MotherMobile = submittionMotherMobile?.TextAnswer,
                PreviousSchool = submittionPrevSchool?.TextAnswer,
                RegistrationFormParentID = registrationFormParentID,
                Parent_Id = parentId != 0 ? parentId : null,  
            }; 

            return studentDto;
        }

        private RegisterationFormSubmittion GetSubmissionByCategory(UOW Unit_Of_Work, long registrationFormParentID, int categoryFieldID)
        {
            RegisterationFormSubmittion submittion = Unit_Of_Work.registerationFormSubmittion_Repository.First_Or_Default(r => r.CategoryFieldID == categoryFieldID && r.RegisterationFormParentID == registrationFormParentID);

            return submittion;
        }

        public async Task<Student> CreateNewStudent(UOW Unit_Of_Work, StudentGetDTO studentDto, string userTypeClaim, long userId, long startAcademicYearID)
        {
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            Student student = new Student
            {
                en_name = studentDto.en_name,
                ar_name = studentDto.ar_name,
                User_Name = studentDto.User_Name,
                Nationality = studentDto.Nationality,
                Parent_Id = studentDto.Parent_Id,
                GenderId = studentDto.GenderId,
                Password = studentDto.Password,
                DateOfBirth = studentDto.DateOfBirth,
                PassportNo = studentDto.PassportNo,
                Religion = studentDto.Religion,
                MotherName = studentDto.MotherName,
                MotherPassportNo = studentDto.MotherPassportNo,
                MotherNationalID = studentDto.MotherNationalID,
                MotherQualification = studentDto.MotherQualification,
                MotherWorkPlace = studentDto.MotherWorkPlace,
                MotherEmail = studentDto.MotherEmail,
                MotherMobile = studentDto.MotherMobile,
                PreviousSchool = studentDto.PreviousSchool,
                RegistrationFormParentID = studentDto.RegistrationFormParentID,
                StartAcademicYearID = startAcademicYearID,
                AdmissionDate = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone).ToString("yyyy-MM-dd"),
                InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone),
                InsertedByOctaId = userTypeClaim == "octa" ? userId : null,
                InsertedByUserId = userTypeClaim == "employee" ? userId : null
            };

            Unit_Of_Work.student_Repository.Add(student);
            await Unit_Of_Work.SaveChangesAsync();

            return student;
        }
    }
}
