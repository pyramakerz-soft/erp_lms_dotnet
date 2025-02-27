import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryMasterComponent } from './inventory-master.component';

describe('InventoryMasterComponent', () => {
  let component: InventoryMasterComponent;
  let fixture: ComponentFixture<InventoryMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InventoryMasterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InventoryMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
