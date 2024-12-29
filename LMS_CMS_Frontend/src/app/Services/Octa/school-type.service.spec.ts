import { TestBed } from '@angular/core/testing';

import { SchoolTypeService } from './school-type.service';

describe('SchoolTypeService', () => {
  let service: SchoolTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SchoolTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
