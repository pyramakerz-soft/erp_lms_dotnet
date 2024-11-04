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
            CreateMap<Employee_AddDTO, Employee>()
                .ForPath(dest => dest.School.Id, opt => opt.MapFrom(src => src.School_id));

            CreateMap<Employee, Employee_GetDTO>()
                .ForMember(dest => dest.School_Id, opt => opt.MapFrom(src => src.School.Id))
                .ForMember(dest => dest.School_Name, opt => opt.MapFrom(src => src.School.Name))
                .ForMember(dest => dest.Domain_id, opt => opt.MapFrom(src => src.School.Domain.Id))
                .ForMember(dest => dest.Domain_Name, opt => opt.MapFrom(src => src.School.Domain.Name));

            CreateMap<Role_GetDTO, Role>();
            CreateMap<Role, Role_GetDTO>();

            CreateMap<Role_AddDTO, Role>();
            CreateMap<Role, Role_AddDTO>();

            //CreateMap<Role_Permissions, Master_Detailes_Permissions>()
            //    .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Detailed_Permissions.ID))
            //    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Detailed_Permissions.Name))
            //    .ForMember(dest => dest.MasterPermission, opt => opt.MapFrom(src => src.Detailed_Permissions.Master_Permissions));

            //CreateMap<Master_Permissions, MasterPermissionDTO>()
            //    .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
            //    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
