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


export const routes: Routes = [
    { path: "", component: LoginComponent, title: "Login", canActivate:[noNavigateToLoginIfLoginGuard] },
    { path: "Pyramakerz/login", component: PyramakerzLoginComponent, title: "login", canActivate:[noNavigateToLoginIfLoginGuard] },

    
    { 
        path: "Employee",     
        component: MainLayoutComponent, 
        title: "Employee Home", 
        canActivate:[noNavigateWithoutLoginGuard,navigateIfEmployeeGuard], 
        children: [
            { path: "", component: EmployeeHomeComponent, title: "EmployeeHome" },        ]
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

