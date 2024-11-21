import { TestBed } from '@angular/core/testing';

import { BusRestrictService } from './bus-restrict.service';

describe('BusRestrictService', () => {
  let service: BusRestrictService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BusRestrictService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
