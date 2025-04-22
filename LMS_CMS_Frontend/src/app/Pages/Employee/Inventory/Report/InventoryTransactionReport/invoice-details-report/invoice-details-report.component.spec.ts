import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoiceDetailsReportComponent } from './invoice-details-report.component';

describe('InvoiceDetailsReportComponent', () => {
  let component: InvoiceDetailsReportComponent;
  let fixture: ComponentFixture<InvoiceDetailsReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvoiceDetailsReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvoiceDetailsReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
