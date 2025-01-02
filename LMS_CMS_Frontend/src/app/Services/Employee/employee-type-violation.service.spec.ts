import { TestBed } from '@angular/core/testing';

import { EmployeeTypeViolationService } from './employee-type-violation.service';

describe('EmployeeTypeViolationService', () => {
  let service: EmployeeTypeViolationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeeTypeViolationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
