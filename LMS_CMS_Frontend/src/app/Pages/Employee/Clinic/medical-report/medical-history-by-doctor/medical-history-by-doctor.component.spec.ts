import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalHistoryByDoctorComponent } from './medical-history-by-doctor.component';

describe('MedicalHistoryByDoctorComponent', () => {
  let component: MedicalHistoryByDoctorComponent;
  let fixture: ComponentFixture<MedicalHistoryByDoctorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalHistoryByDoctorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicalHistoryByDoctorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
