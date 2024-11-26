import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { TokenData } from '../Models/token-data';
import { EmployeeService } from '../Services/Employee/employee.service';
import { LogOutService } from '../Services/shared/log-out.service';
import { NewTokenService } from '../Services/shared/new-token.service';
import { catchError, map, of } from 'rxjs';

// export const noNavigateWithoutLoginGuard: CanActivateFn = (route, state) => {
//   let token = localStorage.getItem("current_token")
//   const router = inject(Router);
//   const account = inject(AccountService);
//   const LogOutServ = inject(LogOutService);
//   const employeeSer = inject(EmployeeService);
//   const communicationService = inject(NewTokenService);
//   let User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
//   let UserID = 0
//   console.log("h3yt2")

//   if(token == null){
//     router.navigateByUrl('');
//     return false;
//   } 
//   // return true;

//   else{
//     console.log("h3yt")
//     User_Data_After_Login = account.Get_Data_Form_Token();
//     UserID=User_Data_After_Login.id;
//     employeeSer.GetByID(UserID).subscribe(
//       (data) => {
//         return true;
//       },
//       (error) => {
//         console.error('Error:', error);
//         LogOutServ.logOut();
//         communicationService.sendAction(true);
//         router.navigateByUrl('');
//       }
//     );
//     return true;
//   }
// };

export const noNavigateWithoutLoginGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const account = inject(AccountService);
  const logOutService = inject(LogOutService);
  const employeeService = inject(EmployeeService);

  let token = localStorage.getItem("current_token");
    console.log("h3yt")

  if (!token) {
    router.navigateByUrl('');
    return false;
  }

  const userData = account.Get_Data_Form_Token();
  const userId = userData.id;

  return employeeService.GetByID(userId).pipe(
    map((data) => {
      return true; // Allow navigation if user data is valid
    }),
    catchError((error) => {
      console.error('Error fetching user:', error);
      logOutService.logOut();
      router.navigateByUrl('');
      return of(false); // Prevent navigation if an error occurs
    })
  );
};
