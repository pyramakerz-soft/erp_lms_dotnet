import { TestBed } from '@angular/core/testing';

import { RegisterationFormTestParentService } from './registeration-form-test-parent.service';

describe('RegisterationFormTestParentService', () => {
  let service: RegisterationFormTestParentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegisterationFormTestParentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
