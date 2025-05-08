import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { EmployeeService } from '../Services/Employee/employee.service';
import { ParentService } from '../Services/parent.service';
import { LogOutService } from '../Services/shared/log-out.service';
import { catchError, map, of } from 'rxjs';
import { StudentService } from '../Services/student.service';

export const noNavigateWithoutLoginGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const accountService = inject(AccountService);
  const employeeService = inject(EmployeeService);
  const parentService = inject(ParentService);
  const logOutService = inject(LogOutService);
  const studentSer = inject(StudentService);


  const token = localStorage.getItem('current_token');

  if (!token) {
    router.navigateByUrl('');
    return false;
  }

  const userData = accountService.Get_Data_Form_Token();
  const userId = userData.id;

  switch (userData.type) {

    case 'octa':
      return true;

    case 'employee':
      return employeeService.Get_Employee_By_ID(userId).pipe(
        map(() => true), 
        catchError((error) => {
          console.error('Error fetching employee dataaaaaa:', error);
          logOutService.logOut();
          router.navigateByUrl('');
          return of(false); 
        })
      );

    case 'parent':
      return parentService.GetByID(userId).pipe(
        map(() => true), 
        catchError((error) => {
          console.error('Error fetching parent data:', error); 
          logOutService.logOut();
          router.navigateByUrl('');
          return of(false);
        })
      );

    case 'student':
      return studentSer.GetByID(userId).pipe(
        map(() => true), 
        catchError((error) => {
          console.error('Error fetching parent data:', error); 
          logOutService.logOut();
          router.navigateByUrl('');
          return of(false);
        })
      );

    default: 
      router.navigateByUrl('');
      return false; 
  }
};
