import { inject } from '@angular/core';
import { CanActivateFn, ActivatedRouteSnapshot, Router } from '@angular/router';
import { GetDataFromLayoutService } from '../Services/Employee/get-data-from-layout.service';
import { map } from 'rxjs/operators';

export const navigateIfRoleHasPermissionGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const getDataService = inject(GetDataFromLayoutService);
  const routeNavigate = inject(Router);

  return getDataService.sharedData$.pipe(
    map(data => {
      if (!data) {
        return false; 
      }

      const path = route.url.map(segment => segment.path).join('/');

      let hasPermission = false;

      data.roles.forEach(role => {
        role.modules.forEach(mod => {
          mod.masterPermissions.forEach(master => {
            if (`${mod.moduleName.replace(" ", "-")}/${master.masterPermissionName.replace(" ", "-")}` === path) {
              hasPermission = true; 
            }
          });
        });
      });

      if(!hasPermission){
        routeNavigate.navigateByUrl("/Employee")
      }
      return hasPermission; 
    })
  );
};
