import { TestBed } from '@angular/core/testing';

import { BusTypeAddService } from './bus-type-add.service';

describe('BusTypeAddService', () => {
  let service: BusTypeAddService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BusTypeAddService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
