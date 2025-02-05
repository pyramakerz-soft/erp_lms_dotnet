import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstallmentDeductionDetailComponent } from './installment-deduction-detail.component';

describe('InstallmentDeductionDetailComponent', () => {
  let component: InstallmentDeductionDetailComponent;
  let fixture: ComponentFixture<InstallmentDeductionDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InstallmentDeductionDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InstallmentDeductionDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
