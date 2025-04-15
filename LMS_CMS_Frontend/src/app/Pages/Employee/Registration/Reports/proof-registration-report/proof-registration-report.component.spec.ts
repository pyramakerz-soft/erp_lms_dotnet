import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProofRegistrationReportComponent } from './proof-registration-report.component';

describe('ProofRegistrationReportComponent', () => {
  let component: ProofRegistrationReportComponent;
  let fixture: ComponentFixture<ProofRegistrationReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProofRegistrationReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProofRegistrationReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
