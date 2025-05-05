import { TestBed } from '@angular/core/testing';

import { PerformanceTypeService } from './performance-type.service';

describe('PerformanceTypeService', () => {
  let service: PerformanceTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PerformanceTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
