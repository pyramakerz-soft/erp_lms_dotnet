import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluationFeedbackComponent } from './evaluation-feedback.component';

describe('EvaluationFeedbackComponent', () => {
  let component: EvaluationFeedbackComponent;
  let fixture: ComponentFixture<EvaluationFeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EvaluationFeedbackComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EvaluationFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
