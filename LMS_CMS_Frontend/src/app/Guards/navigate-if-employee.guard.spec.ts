import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { navigateIfEmployeeGuard } from './navigate-if-employee.guard';

describe('navigateIfEmployeeGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => navigateIfEmployeeGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
