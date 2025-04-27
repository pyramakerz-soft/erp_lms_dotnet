import { TestBed } from '@angular/core/testing';

import { EvaluationEmployeeService } from './evaluation-employee.service';

describe('EvaluationEmployeeService', () => {
  let service: EvaluationEmployeeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EvaluationEmployeeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
