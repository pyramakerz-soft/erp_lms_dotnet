import { TestBed } from '@angular/core/testing';

import { RegisterationFormTestAnswerService } from './registeration-form-test-answer.service';

describe('RegisterationFormTestAnswerService', () => {
  let service: RegisterationFormTestAnswerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegisterationFormTestAnswerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
