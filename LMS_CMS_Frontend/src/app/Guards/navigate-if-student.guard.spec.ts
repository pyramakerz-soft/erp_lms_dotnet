import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { navigateIfStudentGuard } from './navigate-if-student.guard';

describe('navigateIfStudentGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => navigateIfStudentGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
