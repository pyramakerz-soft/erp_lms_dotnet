import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluationEmployeeAnswerComponent } from './evaluation-employee-answer.component';

describe('EvaluationEmployeeAnswerComponent', () => {
  let component: EvaluationEmployeeAnswerComponent;
  let fixture: ComponentFixture<EvaluationEmployeeAnswerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EvaluationEmployeeAnswerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EvaluationEmployeeAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
