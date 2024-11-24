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
            { path: "Settings", component: SettingsComponent, title: "EmployeeHome" , canActivate:[navigateIfHaveSettingPageGuard]},    
            { path: "Bus", component: BusDetailsComponent, title: "Bus" , canActivate:[navigateIfHaveSettingPageGuard]},    
            { path: "Bus Type", component: BusTypesComponent, title: "BusType" , canActivate:[navigateIfHaveSettingPageGuard]},    
            { path: "Bus Status", component: BusStatusComponent, title: "BusType" , canActivate:[navigateIfHaveSettingPageGuard]},    
            { path: "Bus Restrict", component: BusRestrictsComponent, title: "BusType" , canActivate:[navigateIfHaveSettingPageGuard]},    
            { path: "Bus Category", component: BusCategoriesComponent, title: "BusType" , canActivate:[navigateIfHaveSettingPageGuard]},    
            { path: "Bus Comapny", component: BusCompaniesComponent, title: "BusType" , canActivate:[navigateIfHaveSettingPageGuard]},    
        
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
        canActivate:[noNavigateWithoutPyramakerzLoginGuard, navigateIfPyramakerzGuard], 
        children: [
           
        ]
    },

    { path: '**', redirectTo: '/' }
];

