using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_DAL.Models.Domains.ViolationModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains
{
    public partial class LMS_CMS_Context : DbContext
    {
        public DbSet<Parent> Parent { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Role_Detailes> Role_Detailes { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<AcademicYear> AcademicYear { get; set; }
        public DbSet<Semester> Semester { get; set; }
        public DbSet<BusType> BusType { get; set; }
        public DbSet<BusDistrict> BusDistrict { get; set; }
        public DbSet<BusStatus> BusStatus { get; set; }
        public DbSet<BusCategory> BusCategory { get; set; }
        public DbSet<BusCompany> BusCompany { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<BusStudent> BusStudent { get; set; }
        public DbSet<StudentAcademicYear> StudentAcademicYear { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<EmployeeAttachment> EmployeeAttachment { get; set; }
        public DbSet<Violation> Violation { get; set; }
        public DbSet<EmployeeTypeViolation> EmployeeTypeViolation { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<SubjectCategory> SubjectCategory { get; set; }
        public DbSet<Floor> Floor { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<SchoolType> SchoolType { get; set; }
        public DbSet<RegisterationFormTestAnswer> RegisterationFormTestAnswer { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<MCQQuestionOption> MCQQuestionOption { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<TestState> TestState { get; set; }
        public DbSet<InterviewTime> InterviewTime { get; set; }
        public DbSet<RegisterationFormInterview> RegisterationFormInterview { get; set; }
        public DbSet<FieldOption> FieldOption { get; set; }
        public DbSet<FieldType> FieldType { get; set; }
        public DbSet<CategoryField> CategoryField { get; set; }
        public DbSet<RegistrationCategory> RegistrationCategory { get; set; }
        public DbSet<RegisterationFormTest> RegisterationFormTest { get; set; }
        public DbSet<RegistrationForm> RegistrationForm  { get; set; }
        public DbSet<RegisterationFormSubmittion> RegisterationFormSubmittion { get; set; }
        public DbSet<RegisterationFormParent> RegisterationFormParent { get; set; }
        public DbSet<RegisterationFormState> RegisterationFormState { get; set; }
        public DbSet<InterviewState> InterViewState { get; set; }
        public DbSet<RegistrationFormCategory> RegistrationFormCategory { get; set; }
        public DbSet<EndType> EndTypes { get; set; }
        public DbSet<AccountingEntriesDocType> AccountingEntriesDocTypes { get; set; }
        public DbSet<MotionType> MotionTypes { get; set; }
        public DbSet<SubType> SubTypes { get; set; }
        public DbSet<AccountingTreeChart> AccountingTreeCharts { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Debit> Debits { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Outcome> Outcomes { get; set; }
        public DbSet<Save> Saves { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<TuitionFeesType> TuitionFeesTypes { get; set; }
        public DbSet<TuitionDiscountType> TuitionDiscountTypes { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<ReasonForLeavingWork> ReasonsForLeavingWork { get; set; }
        public DbSet<Days> Days { get; set; }
        public DbSet<EmployeeDays> EmployeeDays { get; set; }
        public DbSet<LinkFile> LinkFile { get; set; }
        public DbSet<EmployeeStudent> EmployeeStudent { get; set; }
        public DbSet<ReceivableDocType> ReceivableDocType { get; set; }
        public DbSet<PayableDocType> PayableDocType { get; set; }
        public DbSet<ReceivableMaster> ReceivableMaster { get; set; }
        public DbSet<PayableMaster> PayableMaster { get; set; }
        public DbSet<ReceivableDetails> ReceivableDetails { get; set; }
        public DbSet<PayableDetails> PayableDetails { get; set; }
        public DbSet<AccountingEntriesMaster> AccountingEntriesMaster { get; set; }
        public DbSet<AccountingEntriesDetails> AccountingEntriesDetails { get; set; }
        public DbSet<InstallmentDeductionMaster> InstallmentDeductionMaster { get; set; }
        public DbSet<InstallmentDeductionDetails> InstallmentDeductionDetails { get; set; }
        public DbSet<FeesActivation> FeesActivation { get; set; }



        public LMS_CMS_Context(DbContextOptions<LMS_CMS_Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///////////////////////// Unique Values: /////////////////////////
            modelBuilder.Entity<Parent>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Parent>()
                .HasIndex(p => p.User_Name)
                .IsUnique();

            modelBuilder.Entity<Role>()
               .HasIndex(p => p.Name)
               .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(p => p.Email)
                .IsUnique();
            modelBuilder.Entity<Student>()
                .HasIndex(p => p.User_Name)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.User_Name)
                .IsUnique();
            
            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Violation>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<EmployeeType>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<TestState>()
               .HasIndex(p => p.Name)
               .IsUnique();

            modelBuilder.Entity<InterviewState>()
               .HasIndex(p => p.Name)
               .IsUnique();

            modelBuilder.Entity<RegisterationFormState>()
               .HasIndex(p => p.Name)
               .IsUnique();

            ///////////////////////// No Identity: /////////////////////////
            
            modelBuilder.Entity<Page>()
                .Property(p => p.ID)
                .ValueGeneratedNever();

            modelBuilder.Entity<EmployeeType>()
                .Property(p => p.ID)
                .ValueGeneratedNever();

            modelBuilder.Entity<SchoolType>()
                .Property(p => p.ID)
                .ValueGeneratedNever();

            modelBuilder.Entity<FieldType>()
                .Property(p => p.ID)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<QuestionType>()
                .Property(p => p.ID)
                .ValueGeneratedNever();

            modelBuilder.Entity<InterviewState>()
                .Property(p => p.ID)
                .ValueGeneratedNever();

            modelBuilder.Entity<TestState>()
                .Property(p => p.ID)
                .ValueGeneratedNever();

            modelBuilder.Entity<RegisterationFormState>()
                .Property(p => p.ID)
                .ValueGeneratedNever();

            modelBuilder.Entity<AccountingTreeChart>()
                .Property(p => p.ID)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<Days>()
                .Property(p => p.ID)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<SubType>()
                .Property(p => p.ID)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<MotionType>()
                .Property(p => p.ID)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<EndType>()
                .Property(p => p.ID)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<AcademicDegree>()
                .Property(p => p.ID)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<LinkFile>()
                .Property(p => p.ID)
                .ValueGeneratedNever();

            ///////////////////////// OnDelete: /////////////////////////
            modelBuilder.Entity<Page>()
                 .HasOne(p => p.Parent)
                 .WithMany(p => p.ChildPages)
                 .HasForeignKey(p => p.Page_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.Role)
                 .WithMany(p => p.Employess)
                 .HasForeignKey(p => p.Role_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.EmployeeType)
                 .WithMany(p => p.Employees)
                 .HasForeignKey(p => p.EmployeeTypeID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.BusCompany)
                 .WithMany(p => p.Employees)
                 .HasForeignKey(p => p.BusCompanyID)
                 .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.ReasonForLeavingWork)
                 .WithMany(p => p.Employees)
                 .HasForeignKey(p => p.ReasonOfLeavingID)
                 .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.AccountNumber)
                 .WithMany(p => p.Employees)
                 .HasForeignKey(p => p.AccountNumberID)
                 .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.Department)
                 .WithMany(p => p.Employees)
                 .HasForeignKey(p => p.DepartmentID)
                 .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.Job)
                 .WithMany(p => p.Employees)
                 .HasForeignKey(p => p.JobID)
                 .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<Employee>()
                 .HasOne(p => p.AcademicDegree)
                 .WithMany(p => p.Employees)
                 .HasForeignKey(p => p.AcademicDegreeID)
                 .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<Role_Detailes>()
                 .HasOne(p => p.Role)
                 .WithMany(p => p.Role_Detailes)
                 .HasForeignKey(p => p.Role_ID)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role_Detailes>()
                 .HasOne(p => p.Page)
                 .WithMany(p => p.Role_Detailes)
                 .HasForeignKey(p => p.Page_ID)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                 .HasOne(p => p.Parent)
                 .WithMany(p => p.Students)
                 .HasForeignKey(p => p.Parent_Id)
                 .OnDelete(DeleteBehavior.Restrict);
          
            modelBuilder.Entity<Student>()
                 .HasOne(p => p.AccountNumber)
                 .WithMany(p => p.Students)
                 .HasForeignKey(p => p.AccountNumberID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicYear>()
                 .HasOne(p => p.School)
                 .WithMany(p => p.AcademicYears)
                 .HasForeignKey(p => p.SchoolID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Semester>()
                 .HasOne(p => p.AcademicYear)
                 .WithMany(p => p.Semesters)
                 .HasForeignKey(p => p.AcademicYearID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.BusType)
                 .WithMany(p => p.Buses)
                 .HasForeignKey(p => p.BusTypeID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.BusDistrict)
                 .WithMany(p => p.Buses)
                 .HasForeignKey(p => p.BusDistrictID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.BusStatus)
                 .WithMany(p => p.Buses)
                 .HasForeignKey(p => p.BusStatusID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.Driver)
                 .WithMany(p => p.DrivenBuses)
                 .HasForeignKey(p => p.DriverID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.DriverAssistant)
                 .WithMany(p => p.DriverAssistant)
                 .HasForeignKey(p => p.DriverAssistantID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                 .HasOne(p => p.BusCompany)
                 .WithMany(p => p.Buses)
                 .HasForeignKey(p => p.BusCompanyID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusStudent>()
                 .HasOne(p => p.Bus)
                 .WithMany(p => p.BusStudents)
                 .HasForeignKey(p => p.BusID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusStudent>()
                 .HasOne(p => p.Student)
                 .WithMany(p => p.BusStudents)
                 .HasForeignKey(p => p.StudentID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusStudent>()
                 .HasOne(p => p.BusCategory)
                 .WithMany(p => p.BusStudents)
                 .HasForeignKey(p => p.BusCategoryID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusStudent>()
                 .HasOne(p => p.Semester)
                 .WithMany(p => p.BusStudents)
                 .HasForeignKey(p => p.SemseterID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentAcademicYear>()
                 .HasOne(p => p.Student)
                 .WithMany(p => p.StudentAcademicYears)
                 .HasForeignKey(p => p.StudentID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentAcademicYear>()
                 .HasOne(p => p.School)
                 .WithMany(p => p.StudentAcademicYears)
                 .HasForeignKey(p => p.SchoolID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentAcademicYear>()
                 .HasOne(p => p.Classroom)
                 .WithMany(p => p.StudentAcademicYears)
                 .HasForeignKey(p => p.ClassID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentAcademicYear>()
                 .HasOne(p => p.Grade)
                 .WithMany(p => p.StudentAcademicYears)
                 .HasForeignKey(p => p.GradeID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                 .HasOne(p => p.Grade)
                 .WithMany(p => p.Subjects)
                 .HasForeignKey(p => p.GradeID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                 .HasOne(p => p.SubjectCategory)
                 .WithMany(p => p.Subjects)
                 .HasForeignKey(p => p.SubjectCategoryID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Floor>()
                 .HasOne(p => p.building)
                 .WithMany(p => p.Floors)
                 .HasForeignKey(p => p.buildingID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Building>()
                 .HasOne(p => p.school)
                 .WithMany(p => p.Buildings)
                 .HasForeignKey(p => p.SchoolID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Section>()
                 .HasOne(p => p.school)
                 .WithMany(p => p.Sections)
                 .HasForeignKey(p => p.SchoolID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                 .HasOne(p => p.Section)
                 .WithMany(p => p.Grades)
                 .HasForeignKey(p => p.SectionID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classroom>()
                 .HasOne(p => p.Grade)
                 .WithMany(p => p.Classrooms)
                 .HasForeignKey(p => p.GradeID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classroom>()
                 .HasOne(p => p.Floor)
                 .WithMany(p => p.Classrooms)
                 .HasForeignKey(p => p.FloorID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Semester>()
                 .HasOne(p => p.AcademicYear)
                 .WithMany(p => p.Semesters)
                 .HasForeignKey(p => p.AcademicYearID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<School>()
                 .HasOne(p => p.SchoolType)
                 .WithMany(p => p.Schools)
                 .HasForeignKey(p => p.SchoolTypeID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Floor>()
                .HasOne(f => f.floorMonitor) 
                .WithMany(e => e.Floors) 
                .HasForeignKey(f => f.FloorMonitorID) 
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeAttachments)
                .WithOne(ea => ea.Employee)
                .HasForeignKey(ea => ea.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormTest>()
                .HasOne(p => p.RegisterationFormParent)
                .WithMany(p => p.RegisterationFormTests)
                .HasForeignKey(p => p.RegisterationFormParentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormTest>()
                .HasOne(p => p.TestState)
                .WithMany(p => p.RegisterationFormTests)
                .HasForeignKey(p => p.StateID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormTest>()
                .HasOne(p => p.Test)
                .WithMany(p => p.RegisterationFormTests)
                .HasForeignKey(p => p.TestID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormTestAnswer>()
                .HasOne(p => p.RegisterationFormParent)
                .WithMany(p => p.RegisterationFormTestAnswers)
                .HasForeignKey(p => p.RegisterationFormParentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormTestAnswer>()
                .HasOne(p => p.Question)
                .WithMany(p => p.RegisterationFormTestAnswers)
                .HasForeignKey(p => p.QuestionID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormTestAnswer>()
                .HasOne(p => p.MCQQuestionOption)
                .WithMany(p => p.RegisterationFormTestAnswers)
                .HasForeignKey(p => p.AnswerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MCQQuestionOption>()
                .HasOne(p => p.Question)
                .WithMany(p => p.MCQQuestionOptions)
                .HasForeignKey(p => p.Question_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasOne(p => p.QuestionType)
                .WithMany(p => p.Questions)
                .HasForeignKey(p => p.QuestionTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasOne(p => p.mCQQuestionOption)
                .WithMany(p => p.Questions)
                .HasForeignKey(p => p.CorrectAnswerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasOne(p => p.test)
                .WithMany(p => p.Questions)
                .HasForeignKey(p => p.TestID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Test>()
                .HasOne(p => p.academicYear)
                .WithMany(p => p.Tests)
                .HasForeignKey(p => p.AcademicYearID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Test>()
                .HasOne(p => p.subject)
                .WithMany(p => p.Tests)
                .HasForeignKey(p => p.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Test>()
                .HasOne(p => p.Grade)
                .WithMany(p => p.Tests)
                .HasForeignKey(p => p.GradeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormInterview>()
                .HasOne(p => p.InterviewState)
                .WithMany(p => p.RegisterationFormInterviews)
                .HasForeignKey(p => p.InterviewStateID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormInterview>()
                .HasOne(p => p.RegisterationFormParent)
                .WithMany(p => p.RegisterationFormInterviews)
                .HasForeignKey(p => p.RegisterationFormParentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormInterview>()
                .HasOne(p => p.InterviewTime)
                .WithMany(p => p.RegisterationFormInterviews)
                .HasForeignKey(p => p.InterviewTimeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InterviewTime>()
                .HasOne(p => p.AcademicYear)
                .WithMany(p => p.InterviewTimes)
                .HasForeignKey(p => p.AcademicYearID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FieldOption>()
                .HasOne(p => p.CategoryField)
                .WithMany(p => p.FieldOptions)
                .HasForeignKey(p => p.CategoryFieldID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoryField>()
                .HasOne(p => p.FieldType)
                .WithMany(p => p.CategoryFields)
                .HasForeignKey(p => p.FieldTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoryField>()
                .HasOne(p => p.RegistrationCategory)
                .WithMany(p => p.CategoryFields)
                .HasForeignKey(p => p.RegistrationCategoryID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<RegistrationFormCategory>()
                .HasOne(p => p.RegistrationForm)
                .WithMany(p => p.RegistrationFormCategories)
                .HasForeignKey(p => p.RegistrationFormID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<RegistrationFormCategory>()
                .HasOne(p => p.RegistrationCategory)
                .WithMany(p => p.RegistrationFormCategories)
                .HasForeignKey(p => p.RegistrationCategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormSubmittion>()
                .HasOne(p => p.RegisterationFormParent)
                .WithMany(p => p.RegisterationFormSubmittions)
                .HasForeignKey(p => p.RegisterationFormParentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormSubmittion>()
                .HasOne(p => p.CategoryField)
                .WithMany(p => p.RegisterationFormSubmittions)
                .HasForeignKey(p => p.CategoryFieldID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormSubmittion>()
                .HasOne(p => p.FieldOption)
                .WithMany(p => p.RegisterationFormSubmittions)
                .HasForeignKey(p => p.SelectedFieldOptionID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormParent>()
                .HasOne(p => p.RegisterationFormState)
                .WithMany(p => p.RegisterationFormParents)
                .HasForeignKey(p => p.RegisterationFormStateID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormParent>()
                .HasOne(p => p.Parent)
                .WithMany(p => p.RegisterationFormParents)
                .HasForeignKey(p => p.ParentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisterationFormParent>()
                .HasOne(p => p.RegistrationForm)
                .WithMany(p => p.RegisterationFormParents)
                .HasForeignKey(p => p.RegistrationFormID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<EmployeeDays>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.EmployeeDays)
                .HasForeignKey(p => p.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<EmployeeDays>()
                .HasOne(p => p.Day)
                .WithMany(p => p.EmployeeDays)
                .HasForeignKey(p => p.DayID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Job>()
                .HasOne(p => p.JobCategory)
                .WithMany(p => p.Jobs)
                .HasForeignKey(p => p.JobCategoryID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Supplier>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.Suppliers)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Bank>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.Banks)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<TuitionDiscountType>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.TuitionDiscountTypes)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<TuitionFeesType>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.TuitionFeesTypes)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Asset>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.Assets)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Save>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.Saves)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Outcome>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.Outcomes)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Income>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.Incomes)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Debit>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.Debits)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Credit>()
                .HasOne(p => p.AccountNumber)
                .WithMany(p => p.Credits)
                .HasForeignKey(p => p.AccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AccountingTreeChart>()
                .HasOne(p => p.SubType)
                .WithMany(p => p.AccountingTreeCharts)
                .HasForeignKey(p => p.SubTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AccountingTreeChart>()
                .HasOne(p => p.EndType)
                .WithMany(p => p.AccountingTreeCharts)
                .HasForeignKey(p => p.EndTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AccountingTreeChart>()
                .HasOne(p => p.Parent)
                .WithMany(p => p.ChildAccountingTreeCharts)
                .HasForeignKey(p => p.MainAccountNumberID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AccountingTreeChart>()
                .HasOne(p => p.LinkFile)
                .WithMany(p => p.AccountingTreeCharts)
                .HasForeignKey(p => p.LinkFileID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeStudent>()
                .HasOne(p => p.employee)
                .WithMany(p => p.EmployeeStudents)
                .HasForeignKey(p => p.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeStudent>()
                .HasOne(p => p.Student)
                .WithMany(p => p.EmployeeStudents)
                .HasForeignKey(p => p.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LinkFile>()
                .HasOne(p => p.MotionType)
                .WithMany(p => p.LinkFiles)
                .HasForeignKey(p => p.MotionTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<FeesActivation>()
                .HasOne(p => p.Student)
                .WithMany(p => p.FeesActivations)
                .HasForeignKey(p => p.StudentID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<FeesActivation>()
                .HasOne(p => p.TuitionFeesType)
                .WithMany(p => p.FeesActivations)
                .HasForeignKey(p => p.FeeTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<FeesActivation>()
                .HasOne(p => p.TuitionDiscountType)
                .WithMany(p => p.FeesActivations)
                .HasForeignKey(p => p.FeeDiscountTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<InstallmentDeductionDetails>()
                .HasOne(p => p.TuitionFeesType)
                .WithMany(p => p.InstallmentDeductionDetails)
                .HasForeignKey(p => p.FeeTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<InstallmentDeductionDetails>()
                .HasOne(p => p.InstallmentDeductionMaster)
                .WithMany(p => p.InstallmentDeductionDetails)
                .HasForeignKey(p => p.InstallmentDeductionMasterID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<InstallmentDeductionMaster>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.InstallmentDeductionMasters)
                .HasForeignKey(p => p.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<InstallmentDeductionMaster>()
                .HasOne(p => p.Student)
                .WithMany(p => p.InstallmentDeductionMasters)
                .HasForeignKey(p => p.StudentID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AccountingEntriesMaster>()
                .HasOne(p => p.AccountingEntriesDocType)
                .WithMany(p => p.AccountingEntriesMasters)
                .HasForeignKey(p => p.AccountingEntriesDocTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AccountingEntriesDetails>()
                .HasOne(p => p.AccountingTreeChart)
                .WithMany(p => p.AccountingEntriesDetails)
                .HasForeignKey(p => p.AccountingTreeChartID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AccountingEntriesDetails>()
                .HasOne(p => p.AccountingEntriesMaster)
                .WithMany(p => p.AccountingEntriesDetails)
                .HasForeignKey(p => p.AccountingEntriesMasterID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<PayableMaster>()
                .HasOne(p => p.LinkFile)
                .WithMany(p => p.PayableMaster)
                .HasForeignKey(p => p.LinkFileID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<PayableMaster>()
                .HasOne(p => p.PayableDocType)
                .WithMany(p => p.PayableMaster)
                .HasForeignKey(p => p.PayableDocTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<PayableDetails>()
                .HasOne(p => p.LinkFile)
                .WithMany(p => p.PayableDetails)
                .HasForeignKey(p => p.LinkFileID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<PayableDetails>()
                .HasOne(p => p.PayableMaster)
                .WithMany(p => p.PayableDetails)
                .HasForeignKey(p => p.PayableMasterID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ReceivableMaster>()
                .HasOne(p => p.LinkFile)
                .WithMany(p => p.ReceivableMasters)
                .HasForeignKey(p => p.LinkFileID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ReceivableMaster>()
                .HasOne(p => p.ReceivableDocType)
                .WithMany(p => p.ReceivableMasters)
                .HasForeignKey(p => p.ReceivableDocTypesID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ReceivableDetails>()
                .HasOne(p => p.LinkFile)
                .WithMany(p => p.ReceivableDetails)
                .HasForeignKey(p => p.LinkFileID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ReceivableDetails>()
                .HasOne(p => p.ReceivableMaster)
                .WithMany(p => p.ReceivableDetails)
                .HasForeignKey(p => p.ReceivableMasterID)
                .OnDelete(DeleteBehavior.Restrict);


            ///////////////////////// Exception: /////////////////////////
            modelBuilder.Entity<Bus>()
                .HasOne(b => b.DeletedByEmployee)
                .WithMany()
                .HasForeignKey(b => b.DeletedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Floor>()
               .HasOne(f => f.DeletedByEmployee)
               .WithMany()  
               .HasForeignKey(f => f.DeletedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeDays>()
               .HasOne(f => f.DeletedByEmployee)
               .WithMany()  
               .HasForeignKey(f => f.DeletedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
               .HasOne(f => f.DeletedByEmployee)
               .WithMany()  
               .HasForeignKey(f => f.DeletedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<EmployeeStudent>()
               .HasOne(f => f.DeletedByEmployee)
               .WithMany()  
               .HasForeignKey(f => f.DeletedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<InstallmentDeductionMaster>()
               .HasOne(f => f.DeletedByEmployee)
               .WithMany()  
               .HasForeignKey(f => f.DeletedByUserId)
               .OnDelete(DeleteBehavior.Restrict);


            ///////////////////////// Optional ID According to other field: /////////////////////////  
            modelBuilder.Entity<ReceivableMaster>()
                .Ignore(r => r.Bank)
                .Ignore(r => r.Save);

            modelBuilder.Entity<PayableMaster>()
                .Ignore(r => r.Bank)
                .Ignore(r => r.Save);

            modelBuilder.Entity<ReceivableDetails>()
               .Ignore(r => r.Bank)
               .Ignore(r => r.Credit)
               .Ignore(r => r.Debit)
               .Ignore(r => r.Income)
               .Ignore(r => r.Outcome)
               .Ignore(r => r.Asset)
               .Ignore(r => r.TuitionFeesType)
               .Ignore(r => r.TuitionDiscountType)
               .Ignore(r => r.Supplier)
               .Ignore(r => r.Employee)
               .Ignore(r => r.Student)
               .Ignore(r => r.Save);

            modelBuilder.Entity<PayableDetails>()
               .Ignore(r => r.Bank)
               .Ignore(r => r.Credit)
               .Ignore(r => r.Debit)
               .Ignore(r => r.Income)
               .Ignore(r => r.Outcome)
               .Ignore(r => r.Asset)
               .Ignore(r => r.TuitionFeesType)
               .Ignore(r => r.TuitionDiscountType)
               .Ignore(r => r.Supplier)
               .Ignore(r => r.Employee)
               .Ignore(r => r.Student)
               .Ignore(r => r.Save);

            modelBuilder.Entity<AccountingEntriesDetails>()
               .Ignore(r => r.Bank)
               .Ignore(r => r.Credit)
               .Ignore(r => r.Debit)
               .Ignore(r => r.Income)
               .Ignore(r => r.Outcome)
               .Ignore(r => r.Asset)
               .Ignore(r => r.TuitionFeesType)
               .Ignore(r => r.TuitionDiscountType)
               .Ignore(r => r.Supplier)
               .Ignore(r => r.Employee)
               .Ignore(r => r.Student)
               .Ignore(r => r.Save);
        }
    }
}