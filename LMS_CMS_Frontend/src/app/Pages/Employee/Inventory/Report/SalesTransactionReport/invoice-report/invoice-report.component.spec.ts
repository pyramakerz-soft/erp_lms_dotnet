import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoiceReportComponent } from './invoice-report.component';

describe('InvoiceReportComponent', () => {
  let component: InvoiceReportComponent;
  let fixture: ComponentFixture<InvoiceReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvoiceReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvoiceReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
