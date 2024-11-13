import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const noNavigateWithoutPyramakerzLoginGuard: CanActivateFn = (route, state) => {
  let token = localStorage.getItem("current_token")
  const router = inject(Router);

  if(token == null){
    router.navigateByUrl('Pyramakerz/login');
    return false;
  }
  
  return true;
};
