import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { OctaService } from '../Services/Octa/octa.service';
import { LogOutService } from '../Services/shared/log-out.service';
import { catchError, map, of } from 'rxjs';

export const noNavigateWithoutOctaLoginGuard: CanActivateFn = (route, state) => {
  let token = localStorage.getItem("current_token")
  const router = inject(Router);
  const accountService = inject(AccountService);
  const octaService = inject(OctaService);
  const logOutService = inject(LogOutService);


  if(token == null){
    router.navigateByUrl('Octa/login');
    return false;
  }
  
  const userData = accountService.Get_Data_Form_Token();
  const userId = userData.id;

  switch (userData.type) {

    case 'octa':
      return octaService.GetByID(userId).pipe(
        map(() => true), 
        catchError((error: any) => {
          console.error('Error fetching employee data:', error);
          logOutService.logOut();
          router.navigateByUrl('');
          return of(false); 
        })
      );

    default:
      logOutService.logOut();
      router.navigateByUrl('');
      return false; 
  }
};
