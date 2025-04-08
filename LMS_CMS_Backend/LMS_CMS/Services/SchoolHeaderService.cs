using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace LMS_CMS_PL.Services
{
    public class SchoolHeaderService
    {
        IMapper mapper;
        public SchoolHeaderService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public School_GetDTO GetSchoolHeader(UOW Unit_Of_Work, long schoolId, HttpRequest Request)
        {

            School school = Unit_Of_Work.school_Repository.First_Or_Default(
                d => d.IsDeleted != true && d.ID == schoolId
                );
            if (school == null)
            { 
                throw new Exception("No School with this Id");
            }

            School_GetDTO schoolDTO = mapper.Map<School_GetDTO>(school);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";
            if (!string.IsNullOrEmpty(schoolDTO.ReportImage))
            {
                schoolDTO.ReportImage = $"{serverUrl}{schoolDTO.ReportImage.Replace("\\", "/")}";
            }

            return schoolDTO;
        }
    }
}
