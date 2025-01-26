import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/Login/login/login.component';
import { HomeParentComponent } from './Pages/Parent/home-parent/home-parent.component';
import { StudentHomeComponent } from './Pages/Student/student-home/student-home.component';
import { EmployeeHomeComponent } from './Pages/Employee/employee-home/employee-home.component';
import { noNavigateWithoutLoginGuard } from './Guards/no-navigate-without-login.guard';
import { noNavigateToLoginIfLoginGuard } from './Guards/no-navigate-to-login-if-login.guard';
import { navigateIfParentGuard } from './Guards/navigate-if-parent.guard';
import { navigateIfStudentGuard } from './Guards/navigate-if-student.guard';
import { navigateIfEmployeeGuard } from './Guards/navigate-if-employee.guard';
import { MainLayoutComponent } from './Pages/Layouts/main-layout/main-layout.component';
import { OctaLoginComponent } from './Pages/Login/octa-login/octa-login.component';
import { navigateIfOctaGuard } from './Guards/navigate-if-octa.guard';
import { noNavigateWithoutOctaLoginGuard } from './Guards/no-navigate-without-octa-login.guard';
import { navigateIfHaveSettingPageGuard } from './Guards/navigate-if-have-This-page.guard';
import { BusTypesComponent } from './Pages/Employee/Bus/bus-types/bus-types.component';
import { BusStatusComponent } from './Pages/Employee/Bus/bus-status/bus-status.component';
import { BusCategoriesComponent } from './Pages/Employee/Bus/bus-categories/bus-categories.component';
import { BusCompaniesComponent } from './Pages/Employee/Bus/bus-companies/bus-companies.component';
import { BusDetailsComponent } from './Pages/Employee/Bus/bus-details/bus-details.component';
import { BusStudentComponent } from './Pages/Employee/Bus/bus-student/bus-student.component';
import { BusPrintNameTagComponent } from './Pages/Employee/Bus/bus-print-name-tag/bus-print-name-tag.component';
import { BusDistrictsComponent } from './Pages/Employee/Bus/bus-districts/bus-districts.component';
import { DomainsComponent } from './Pages/Octa/domains/domains.component';
import { RoleComponent } from './Pages/Employee/Administrator/role/role.component';
import { RoleAddEditComponent } from './Pages/Employee/Administrator/role-add-edit/role-add-edit.component';
import { SchoolTypeComponent } from './Pages/Octa/school-type/school-type.component';
import { SchoolComponent as SchoolComponentOcta  } from './Pages/Octa/school/school.component';
import { SchoolComponent as SchoolComponentEmployee } from './Pages/Employee/Administrator/school/school.component';
import { AccountComponent } from './Pages/Octa/account/account.component';
import { SubjectCategoryComponent } from './Pages/Employee/LMS/subject-category/subject-category.component';
import { SubjectComponent } from './Pages/Employee/LMS/subject/subject.component';
import { SubjectViewComponent } from './Pages/Employee/LMS/subject-view/subject-view.component';
import { EmployeeComponent } from './Pages/Employee/Administrator/employee/employee.component';
import { EmployeeAddEditComponent } from './Pages/Employee/Administrator/employee-add-edit/employee-add-edit.component';
import { EmployeeViewComponent } from './Pages/Employee/Administrator/employee-view/employee-view.component';
import { BuildingComponent } from './Pages/Employee/LMS/building/building.component';
import { FloorComponent } from './Pages/Employee/LMS/floor/floor.component';
import { ClassroomComponent } from './Pages/Employee/LMS/classroom/classroom.component';
import { ViolationTypesComponent } from './Pages/Employee/Administrator/violation-types/violation-types.component';
import { SectionComponent } from './Pages/Employee/LMS/section/section.component';
import { GradeComponent } from './Pages/Employee/LMS/grade/grade.component';
import { AcademicYearComponent } from './Pages/Employee/LMS/academic-year/academic-year.component';
import { SemesterComponent } from './Pages/Employee/LMS/semester/semester.component';
import { SemesterViewComponent } from './Pages/Employee/LMS/semester-view/semester-view.component';
import { RegistrationFormFieldComponent } from './Pages/Employee/Registration/registration-form-field/registration-form-field.component';
import { FieldsComponent } from './Pages/Employee/Registration/fields/fields.component';
import { AdmissionTestComponent } from './Pages/Employee/Registration/admission-test/admission-test.component';
import { QuestionsComponent } from './Pages/Employee/Registration/questions/questions.component';
import { RegistrationFormComponent } from './Pages/Employee/Registration/registration-form/registration-form.component';
import { RegistrationConfirmationTestDetailsComponent } from './Pages/Employee/Registration/registration-confirmation-test-details/registration-confirmation-test-details.component';
import { RegistrationFormTestAnswerComponent } from './Pages/Employee/Registration/registration-form-test-answer/registration-form-test-answer.component';
import { RegistrationConfirmationComponent } from './Pages/Employee/Registration/registration-confirmation/registration-confirmation.component';
import { RegistrationConfirmationDetailsComponent } from './Pages/Employee/Registration/registration-confirmation-details/registration-confirmation-details.component';
import { InterviewTimeTableComponent } from './Pages/Employee/Registration/interview-time-table/interview-time-table.component';
import { InterviewRegistrationComponent as InterviewRegistrationComponentEmployee} from './Pages/Employee/Registration/interview-registration/interview-registration.component';
import { InterviewRegistrationComponent as InterviewRegistrationComponentParent} from './Pages/Parent/interview-registration/interview-registration.component';
import { AdmissionTestParentComponent } from './Pages/Parent/RegistrationModule/admission-test-parent/admission-test-parent.component';
import { RegistraionTestComponent } from './Pages/Parent/RegistrationModule/registraion-test/registraion-test.component';
import { ClassroomsAccommodationComponent } from './Pages/Employee/Registration/classrooms-accommodation/classrooms-accommodation.component';
import { SignUpComponent } from './Pages/Login/sign-up/sign-up.component';
import { SuppliersComponent } from './Pages/Employee/Accounting/suppliers/suppliers.component';
import { DebitsComponent } from './Pages/Employee/Accounting/debits/debits.component';
import { CreditsComponent } from './Pages/Employee/Accounting/credits/credits.component';
import { AssetsComponent } from './Pages/Employee/Accounting/assets/assets.component';
import { TuitionFeesTypesComponent } from './Pages/Employee/Accounting/tuition-fees-types/tuition-fees-types.component';
import { TuitionDiscountTypesComponent } from './Pages/Employee/Accounting/tuition-discount-types/tuition-discount-types.component';
import { AccountingEntriesDocTypeComponent } from './Pages/Employee/Accounting/accounting-entries-doc-type/accounting-entries-doc-type.component';
import { JobComponent } from './Pages/Employee/Administrator/job/job.component';
import { JobCategoriesComponent } from './Pages/Employee/Administrator/job-categories/job-categories.component';
import { AcademicDegreeComponent } from './Pages/Employee/Administrator/academic-degree/academic-degree.component';
import { ReasonsforleavingworkComponent } from './Pages/Employee/Administrator/reasonsforleavingwork/reasonsforleavingwork.component';
import { DepartmentComponent } from './Pages/Employee/Administrator/department/department.component';
import { OutcomesComponent } from './Pages/Employee/Accounting/outcomes/outcomes.component';
import { IncomesComponent } from './Pages/Employee/Accounting/incomes/incomes.component';
import { SavesComponent } from './Pages/Employee/Accounting/saves/saves.component';
import { AccountingTreeComponent } from './Pages/Employee/Accounting/accounting-tree/accounting-tree.component';
import { BankComponent } from './Pages/Employee/Accounting/bank/bank.component';

export const routes: Routes = [
    { path: "", component: LoginComponent, title: "Login", canActivate:[noNavigateToLoginIfLoginGuard] },
    { path: "Octa/login", component: OctaLoginComponent, title: "login", canActivate:[noNavigateToLoginIfLoginGuard] },
    { path: "SignUp", component: SignUpComponent, title: "SignUp", canActivate:[noNavigateToLoginIfLoginGuard] },

    
    { 
        path: "Employee",     
        component: MainLayoutComponent, 
        title: "Employee Home", 
        canActivate:[noNavigateWithoutLoginGuard,navigateIfEmployeeGuard], 
        children: [
            { path: "", component: EmployeeHomeComponent, title: "EmployeeHome" }, 
            { path: "Bus Details",component: BusDetailsComponent,title: "Bus"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Students/:domainName/:busId", component: BusStudentComponent, title: "Bus Students", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]}, 
            { path: "Bus Types", component: BusTypesComponent, title: "Bus Type"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Status", component: BusStatusComponent, title: "Bus Status"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Districts", component: BusDistrictsComponent, title: "Bus Districts"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Categories", component: BusCategoriesComponent, title: "Bus Category"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Companies", component: BusCompaniesComponent, title: "Bus Company"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  
            { path: "Print Name Tag", component: BusPrintNameTagComponent, title: "Print Name Tag"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  
            { path: "Role", component: RoleComponent, title: "Role"  , canActivate:[noNavigateWithoutLoginGuard,navigateIfHaveSettingPageGuard]}, 
            { path: "Role Create", component: RoleAddEditComponent, title: "Role Create"  , canActivate:[noNavigateWithoutLoginGuard]},  //
            { path: "Role Edit/:id", component: RoleAddEditComponent, title: "Role Edit"  , canActivate:[noNavigateWithoutLoginGuard]},  //
            { path: "Subject Categories", component: SubjectCategoryComponent, title: "Subject Categories"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  
            { path: "Subject", component: SubjectComponent, title: "Subjects"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  
            { path: "Subject/:domainName/:SubId", component: SubjectViewComponent, title: "Subject", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Employee", component: EmployeeComponent, title: "Employee", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Employee Create", component: EmployeeAddEditComponent, title: "Employee Create", canActivate:[noNavigateWithoutLoginGuard ]}, //
            { path: "Employee Edit/:id", component: EmployeeAddEditComponent, title: "Employee Edit", canActivate:[noNavigateWithoutLoginGuard ]}, //
            { path: "Employee Details/:id", component: EmployeeViewComponent, title: "Employee Details", canActivate:[noNavigateWithoutLoginGuard ]}, //
            { path: "Building", component: BuildingComponent, title: "Building", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Floor/:domainName/:Id", component: FloorComponent, title: "Floor", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Classroom", component: ClassroomComponent, title: "Classroom", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Violation Types", component: ViolationTypesComponent, title: "Violation Types", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Section", component: SectionComponent, title: "Section", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Grade/:domainName/:Id", component: GradeComponent, title: "Grade", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Academic Years", component: AcademicYearComponent, title: "Academic Year", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Semester/:domainName/:Id", component: SemesterComponent, title: "Semester", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "SemesterView/:domainName/:Id", component: SemesterViewComponent, title: "Semester", canActivate:[noNavigateWithoutLoginGuard ]}, //
            { path: "School", component: SchoolComponentEmployee, title: "Schools"  , canActivate:[noNavigateWithoutLoginGuard,navigateIfHaveSettingPageGuard]},  
            { path: "Registration Form", component: RegistrationFormComponent, title: "Registration Form"  , canActivate:[noNavigateWithoutLoginGuard,navigateIfHaveSettingPageGuard]},  
            { path: "Registration Form Field", component: RegistrationFormFieldComponent, title: "RegistrationFormField", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Registration Form Field/:id", component: FieldsComponent, title: "CategoryFields", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]},
            { path: "Admission Test", component: AdmissionTestComponent, title: "Admission Test", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]},
            { path: "Question/:id", component: QuestionsComponent, title: "question", canActivate:[noNavigateWithoutLoginGuard,navigateIfHaveSettingPageGuard]},
            { path: "Registration Confirmation", component: RegistrationConfirmationComponent, title: "Registration Confirmation"  , canActivate:[noNavigateWithoutLoginGuard,navigateIfHaveSettingPageGuard]},  
            { path: "Registration Confirmation/:Id", component: RegistrationConfirmationDetailsComponent, title: "Registration Confirmation"  , canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]},  
            { path: "Registration Confirmation Test/:id", component: RegistrationConfirmationTestDetailsComponent, title: "Registration Confirmation Test" , canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]},
            { path: "Registration Confirmation Test/:Rid/:Pid/:Tid", component: RegistrationFormTestAnswerComponent, title: "Registration Confirmation Test" , canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]},
            { path: "Interview Time Table", component: InterviewTimeTableComponent, title: "Interview Time Table"  , canActivate:[noNavigateWithoutLoginGuard,navigateIfHaveSettingPageGuard]},  
            { path: "Interview Registration/:Id", component: InterviewRegistrationComponentEmployee, title: "Interview Registration"  , canActivate:[noNavigateWithoutLoginGuard,navigateIfHaveSettingPageGuard]},  
            { path: "Classroom Accommodation", component: ClassroomsAccommodationComponent, title: "Classroom Accommodation" , canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]} ,
            { path: "Supplier", component: SuppliersComponent, title: "Suppliers" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Debit", component: DebitsComponent, title: "Debits" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Credit", component: CreditsComponent, title: "Credits" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Asset", component: AssetsComponent, title: "Assets" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Tuition Fees Type", component: TuitionFeesTypesComponent, title: "Tuition Fees Types" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Tuition Discount Type", component: TuitionDiscountTypesComponent, title: "TuitionDiscountTypes" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Accounting Entries Doc Type", component: AccountingEntriesDocTypeComponent, title: "AccountingEntriesDocTypes" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Job/:id", component: JobComponent, title: "Job" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Job Category", component: JobCategoriesComponent, title: "Job Categories" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Academic Degree", component: AcademicDegreeComponent, title: "Academic Degree" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Reasons For Leaving Work", component: ReasonsforleavingworkComponent, title: "Reasons for leaving work" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Department", component: DepartmentComponent, title: "Department" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Outcome", component: OutcomesComponent, title: "Outcome" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Income", component: IncomesComponent, title: "Income" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Save", component: SavesComponent, title: "Save" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Accounting Tree", component: AccountingTreeComponent, title: "Accounting Tree" , canActivate:[noNavigateWithoutLoginGuard ]} ,
            { path: "Bank", component: BankComponent, title: "Bank" , canActivate:[noNavigateWithoutLoginGuard ]} ,
        
        ]
    },
    { 
        path: "Parent", 
        component: MainLayoutComponent, 
        title: "Parent Home", 
        canActivate:[noNavigateWithoutLoginGuard,navigateIfParentGuard], 
        children: [
            { path: "", component: HomeParentComponent, title: "ParentHome" },
            { path: "Admission Test", component: AdmissionTestParentComponent, title: "Admission Test" },
            { path: "Test/:registerationFormParentID/:TestId", component: RegistraionTestComponent, title: "Test" },
            { path: "Registration Form", component: RegistrationFormComponent, title: "Registration Form"},  
            { path: "Interview Registration", component: InterviewRegistrationComponentParent, title: "Interview Registration"},  
        ]
    },
    { 
        path: "Student", 
        component: MainLayoutComponent, 
        title: "Student Home", 
        canActivate:[noNavigateWithoutLoginGuard,navigateIfStudentGuard], 
        children: [
            { path: "", component: StudentHomeComponent, title: "StudentHome" }
        ]
    },
    { 
        path: "Octa", 
        component: MainLayoutComponent, 
        title: "Octa Home",
        canActivate:[noNavigateWithoutOctaLoginGuard, navigateIfOctaGuard ,navigateIfHaveSettingPageGuard], 
        children: [ 
            { path: "Bus Details",component: BusDetailsComponent,title: "Bus", canActivate: [noNavigateWithoutLoginGuard]},    
            { path: "Bus Students/:domainName/:busId", component: BusStudentComponent, title: "Bus Students", canActivate:[noNavigateWithoutLoginGuard] },    
            { path: "Bus Types", component: BusTypesComponent, title: "Bus Type" , canActivate:[noNavigateWithoutLoginGuard]},    
            { path: "Bus Status", component: BusStatusComponent, title: "Bus Status" , canActivate:[noNavigateWithoutLoginGuard]},    
            { path: "Bus Districts", component: BusDistrictsComponent, title: "Bus Districts" , canActivate:[noNavigateWithoutLoginGuard]},    
            { path: "Bus Categories", component: BusCategoriesComponent, title: "Bus Category" , canActivate:[noNavigateWithoutLoginGuard]},    
            { path: "Bus Companies", component: BusCompaniesComponent, title: "Bus Company" , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Print Name Tag", component: BusPrintNameTagComponent, title: "Print Name Tag"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Domains", component: DomainsComponent, title: "Domains"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "School Types", component: SchoolTypeComponent, title: "School Types"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "School", component: SchoolComponentOcta, title: "Schools"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Account", component: AccountComponent, title: "Accounts"  , canActivate:[noNavigateWithoutLoginGuard]},  
        ]
    },

    { path: '**', redirectTo: '/' }
];

