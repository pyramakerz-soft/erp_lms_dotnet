import { TestBed } from '@angular/core/testing';

import { SchoolRoleService } from './school-role.service';

describe('SchoolRoleService', () => {
  let service: SchoolRoleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SchoolRoleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
