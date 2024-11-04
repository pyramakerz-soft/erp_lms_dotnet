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

            CreateMap<Employee_Role, RoleDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Role.ID))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.DetailedPermissions, opt => opt.MapFrom(src => src.Role.Role_Permissions));

            //CreateMap<Role_Permissions, Master_Detailes_Permissions>()
            //    .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Detailed_Permissions.ID))
            //    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Detailed_Permissions.Name))
            //    .ForMember(dest => dest.MasterPermission, opt => opt.MapFrom(src => src.Detailed_Permissions.Master_Permissions));

            //CreateMap<Master_Permissions, MasterPermissionDTO>()
            //    .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
            //    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name));


            CreateMap<Domain, DomainDTO>()
          .ForMember(dest => dest.Schools, opt => opt.MapFrom(src => src.Schools));

            CreateMap<DomainDTO, Domain>()
                .ForMember(dest => dest.Schools, opt => opt.MapFrom(src => src.Schools));

            // Mapping School to SchoolDTO
            CreateMap<School, SchoolDTO>();
            // Mapping School to SchoolDTO
            CreateMap<SchoolDTO, School>();
        }
    }
}
