using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.DTO.Administration;

using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.DTO.Octa;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.DTO.Violation;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_DAL.Models.Domains.ViolationModule;
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
                .ForMember(dest => dest.StudentID, opt => opt.MapFrom(src => src.Student.ID))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.en_name))
                .ForMember(dest => dest.BusCategoryName, opt => opt.MapFrom(src => src.BusCategory.Name))
                .ForMember(dest => dest.SemseterName, opt => opt.MapFrom(src => src.Semester.Name))
                .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.Semester.AcademicYear.School.ID))
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.Semester.AcademicYear.School.Name))
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

            CreateMap<Student, StudentGetDTO>()
              .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
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
            CreateMap<AcademicYear, AcademicYearEditDTO>()
              .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.School.ID));
            CreateMap<AcademicYearEditDTO, AcademicYear>();


            CreateMap<School, School_GetDTO>()
                 .ForMember(dest => dest.SchoolTypeName, opt => opt.MapFrom(src => src.SchoolType.Name))
                 .ForMember(dest => dest.SchoolTypeID, opt => opt.MapFrom(src => src.SchoolType.ID));
            CreateMap<School_GetDTO, School>();

            CreateMap<School, SchoolAddDTO>()
                .ForMember(dest => dest.SchoolTypeID, opt => opt.MapFrom(src => src.SchoolType.ID));
            CreateMap<SchoolAddDTO, School>();

            CreateMap<School, SchoolEditDTO>()
                .ForMember(dest => dest.SchoolTypeID, opt => opt.MapFrom(src => src.SchoolType.ID));
            CreateMap<SchoolEditDTO, School>();

            CreateMap<EmployeeTypeViolation, EmployeeTypeViolationGetDTO>()
                .ForMember(dest => dest.ViolationID, opt => opt.MapFrom(src => src.Violation.ID))
                .ForMember(dest => dest.ViolationsTypeName, opt => opt.MapFrom(src => src.Violation.Name));
            CreateMap<EmployeeTypeViolation, EmployeeTypeViolationAddDTO>();
            CreateMap<EmployeeTypeViolationAddDTO, EmployeeTypeViolation>();

            CreateMap<EmployeeTypeViolation, ViolationGetDTO>();
            CreateMap<ViolationGetDTO, EmployeeTypeViolation>();

            CreateMap<EmployeeTypeViolation, EmployeeTypeGetDTO>()
                 .ForMember(dest => dest.EmpTypeVId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.EmployeeType.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EmployeeType.Name));
            CreateMap<EmployeeTypeGetDTO, EmployeeTypeViolation>();


            CreateMap<Violation, ViolationGetDTO>();
            CreateMap<ViolationGetDTO, Violation>();

            CreateMap<Violation, ViolationAddDTO>();
            CreateMap<ViolationAddDTO, Violation>();

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

            CreateMap<Semester, SemesterEditDTO>()
              .ForMember(dest => dest.AcademicYearID, opt => opt.MapFrom(src => src.AcademicYear.ID));
            CreateMap<SemesterEditDTO, Semester>();

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

            CreateMap<Grade , GradeAddDTO>()
                .ForMember(dest => dest.SectionID, opt => opt.MapFrom(src => src.Section.ID));
            CreateMap<GradeAddDTO, Grade>();

            CreateMap<Grade, GradeEditDTO>()
               .ForMember(dest => dest.SectionID, opt => opt.MapFrom(src => src.Section.ID));
            CreateMap<GradeEditDTO, Grade>();

            CreateMap<Grade, GradeGetDTO>()
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.Name))
                .ForMember(dest => dest.SectionID, opt => opt.MapFrom(src => src.Section.ID));
            CreateMap<GradeGetDTO, Grade>();

            CreateMap<Section, SectionGetDTO>()
               .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.school.ID))
               .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.school.Name));
            CreateMap<SectionGetDTO, Section>();

            CreateMap<Section, SectionAddDTO>()
               .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.school.ID));
            CreateMap<SectionAddDTO, Section>();


            CreateMap<Section, SectionEditDTO>()
               .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.school.ID));
            CreateMap<SectionEditDTO, Section>();

            CreateMap<Classroom, ClassroomGetDTO>()
                .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade.Name))
                .ForMember(dest => dest.AcademicYearName, opt => opt.MapFrom(src => src.AcademicYear.Name))
                .ForMember(dest => dest.FloorName, opt => opt.MapFrom(src => src.Floor.Name));
            CreateMap<ClassroomAddDTO, Classroom>();
            CreateMap<Classroom, ClassroomAddDTO>();
            CreateMap<ClassroomPutDTO, Classroom>();

            CreateMap<DTO.Octa.SchoolTypeAddDTO, LMS_CMS_DAL.Models.Octa.SchoolType>();
            CreateMap<SchoolTypePutDTO, LMS_CMS_DAL.Models.Octa.SchoolType>();
            CreateMap<SchoolTypePutDTO, LMS_CMS_DAL.Models.Domains.LMS.SchoolType>();

            CreateMap<Domain, DomainGetDTO>();

            CreateMap<EmployeeType, EmployeeTypeGetDTO>();
            CreateMap<EmployeeTypeGetDTO, EmployeeType>();

            CreateMap<Octa, OctaGetDTO>();
            CreateMap<OctaAddDTO, Octa>();
            CreateMap<OctaPutDTO, Octa>();

            CreateMap<RegistrationCategory, RegistrationCategoryGetDTO>();
            CreateMap<RegistrationCategoryAddDTO, RegistrationCategory>();
            CreateMap<RegistrationCategoryEditDTO, RegistrationCategory>();


            CreateMap<CategoryField, CategoryFieldGetDTO>()
                .ForMember(dest => dest.FieldTypeID, opt => opt.MapFrom(src => src.FieldType.ID))
                .ForMember(dest => dest.FieldTypeName, opt => opt.MapFrom(src => src.FieldType.Name))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.FieldOptions));
            CreateMap<CategoryFieldAddDTO, CategoryField>();
            CreateMap<CategoryFieldEditDTO, CategoryField>();

            CreateMap<FieldType, FieldTypeGetDTO>();
            CreateMap<QuestionType, FieldTypeGetDTO>();


            CreateMap<FieldOption, FieldOptionGetDTO>()
               .ForMember(dest => dest.CategoryFieldID, opt => opt.MapFrom(src => src.CategoryField.ID))
               .ForMember(dest => dest.CategoryFieldName, opt => opt.MapFrom(src => src.CategoryField.EnName));

            CreateMap<Test, TestGetDTO>()
                 .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade.Name))
                 .ForMember(dest => dest.AcademicYearName, opt => opt.MapFrom(src => src.academicYear.Name))
                 .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.subject.en_name))
                 .ForMember(dest => dest.SchoolID, opt => opt.MapFrom(src => src.academicYear.School.ID))
                 .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.academicYear.School.Name));

            CreateMap<TestAddDTO, Test>();
            CreateMap<TestEditDTO, Test>();

            CreateMap<MCQQuestionOption, MCQQuestionOptionGetDto>();
            CreateMap<Question, questionGetDTO>()
               .ForMember(dest => dest.CorrectAnswerName, opt => opt.MapFrom(src => src.mCQQuestionOption.Name))
               .ForMember(dest => dest.QuestionTypeName, opt => opt.MapFrom(src => src.QuestionType.Name))
               .ForMember(dest => dest.TestName, opt => opt.MapFrom(src => src.test.Title))
               .ForMember(dest => dest.options, opt => opt.MapFrom(src => src.MCQQuestionOptions));

            CreateMap<QuestionAddDTO, Question>();
            CreateMap<QuestionEditDTO, Question>();


            CreateMap<RegisterationFormParent, RegisterationFormParentGetDTO>()
               .ForMember(dest => dest.RegisterationFormStateName, opt => opt.MapFrom(src => src.RegisterationFormState.Name))
               .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.en_name))
               .ForMember(dest => dest.RegistrationFormName, opt => opt.MapFrom(src => src.RegistrationForm.Name));

            CreateMap<RegisterationFormSubmittion, RegisterationFormSubmittionGetDTO>()
              .ForMember(dest => dest.RegistrationFormParentName, opt => opt.MapFrom(src => src.RegisterationFormParent.StudentName))
              .ForMember(dest => dest.CategoryFieldName, opt => opt.MapFrom(src => src.CategoryField.EnName))
              .ForMember(dest => dest.CategoryFieldOrderInForm, opt => opt.MapFrom(src => src.CategoryField.OrderInForm))
              .ForMember(dest => dest.RegistrationCategoryID, opt => opt.MapFrom(src => src.CategoryField.RegistrationCategory.ID))
              .ForMember(dest => dest.RegistrationCategoryName, opt => opt.MapFrom(src => src.CategoryField.RegistrationCategory.EnName))
              .ForMember(dest => dest.RegistrationCategoryOrderInForm, opt => opt.MapFrom(src => src.CategoryField.RegistrationCategory.OrderInForm))
              .ForMember(dest => dest.SelectedFieldOptionName, opt => opt.MapFrom(src => src.FieldOption.Name));

            CreateMap<RegisterationFormParentPutDTO, RegisterationFormParent>();

            CreateMap<InterviewTime, InterviewTimeTableGetDTO>()
               .ForMember(dest => dest.AcademicYearName, opt => opt.MapFrom(src => src.AcademicYear.Name));
            CreateMap<InterviewTimeTablePutDTO, InterviewTime>();

            CreateMap<RegisterationFormInterview, RegisterationFormInterviewGetDTO>()
               .ForMember(dest => dest.InterviewStateName, opt => opt.MapFrom(src => src.InterviewState.Name))
               .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.RegisterationFormParent.StudentName))
               .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.RegisterationFormParent.Phone))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.RegisterationFormParent.Email))
               .ForMember(dest => dest.GradeID, opt => opt.MapFrom(src => src.RegisterationFormParent.GradeID))
               .ForMember(dest => dest.InterviewTimeDate, opt => opt.MapFrom(src => src.InterviewTime.Date));
            CreateMap<RegistrationFormInterviewPutDTO, RegisterationFormInterview>();
            CreateMap<RegistrationFormInterviewPutByParentDTO, RegisterationFormInterview>();
            CreateMap<RegisterationFormInterviewAddDTO, RegisterationFormInterview>();

            CreateMap<RegisterationFormParent, RegistrationFormParentIncludeRegistrationFormInterviewGetDTO>()
               .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.en_name))
               .ForMember(dest => dest.RegistrationFormName, opt => opt.MapFrom(src => src.RegistrationForm.Name));
       
            CreateMap<RegisterationFormTest, RegisterationFormTestGetDTO>()
              .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.RegisterationFormParent.StudentName))
              .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.TestState.Name))
              .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Test.subject.en_name))
              .ForMember(dest => dest.TotalMark, opt => opt.MapFrom(src => src.Test.TotalMark))
              .ForMember(dest => dest.TestName, opt => opt.MapFrom(src => src.Test.Title));

            CreateMap<RegisterationFormTestEditDTO, RegisterationFormTest>();

            CreateMap<RegisterationFormTestAnswer, RegisterationFormTestAnswerGetDTO>()
              .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.RegisterationFormParent.StudentName))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Question.Description))
              .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Question.Video))
              .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Question.Image))
              .ForMember(dest => dest.CorrectAnswerID, opt => opt.MapFrom(src => src.Question.CorrectAnswerID))
              .ForMember(dest => dest.CorrectAnswerName, opt => opt.MapFrom(src => src.Question.mCQQuestionOption.Name))
              .ForMember(dest => dest.QuestionTypeID, opt => opt.MapFrom(src => src.Question.QuestionType.ID))
              .ForMember(dest => dest.QuestionTypeName, opt => opt.MapFrom(src => src.Question.QuestionType.Name))
              .ForMember(dest => dest.QuestionTypeID, opt => opt.MapFrom(src => src.Question.QuestionType.ID))
              .ForMember(dest => dest.AnswerName, opt => opt.MapFrom(src => src.MCQQuestionOption.Name));

            CreateMap<MCQQuestionOption, MCQQuestionOptionGetDto>();

            CreateMap<RegisterationFormTestAnswerAddDTO, RegisterationFormTestAnswer>();

            CreateMap<RegisterationFormState, RegistrationFormStateGetDTO>();
            CreateMap<InterviewState, InterviewStateGetDTO>();

            CreateMap<ParentDTO, Parent>();

            CreateMap<Department, DepartmentGetDTO>();
            CreateMap<DepartmentGetDTO, Department>();
            CreateMap<DepartmentAddDto, Department>();

            CreateMap<JobCategory, JobCategoryGetDto>();
            CreateMap<JobCategoryGetDto, JobCategory>();
            CreateMap<JobCategoryAddDto, JobCategory>();

            CreateMap<AcademicDegree, AcademicDegreeGetDTO>();
            CreateMap<AcademicDegreeGetDTO, AcademicDegree>();
            CreateMap<AcademicDegreeAddDTO, AcademicDegree>();

            CreateMap<ReasonForLeavingWork, ReasonsForLeavingWorkGetDTO>();
            CreateMap<ReasonsForLeavingWorkGetDTO, ReasonForLeavingWork>();
            CreateMap<ReasonsForLeavingWorkAddDTO, ReasonForLeavingWork>();

            CreateMap<AccountingEntriesDocType, AccountingEntriesDocTypeGetDTO>();
            CreateMap<AccountingEntriesDocTypeGetDTO, AccountingEntriesDocType>();
            CreateMap<AccountingEntriesDocTypesAddDto, AccountingEntriesDocType>();

            CreateMap<Job, JobGetDTO>()
              .ForMember(dest => dest.JobCategoryName, opt => opt.MapFrom(src => src.JobCategory.Name));
            CreateMap<JobGetDTO, Job>();
            CreateMap<JobAddDto, Job>();
             
            CreateMap<Debit, DebitGetDTO>()
                .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<DebitAddDTO, Debit>();
            CreateMap<DebitPutDTO, Debit>();

            CreateMap<Credit, CreditGetDTO>()
                .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<CreditAddDTO, Credit>();
            CreateMap<CreditPutDTO, Credit>();

            CreateMap<Save, SaveGetDTO>()
                .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<SaveAddDTO, Save>();
            CreateMap<SavePutDTO, Save>();

            CreateMap<Income, IncomeGetDTO>()
                .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<IncomeAddDTO, Income>();
            CreateMap<IncomePutDTO, Income>();

            CreateMap<Outcome, OutcomeGetDTO>()
                .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<OutcomeAddDTO, Outcome>();
            CreateMap<OutcomePutDTO, Outcome>();

            CreateMap<Asset, AssetGetDTO>()
                .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<AssetAddDTO, Asset>();
            CreateMap<AssetPutDTO, Asset>();

            CreateMap<TuitionFeesType, TuitionFeesTypeGetDTO>()
                .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<TuitionFeesTypeAddDTO, TuitionFeesType>();
            CreateMap<TuitionFeesTypePutDTO, TuitionFeesType>();

            CreateMap<Supplier, SupplierGetDTO>()
               .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<SupplierAddDTO, Supplier>();
            CreateMap<SupplierGetDTO, Supplier>();

            CreateMap<Bank, BankGetDTO>()
               .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<BankAddDto, Bank>();
            CreateMap<BankGetDTO, Bank>();

            CreateMap<TuitionDiscountType, TuitionDiscountTypeGetDTO>()
                .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name));
            CreateMap<TuitionDiscountTypePutDTO, TuitionDiscountType>();
            CreateMap<TuitionDiscountTypeAddDTO, TuitionDiscountType>();

            CreateMap<AccountingTreeChartAddDTO, AccountingTreeChart>();
            
            CreateMap<AccountingTreeChart, AccountingTreeChartGetDTO>()
                .ForMember(dest => dest.MainAccountNumberName, opt => opt.MapFrom(src => src.Parent.Name))
                .ForMember(dest => dest.EndTypeName, opt => opt.MapFrom(src => src.EndType.Name))
                .ForMember(dest => dest.LinkFileName, opt => opt.MapFrom(src => src.LinkFile.Name))
                .ForMember(dest => dest.SubTypeName, opt => opt.MapFrom(src => src.SubType.Name))
                .ForMember(dest => dest.MotionTypeName, opt => opt.MapFrom(src => src.MotionType.Name))
                ;

            CreateMap<EmployeeAccountingPut, Employee>();
            CreateMap<Employee, EmployeeAccountingGetDTO>()
                .ForMember(dest => dest.AccountNumberName, opt => opt.MapFrom(src => src.AccountNumber.Name))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.Job.Name))
                .ForMember(dest => dest.ReasonForLeavingWork, opt => opt.MapFrom(src => src.ReasonForLeavingWork.Name))
                .ForMember(dest => dest.JobCategoryId, opt => opt.MapFrom(src => src.Job.JobCategoryID))
                .ForMember(dest => dest.AcademicDegreeName, opt => opt.MapFrom(src => src.AcademicDegree.Name));

            CreateMap<Country, CountriesGetDTO>();
            CreateMap<Nationality, NationalityGetDTO>();
            CreateMap<Days, DayGetDTO>();
            CreateMap<ReasonForLeavingWork, ReasonForLeavingWorkGetDTO>();
            CreateMap<AccountingStudentPutDTO, Student>();


            CreateMap<SubType, SubTypeGetDTO>();
            CreateMap<MotionType, MotionTypeGetDTO1>();
            CreateMap<EndType, EndTypeGetDTO>();
            CreateMap<LinkFile, LinkFileGetDTO>();

        }
    }
}
