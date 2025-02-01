import { TestBed } from '@angular/core/testing';

import { EmployeeStudentService } from './employee-student.service';

describe('EmployeeStudentService', () => {
  let service: EmployeeStudentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeeStudentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
