import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProofRegistrationAndSuccessFormReportComponent } from './proof-registration-and-success-form-report.component';

describe('ProofRegistrationAndSuccessFormReportComponent', () => {
  let component: ProofRegistrationAndSuccessFormReportComponent;
  let fixture: ComponentFixture<ProofRegistrationAndSuccessFormReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProofRegistrationAndSuccessFormReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProofRegistrationAndSuccessFormReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
