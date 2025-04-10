import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalHistoryByParentComponent } from './medical-history-by-parent.component';

describe('MedicalHistoryByParentComponent', () => {
  let component: MedicalHistoryByParentComponent;
  let fixture: ComponentFixture<MedicalHistoryByParentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalHistoryByParentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicalHistoryByParentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
