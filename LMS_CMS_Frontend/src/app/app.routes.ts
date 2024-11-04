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
import { NavMenuComponent } from './Component/nav-menu/nav-menu.component';
import { MainLayoutComponent } from './Pages/Layouts/main-layout/main-layout.component';
import { UserManagementComponent } from './Pages/Employee/user-management/user-management.component';
import { navigateIfRoleHasPermissionGuard } from './Guards/navigate-if-role-has-permission.guard';
import { PyramakerzLoginComponent } from './Pages/Login/pyramakerz-login/pyramakerz-login.component';
import { DomainLoginComponent } from './Pages/Login/domain-login/domain-login.component';
import { DomainComponent } from './Pages/Pyramakerz/domain/domain.component';

export const routes: Routes = [
    { path: "", component: LoginComponent, title: "Login", canActivate:[noNavigateToLoginIfLoginGuard] },
    
    { 
        path: "Employee", 
        component: MainLayoutComponent, 
        title: "Employee Home", 
        canActivate:[noNavigateWithoutLoginGuard,navigateIfEmployeeGuard], 
        children: [
            { path: "", component: EmployeeHomeComponent, title: "EmployeeHome" },
            { path: "Admin-Module/User-Management", component: UserManagementComponent, title: "EmployeeUserManagement" ,canActivate:[navigateIfRoleHasPermissionGuard]}
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
    
    { path: "nav", component: NavMenuComponent, title: "Home" },
    { path: "Pyramakerz/login", component: PyramakerzLoginComponent, title: "login" },
    { path: "Domain/login", component: DomainLoginComponent, title: "login" },
    { path: "Domain/Home", component: DomainComponent, title: "Domain" },



    { path: '**', redirectTo: '/' }
];

