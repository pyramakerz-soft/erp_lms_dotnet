import { TestBed } from '@angular/core/testing';

import { PyramakerzService } from './pyramakerz.service';

describe('PyramakerzService', () => {
  let service: PyramakerzService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PyramakerzService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
