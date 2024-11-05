import { TestBed } from '@angular/core/testing';

import { ModuleDomainService } from './module-domain.service';

describe('ModuleDomainService', () => {
  let service: ModuleDomainService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModuleDomainService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
