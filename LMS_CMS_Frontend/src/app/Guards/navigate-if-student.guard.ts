import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { inject } from '@angular/core';
import { TokenData } from '../Models/token-data';

export const navigateIfStudentGuard: CanActivateFn = (route, state) => {
  let token = localStorage.getItem("current_token")
  const router = inject(Router);
  const account = inject(AccountService);
  let User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  User_Data_After_Login = account.Get_Data_Form_Token()
  let User_Type = User_Data_After_Login.type

  if(User_Type!="student"){
    if(User_Type=="parent"){
      router.navigateByUrl('Parent');
      return false;
    }else if(User_Type=="employee"){
      router.navigateByUrl('Employee');
      return false;
    }else if(User_Type=="octa"){
      router.navigateByUrl('octa');
      return false;
    }
  }
  return true;
};
