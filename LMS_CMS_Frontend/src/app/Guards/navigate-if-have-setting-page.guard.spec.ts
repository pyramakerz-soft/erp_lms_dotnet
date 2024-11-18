import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { navigateIfHaveSettingPageGuard } from './navigate-if-have-This-page.guard';

describe('navigateIfHaveSettingPageGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => navigateIfHaveSettingPageGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
