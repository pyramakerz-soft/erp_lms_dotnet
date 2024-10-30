import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { noNavigateWithoutLoginGuard } from './no-navigate-without-login.guard';

describe('noNavigateWithoutLoginGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => noNavigateWithoutLoginGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
