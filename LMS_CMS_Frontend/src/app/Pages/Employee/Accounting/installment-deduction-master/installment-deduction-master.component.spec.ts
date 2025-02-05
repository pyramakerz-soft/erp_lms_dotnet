import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstallmentDeductionMasterComponent } from './installment-deduction-master.component';

describe('InstallmentDeductionMasterComponent', () => {
  let component: InstallmentDeductionMasterComponent;
  let fixture: ComponentFixture<InstallmentDeductionMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InstallmentDeductionMasterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InstallmentDeductionMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
