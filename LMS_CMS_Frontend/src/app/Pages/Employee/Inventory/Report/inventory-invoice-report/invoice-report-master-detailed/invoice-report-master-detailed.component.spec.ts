import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoiceReportMasterDetailedComponent } from './invoice-report-master-detailed.component';

describe('InvoiceReportMasterDetailedComponent', () => {
  let component: InvoiceReportMasterDetailedComponent;
  let fixture: ComponentFixture<InvoiceReportMasterDetailedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvoiceReportMasterDetailedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvoiceReportMasterDetailedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
