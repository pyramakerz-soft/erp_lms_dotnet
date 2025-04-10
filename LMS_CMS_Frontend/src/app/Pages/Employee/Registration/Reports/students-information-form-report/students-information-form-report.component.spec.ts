import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentsInformationFormReportComponent } from './students-information-form-report.component';

describe('StudentsInformationFormReportComponent', () => {
  let component: StudentsInformationFormReportComponent;
  let fixture: ComponentFixture<StudentsInformationFormReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentsInformationFormReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentsInformationFormReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
