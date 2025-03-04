import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMedicalReportComponent } from './edit-medical-report.component';

describe('EditMedicalReportComponent', () => {
  let component: EditMedicalReportComponent;
  let fixture: ComponentFixture<EditMedicalReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditMedicalReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditMedicalReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
