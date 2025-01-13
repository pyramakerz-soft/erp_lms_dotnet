import { TestBed } from '@angular/core/testing';

import { RegisterationFormTestService } from './registeration-form-test.service';

describe('RegisterationFormTestService', () => {
  let service: RegisterationFormTestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegisterationFormTestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
