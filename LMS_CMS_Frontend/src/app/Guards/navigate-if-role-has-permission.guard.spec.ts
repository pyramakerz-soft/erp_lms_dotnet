import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { navigateIfRoleHasPermissionGuard } from './navigate-if-role-has-permission.guard';

describe('navigateIfRoleHasPermissionGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => navigateIfRoleHasPermissionGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
