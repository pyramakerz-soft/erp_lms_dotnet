using LMS_CMS_BL.Repository;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_DAL.Models.Domains.ECommerce;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_DAL.Models.Domains.ViolationModule;
using LMS_CMS_DAL.Models.Domains.Zatca;
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
        GenericRepo<TestState> TestState_Repository;
        GenericRepo<RegisterationFormTest> RegisterationFormTest_Repository;
        GenericRepo<RegisterationFormTestAnswer> RegisterationFormTestAnswer_Repository;
        GenericRepo<QuestionType> QuestionType_Repository;
        GenericRepo<Question> Question_Repository;
        GenericRepo<Test> Test_Repository;
        GenericRepo<MCQQuestionOption> MCQQuestionOption_Repository;
        GenericRepo<RegistrationFormCategory> RegistrationFormCategory_Repository;
        GenericRepo<EndType> EndType_Repository;
        GenericRepo<AccountingEntriesDocType> AccountingEntriesDocType_Repository;
        GenericRepo<MotionType> MotionType_Repository;
        GenericRepo<SubType> SubType_Repository;
        GenericRepo<AccountingTreeChart> AccountingTreeChart_Repository;
        GenericRepo<Credit> Credit_Repository;
        GenericRepo<Debit> Debit_Repository;
        GenericRepo<Income> Income_Repository;
        GenericRepo<Outcome> Outcome_Repository;
        GenericRepo<Save> Save_Repository;
        GenericRepo<Asset> Asset_Repository;
        GenericRepo<TuitionFeesType> TuitionFeesType_Repository;
        GenericRepo<TuitionDiscountType> TuitionDiscountType_Repository;
        GenericRepo<Bank> Bank_Repository;
        GenericRepo<Supplier> Supplier_Repository;
        GenericRepo<Department> Department_Repository;
        GenericRepo<AcademicDegree> AcademicDegree_Repository;
        GenericRepo<Job> Job_Repository;
        GenericRepo<JobCategory> JobCategory_Repository;
        GenericRepo<ReasonForLeavingWork> ReasonForLeavingWork_Repository;
        GenericRepo<Days> Days_Repository;
        GenericRepo<EmployeeDays> EmployeeDays_Repository;
        GenericRepo<Country> Country_Repository;
        GenericRepo<Nationality> Nationality_Repository;
        GenericRepo<LinkFile> LinkFile_Repository;
        GenericRepo<EmployeeStudent> EmployeeStudent_Repository;
        GenericRepo<PayableDocType> PayableDocType_Repository;
        GenericRepo<ReceivableDocType> ReceivableDocType_Repository;
        GenericRepo<PayableMaster> PayableMaster_Repository;
        GenericRepo<ReceivableMaster> ReceivableMaster_Repository;
        GenericRepo<PayableDetails> PayableDetails_Repository;
        GenericRepo<ReceivableDetails> ReceivableDetails_Repository;
        GenericRepo<InstallmentDeductionMaster> InstallmentDeductionMaster_Repository;
        GenericRepo<InstallmentDeductionDetails> InstallmentDeductionDetails_Repository;
        GenericRepo<FeesActivation> FeesActivation_Repository;
        GenericRepo<AccountingEntriesMaster> AccountingEntriesMaster_Repository;
        GenericRepo<AccountingEntriesDetails> AccountingEntriesDetails_Repository;
        GenericRepo<Gender> Gender_Repository;
        GenericRepo<ShopItem> ShopItem_Repository;
        GenericRepo<InventoryMaster> InventoryMaster_Repository;
        GenericRepo<InventoryDetails> InventoryDetails_Repository;
        GenericRepo<SalesItemAttachment> SalesItemAttachment_Repository;
        GenericRepo<Store> Store_Repository;
        GenericRepo<ShopItemSize> ShopItemSize_Repository;
        GenericRepo<ShopItemColor> ShopItemColor_Repository;
        GenericRepo<StoreCategories> StoreCategories_Repository;
        GenericRepo<InventorySubCategories> InventorySubCategories_Repository;
        GenericRepo<InventoryCategories> InventoryCategories_Repository;
        GenericRepo<InventoryFlags> InventoryFlags_Repository;
        GenericRepo<HygieneType> HygieneType_Repository;
        GenericRepo<Diagnosis> Diagnosis_Repository;
        GenericRepo<Drug> Drug_Repository;
        GenericRepo<Dose> Dose_Repository;
        GenericRepo<HygieneForm> HygieneForm_Repository;
        GenericRepo<StudentHygieneTypes> StudentHygieneTypes_Repository;
        GenericRepo<StudentHygienes> StudentHygiens_Repository;
        GenericRepo<FollowUp> FollowUp_Repository;
        GenericRepo<FollowUpDrug> FollowUpDrug_Repository;
        GenericRepo<MedicalHistory> MedicalHistory_Repository;
        GenericRepo<Cart> Cart_Repository; 
        GenericRepo<OrderState> OrderState_Repository;
        GenericRepo<Order> Order_Repository;
        GenericRepo<Cart_ShopItem> Cart_ShopItem_Repository;
        GenericRepo<Stocking> Stocking_Repository;
        GenericRepo<StockingDetails> StockingDetails_Repository;
        GenericRepo<EvaluationTemplate> EvaluationTemplate_Repository;
        GenericRepo<EvaluationTemplateGroup> EvaluationTemplateGroup_Repository;
        GenericRepo<EvaluationTemplateGroupQuestion> EvaluationTemplateGroupQuestion_Repository;
        GenericRepo<EvaluationBookCorrection> EvaluationBookCorrection_Repository;
        GenericRepo<EvaluationEmployeeStudentBookCorrection> EvaluationEmployeeStudentBookCorrection_Repository;
        GenericRepo<EvaluationEmployee> EvaluationEmployee_Repository;
        GenericRepo<EvaluationEmployeeQuestion> EvaluationEmployeeQuestion_Repository;
        GenericRepo<SchoolPCs> SchoolPCs_Repository;
        GenericRepo<BloomLevel> BloomLevel_Repository;
        GenericRepo<DokLevel> DokLevel_Repository;
        GenericRepo<Lesson> Lesson_Repository;
        GenericRepo<LessonActivity> LessonActivity_Repository;
        GenericRepo<LessonActivityType> LessonActivityType_Repository;
        GenericRepo<LessonLive> LessonLive_Repository;
        GenericRepo<LessonResource> LessonResource_Repository;
        GenericRepo<LessonResourceType> LessonResourceType_Repository;
        GenericRepo<LessonResourceClassroom> LessonResourceClassroom_Repository;
        GenericRepo<LessonTag> LessonTag_Repository;
        GenericRepo<Medal> Medal_Repository;
        GenericRepo<StudentMedal> StudentMedal_Repository;
        GenericRepo<StudentPerformance> StudentPerformance_Repository;
        GenericRepo<WeightType> WeightType_Repository;
        GenericRepo<SubjectWeightType> SubjectWeightType_Repository;
        GenericRepo<SemesterWorkingWeek> SemesterWorkingWeek_Repository;
        GenericRepo<Tag> Tag_Repository;
        GenericRepo<PerformanceType> PerformanceType_Repository;
        GenericRepo<DailyPerformance> DailyPerformance_Repository;
        GenericRepo<QuestionBank> QuestionBank_Repository;
        GenericRepo<QuestionBankTags> QuestionBankTags_Repository;
        GenericRepo<QuestionBankOption> QuestionBankOption_Repository;
        GenericRepo<SubBankQuestion> SubBankQuestion_Repository;
        GenericRepo<DragAndDropAnswer> DragAndDropAnswer_Repository;
        GenericRepo<QuestionBankType> QuestionBankType_Repository;




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
        public GenericRepo<EmployeeStudent> employeeStudent_Repository
        {
            get
            {
                if (EmployeeStudent_Repository == null)
                {
                    EmployeeStudent_Repository = new GenericRepo<EmployeeStudent>(db);
                }
                return EmployeeStudent_Repository;
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
        public GenericRepo<TestState> testState_Repository
        {
            get
            {
                if (TestState_Repository == null)
                {
                    TestState_Repository = new GenericRepo<TestState>(db);
                }
                return TestState_Repository;
            }
        }
        public GenericRepo<RegisterationFormTest> registerationFormTest_Repository
        {
            get
            {
                if (RegisterationFormTest_Repository == null)
                {
                    RegisterationFormTest_Repository = new GenericRepo<RegisterationFormTest>(db);
                }
                return RegisterationFormTest_Repository;
            }
        }
        public GenericRepo<RegisterationFormTestAnswer> registerationFormTestAnswer_Repository
        {
            get
            {
                if (RegisterationFormTestAnswer_Repository == null)
                {
                    RegisterationFormTestAnswer_Repository = new GenericRepo<RegisterationFormTestAnswer>(db);
                }
                return RegisterationFormTestAnswer_Repository;
            }
        } 
        public GenericRepo<QuestionType> questionType_Repository
        {
            get
            {
                if (QuestionType_Repository == null)
                {
                    QuestionType_Repository = new GenericRepo<QuestionType>(db);
                }
                return QuestionType_Repository;
            }
        }
        public GenericRepo<Question> question_Repository
        {
            get
            {
                if (Question_Repository == null)
                {
                    Question_Repository = new GenericRepo<Question>(db);
                }
                return Question_Repository;
            }
        }
        public GenericRepo<Test> test_Repository
        {
            get
            {
                if (Test_Repository == null)
                {
                    Test_Repository = new GenericRepo<Test>(db);
                }
                return Test_Repository;
            }
        }

        public GenericRepo<MCQQuestionOption> mCQQuestionOption_Repository
        {
            get
            {
                if (MCQQuestionOption_Repository == null)
                {
                    MCQQuestionOption_Repository = new GenericRepo<MCQQuestionOption>(db);
                }
                return MCQQuestionOption_Repository;
            }
        }

        public GenericRepo<RegistrationFormCategory> registrationFormCategory_Repository
        {
            get
            {
                if (RegistrationFormCategory_Repository == null)
                {
                    RegistrationFormCategory_Repository = new GenericRepo<RegistrationFormCategory>(db);
                }
                return RegistrationFormCategory_Repository;
            }
        }
        
        public GenericRepo<AccountingEntriesDocType> accountingEntriesDocType_Repository
        {
            get
            {
                if (AccountingEntriesDocType_Repository == null)
                {
                    AccountingEntriesDocType_Repository = new GenericRepo<AccountingEntriesDocType>(db);
                }
                return AccountingEntriesDocType_Repository;
            }
        }
        
        public GenericRepo<EndType> endType_Repository
        {
            get
            {
                if (EndType_Repository == null)
                {
                    EndType_Repository = new GenericRepo<EndType>(db);
                }
                return EndType_Repository;
            }
        }
        
        public GenericRepo<SubType> subType_Repository
        {
            get
            {
                if (SubType_Repository == null)
                {
                    SubType_Repository = new GenericRepo<SubType>(db);
                }
                return SubType_Repository;
            }
        }
        
        public GenericRepo<MotionType> motionType_Repository
        {
            get
            {
                if (MotionType_Repository == null)
                {
                    MotionType_Repository = new GenericRepo<MotionType>(db);
                }
                return MotionType_Repository;
            }
        }
        
        public GenericRepo<AccountingTreeChart> accountingTreeChart_Repository
        {
            get
            {
                if (AccountingTreeChart_Repository == null)
                {
                    AccountingTreeChart_Repository = new GenericRepo<AccountingTreeChart>(db);
                }
                return AccountingTreeChart_Repository;
            }
        }
        
        public GenericRepo<Credit> credit_Repository
        {
            get
            {
                if (Credit_Repository == null)
                {
                    Credit_Repository = new GenericRepo<Credit>(db);
                }
                return Credit_Repository;
            }
        }
        
        public GenericRepo<Debit> debit_Repository
        {
            get
            {
                if (Debit_Repository == null)
                {
                    Debit_Repository = new GenericRepo<Debit>(db);
                }
                return Debit_Repository;
            }
        }
        
        public GenericRepo<Income> income_Repository
        {
            get
            {
                if (Income_Repository == null)
                {
                    Income_Repository = new GenericRepo<Income>(db);
                }
                return Income_Repository;
            }
        }
        
        public GenericRepo<Outcome> outcome_Repository
        {
            get
            {
                if (Outcome_Repository == null)
                {
                    Outcome_Repository = new GenericRepo<Outcome>(db);
                }
                return Outcome_Repository;
            }
        }
        
        public GenericRepo<Save> save_Repository
        {
            get
            {
                if (Save_Repository == null)
                {
                    Save_Repository = new GenericRepo<Save>(db);
                }
                return Save_Repository;
            }
        }
        
        public GenericRepo<Asset> asset_Repository
        {
            get
            {
                if (Asset_Repository == null)
                {
                    Asset_Repository = new GenericRepo<Asset>(db);
                }
                return Asset_Repository;
            }
        }
        
        public GenericRepo<TuitionFeesType> tuitionFeesType_Repository
        {
            get
            {
                if (TuitionFeesType_Repository == null)
                {
                    TuitionFeesType_Repository = new GenericRepo<TuitionFeesType>(db);
                }
                return TuitionFeesType_Repository;
            }
        }
        
        public GenericRepo<TuitionDiscountType> tuitionDiscountType_Repository
        {
            get
            {
                if (TuitionDiscountType_Repository == null)
                {
                    TuitionDiscountType_Repository = new GenericRepo<TuitionDiscountType>(db);
                }
                return TuitionDiscountType_Repository;
            }
        }
        
        public GenericRepo<Bank> bank_Repository
        {
            get
            {
                if (Bank_Repository == null)
                {
                    Bank_Repository = new GenericRepo<Bank>(db);
                }
                return Bank_Repository;
            }
        }
        
        public GenericRepo<Supplier> supplier_Repository
        {
            get
            {
                if (Supplier_Repository == null)
                {
                    Supplier_Repository = new GenericRepo<Supplier>(db);
                }
                return Supplier_Repository;
            }
        }
        
        public GenericRepo<Department> department_Repository
        {
            get
            {
                if (Department_Repository == null)
                {
                    Department_Repository = new GenericRepo<Department>(db);
                }
                return Department_Repository;
            }
        }
        
        public GenericRepo<AcademicDegree> academicDegree_Repository
        {
            get
            {
                if (AcademicDegree_Repository == null)
                {
                    AcademicDegree_Repository = new GenericRepo<AcademicDegree>(db);
                }
                return AcademicDegree_Repository;
            }
        }
        
        public GenericRepo<Job> job_Repository
        {
            get
            {
                if (Job_Repository == null)
                {
                    Job_Repository = new GenericRepo<Job>(db);
                }
                return Job_Repository;
            }
        }
        
        public GenericRepo<JobCategory> jobCategory_Repository
        {
            get
            {
                if (JobCategory_Repository == null)
                {
                    JobCategory_Repository = new GenericRepo<JobCategory>(db);
                }
                return JobCategory_Repository;
            }
        }
        
        public GenericRepo<ReasonForLeavingWork> reasonForLeavingWork_Repository
        {
            get
            {
                if (ReasonForLeavingWork_Repository == null)
                {
                    ReasonForLeavingWork_Repository = new GenericRepo<ReasonForLeavingWork>(db);
                }
                return ReasonForLeavingWork_Repository;
            }
        }
        
        public GenericRepo<Days> days_Repository
        {
            get
            {
                if (Days_Repository == null)
                {
                    Days_Repository = new GenericRepo<Days>(db);
                }
                return Days_Repository;
            }
        }
        
        public GenericRepo<EmployeeDays> employeeDays_Repository
        {
            get
            {
                if (EmployeeDays_Repository == null)
                {
                    EmployeeDays_Repository = new GenericRepo<EmployeeDays>(db);
                }
                return EmployeeDays_Repository;
            }
        }
        
        public GenericRepo<Country> country_Repository
        {
            get
            {
                if (Country_Repository == null)
                {
                    Country_Repository = new GenericRepo<Country>(octa_Db);
                }
                return Country_Repository;
            }
        }
        
        public GenericRepo<Nationality> nationality_Repository
        {
            get
            {
                if (Nationality_Repository == null)
                {
                    Nationality_Repository = new GenericRepo<Nationality>(octa_Db);
                }
                return Nationality_Repository;
            }
        }
         
        public GenericRepo<LinkFile> linkFile_Repository
        {
            get
            {
                if (LinkFile_Repository == null)
                {
                    LinkFile_Repository = new GenericRepo<LinkFile>(db);
                }
                return LinkFile_Repository;
            }
        }
         
        public GenericRepo<FeesActivation> feesActivation_Repository
        {
            get
            {
                if (FeesActivation_Repository == null)
                {
                    FeesActivation_Repository = new GenericRepo<FeesActivation>(db);
                }
                return FeesActivation_Repository;
            }
        }
         
        public GenericRepo<InstallmentDeductionMaster> installmentDeductionMaster_Repository
        {
            get
            {
                if (InstallmentDeductionMaster_Repository == null)
                {
                    InstallmentDeductionMaster_Repository = new GenericRepo<InstallmentDeductionMaster>(db);
                }
                return InstallmentDeductionMaster_Repository;
            }
        }
         
        public GenericRepo<InstallmentDeductionDetails> installmentDeductionDetails_Repository
        {
            get
            {
                if (InstallmentDeductionDetails_Repository == null)
                {
                    InstallmentDeductionDetails_Repository = new GenericRepo<InstallmentDeductionDetails>(db);
                }
                return InstallmentDeductionDetails_Repository;
            }
        }
         
        public GenericRepo<PayableDocType> payableDocType_Repository
        {
            get
            {
                if (PayableDocType_Repository == null)
                {
                    PayableDocType_Repository = new GenericRepo<PayableDocType>(db);
                }
                return PayableDocType_Repository;
            }
        }
         
        public GenericRepo<PayableMaster> payableMaster_Repository
        {
            get
            {
                if (PayableMaster_Repository == null)
                {
                    PayableMaster_Repository = new GenericRepo<PayableMaster>(db);
                }
                return PayableMaster_Repository;
            }
        }
         
        public GenericRepo<PayableDetails> payableDetails_Repository
        {
            get
            {
                if (PayableDetails_Repository == null)
                {
                    PayableDetails_Repository = new GenericRepo<PayableDetails>(db);
                }
                return PayableDetails_Repository;
            }
        }
         
        public GenericRepo<ReceivableDocType> receivableDocType_Repository
        {
            get
            {
                if (ReceivableDocType_Repository == null)
                {
                    ReceivableDocType_Repository = new GenericRepo<ReceivableDocType>(db);
                }
                return ReceivableDocType_Repository;
            }
        }
         
        public GenericRepo<ReceivableMaster> receivableMaster_Repository
        {
            get
            {
                if (ReceivableMaster_Repository == null)
                {
                    ReceivableMaster_Repository = new GenericRepo<ReceivableMaster>(db);
                }
                return ReceivableMaster_Repository;
            }
        }
         
        public GenericRepo<ReceivableDetails> receivableDetails_Repository
        {
            get
            {
                if (ReceivableDetails_Repository == null)
                {
                    ReceivableDetails_Repository = new GenericRepo<ReceivableDetails>(db);
                }
                return ReceivableDetails_Repository;
            }
        }
         
        public GenericRepo<AccountingEntriesMaster> accountingEntriesMaster_Repository
        {
            get
            {
                if (AccountingEntriesMaster_Repository == null)
                {
                    AccountingEntriesMaster_Repository = new GenericRepo<AccountingEntriesMaster>(db);
                }
                return AccountingEntriesMaster_Repository;
            }
        }
         
        public GenericRepo<AccountingEntriesDetails> accountingEntriesDetails_Repository
        {
            get
            {
                if (AccountingEntriesDetails_Repository == null)
                {
                    AccountingEntriesDetails_Repository = new GenericRepo<AccountingEntriesDetails>(db);
                }
                return AccountingEntriesDetails_Repository;
            }
        }
        
        public GenericRepo<Gender> gender_Repository
        {
            get
            {
                if (Gender_Repository == null)
                {
                    Gender_Repository = new GenericRepo<Gender>(db);
                }
                return Gender_Repository;
            }
        }
        
        public GenericRepo<InventoryMaster> inventoryMaster_Repository
        {
            get
            {
                if (InventoryMaster_Repository == null)
                {
                    InventoryMaster_Repository = new GenericRepo<InventoryMaster>(db);
                }
                return InventoryMaster_Repository;
            }
        }
        
        public GenericRepo<InventoryDetails> inventoryDetails_Repository
        {
            get
            {
                if (InventoryDetails_Repository == null)
                {
                    InventoryDetails_Repository = new GenericRepo<InventoryDetails>(db);
                }
                return InventoryDetails_Repository;
            }
        }

        public GenericRepo<Stocking> stocking_Repository
        {
            get
            {
                if (Stocking_Repository == null)
                {
                    Stocking_Repository = new GenericRepo<Stocking>(db);
                }
                return Stocking_Repository;
            }
        }

        public GenericRepo<StockingDetails> stockingDetails_Repository
        {
            get
            {
                if (StockingDetails_Repository == null)
                {
                    StockingDetails_Repository = new GenericRepo<StockingDetails>(db);
                }
                return StockingDetails_Repository;
            }
        }

        public GenericRepo<SalesItemAttachment> salesItemAttachment_Repository
        {
            get
            {
                if (SalesItemAttachment_Repository == null)
                {
                    SalesItemAttachment_Repository = new GenericRepo<SalesItemAttachment>(db);
                }
                return SalesItemAttachment_Repository;
            }
        }
        
        public GenericRepo<ShopItem> shopItem_Repository
        {
            get
            {
                if (ShopItem_Repository == null)
                {
                    ShopItem_Repository = new GenericRepo<ShopItem>(db);
                }
                return ShopItem_Repository;
            }
        }
        
        public GenericRepo<Store> store_Repository
        {
            get
            {
                if (Store_Repository == null)
                {
                    Store_Repository = new GenericRepo<Store>(db);
                }
                return Store_Repository;
            }
        }
        
        public GenericRepo<ShopItemSize> shopItemSize_Repository
        {
            get
            {
                if (ShopItemSize_Repository == null)
                {
                    ShopItemSize_Repository = new GenericRepo<ShopItemSize>(db);
                }
                return ShopItemSize_Repository;
            }
        }
        
        public GenericRepo<ShopItemColor> shopItemColor_Repository
        {
            get
            {
                if (ShopItemColor_Repository == null)
                {
                    ShopItemColor_Repository = new GenericRepo<ShopItemColor>(db);
                }
                return ShopItemColor_Repository;
            }
        }
        
        public GenericRepo<InventoryCategories> inventoryCategories_Repository
        {
            get
            {
                if (InventoryCategories_Repository == null)
                {
                    InventoryCategories_Repository = new GenericRepo<InventoryCategories>(db);
                }
                return InventoryCategories_Repository;
            }
        }
        
        public GenericRepo<InventorySubCategories> inventorySubCategories_Repository
        {
            get
            {
                if (InventorySubCategories_Repository == null)
                {
                    InventorySubCategories_Repository = new GenericRepo<InventorySubCategories>(db);
                }
                return InventorySubCategories_Repository;
            }
        }
        
        public GenericRepo<StoreCategories> storeCategories_Repository
        {
            get
            {
                if (StoreCategories_Repository == null)
                {
                    StoreCategories_Repository = new GenericRepo<StoreCategories>(db);
                }
                return StoreCategories_Repository;
            }
        }

        public GenericRepo<InventoryFlags> inventoryFlags_Repository
        {
            get
            {
                if (InventoryFlags_Repository == null)
                {
                    InventoryFlags_Repository = new GenericRepo<InventoryFlags>(db);
                }
                return InventoryFlags_Repository;
            }
        }

        public GenericRepo<HygieneType> hygieneType_Repository
        {
            get
            {
                if (HygieneType_Repository == null)
                {   
                    HygieneType_Repository = new GenericRepo<HygieneType>(db);
                }
                return HygieneType_Repository;
            }
        }

        public GenericRepo<Diagnosis> diagnosis_Repository
        {
            get
            {
                if (Diagnosis_Repository == null)
                {
                    Diagnosis_Repository = new GenericRepo<Diagnosis>(db);
                }
                return Diagnosis_Repository;
            }
        }

        public GenericRepo<Drug> drug_Repository
        {
            get
            {
                if (Drug_Repository == null)
                {
                    Drug_Repository = new GenericRepo<Drug>(db);
                }
                return Drug_Repository;
            }
        }

        public GenericRepo<Dose> dose_Repository
        {
            get
            {
                if (Dose_Repository == null)
                {
                    Dose_Repository = new GenericRepo<Dose>(db);
                }
                return Dose_Repository;
            }
        }

        public GenericRepo<HygieneForm> hygieneForm_Repository
        {
            get
            {
                if (HygieneForm_Repository == null)
                {
                    HygieneForm_Repository = new GenericRepo<HygieneForm>(db);
                }
                return HygieneForm_Repository;
            }
        }

        public GenericRepo<FollowUp> followUp_Repository
        {
            get
            {
                if (FollowUp_Repository == null)
                {
                    FollowUp_Repository = new GenericRepo<FollowUp>(db);
                }
                return FollowUp_Repository;
            }
        }

        public GenericRepo<FollowUpDrug> followUpDrug_Repository
        {
            get
            {
                if (FollowUpDrug_Repository == null)
                {
                    FollowUpDrug_Repository = new GenericRepo<FollowUpDrug>(db);
                }
                return FollowUpDrug_Repository;
            }
        }

        public GenericRepo<StudentHygieneTypes> studentHygieneTypes_Repository
        {
            get
            {
                if (StudentHygieneTypes_Repository == null)
                {
                    StudentHygieneTypes_Repository = new GenericRepo<StudentHygieneTypes>(db);
                }
                return StudentHygieneTypes_Repository;
            }
        }

        public GenericRepo<StudentHygienes> studentHygiens_Repository
        {
            get
            {
                if (StudentHygiens_Repository == null)
                {
                    StudentHygiens_Repository = new GenericRepo<StudentHygienes>(db);
                }
                return StudentHygiens_Repository;
            }
        }

        public GenericRepo<MedicalHistory> medicalHistory_Repository
        {
            get
            {
                if (MedicalHistory_Repository == null)
                {
                    MedicalHistory_Repository = new GenericRepo<MedicalHistory>(db);
                }
                return MedicalHistory_Repository;
            }
        } 

        public GenericRepo<Cart> cart_Repository
        {
            get
            {
                if (Cart_Repository == null)
                {
                    Cart_Repository = new GenericRepo<Cart>(db);
                }
                return Cart_Repository;
            }
        }

        public GenericRepo<Cart_ShopItem> cart_ShopItem_Repository
        {
            get
            {
                if (Cart_ShopItem_Repository == null)
                {
                    Cart_ShopItem_Repository = new GenericRepo<Cart_ShopItem>(db);
                }
                return Cart_ShopItem_Repository;
            }
        }

        public GenericRepo<OrderState> orderState_Repository
        {
            get
            {
                if (OrderState_Repository == null)
                {
                    OrderState_Repository = new GenericRepo<OrderState>(db);
                }
                return OrderState_Repository;
            }
        }

        public GenericRepo<Order> order_Repository
        {
            get
            {
                if (Order_Repository == null)
                {
                    Order_Repository = new GenericRepo<Order>(db);
                }
                return Order_Repository;
            }
        }
        
        public GenericRepo<EvaluationBookCorrection> evaluationBookCorrection_Repository
        {
            get
            {
                if (EvaluationBookCorrection_Repository == null)
                {
                    EvaluationBookCorrection_Repository = new GenericRepo<EvaluationBookCorrection>(db);
                }
                return EvaluationBookCorrection_Repository;
            }
        }
        
        public GenericRepo<EvaluationEmployee> evaluationEmployee_Repository
        {
            get
            {
                if (EvaluationEmployee_Repository == null)
                {
                    EvaluationEmployee_Repository = new GenericRepo<EvaluationEmployee>(db);
                }
                return EvaluationEmployee_Repository;
            }
        }
        
        public GenericRepo<EvaluationEmployeeQuestion> evaluationEmployeeQuestion_Repository
        {
            get
            {
                if (EvaluationEmployeeQuestion_Repository == null)
                {
                    EvaluationEmployeeQuestion_Repository = new GenericRepo<EvaluationEmployeeQuestion>(db);
                }
                return EvaluationEmployeeQuestion_Repository;
            }
        }
        
        public GenericRepo<EvaluationEmployeeStudentBookCorrection> evaluationEmployeeStudentBookCorrection_Repository
        {
            get
            {
                if (EvaluationEmployeeStudentBookCorrection_Repository == null)
                {
                    EvaluationEmployeeStudentBookCorrection_Repository = new GenericRepo<EvaluationEmployeeStudentBookCorrection>(db);
                }
                return EvaluationEmployeeStudentBookCorrection_Repository;
            }
        }
        
        public GenericRepo<EvaluationTemplate> evaluationTemplate_Repository
        {
            get
            {
                if (EvaluationTemplate_Repository == null)
                {
                    EvaluationTemplate_Repository = new GenericRepo<EvaluationTemplate>(db);
                }
                return EvaluationTemplate_Repository;
            }
        }
        
        public GenericRepo<EvaluationTemplateGroup> evaluationTemplateGroup_Repository
        {
            get
            {
                if (EvaluationTemplateGroup_Repository == null)
                {
                    EvaluationTemplateGroup_Repository = new GenericRepo<EvaluationTemplateGroup>(db);
                }
                return EvaluationTemplateGroup_Repository;
            }
        }
        
        public GenericRepo<EvaluationTemplateGroupQuestion> evaluationTemplateGroupQuestion_Repository
        {
            get
            {
                if (EvaluationTemplateGroupQuestion_Repository == null)
                {
                    EvaluationTemplateGroupQuestion_Repository = new GenericRepo<EvaluationTemplateGroupQuestion>(db);
                }
                return EvaluationTemplateGroupQuestion_Repository;
            }
        }
        
        public GenericRepo<SchoolPCs> schoolPCs_Repository
        {
            get
            {
                if (SchoolPCs_Repository == null)
                {
                    SchoolPCs_Repository = new GenericRepo<SchoolPCs>(db);
                }
                return SchoolPCs_Repository;
            }
        }
        
        public GenericRepo<BloomLevel> bloomLevel_Repository
        {
            get
            {
                if (BloomLevel_Repository == null)
                {
                    BloomLevel_Repository = new GenericRepo<BloomLevel>(db);
                }
                return BloomLevel_Repository;
            }
        }
        
        public GenericRepo<DokLevel> dokLevel_Repository
        {
            get
            {
                if (DokLevel_Repository == null)
                {
                    DokLevel_Repository = new GenericRepo<DokLevel>(db);
                }
                return DokLevel_Repository;
            }
        }
        
        public GenericRepo<Lesson> lesson_Repository
        {
            get
            {
                if (Lesson_Repository == null)
                {
                    Lesson_Repository = new GenericRepo<Lesson>(db);
                }
                return Lesson_Repository;
            }
        }
        
        public GenericRepo<LessonActivity> lessonActivity_Repository
        {
            get
            {
                if (LessonActivity_Repository == null)
                {
                    LessonActivity_Repository = new GenericRepo<LessonActivity>(db);
                }
                return LessonActivity_Repository;
            }
        }
        
        public GenericRepo<LessonActivityType> lessonActivityType_Repository
        {
            get
            {
                if (LessonActivityType_Repository == null)
                {
                    LessonActivityType_Repository = new GenericRepo<LessonActivityType>(db);
                }
                return LessonActivityType_Repository;
            }
        }
        
        public GenericRepo<LessonLive> lessonLive_Repository
        {
            get
            {
                if (LessonLive_Repository == null)
                {
                    LessonLive_Repository = new GenericRepo<LessonLive>(db);
                }
                return LessonLive_Repository;
            }
        }
        
        public GenericRepo<LessonResource> lessonResource_Repository
        {
            get
            {
                if (LessonResource_Repository == null)
                {
                    LessonResource_Repository = new GenericRepo<LessonResource>(db);
                }
                return LessonResource_Repository;
            }
        }
        
        public GenericRepo<LessonResourceType> lessonResourceType_Repository
        {
            get
            {
                if (LessonResourceType_Repository == null)
                {
                    LessonResourceType_Repository = new GenericRepo<LessonResourceType>(db);
                }
                return LessonResourceType_Repository;
            }
        }
        
        public GenericRepo<LessonResourceClassroom> lessonResourceClassroom_Repository
        {
            get
            {
                if (LessonResourceClassroom_Repository == null)
                {
                    LessonResourceClassroom_Repository = new GenericRepo<LessonResourceClassroom>(db);
                }
                return LessonResourceClassroom_Repository;
            }
        }
        
        public GenericRepo<LessonTag> lessonTag_Repository
        {
            get
            {
                if (LessonTag_Repository == null)
                {
                    LessonTag_Repository = new GenericRepo<LessonTag>(db);
                }
                return LessonTag_Repository;
            }
        }
        
        public GenericRepo<Medal> medal_Repository
        {
            get
            {
                if (Medal_Repository == null)
                {
                    Medal_Repository = new GenericRepo<Medal>(db);
                }
                return Medal_Repository;
            }
        }
        
        public GenericRepo<StudentMedal> studentMedal_Repository
        {
            get
            {
                if (StudentMedal_Repository == null)
                {
                    StudentMedal_Repository = new GenericRepo<StudentMedal>(db);
                }
                return StudentMedal_Repository;
            }
        }
        
        public GenericRepo<StudentPerformance> studentPerformance_Repository
        {
            get
            {
                if (StudentPerformance_Repository == null)
                {
                    StudentPerformance_Repository = new GenericRepo<StudentPerformance>(db);
                }
                return StudentPerformance_Repository;
            }
        }

        public GenericRepo<DailyPerformance> dailyPerformance_Repository
        {
            get
            {
                if (DailyPerformance_Repository == null)
                {
                    DailyPerformance_Repository = new GenericRepo<DailyPerformance>(db);
                }
                return DailyPerformance_Repository;
            }
        }

        public GenericRepo<WeightType> weightType_Repository
        {
            get
            {
                if (WeightType_Repository == null)
                {
                    WeightType_Repository = new GenericRepo<WeightType>(db);
                }
                return WeightType_Repository;
            }
        }
        
        public GenericRepo<SubjectWeightType> subjectWeightType_Repository
        {
            get
            {
                if (SubjectWeightType_Repository == null)
                {
                    SubjectWeightType_Repository = new GenericRepo<SubjectWeightType>(db);
                }
                return SubjectWeightType_Repository;
            }
        }
        
        public GenericRepo<SemesterWorkingWeek> semesterWorkingWeek_Repository
        {
            get
            {
                if (SemesterWorkingWeek_Repository == null)
                {
                    SemesterWorkingWeek_Repository = new GenericRepo<SemesterWorkingWeek>(db);
                }
                return SemesterWorkingWeek_Repository;
            }
        }
        
        public GenericRepo<Tag> tag_Repository
        {
            get
            {
                if (Tag_Repository == null)
                {
                    Tag_Repository = new GenericRepo<Tag>(db);
                }
                return Tag_Repository;
            }
        }
        
        public GenericRepo<PerformanceType> performanceType_Repository
        {
            get
            {
                if (PerformanceType_Repository == null)
                {
                    PerformanceType_Repository = new GenericRepo<PerformanceType>(db);
                }
                return PerformanceType_Repository;
            }
        }

        public GenericRepo<QuestionBank> questionBank_Repository
        {
            get
            {
                if (QuestionBank_Repository == null)
                {
                    QuestionBank_Repository = new GenericRepo<QuestionBank>(db);
                }
                return QuestionBank_Repository;
            }
        }

        public GenericRepo<QuestionBankTags> questionBankTags_Repository
        {
            get
            {
                if (QuestionBankTags_Repository == null)
                {
                    QuestionBankTags_Repository = new GenericRepo<QuestionBankTags>(db);
                }
                return QuestionBankTags_Repository;
            }
        }
        public GenericRepo<QuestionBankOption> questionBankOption_Repository
        {
            get
            {
                if (QuestionBankOption_Repository == null)
                {
                    QuestionBankOption_Repository = new GenericRepo<QuestionBankOption>(db);
                }
                return QuestionBankOption_Repository;
            }
        }
        public GenericRepo<SubBankQuestion> subBankQuestion_Repository
        {
            get
            {
                if (SubBankQuestion_Repository == null)
                {
                    SubBankQuestion_Repository = new GenericRepo<SubBankQuestion>(db);
                }
                return SubBankQuestion_Repository;
            }
        }
        public GenericRepo<DragAndDropAnswer> dragAndDropAnswer_Repository
        {
            get
            {
                if (DragAndDropAnswer_Repository == null)
                {
                    DragAndDropAnswer_Repository = new GenericRepo<DragAndDropAnswer>(db);
                }
                return DragAndDropAnswer_Repository;
            }
        }

        public GenericRepo<QuestionBankType> questionBankType_Repository
        {
            get
            {
                if (QuestionBankType_Repository == null)
                {
                    QuestionBankType_Repository = new GenericRepo<QuestionBankType>(db);
                }
                return QuestionBankType_Repository;
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
