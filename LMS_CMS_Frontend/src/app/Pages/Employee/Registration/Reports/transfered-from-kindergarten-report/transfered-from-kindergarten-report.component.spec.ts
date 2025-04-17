import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferedFromKindergartenReportComponent } from './transfered-from-kindergarten-report.component';

describe('TransferedFromKindergartenReportComponent', () => {
  let component: TransferedFromKindergartenReportComponent;
  let fixture: ComponentFixture<TransferedFromKindergartenReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransferedFromKindergartenReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransferedFromKindergartenReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
