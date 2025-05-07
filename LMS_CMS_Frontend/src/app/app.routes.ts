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
import { AccountingEmployee } from './Models/Accounting/accounting-employee';
import { AccountingEmployeeComponent } from './Pages/Employee/Accounting/accounting-employee/accounting-employee.component';
import { AccountingEmployeeEditComponent } from './Pages/Employee/Accounting/accounting-employee-edit/accounting-employee-edit.component';
import { AccountingStudentComponent } from './Pages/Employee/Accounting/accounting-student/accounting-student.component';
import { AccountingStudentEditComponent } from './Pages/Employee/Accounting/accounting-student-edit/accounting-student-edit.component';
import { AddChildComponent } from './Pages/Employee/Accounting/add-child/add-child.component';
import { FeesActivationComponent } from './Pages/Employee/Accounting/fees-activation/fees-activation.component';
import { ReceivableComponent } from './Pages/Employee/Accounting/receivable/receivable.component';
import { ReceivableDetailsComponent } from './Pages/Employee/Accounting/receivable-details/receivable-details.component';
import { PayableComponent } from './Pages/Employee/Accounting/payable/payable.component';
import { PayableDetailsComponent } from './Pages/Employee/Accounting/payable-details/payable-details.component';
import { AccountingEntriesComponent } from './Pages/Employee/Accounting/accounting-entries/accounting-entries.component';
import { AccountingEntriesDetailsComponent } from './Pages/Employee/Accounting/accounting-entries-details/accounting-entries-details.component';
import { InstallmentDeductionDetailComponent } from './Pages/Employee/Accounting/installment-deduction-detail/installment-deduction-detail.component';
import { InstallmentDeductionMasterComponent } from './Pages/Employee/Accounting/installment-deduction-master/installment-deduction-master.component';
import { PayableDocTypeComponent } from './Pages/Employee/Accounting/payable-doc-type/payable-doc-type.component';
import { ReceivableDocTypeComponent } from './Pages/Employee/Accounting/receivable-doc-type/receivable-doc-type.component';
import { CategoriesComponent } from './Pages/Employee/Inventory/categories/categories.component';
import { SubCategoryComponent } from './Pages/Employee/Inventory/sub-category/sub-category.component';
import { ShopItemsComponent } from './Pages/Employee/Inventory/shop-items/shop-items.component';
import { ShopItemsAddEditComponent } from './Pages/Employee/Inventory/shop-items-add-edit/shop-items-add-edit.component';
import { StoresComponent } from './Pages/Employee/Inventory/stores/stores.component';
import { InventoryMasterComponent } from './Pages/Employee/Inventory/inventory-master/inventory-master.component';
import { InventoryDetailsComponent } from './Pages/Employee/Inventory/inventory-details/inventory-details.component';
import { CreateHygieneFormComponent } from './Pages/Employee/Clinic/hygiene_form/create-hygiene-form/create-hygiene-form.component';
import { HygieneFormComponent } from './Pages/Employee/Clinic/hygiene_form/hygiene-form/hygiene-form.component';
import { DrugsComponent } from './Pages/Employee/Clinic/drugs/drugs.component';
import { DiagnosisComponent } from './Pages/Employee/Clinic/diagnosis/diagnosis.component';
import { HygieneTypesComponent } from './Pages/Employee/Clinic/hygiene-types/hygiene-types.component';
import { FollowUpComponent } from './Pages/Employee/Clinic/follow-up/follow-up.component';
import { MedicalHistoryComponent } from './Pages/Employee/Clinic/medical-history/medical-history-table/medical-history.component';
import { ShopComponent } from './Pages/Student/Ecommerce/shop/shop.component';
import { ShopItemComponent } from './Pages/Student/Ecommerce/shop-item/shop-item.component';
import { CartComponent } from './Pages/Student/Ecommerce/cart/cart.component';
import { OrderComponent } from './Pages/Student/Ecommerce/order/order.component';
import { DosesComponent } from './Pages/Employee/Clinic/doses/doses.component';
import { MedicalReportComponent } from './Pages/Employee/Clinic/medical-report/medical-report/medical-report.component';
import { OrderItemsComponent } from './Pages/Student/Ecommerce/order-items/order-items.component';
import { OrderHistoryComponent } from './Pages/Employee/E-Commerce/order-history/order-history.component';
import { StockingComponent } from './Pages/Employee/Inventory/stocking/stocking.component';
import { StockingDetailsComponent } from './Pages/Employee/Inventory/stocking-details/stocking-details.component';
import { ViewHygieneFormComponent } from './Pages/Employee/Clinic/hygiene_form/veiw-hygiene-form/veiw-hygiene-form.component';
import { StudentsNamesInClassComponent } from './Pages/Employee/Registration/Reports/students-names-in-class/students-names-in-class.component';
import { StudentInformationComponent } from './Pages/Employee/Registration/Reports/student-information/student-information.component';
import { MedicalHistoryByDoctorComponent } from './Pages/Employee/Clinic/medical-report/medical-history-by-doctor/medical-history-by-doctor.component';
import { MedicalHistoryByParentComponent } from './Pages/Employee/Clinic/medical-report/medical-history-by-parent/medical-history-by-parent.component';
import { ProofRegistrationAndSuccessFormReportComponent } from './Pages/Employee/Registration/Reports/proof-registration-and-success-form-report/proof-registration-and-success-form-report.component';
import { ProofRegistrationReportComponent } from './Pages/Employee/Registration/Reports/proof-registration-report/proof-registration-report.component';
import { StudentsInformationFormReportComponent } from './Pages/Employee/Registration/Reports/students-information-form-report/students-information-form-report.component';
import { PdfPrintComponent } from './Component/pdf-print/pdf-print.component';
import { AcademicSequentialReportComponent } from './Pages/Employee/Registration/Reports/academic-sequential-report/academic-sequential-report.component';
import { TransferedFromKindergartenReportComponent } from './Pages/Employee/Registration/Reports/transfered-from-kindergarten-report/transfered-from-kindergarten-report.component';
import { TemplateComponent } from './Pages/Employee/LMS/template/template.component';
import { InventoryTransactionReportComponent } from './Pages/Employee/Inventory/Report/inventory-invoice-report/invoice-report-master/invoice-report-master.component';
import { InvoiceReportMasterDetailedComponent } from './Pages/Employee/Inventory/Report/inventory-invoice-report/invoice-report-master-detailed/invoice-report-master-detailed.component';
import { EvaluationComponent } from './Pages/Employee/LMS/evaluation/evaluation.component';
import { EvaluationTemplateGroupComponent } from './Pages/Employee/LMS/evaluation-template-group/evaluation-template-group.component';
import { EvaluationTemplateGroupQuestionComponent } from './Pages/Employee/LMS/evaluation-template-group-question/evaluation-template-group-question.component';
import { EvaluationFeedbackComponent } from './Pages/Employee/LMS/evaluation-feedback/evaluation-feedback.component';
import { EvaluationEmployeeAnswerComponent } from './Pages/Employee/LMS/evaluation-employee-answer/evaluation-employee-answer.component';
import { BookCorrectionComponent } from './Pages/Employee/LMS/book-correction/book-correction.component';
import { MedalComponent } from './Pages/Employee/LMS/medal/medal.component';
import { LessonActivityTypeComponent } from './Pages/Employee/LMS/lesson-activity-type/lesson-activity-type.component';
import { LessonResourcesTypeComponent } from './Pages/Employee/LMS/lesson-resources-type/lesson-resources-type.component';
import { StudentMedalComponent } from './Pages/Employee/LMS/student-medal/student-medal.component';
import { LessonComponent } from './Pages/Employee/LMS/lesson/lesson.component';
import { PerformanceTypeComponent } from './Pages/Employee/LMS/performance-type/performance-type.component';
import { DailyPerformanceComponent } from './Pages/Employee/LMS/daily-performance/daily-performance.component';
import { LessonResourceComponent } from './Pages/Employee/LMS/lesson-resource/lesson-resource.component';
import { LessonActivityComponent } from './Pages/Employee/LMS/lesson-activity/lesson-activity.component';
import { LessonLiveComponent } from './Pages/Employee/LMS/lesson-live/lesson-live.component';
import { StudentLessonLiveComponent } from './Pages/Student/LMS/student-lesson-live/student-lesson-live.component';

export const routes: Routes = [
    { path: "", component: LoginComponent, title: "Login", canActivate:[noNavigateToLoginIfLoginGuard] },
    { path: "Octa/login", component: OctaLoginComponent, title: "login", canActivate:[noNavigateToLoginIfLoginGuard] },
    { path: "SignUp", component: SignUpComponent, title: "SignUp", canActivate:[noNavigateToLoginIfLoginGuard] },

    
    { 
        path: "Employee",     
        component: MainLayoutComponent, 
        title: "Employee Home", 
        canActivate:[navigateIfEmployeeGuard, noNavigateWithoutLoginGuard], 
        children: [
            { path: "Hygiene Types", component: HygieneTypesComponent, title: "Hygiene Types", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard] },
            { path: "Diagnosis", component: DiagnosisComponent, title: "Diagnosis" },
            { path: "Drugs", component: DrugsComponent, title: "Drugs" },
            { path: "Hygiene Form Medical Report", component: HygieneFormComponent, title: "Hygiene Form" },
            { path: "Create Hygiene Form", component: CreateHygieneFormComponent, title: "Create Hygiene Form" },
            { path: 'view hygiene form/:id', component: ViewHygieneFormComponent },
            { path: 'mh by parent/:id', component: MedicalHistoryByParentComponent },
            {path:  'mh by doctor/:id',  component: MedicalHistoryByDoctorComponent},
            { path: "Follow Up", component: FollowUpComponent, title: "Follow Up" },
            { path: "Medical History", component: MedicalHistoryComponent, title: "Medical History" },
            { path: "Medical Report", component: MedicalReportComponent, title: "Medical Report" },
            { path: "Doses", component: DosesComponent, title: "Doses" },
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
            { path: "Role Create", component: RoleAddEditComponent, title: "Role Create"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  //
            { path: "Role Edit/:id", component: RoleAddEditComponent, title: "Role Edit"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  //
            { path: "Subject Categories", component: SubjectCategoryComponent, title: "Subject Categories"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  
            { path: "Subject", component: SubjectComponent, title: "Subjects"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  
            { path: "Subject/:domainName/:SubId", component: SubjectViewComponent, title: "Subject", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Employee", component: EmployeeComponent, title: "Employee", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Employee Create", component: EmployeeAddEditComponent, title: "Employee Create", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]}, //
            { path: "Employee Edit/:id", component: EmployeeAddEditComponent, title: "Employee Edit", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]}, //
            { path: "Employee Details/:id", component: EmployeeViewComponent, title: "Employee Details", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]}, //
            { path: "Building", component: BuildingComponent, title: "Building", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Floor/:domainName/:Id", component: FloorComponent, title: "Floor", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Classroom", component: ClassroomComponent, title: "Classroom", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Violation Types", component: ViolationTypesComponent, title: "Violation Types", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Section", component: SectionComponent, title: "Section", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Grade/:domainName/:Id", component: GradeComponent, title: "Grade", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Academic Years", component: AcademicYearComponent, title: "Academic Year", canActivate:[noNavigateWithoutLoginGuard , navigateIfHaveSettingPageGuard]}, 
            { path: "Semester/:domainName/:Id", component: SemesterComponent, title: "Semester", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "SemesterView/:domainName/:Id", component: SemesterViewComponent, title: "Semester", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]}, //
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
            { path: "Supplier", component: SuppliersComponent, title: "Suppliers" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Debit", component: DebitsComponent, title: "Debits" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Credit", component: CreditsComponent, title: "Credits" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Asset", component: AssetsComponent, title: "Assets" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Tuition Fees Type", component: TuitionFeesTypesComponent, title: "Tuition Fees Types" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Tuition Discount Type", component: TuitionDiscountTypesComponent, title: "TuitionDiscountTypes" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Accounting Entries Doc Type", component: AccountingEntriesDocTypeComponent, title: "AccountingEntriesDocTypes" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Job/:id", component: JobComponent, title: "Job" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Job Category", component: JobCategoriesComponent, title: "Job Categories" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Academic Degree", component: AcademicDegreeComponent, title: "Academic Degree" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Reasons For Leaving Work", component: ReasonsforleavingworkComponent, title: "Reasons for leaving work" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Department", component: DepartmentComponent, title: "Department" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Outcome", component: OutcomesComponent, title: "Outcome" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Income", component: IncomesComponent, title: "Income" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Safe", component: SavesComponent, title: "Save" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Accounting Tree", component: AccountingTreeComponent, title: "Accounting Tree" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Bank", component: BankComponent, title: "Bank" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Employee Accounting", component: AccountingEmployeeComponent, title: "Employee Accounting" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Employee Edit Accounting/:id", component: AccountingEmployeeEditComponent, title: "Employee Edit Accounting" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Student Accounting", component: AccountingStudentComponent, title: "Student Accounting" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Student Edit Accounting/:id", component: AccountingStudentEditComponent, title: "Student Edit Accounting" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Add Children", component: AddChildComponent, title: "Add Children" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Fees Activation", component: FeesActivationComponent, title: "Fees Activation" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Receivable", component: ReceivableComponent, title: "Receivable" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Receivable Details", component: ReceivableDetailsComponent, title: "Receivable Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Receivable Details/:id", component: ReceivableDetailsComponent, title: "Edit Receivable Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Receivable Details/View/:id", component: ReceivableDetailsComponent, title: "View Receivable Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Payable", component: PayableComponent, title: "Payable" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Payable Details", component: PayableDetailsComponent, title: "Payable Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Payable Details/:id", component: PayableDetailsComponent, title: "Edit Payable Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Payable Details/View/:id", component: PayableDetailsComponent, title: "View Payable Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Accounting Entries", component: AccountingEntriesComponent, title: "AccountingEntries" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]} ,
            { path: "Accounting Entries Details", component: AccountingEntriesDetailsComponent, title: "AccountingEntries Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Accounting Entries Details/:id", component: AccountingEntriesDetailsComponent, title: "Edit AccountingEntries Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Accounting Entries Details/View/:id", component: AccountingEntriesDetailsComponent, title: "View AccountingEntries Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Installment Deduction", component: InstallmentDeductionMasterComponent, title: "Installment Deduction" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Installment Deduction Details/View/:id", component: InstallmentDeductionDetailComponent, title: "View Installment Deduction Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Installment Deduction Details/Edit/:id", component: InstallmentDeductionDetailComponent, title: "View Installment Deduction Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Installment Deduction Details", component: InstallmentDeductionDetailComponent, title: "View Installment Deduction Details" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Payable Doc Type", component: PayableDocTypeComponent, title: "Payable Doc Type" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Receivable Doc Type", component: ReceivableDocTypeComponent, title: "Receivable Doc Type" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Inventory Categories", component: CategoriesComponent, title: "Inventory categories" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Inventory Sub Categories/:id", component: SubCategoryComponent, title: "Sub_categories" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Items", component: ShopItemsComponent, title: "Shop Items" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Shop Item/Create", component: ShopItemsAddEditComponent, title: "Create Shop Items" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Shop Item/:id", component: ShopItemsAddEditComponent, title: "Edit Shop Items" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Stores", component: StoresComponent, title: "Store" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Sales", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 11 } },
            { path: "Sales Item/:FlagId", component: InventoryDetailsComponent, title: "Sales Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Sales Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Sales Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Sales Returns", component: InventoryMasterComponent, title: "Sales Returns", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 12 } },
            { path: "Sales Returns Item/:FlagId", component: InventoryDetailsComponent, title: "Sales Returns Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Sales Returns Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Sales Returns Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Purchase Returns", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 10 } },
            { path: "Purchase Returns Item/:FlagId", component: InventoryDetailsComponent, title: "Purchases Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Purchase Returns Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Purchases Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Purchases", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 9 } },
            { path: "Purchases Item/:FlagId", component: InventoryDetailsComponent, title: "Purchases Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Purchases Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Purchases Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Opening Balances", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 1 } },
            { path: "Opening Balances Item/:FlagId", component: InventoryDetailsComponent, title: "Opening Balances Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Opening Balances Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Opening Balances Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Addition", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 2 } },
            { path: "Addition Item/:FlagId", component: InventoryDetailsComponent, title: "Addition Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Addition Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Addition Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Addition Adjustment", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 3 } },
            { path: "Addition Adjustment Item/:FlagId", component: InventoryDetailsComponent, title: "Addition Adjustment Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Addition Adjustment Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Addition Adjustment Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Disbursement", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 4 } },
            { path: "Disbursement Item/:FlagId", component: InventoryDetailsComponent, title: "Disbursement Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Disbursement Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Disbursement Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Disbursement Adjustment", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 5 } },
            { path: "Disbursement Adjustment Item/:FlagId", component: InventoryDetailsComponent, title: "Disbursement Adjustment Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Disbursement Adjustment Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Disbursement Adjustment Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Gifts", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 6 } },
            { path: "Gifts Item/:FlagId", component: InventoryDetailsComponent, title: "Gifts Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Gifts Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Gifts Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Purchase Order", component: InventoryMasterComponent, title: "Purchase Order", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 13 } },
            { path: "Purchase Order Item/:FlagId", component: InventoryDetailsComponent, title: "Purchase Order Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Purchase Order Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Purchase Order Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Transfer to Store", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 8 } },
            { path: "Transfer to Store Item/:FlagId", component: InventoryDetailsComponent, title: "Transfer to Store Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Transfer to Store Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Transfer to Store Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Damaged", component: InventoryMasterComponent, title: "Sales", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard], data: { id: 7 } },
            { path: "Damaged Item/:FlagId", component: InventoryDetailsComponent, title: "Damaged Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "Damaged Item/Edit/:FlagId/:id", component: InventoryDetailsComponent, title: "Damaged Item" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ]},
            { path: "The Shop", component: ShopComponent, title: "Shop", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ] },
            { path: "ShopItem/:id", component: ShopItemComponent, title: "Shop Item", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ] },
            { path: "Cart", component: CartComponent, title: "Cart", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ] },
            { path: "Order", component: OrderComponent, title: "Order", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ] },
            { path: "Order History", component: OrderHistoryComponent, title: "Order History", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ] },
            { path: "Order/:id", component: OrderItemsComponent, title: "Order Items", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ] },
            { path: "Stocking", component: StockingComponent, title: "Stocking", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ] },
            { path: "Stocking Item", component: StockingDetailsComponent, title: "Stocking Item", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ] },
            { path: "Stocking Item/Edit/:id", component: StockingDetailsComponent, title: "Stocking Item", canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard ] },
            { path: "StudentsNamesInClass", component: StudentsNamesInClassComponent, title: "Students' Names In Class", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "StudentInformation", component: StudentInformationComponent, title: "StudentInformation", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "ProofRegistrationAndSuccessForm", component: ProofRegistrationAndSuccessFormReportComponent, title: "ProofRegistrationAndSuccessForm", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "ProofRegistration", component: ProofRegistrationReportComponent, title: "ProofRegistration", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "StudentsInformationFormReport", component: StudentsInformationFormReportComponent, title: "StudentsInformationFormReport", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "AcademicSequentialReport", component: AcademicSequentialReportComponent, title: "AcademicSequentialReport", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "TransferedFromKindergartenReport", component: TransferedFromKindergartenReportComponent, title: "TransferedFromKindergartenReport", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Template", component: TemplateComponent, title: "Template", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: 'Inventory-Transaction-Report', component: InventoryTransactionReportComponent, title: 'Inventory Transaction Report',canActivate: [noNavigateWithoutLoginGuard],data: { reportType: 'inventory' }},
            { path: 'Sales-Transaction-Report', component: InventoryTransactionReportComponent, title: 'Sales Transaction Report',canActivate: [noNavigateWithoutLoginGuard],data: { reportType: 'sales' }},
            { path: 'Purchase-Transaction-Report', component: InventoryTransactionReportComponent, title: 'Purchase Transaction Report',canActivate: [noNavigateWithoutLoginGuard],data: { reportType: 'purchase' }},
            { path: 'Inventory-Transaction-Report-Detailed', component: InvoiceReportMasterDetailedComponent, title: 'Inventory Transaction Report Detailed',canActivate: [noNavigateWithoutLoginGuard],data: { reportType: 'inventory' }},
            { path: 'Sales-Transaction-Detailed', component: InvoiceReportMasterDetailedComponent, title: 'Sales Transaction Report Detailed',canActivate: [noNavigateWithoutLoginGuard],data: { reportType: 'sales' }},
            { path: 'Purchase-Transaction-Report-Detailed', component: InvoiceReportMasterDetailedComponent, title: 'Purchase Transaction Report Detailed',canActivate: [noNavigateWithoutLoginGuard],data: { reportType: 'purchase' }},
            { path: "Book Correction", component: BookCorrectionComponent, title: "BookCorrectionComponent", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Evaluation", component: EvaluationComponent, title: "Evaluation", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "EvaluationTemplateGroup/:id", component: EvaluationTemplateGroupComponent, title: "EvaluationTemplateGroup", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "EvaluationTemplateGroupQuestion/:id", component: EvaluationTemplateGroupQuestionComponent, title: "EvaluationTemplateGroup", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Received Evaluations", component: EvaluationFeedbackComponent, title: "Received Evaluations", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Created Evaluations", component: EvaluationFeedbackComponent, title: "Created Evaluations", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Received Evaluations/:id", component: EvaluationEmployeeAnswerComponent, title: "Received Evaluations", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Created Evaluations/:id", component: EvaluationEmployeeAnswerComponent, title: "Created Evaluations", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Medal", component: MedalComponent, title: "Medal", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Lesson Activity Type", component: LessonActivityTypeComponent, title: "Lesson Activity Type", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Lesson Resource Type", component: LessonResourcesTypeComponent, title: "Lesson Resource Type", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Student Medal", component: StudentMedalComponent, title: "Student Medal", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Lesson", component: LessonComponent, title: "Lesson", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Daily Performance", component: DailyPerformanceComponent, title: "Daily Performance", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Student Medal", component: StudentMedalComponent, title: "Student Medal", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Performance Type", component: PerformanceTypeComponent, title: "Performance Type", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Lesson Activity/:id", component: LessonActivityComponent, title: "Lesson Activity", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Lesson Resource/:id", component: LessonResourceComponent, title: "Lesson Resource", canActivate:[noNavigateWithoutLoginGuard ] },
            { path: "Lesson Live", component: LessonLiveComponent, title: "Lesson Live", canActivate:[noNavigateWithoutLoginGuard ] },
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
            { path: "", component: StudentHomeComponent, title: "StudentHome" },
            { path: "Ecommerce/Shop", component: ShopComponent, title: "Shop" },
            { path: "Ecommerce/ShopItem/:id", component: ShopItemComponent, title: "Shop Item" },
            { path: "Ecommerce/Cart", component: CartComponent, title: "Cart" },
            { path: "Ecommerce/Order", component: OrderComponent, title: "Order" },
            { path: "Ecommerce/Order/:id", component: OrderItemsComponent, title: "Order Items" },
            { path: "Lesson Live", component: StudentLessonLiveComponent, title: "Lesson Live" }

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