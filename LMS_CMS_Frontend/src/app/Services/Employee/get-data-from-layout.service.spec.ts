import { TestBed } from '@angular/core/testing';

import { GetDataFromLayoutService } from './get-data-from-layout.service';

describe('GetDataFromLayoutService', () => {
  let service: GetDataFromLayoutService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetDataFromLayoutService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
