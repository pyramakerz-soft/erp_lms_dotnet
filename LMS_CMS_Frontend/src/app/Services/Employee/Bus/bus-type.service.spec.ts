import { TestBed } from '@angular/core/testing';

import { BusTypeService } from './bus-type.service';

describe('BusTypeService', () => {
  let service: BusTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BusTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
