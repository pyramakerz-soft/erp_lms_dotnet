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
             
            modelBuilder.Entity<Role_Detailes>()
                 .HasOne(p => p.Role)
                 .WithMany(p => p.Role_Detailes)
                 .HasForeignKey(p => p.Role_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role_Detailes>()
                 .HasOne(p => p.Page)
                 .WithMany(p => p.Role_Detailes)
                 .HasForeignKey(p => p.Page_ID)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                 .HasOne(p => p.Parent)
                 .WithMany(p => p.Students)
                 .HasForeignKey(p => p.Parent_Id)
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
        }
    }
}