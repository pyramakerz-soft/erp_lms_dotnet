import { TestBed } from '@angular/core/testing';

import { BusCategoryService } from './bus-category.service';

describe('BusCategoryService', () => {
  let service: BusCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BusCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
