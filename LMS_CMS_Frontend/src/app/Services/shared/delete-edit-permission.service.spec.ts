import { TestBed } from '@angular/core/testing';

import { DeleteEditPermissionService } from './delete-edit-permission.service';

describe('DeleteEditPermissionService', () => {
  let service: DeleteEditPermissionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeleteEditPermissionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
