import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoiceReportMasterComponent } from './invoice-report-master.component';

describe('InvoiceReportMasterComponent', () => {
  let component: InvoiceReportMasterComponent;
  let fixture: ComponentFixture<InvoiceReportMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvoiceReportMasterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvoiceReportMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
