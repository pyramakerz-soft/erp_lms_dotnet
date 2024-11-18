import { TestBed } from '@angular/core/testing';

import { NewTokenService } from './new-token.service';

describe('NewTokenService', () => {
  let service: NewTokenService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NewTokenService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
