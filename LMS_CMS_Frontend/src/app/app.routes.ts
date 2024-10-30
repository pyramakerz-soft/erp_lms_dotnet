import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/Login/login/login.component';
import { HomeParentComponent } from './Pages/Parent/home-parent/home-parent.component';
import { StudentHomeComponent } from './Pages/Student/student-home/student-home.component';
import { EmployeeHomeComponent } from './Pages/Employee/employee-home/employee-home.component';

export const routes: Routes = [
    { path: "", component: LoginComponent, title: "Login" },
    { path: "ParentHome", component: HomeParentComponent, title: "Home" },
    { path: "StudentHome", component: StudentHomeComponent, title: "Home" },
    { path: "EmployeeHome", component: EmployeeHomeComponent, title: "Home" },
];
