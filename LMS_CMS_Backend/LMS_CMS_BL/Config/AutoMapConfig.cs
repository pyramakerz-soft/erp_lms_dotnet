using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_DAL.Models;
using LMS_CMS_DAL.Models.BusModule;
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
            CreateMap<Page, Page_AddDTO>();
            CreateMap<Page_AddDTO, Page>();

            CreateMap<Page, Page_GetDTO>();

            CreateMap<Bus, Bus_GetDTO>()
                .ForMember(dest => dest.BusTypeName, opt => opt.MapFrom(src => src.BusType.Name))
                .ForMember(dest => dest.BusRestrictName, opt => opt.MapFrom(src => src.BusRestrict.Name))
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
                .ForMember(dest => dest.SemseterName, opt => opt.MapFrom(src => src.Semester.Name));
            CreateMap<BusStudent_AddDTO, BusStudent>();
            CreateMap<BusStudent_PutDTO, BusStudent>();

            CreateMap<BusType, BusTypeGetDTO>();
            CreateMap<BusTypeGetDTO, BusType>();
            CreateMap<BusType, BusTypeAddDTO>();
            CreateMap<BusTypeAddDTO, BusType>();
            CreateMap<BusTypeEditDTO, BusType>();
            CreateMap<BusType, BusTypeEditDTO>();



            CreateMap<BusRestrict, BusRestrictAddDTO>();
            CreateMap<BusRestrictAddDTO, BusRestrict>();
            CreateMap<BusRestrict, BusRestrictGetDTO>();
            CreateMap<BusRestrictGetDTO, BusRestrict>();
            CreateMap<BusRestrictEditDTO, BusRestrict>();
            CreateMap<BusRestrict, BusRestrictEditDTO>();


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

            CreateMap<Domain, DomainGetDTO>();
            CreateMap<DomainGetDTO, Domain>();



        }
    }
}
