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
import { SchoolComponent } from './Pages/Octa/school/school.component';
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

export const routes: Routes = [
    { path: "", component: LoginComponent, title: "Login", canActivate:[noNavigateToLoginIfLoginGuard] },
    { path: "Octa/login", component: OctaLoginComponent, title: "login", canActivate:[noNavigateToLoginIfLoginGuard] },

    
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
            { path: "Print Name Tag", component: BusPrintNameTagComponent, title: "Print Name Tag"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Role", component: RoleComponent, title: "Role"  , canActivate:[noNavigateWithoutLoginGuard,navigateIfHaveSettingPageGuard]}, 
            { path: "Role Create", component: RoleAddEditComponent, title: "Role Create"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Role Edit/:id", component: RoleAddEditComponent, title: "Role Create"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Subject Categories", component: SubjectCategoryComponent, title: "Subject Categories"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Subject", component: SubjectComponent, title: "Subjects"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Subject/:domainName/:SubId", component: SubjectViewComponent, title: "Subject", canActivate:[noNavigateWithoutLoginGuard]}, 
            { path: "Employee", component: EmployeeComponent, title: "Employee", canActivate:[noNavigateWithoutLoginGuard ,navigateIfHaveSettingPageGuard]}, 
            { path: "Employee Create", component: EmployeeAddEditComponent, title: "Employee Create", canActivate:[noNavigateWithoutLoginGuard ]}, 
            { path: "Employee Edit/:id", component: EmployeeAddEditComponent, title: "Employee Edit", canActivate:[noNavigateWithoutLoginGuard ]}, 
            { path: "Employee Details/:id", component: EmployeeViewComponent, title: "Employee Details", canActivate:[noNavigateWithoutLoginGuard ]}, 
            { path: "Building", component: BuildingComponent, title: "Building", canActivate:[noNavigateWithoutLoginGuard ]}, 
            { path: "Floor/:domainName/:Id", component: FloorComponent, title: "Floor", canActivate:[noNavigateWithoutLoginGuard ]}, 
            { path: "Classroom", component: ClassroomComponent, title: "Classroom", canActivate:[noNavigateWithoutLoginGuard ]}, 

        ]
    },
    { 
        path: "Parent", 
        component: MainLayoutComponent, 
        title: "Parent Home", 
        canActivate:[noNavigateWithoutLoginGuard,navigateIfParentGuard], 
        children: [
            { path: "", component: HomeParentComponent, title: "ParentHome" }
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
            { path: "Bus Details",component: BusDetailsComponent,title: "Bus", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Students/:domainName/:busId", component: BusStudentComponent, title: "Bus Students", canActivate:[noNavigateWithoutLoginGuard] },    
            { path: "Bus Types", component: BusTypesComponent, title: "Bus Type" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Status", component: BusStatusComponent, title: "Bus Status" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Districts", component: BusDistrictsComponent, title: "Bus Districts" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Categories", component: BusCategoriesComponent, title: "Bus Category" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Companies", component: BusCompaniesComponent, title: "Bus Company" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  
            { path: "Print Name Tag", component: BusPrintNameTagComponent, title: "Print Name Tag"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Domains", component: DomainsComponent, title: "Domains"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "School Types", component: SchoolTypeComponent, title: "School Types"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "School", component: SchoolComponent, title: "Schools"  , canActivate:[noNavigateWithoutLoginGuard]},  
            { path: "Account", component: AccountComponent, title: "Accounts"  , canActivate:[noNavigateWithoutLoginGuard]},  
        ]
    },

    { path: '**', redirectTo: '/' }
];

