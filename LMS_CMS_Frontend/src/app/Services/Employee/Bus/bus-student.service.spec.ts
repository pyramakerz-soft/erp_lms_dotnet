import { TestBed } from '@angular/core/testing';

import { BusStudentService } from './bus-student.service';

describe('BusStudentService', () => {
  let service: BusStudentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BusStudentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
