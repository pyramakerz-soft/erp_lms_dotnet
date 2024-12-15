using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Octa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.Config
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            CreateMap<LMS_CMS_DAL.Models.Domains.Page, Page_AddDTO>();
            CreateMap<Page_AddDTO,  LMS_CMS_DAL.Models.Domains.Page > ();

            CreateMap <LMS_CMS_DAL.Models.Domains.Page, Page_GetDTO >();

            CreateMap<Bus, Bus_GetDTO>()
                .ForMember(dest => dest.BusTypeName, opt => opt.MapFrom(src => src.BusType.Name))
                .ForMember(dest => dest.BusDistrictName, opt => opt.MapFrom(src => src.BusDistrict.Name))
                .ForMember(dest => dest.BusStatusName, opt => opt.MapFrom(src => src.BusStatus.Name))
                .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.Driver.en_name))
                .ForMember(dest => dest.DriverAssistantName, opt => opt.MapFrom(src => src.DriverAssistant.en_name))
                .ForMember(dest => dest.BusCompanyName, opt => opt.MapFrom(src => src.BusCompany.Name));
            CreateMap<Bus_AddDTO, Bus>();
            CreateMap<Bus_PutDTO, Bus>();

            CreateMap<BusStudent, BusStudentGetDTO>()
                .ForMember(dest => dest.BusName, opt => opt.MapFrom(src => src.Bus.Name))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.en_name))
                .ForMember(dest => dest.BusCategoryName, opt => opt.MapFrom(src => src.BusCategory.Name))
                .ForMember(dest => dest.SemseterName, opt => opt.MapFrom(src => src.Semester.Name))
                .ForMember(dest => dest.StudentAcademicYear, opt => opt.MapFrom(src => src.Semester.AcademicYear.Name));

            CreateMap<BusStudent_AddDTO, BusStudent>();
            CreateMap<BusStudent_PutDTO, BusStudent>();

            CreateMap<BusType, BusTypeGetDTO>();
            CreateMap<BusTypeGetDTO, BusType>();
            CreateMap<BusType, BusTypeAddDTO>();
            CreateMap<BusTypeAddDTO, BusType>();
            CreateMap<BusTypeEditDTO, BusType>();
            CreateMap<BusType, BusTypeEditDTO>();



            CreateMap<BusDistrict, BusDistrictAddDTO>();
            CreateMap<BusDistrictAddDTO, BusDistrict>();
            CreateMap<BusDistrict, BusDistrictGetDTO>();
            CreateMap<BusDistrictGetDTO, BusDistrict>();
            CreateMap<BusDistrictEditDTO, BusDistrict>();
            CreateMap<BusDistrict, BusDistrictEditDTO>();


            CreateMap<BusCategory, BusCatigoryAddDTO>();
            CreateMap<BusCatigoryAddDTO, BusCategory>();
            CreateMap<BusCategory, BusCatigoryGetDTO>();
            CreateMap<BusCatigoryGetDTO, BusCategory>();
            CreateMap<BusCategory, BusCategoryEditDTO>();
            CreateMap<BusCategoryEditDTO, BusCategory>();

            CreateMap<BusStatus, BusStatusAddDTO>();
            CreateMap<BusStatusAddDTO, BusStatus>();
            CreateMap<BusStatus, BusStatusGetDTO>();
            CreateMap<BusStatusGetDTO, BusStatus>();
            CreateMap<BusStatus, BusStatusEditDTO>();
            CreateMap<BusStatusEditDTO, BusStatus>();

            CreateMap<BusCompany, BusCompanyAddDTO>();
            CreateMap<BusCompanyAddDTO, BusCompany>();
            CreateMap<BusCompany, BusCompanyGetDTO>();
            CreateMap<BusCompanyGetDTO, BusCompany>();
            CreateMap<BusCompany, BusCompanyEditDTO>();
            CreateMap<BusCompanyEditDTO, BusCompany>();

            CreateMap<Employee, Employee_GetDTO>();

            CreateMap<Parent, ParentGetDTO>();
            CreateMap<ParentGetDTO, Parent>();

            CreateMap<Student, StudentGetDTO>();
            CreateMap<StudentGetDTO, Student>();

            CreateMap<Semester, Semester_GetDTO>();

            CreateMap<LMS_CMS_DAL.Models.Octa.Page, Page_GetDTO>();
            CreateMap<Page_GetDTO, LMS_CMS_DAL.Models.Octa.Page>();

            CreateMap<AcademicYear, AcademicYearGet>();
            CreateMap<AcademicYearGet, AcademicYear>();


        }
    }
}
