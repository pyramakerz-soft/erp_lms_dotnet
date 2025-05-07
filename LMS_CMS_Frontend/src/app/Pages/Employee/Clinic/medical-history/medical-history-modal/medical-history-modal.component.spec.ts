import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalHistoryModalComponent } from './medical-history-modal.component';

describe('MedicalHistoryModalComponent', () => {
  let component: MedicalHistoryModalComponent;
  let fixture: ComponentFixture<MedicalHistoryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalHistoryModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicalHistoryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
