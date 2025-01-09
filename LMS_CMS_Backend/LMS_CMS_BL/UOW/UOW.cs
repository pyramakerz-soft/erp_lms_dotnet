using LMS_CMS_BL.Repository;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_DAL.Models.Domains.ViolationModule;
using LMS_CMS_DAL.Models.Octa;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.UOW
{
    public class UOW
    {
        LMS_CMS_Context db;
        Octa_DbContext octa_Db;

        GenericRepo<Employee> Employee_Repository;
        GenericRepo<Parent> Parent_Repository;
        GenericRepo<Student> Student_Repository;
        GenericRepo<Role> Role_Repository;
        GenericRepo<Domain> Domain_Octa_Repository;
        GenericRepo<School> School_Repository;
        GenericRepo<LMS_CMS_DAL.Models.Domains.Page> Page_Repository;
        GenericRepo<LMS_CMS_DAL.Models.Octa.Page> Page_Octa_Repository;
        GenericRepo<Role_Detailes> Role_Detailes_Repository;
        GenericRepo<Octa> Octa_Repository;
        GenericRepo<Bus> Bus_Repository;
        GenericRepo<BusStudent> BusStudent_Repository;
        GenericRepo<BusType> BusType_Repository;
        GenericRepo<BusDistrict> BusDistrict_Repository;
        GenericRepo<BusCategory> BusCategory_Repository;
        GenericRepo<BusStatus> BusStatus_Repository;
        GenericRepo<BusCompany> BusCompany_Repository;
        GenericRepo<AcademicYear> AcademicYear_Repository;
        GenericRepo<Semester> Semester_Repository;
        GenericRepo<EmployeeType> EmployeeType_Repository;
        GenericRepo<Grade> Grade_Repository;
        GenericRepo<StudentAcademicYear> StudentAcademicYear_Repository;
        GenericRepo<Violation> Violations_Repository;
        GenericRepo<EmployeeAttachment> EmployeeAttachment_Repository;
        GenericRepo<EmployeeTypeViolation> EmployeeTypeViolation_Repository;
        GenericRepo<Subject> Subject_Repository;
        GenericRepo<SubjectCategory> SubjectCategory_Repository;
        GenericRepo<Building> Building_Repository;
        GenericRepo<Floor> Floor_Repository;
        GenericRepo<Section> Section_Repository;
        GenericRepo<LMS_CMS_DAL.Models.Domains.LMS.SchoolType> SchoolType_Repository;
        GenericRepo<LMS_CMS_DAL.Models.Octa.SchoolType> SchoolType_Octa_Repository;
        GenericRepo<Classroom> Classroom_Repository;
        GenericRepo<RegisterationFormState> RegisterationFormState_Repository;
        GenericRepo<RegisterationFormParent> RegisterationFormParent_Repository;
        GenericRepo<RegisterationFormSubmittion> RegisterationFormSubmittion_Repository;
        GenericRepo<RegistrationForm> RegistrationForm_Repository;
        GenericRepo<RegistrationCategory> RegistrationCategory_Repository;
        GenericRepo<CategoryField> CategoryField_Repository;
        GenericRepo<FieldType> FieldType_Repository;
        GenericRepo<FieldOption> FieldOption_Repository;
        GenericRepo<InterviewState> InterviewState_Repository;
        GenericRepo<RegisterationFormInterview> RegisterationFormInterview_Repository;
        GenericRepo<InterviewTime> InterviewTime_Repository;



        public UOW(Octa_DbContext octa_Db)
        {
            this.octa_Db = octa_Db;
        }

        public UOW(string connectionString)
        {
            // Use the connection string to initialize the context dynamically
            var optionsBuilder = new DbContextOptionsBuilder<LMS_CMS_Context>();
            optionsBuilder.UseSqlServer(connectionString);
            db = new LMS_CMS_Context(optionsBuilder.Options);
        }

        public GenericRepo<Employee> employee_Repository
        {
            get
            {
                if (Employee_Repository == null)
                {
                    Employee_Repository = new GenericRepo<Employee>(db);
                }
                return Employee_Repository;
            }
        }

        public GenericRepo<Parent> parent_Repository
        {
            get
            {
                if (Parent_Repository == null)
                {
                    Parent_Repository = new GenericRepo<Parent>(db);
                }
                return Parent_Repository;
            }
        }
        public GenericRepo<Student> student_Repository
        {
            get
            {
                if (Student_Repository == null)
                {
                    Student_Repository = new GenericRepo<Student>(db);
                }
                return Student_Repository;
            }
        }
        public GenericRepo<Role> role_Repository
        {
            get
            {
                if (Role_Repository == null)
                {
                    Role_Repository = new GenericRepo<Role>(db);
                }
                return Role_Repository;
            }
        }
        public GenericRepo<Domain> domain_Octa_Repository
        {
            get
            {
                if (Domain_Octa_Repository == null)
                {
                    Domain_Octa_Repository = new GenericRepo<Domain>(octa_Db);
                }
                return Domain_Octa_Repository;
            }
        }

        public GenericRepo<School> school_Repository
        {
            get
            {
                if (School_Repository == null)
                {
                    School_Repository = new GenericRepo<School>(db);
                }
                return School_Repository;
            }
        }

        public GenericRepo<Octa> octa_Repository
        {
            get
            {
                if (Octa_Repository == null)
                {
                    Octa_Repository = new GenericRepo<Octa>(octa_Db);
                }
                return Octa_Repository;
            }
        }

        public GenericRepo<LMS_CMS_DAL.Models.Domains.Page> page_Repository
        {
            get
            {
                if (Page_Repository == null)
                {
                    Page_Repository = new GenericRepo<LMS_CMS_DAL.Models.Domains.Page>(db);
                }
                return Page_Repository;
            }
        }
        public GenericRepo<LMS_CMS_DAL.Models.Octa.Page> page_Octa_Repository
        {
            get
            {
                if (Page_Octa_Repository == null)
                {
                    Page_Octa_Repository = new GenericRepo<LMS_CMS_DAL.Models.Octa.Page>(octa_Db);
                }
                return Page_Octa_Repository;
            }
        }
        public GenericRepo<LMS_CMS_DAL.Models.Octa.SchoolType> schoolType_Octa_Repository
        {
            get
            {
                if (SchoolType_Octa_Repository == null)
                {
                    SchoolType_Octa_Repository = new GenericRepo<LMS_CMS_DAL.Models.Octa.SchoolType>(octa_Db);
                }
                return SchoolType_Octa_Repository;
            }
        }

        public GenericRepo<Role_Detailes> role_Detailes_Repository
        {
            get
            {
                if (Role_Detailes_Repository == null)
                {
                    Role_Detailes_Repository = new GenericRepo<Role_Detailes>(db);
                }
                return Role_Detailes_Repository;
            }
        }

        public GenericRepo<Bus> bus_Repository
        {
            get
            {
                if (Bus_Repository == null)
                {
                    Bus_Repository = new GenericRepo<Bus>(db);
                }
                return Bus_Repository;
            }
        }

        public GenericRepo<BusStudent> busStudent_Repository
        {
            get
            {
                if (BusStudent_Repository == null)
                {
                    BusStudent_Repository = new GenericRepo<BusStudent>(db);
                }
                return BusStudent_Repository;
            }
        }

        public GenericRepo<BusType> busType_Repository
        {
            get
            {
                if (BusType_Repository == null)
                {
                    BusType_Repository = new GenericRepo<BusType>(db);
                }
                return BusType_Repository;
            }
        }

        public GenericRepo<BusDistrict> busDistrict_Repository
        {
            get
            {
                if (BusDistrict_Repository == null)
                {
                    BusDistrict_Repository = new GenericRepo<BusDistrict>(db);
                }
                return BusDistrict_Repository;
            }
        }
        public GenericRepo<BusCategory> busCategory_Repository
        {
            get
            {
                if (BusCategory_Repository == null)
                {
                    BusCategory_Repository = new GenericRepo<BusCategory>(db);
                }
                return BusCategory_Repository;
            }
        }
        public GenericRepo<BusStatus> busStatus_Repository
        {
            get
            {
                if (BusStatus_Repository == null)
                {
                    BusStatus_Repository = new GenericRepo<BusStatus>(db);
                }
                return BusStatus_Repository;
            }
        }
        public GenericRepo<BusCompany> busCompany_Repository
        {
            get
            {
                if (BusCompany_Repository == null)
                {
                    BusCompany_Repository = new GenericRepo<BusCompany>(db);
                }
                return BusCompany_Repository;
            }
        }

        public GenericRepo<AcademicYear> academicYear_Repository
        {
            get
            {
                if (AcademicYear_Repository == null)
                {
                    AcademicYear_Repository = new GenericRepo<AcademicYear>(db);
                }
                return AcademicYear_Repository;
            }
        }

        public GenericRepo<Semester> semester_Repository
        {
            get
            {
                if (Semester_Repository == null)
                {
                    Semester_Repository = new GenericRepo<Semester>(db);
                }
                return Semester_Repository;
            }
        }

        public GenericRepo<EmployeeType> employeeType_Repository
        {
            get
            {
                if (EmployeeType_Repository == null)
                {
                    EmployeeType_Repository = new GenericRepo<EmployeeType>(db);
                }
                return EmployeeType_Repository;
            }
        }

        public GenericRepo<Grade> grade_Repository
        {
            get
            {
                if (Grade_Repository == null)
                {
                    Grade_Repository = new GenericRepo<Grade>(db);
                }
                return Grade_Repository;
            }
        }

        public GenericRepo<StudentAcademicYear> studentAcademicYear_Repository
        {
            get
            {
                if (StudentAcademicYear_Repository == null)
                {
                    StudentAcademicYear_Repository = new GenericRepo<StudentAcademicYear>(db);
                }
                return StudentAcademicYear_Repository;
            }
        }

        public GenericRepo<Violation> violations_Repository
        {
            get
            {
                if (Violations_Repository == null)
                {
                    Violations_Repository = new GenericRepo<Violation>(db);
                }
                return Violations_Repository;
            }
        }

        public GenericRepo<EmployeeAttachment> employeeAttachment_Repository
        {
            get
            {
                if (EmployeeAttachment_Repository == null)
                {
                    EmployeeAttachment_Repository = new GenericRepo<EmployeeAttachment>(db);
                }
                return EmployeeAttachment_Repository;
            }
        }

        public GenericRepo<EmployeeTypeViolation> employeeTypeViolation_Repository
        {
            get
            {
                if (EmployeeTypeViolation_Repository == null)
                {
                    EmployeeTypeViolation_Repository = new GenericRepo<EmployeeTypeViolation>(db);
                }
                return EmployeeTypeViolation_Repository;
            }
        }
        public GenericRepo<Floor> floor_Repository
        {
            get
            {
                if (Floor_Repository == null)
                {
                    Floor_Repository = new GenericRepo<Floor>(db);
                }
                return Floor_Repository;
            }
        }

        public GenericRepo<Building> building_Repository
        {
            get
            {
                if (Building_Repository == null)
                {
                    Building_Repository = new GenericRepo<Building>(db);
                }
                return Building_Repository;
            }
        }

        public GenericRepo<Section> section_Repository
        {
            get
            {
                if (Section_Repository == null)
                {
                    Section_Repository = new GenericRepo<Section>(db);
                }
                return Section_Repository;
            }
        }
        public GenericRepo<SubjectCategory> subjectCategory_Repository
        {
            get
            {
                if (SubjectCategory_Repository == null)
                {
                    SubjectCategory_Repository = new GenericRepo<SubjectCategory>(db);
                }
                return SubjectCategory_Repository;
            }
        }
        public GenericRepo<Subject> subject_Repository
        {
            get
            {
                if (Subject_Repository == null)
                {
                    Subject_Repository = new GenericRepo<Subject>(db);
                }
                return Subject_Repository;
            }
        }
        public GenericRepo<LMS_CMS_DAL.Models.Domains.LMS.SchoolType> schoolType_Repository
        {
            get
            {
                if (SchoolType_Repository == null)
                {
                    SchoolType_Repository = new GenericRepo<LMS_CMS_DAL.Models.Domains.LMS.SchoolType>(db);
                }
                return SchoolType_Repository;
            }
        }
        public GenericRepo<Classroom> classroom_Repository
        {
            get
            {
                if (Classroom_Repository == null)
                {
                    Classroom_Repository = new GenericRepo<Classroom>(db);
                }
                return Classroom_Repository;
            }
        }
        public GenericRepo<RegisterationFormState> registerationFormState_Repository
        {
            get
            {
                if (RegisterationFormState_Repository == null)
                {
                    RegisterationFormState_Repository = new GenericRepo<RegisterationFormState>(db);
                }
                return RegisterationFormState_Repository;
            }
        }
        public GenericRepo<RegisterationFormParent> registerationFormParent_Repository
        {
            get
            {
                if (RegisterationFormParent_Repository == null)
                {
                    RegisterationFormParent_Repository = new GenericRepo<RegisterationFormParent>(db);
                }
                return RegisterationFormParent_Repository;
            }
        }
        public GenericRepo<RegisterationFormSubmittion> registerationFormSubmittion_Repository
        {
            get
            {
                if (RegisterationFormSubmittion_Repository == null)
                {
                    RegisterationFormSubmittion_Repository = new GenericRepo<RegisterationFormSubmittion>(db);
                }
                return RegisterationFormSubmittion_Repository;
            }
        }
        public GenericRepo<RegistrationForm> registrationForm_Repository
        {
            get
            {
                if (RegistrationForm_Repository == null)
                {
                    RegistrationForm_Repository = new GenericRepo<RegistrationForm>(db);
                }
                return RegistrationForm_Repository;
            }
        }
        public GenericRepo<RegistrationCategory> registrationCategory_Repository
        {
            get
            {
                if (RegistrationCategory_Repository == null)
                {
                    RegistrationCategory_Repository = new GenericRepo<RegistrationCategory>(db);
                }
                return RegistrationCategory_Repository;
            }
        }
        public GenericRepo<CategoryField> categoryField_Repository
        {
            get
            {
                if (CategoryField_Repository == null)
                {
                    CategoryField_Repository = new GenericRepo<CategoryField>(db);
                }
                return CategoryField_Repository;
            }
        }
        public GenericRepo<FieldType> fieldType_Repository
        {
            get
            {
                if (FieldType_Repository == null)
                {
                    FieldType_Repository = new GenericRepo<FieldType>(db);
                }
                return FieldType_Repository;
            }
        }
        public GenericRepo<FieldOption> fieldOption_Repository
        {
            get
            {
                if (FieldOption_Repository == null)
                {
                    FieldOption_Repository = new GenericRepo<FieldOption>(db);
                }
                return FieldOption_Repository;
            }
        }
        public GenericRepo<InterviewState> interviewState_Repository
        {
            get
            {
                if (InterviewState_Repository == null)
                {
                    InterviewState_Repository = new GenericRepo<InterviewState>(db);
                }
                return InterviewState_Repository;
            }
        }
        public GenericRepo<RegisterationFormInterview> registerationFormInterview_Repository
        {
            get
            {
                if (RegisterationFormInterview_Repository == null)
                {
                    RegisterationFormInterview_Repository = new GenericRepo<RegisterationFormInterview>(db);
                }
                return RegisterationFormInterview_Repository;
            }
        }
        public GenericRepo<InterviewTime> interviewTime_Repository
        {
            get
            {
                if (InterviewTime_Repository == null)
                {
                    InterviewTime_Repository = new GenericRepo<InterviewTime>(db);
                }
                return InterviewTime_Repository;
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
        
        public void SaveOctaChanges()
        {
            octa_Db.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
