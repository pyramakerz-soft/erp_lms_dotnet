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

            CreateMap<BusType, BusTypeGetDTO>();
            CreateMap<BusTypeGetDTO, BusType>();

            CreateMap<BusType, BusTypeAddDTO>();
            CreateMap<BusTypeAddDTO, BusType>();
        }
    }
}
