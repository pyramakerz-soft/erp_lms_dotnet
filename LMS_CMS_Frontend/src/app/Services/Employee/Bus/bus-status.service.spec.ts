import { TestBed } from '@angular/core/testing';

import { BusStatusService } from './bus-status.service';

describe('BusStatusService', () => {
  let service: BusStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BusStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
