import { TestBed } from '@angular/core/testing';

import { BusCompanyService } from './bus-company.service';

describe('BusCompanyService', () => {
  let service: BusCompanyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BusCompanyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
