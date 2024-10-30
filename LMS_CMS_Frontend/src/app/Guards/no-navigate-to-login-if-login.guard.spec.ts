import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { noNavigateToLoginIfLoginGuard } from './no-navigate-to-login-if-login.guard';

describe('noNavigateToLoginIfLoginGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => noNavigateToLoginIfLoginGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
