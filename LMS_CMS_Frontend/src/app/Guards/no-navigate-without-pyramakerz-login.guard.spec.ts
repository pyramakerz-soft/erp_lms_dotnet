import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { noNavigateWithoutPyramakerzLoginGuard } from './no-navigate-without-pyramakerz-login.guard';

describe('noNavigateWithoutPyramakerzLoginGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => noNavigateWithoutPyramakerzLoginGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
