import { TestBed } from '@angular/core/testing';

import { RegisterationFormParentService } from './registeration-form-parent.service';

describe('RegisterationFormParentService', () => {
  let service: RegisterationFormParentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegisterationFormParentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
