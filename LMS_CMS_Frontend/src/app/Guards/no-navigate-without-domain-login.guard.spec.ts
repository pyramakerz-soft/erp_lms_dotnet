import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { noNavigateWithoutDomainLoginGuard } from './no-navigate-without-domain-login.guard';

describe('noNavigateWithoutDomainLoginGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => noNavigateWithoutDomainLoginGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
