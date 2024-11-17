import { TestBed } from '@angular/core/testing';

import { RoleDetailsService } from './role-details.service';

describe('RoleDetailsService', () => {
  let service: RoleDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RoleDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
