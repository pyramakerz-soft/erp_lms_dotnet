using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.DTO.Violation;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.Violations;
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

            CreateMap<Employee, Employee_GetDTO>()
                .ForMember(dest => dest.Role_Name, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Role_ID, opt => opt.MapFrom(src => src.Role.ID))
                .ForMember(dest => dest.EmployeeTypeName, opt => opt.MapFrom(src => src.EmployeeType.Name))
                .ForMember(dest => dest.EmployeeTypeID, opt => opt.MapFrom(src => src.EmployeeType.ID))
                .ForMember(dest => dest.BusCompanyID, opt => opt.MapFrom(src => src.BusCompany.ID))
                .ForMember(dest => dest.BusCompanyName, opt => opt.MapFrom(src => src.BusCompany.Name));

            CreateMap<Employee, EmployeeAddDTO>()
               .ForMember(dest => dest.Role_ID, opt => opt.MapFrom(src => src.Role.ID))
               .ForMember(dest => dest.EmployeeTypeID, opt => opt.MapFrom(src => src.EmployeeType.ID))
               .ForMember(dest => dest.BusCompanyID, opt => opt.MapFrom(src => src.BusCompany.ID));
            CreateMap<EmployeeAddDTO, Employee>();

            CreateMap<Employee, EmployeePutDTO>()
              .ForMember(dest => dest.Role_ID, opt => opt.MapFrom(src => src.Role.ID))
              .ForMember(dest => dest.EmployeeTypeID, opt => opt.MapFrom(src => src.EmployeeType.ID))
              .ForMember(dest => dest.BusCompanyID, opt => opt.MapFrom(src => src.BusCompany.ID));
            CreateMap<EmployeePutDTO, Employee>();

            CreateMap<Parent, ParentGetDTO>();
            CreateMap<ParentGetDTO, Parent>();

            CreateMap<Student, StudentGetDTO>();
            CreateMap<StudentGetDTO, Student>();


            CreateMap<LMS_CMS_DAL.Models.Octa.Page, Page_GetDTO>();
            CreateMap<Page_GetDTO, LMS_CMS_DAL.Models.Octa.Page>();

            CreateMap<AcademicYear, AcademicYearGet>()
              .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
              .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.School.ID));
            CreateMap<AcademicYearGet, AcademicYear>();

            CreateMap<AcademicYear, AcademicYearAddDTO>()
              .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.School.ID));
            CreateMap<AcademicYearAddDTO, AcademicYear>();

            CreateMap<School, School_GetDTO>()
                 .ForMember(dest => dest.SchoolTypeName, opt => opt.MapFrom(src => src.SchoolType.Name));
            CreateMap<School_GetDTO, School>();

            CreateMap<School, SchoolAddDTO>()
                .ForMember(dest => dest.SchoolTypeID, opt => opt.MapFrom(src => src.SchoolType.ID));
            CreateMap<SchoolAddDTO, School>();

            CreateMap<School, SchoolEditDTO>()
                .ForMember(dest => dest.SchoolTypeID, opt => opt.MapFrom(src => src.SchoolType.ID));
            CreateMap<SchoolEditDTO, School>();

            CreateMap<EmployeeTypeViolation, EmployeeTypeViolationGetDTO>()
                .ForMember(dest => dest.EmployeeTypeName, opt => opt.MapFrom(src => src.EmployeeType.Name))
                .ForMember(dest => dest.EmployeeTypeID, opt => opt.MapFrom(src => src.EmployeeType.ID))
                .ForMember(dest => dest.ViolationID, opt => opt.MapFrom(src => src.Violation.ID))
                .ForMember(dest => dest.ViolationsTypeName, opt => opt.MapFrom(src => src.Violation.Name));

            CreateMap<Building, BuildingGetDTO>()
               .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.school.ID))
               .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.school.Name));
            CreateMap<BuildingAddDTO, Building>();
            CreateMap<BuildingPutDTO, Building>();

            CreateMap<Semester, Semester_GetDTO>()
               .ForMember(dest => dest.AcademicYearID, opt => opt.MapFrom(src => src.AcademicYear.ID))
               .ForMember(dest => dest.AcademicYearName, opt => opt.MapFrom(src => src.AcademicYear.Name));
            CreateMap<Semester_GetDTO, Semester>();

            CreateMap<Semester, SemesterAddDTO>()
              .ForMember(dest => dest.AcademicYearID, opt => opt.MapFrom(src => src.AcademicYear.ID));
            CreateMap<SemesterAddDTO, Semester>();

            CreateMap<Floor, FloorGetDTO>()
               .ForMember(dest => dest.BuildingID, opt => opt.MapFrom(src => src.building.ID))
               .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.building.Name))
               .ForMember(dest => dest.FloorMonitorID, opt => opt.MapFrom(src => src.floorMonitor.ID))
               .ForMember(dest => dest.FloorMonitorName, opt => opt.MapFrom(src => src.floorMonitor.en_name));
            CreateMap<FloorAddDTO, Floor>();
            CreateMap<FloorPutDTO, Floor>();

            CreateMap<SubjectCategory, SubjectCategoryGetDTO>();
            CreateMap<SubjectCategoryAddDTO, SubjectCategory>();
            CreateMap<SubjectCategoryPutDTO, SubjectCategory>();
 
            CreateMap<EmployeeAttachment, EmployeeAttachmentDTO>();
            CreateMap<EmployeeAttachmentDTO, EmployeeAttachment>();

            CreateMap<Subject, SubjectGetDTO>()
                .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade.Name))
                .ForMember(dest => dest.GradeID, opt => opt.MapFrom(src => src.Grade.ID))
                .ForMember(dest => dest.SubjectCategoryID, opt => opt.MapFrom(src => src.SubjectCategory.ID))
                .ForMember(dest => dest.SubjectCategoryName, opt => opt.MapFrom(src => src.SubjectCategory.Name));
            CreateMap<SubjectAddDTO, Subject>();
            CreateMap<SubjectPutDTO, Subject>();


            CreateMap<Role, RolesGetDTO>();
            CreateMap<RolesGetDTO, Role>();

            CreateMap<Section, SectionGetDTO>()
               .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.school.ID))
               .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.school.Name));
            CreateMap<SectionGetDTO, Section>();

            CreateMap<Section, SectionAddDTO>()
               .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.school.ID));
            CreateMap<SectionAddDTO, Section>();
        }
    }
}
