import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { RoleDetailsService } from '../Services/Employee/role-details.service';
import { MenuService } from '../Services/shared/menu.service';
import { map, switchMap } from 'rxjs';

export const navigateIfHaveSettingPageGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const accountService = inject(AccountService);
  const roleDetailsService = inject(RoleDetailsService);
  const menuService = inject(MenuService);

  const token = localStorage.getItem('current_token');
  
  if (!token) {
    router.navigateByUrl('/login');
    return false;
  }

  const userData = accountService.Get_Data_Form_Token();

  if (userData.type==="octa") {
    return true;  
  }

  if (!userData || !userData.role) {
    router.navigateByUrl('');
    return false;
  }

  return roleDetailsService.Get_Pages_With_RoleID(userData.role).pipe(
    map((accessiblePages: any) => {
      return Array.isArray(accessiblePages) ? accessiblePages : [];
    }),
    switchMap((accessiblePages: any[]) => {
      console.log(accessiblePages)
      const currentPage: string = route.routeConfig?.path || '';
      const baseRoute: string = currentPage.split('/')[0];
      const page = menuService.findByPageName(baseRoute, accessiblePages);
      if (page) {
        return [true];  
      }
      router.navigateByUrl('');
      return [false];  
    })
  );
};
