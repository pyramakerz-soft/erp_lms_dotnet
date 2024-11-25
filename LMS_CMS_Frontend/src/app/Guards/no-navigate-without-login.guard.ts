import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { TokenData } from '../Models/token-data';
import { EmployeeService } from '../Services/Employee/employee.service';

export const noNavigateWithoutLoginGuard: CanActivateFn = (route, state) => {
  let token = localStorage.getItem("current_token")
  const router = inject(Router);
  const account = inject(AccountService);
  const employeeSer = inject(EmployeeService);
  let User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  let UserID = 0

  if(token == null){
    router.navigateByUrl('');
    return false;
  } 
  return true;

  // else{
  //   User_Data_After_Login = account.Get_Data_Form_Token();
  //   UserID=User_Data_After_Login.id;
  //   console.log(UserID)
  //   employeeSer.GetByID(UserID).subscribe(
  //     (data) => {
  //       return true;
  //     },
  //     (error) => {
  //       console.error('Error:', error);
  //       if (error.status === 404) {
  //         router.navigateByUrl('');
  //         return false
  //       }else{
  //         return true
  //       }
  //     }
  //   );
  //   return true;
  // }
};
