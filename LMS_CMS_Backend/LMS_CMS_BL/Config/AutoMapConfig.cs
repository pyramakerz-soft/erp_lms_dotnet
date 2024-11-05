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
            CreateMap<Employee_AddDTO, Employee>();

            CreateMap<Employee, Employee_GetDTO>()
                .ForMember(dest => dest.School_Id, opt => opt.MapFrom(src => src.School.Id))
                .ForMember(dest => dest.School_Name, opt => opt.MapFrom(src => src.School.Name))
                .ForMember(dest => dest.Domain_id, opt => opt.MapFrom(src => src.School.Domain.ID))
                .ForMember(dest => dest.Domain_Name, opt => opt.MapFrom(src => src.School.Domain.Name));

            CreateMap<Employee_GetDTO, Employee>()
                .ForMember(dest => dest.School_id, opt => opt.MapFrom(src => src.School_Id));

            CreateMap<Employee_PutDTO, Employee>();
            CreateMap<Employee, Employee_PutDTO>();

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

            CreateMap<Role_Permissions_AddDTO, Role_Permissions>();



            CreateMap<School_Roles, schoolRoleDTO>()
                .ForMember(dest => dest.Role_Name, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.School_Name, opt => opt.MapFrom(src => src.School.Name));



            CreateMap<schoolRoleDTO, School_Roles>();


            CreateMap<Employee_Role, Employee_Role_GetDTO>()
                .ForMember(dest => dest.Role_Name, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Employee_Name, opt => opt.MapFrom(src => src.Employee.User_Name));

            CreateMap<Employee_Role_AddDTO, Employee_Role>();
            CreateMap<Employee_Role_PutDTO, Employee_Role>();

            CreateMap<Domain_Modules, Domain_Modules_GetDTO>()
                .ForMember(dest => dest.Domain_Name, opt => opt.MapFrom(src => src.Domain.Name))
                .ForMember(dest => dest.Module_Name, opt => opt.MapFrom(src => src.Module.Name));

        }
    }
}
