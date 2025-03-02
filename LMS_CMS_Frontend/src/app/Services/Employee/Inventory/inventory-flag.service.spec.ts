import { TestBed } from '@angular/core/testing';

import { InventoryFlagService } from './inventory-flag.service';

describe('InventoryFlagService', () => {
  let service: InventoryFlagService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InventoryFlagService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
