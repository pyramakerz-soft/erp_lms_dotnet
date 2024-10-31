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
import { SideMenuComponent } from './Component/side-menu/side-menu.component';

export const routes: Routes = [
    { path: "", component: LoginComponent, title: "Login", canActivate:[noNavigateToLoginIfLoginGuard] },
    { path: "ParentHome", component: HomeParentComponent, title: "Home", canActivate:[noNavigateWithoutLoginGuard,navigateIfParentGuard] },
    { path: "StudentHome", component: StudentHomeComponent, title: "Home", canActivate:[noNavigateWithoutLoginGuard,navigateIfStudentGuard] },
    { path: "EmployeeHome", component: EmployeeHomeComponent, title: "Home", canActivate:[noNavigateWithoutLoginGuard,navigateIfEmployeeGuard] },
    { path: "side", component: SideMenuComponent, title: "Home" },
    {path: '**', redirectTo: '/'}
];
