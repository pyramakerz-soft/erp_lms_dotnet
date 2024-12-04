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
import { PyramakerzLoginComponent } from './Pages/Login/pyramakerz-login/pyramakerz-login.component';
import { navigateIfPyramakerzGuard } from './Guards/navigate-if-pyramakerz.guard';
import { noNavigateWithoutPyramakerzLoginGuard } from './Guards/no-navigate-without-pyramakerz-login.guard';
import { SettingsComponent } from './Pages/Employee/settings/settings.component';
import { navigateIfHaveSettingPageGuard } from './Guards/navigate-if-have-This-page.guard';
import { BusTypesComponent } from './Pages/Employee/Bus/bus-types/bus-types.component';
import { BusStatusComponent } from './Pages/Employee/Bus/bus-status/bus-status.component';
import { BusRestrictsComponent } from './Pages/Employee/Bus/bus-restricts/bus-restricts.component';
import { BusCategoriesComponent } from './Pages/Employee/Bus/bus-categories/bus-categories.component';
import { BusCompaniesComponent } from './Pages/Employee/Bus/bus-companies/bus-companies.component';
import { BusDetailsComponent } from './Pages/Employee/Bus/bus-details/bus-details.component';
import { BusStudentComponent } from './Pages/Employee/Bus/bus-student/bus-student.component';


export const routes: Routes = [
    { path: "", component: LoginComponent, title: "Login", canActivate:[noNavigateToLoginIfLoginGuard] },
    { path: "Pyramakerz/login", component: PyramakerzLoginComponent, title: "login", canActivate:[noNavigateToLoginIfLoginGuard] },
    
    { 
        path: "Employee",     
        component: MainLayoutComponent, 
        title: "Employee Home", 
        canActivate:[noNavigateWithoutLoginGuard,navigateIfEmployeeGuard], 
        children: [
            { path: "", component: EmployeeHomeComponent, title: "EmployeeHome" },    
            { path: "Busses", component: BusDetailsComponent, title: "Bus"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Details",component: BusDetailsComponent,title: "Bus"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Student/:busId", component: BusStudentComponent, title: "Bus Student"},    
            { path: "Bus Type", component: BusTypesComponent, title: "Bus Type"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Status", component: BusStatusComponent, title: "Bus Status"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Restricts", component: BusRestrictsComponent, title: "Bus Restrict"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Categories", component: BusCategoriesComponent, title: "Bus Category"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},     
            { path: "Bus Companies", component: BusCompaniesComponent, title: "Bus Company"  , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  
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
        path: "Pyramakerz", 
        component: MainLayoutComponent, 
        title: "Pyramakerz Home",
        canActivate:[noNavigateWithoutPyramakerzLoginGuard, navigateIfPyramakerzGuard ,navigateIfHaveSettingPageGuard], 
        children: [
            { path: "Busses", component: BusDetailsComponent, title: "Bus" , canActivate:[navigateIfHaveSettingPageGuard]},    
            { path: "Bus Details",component: BusDetailsComponent,title: "Bus", canActivate: [noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Student/:busId", component: BusStudentComponent, title: "Bus Student", canActivate:[noNavigateWithoutLoginGuard] },    
            { path: "Bus Types", component: BusTypesComponent, title: "Bus Type" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Status", component: BusStatusComponent, title: "Bus Status" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Restricts", component: BusRestrictsComponent, title: "Bus Restrict" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Categories", component: BusCategoriesComponent, title: "Bus Category" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},    
            { path: "Bus Companies", component: BusCompaniesComponent, title: "Bus Company" , canActivate:[noNavigateWithoutLoginGuard, navigateIfHaveSettingPageGuard]},  
        ]
    },

    { path: '**', redirectTo: '/' }
];

