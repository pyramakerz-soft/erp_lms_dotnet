import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcademicSequentialReportComponent } from './academic-sequential-report.component';

describe('AcademicSequentialReportComponent', () => {
  let component: AcademicSequentialReportComponent;
  let fixture: ComponentFixture<AcademicSequentialReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AcademicSequentialReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcademicSequentialReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
