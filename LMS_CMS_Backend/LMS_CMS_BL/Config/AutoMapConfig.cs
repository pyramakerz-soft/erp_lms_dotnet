using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.Config
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.user_Name, opt => opt.MapFrom(src => src.User_Name))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Employee_Roles));

            CreateMap<Role_GetDTO, Role>();
            CreateMap<Role, Role_GetDTO>();

            CreateMap<Role_AddDTO, Role>();
            CreateMap<Role, Role_AddDTO>();

            CreateMap<Domain, DomainDTO>()
          .ForMember(dest => dest.Schools, opt => opt.MapFrom(src => src.Schools));

            CreateMap<DomainDTO, Domain>()
                .ForMember(dest => dest.Schools, opt => opt.MapFrom(src => src.Schools));

            CreateMap<School, SchoolDTO>();

            CreateMap<SchoolDTO, School>();

            CreateMap<DomainUpdateDTO, Domain>();

            CreateMap<Domain, DomainUpdateDTO>();

            CreateMap<Domain, DomainAddDTO>();

            CreateMap<DomainAddDTO, Domain>();

            CreateMap<SchoolAddDTO, School>()
              .ForMember(dest => dest.Domain_id, opt => opt.MapFrom(src => src.DomainId));

            CreateMap<School, SchoolAddDTO>();

            CreateMap<SchoolDTO, School>()
                .ForMember(dest => dest.Domain_id, opt => opt.MapFrom(src => src.DomainId));

            CreateMap<School, SchoolDTO>();

        }
    }
}
