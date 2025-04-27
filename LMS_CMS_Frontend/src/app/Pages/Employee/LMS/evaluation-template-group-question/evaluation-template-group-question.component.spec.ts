import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluationTemplateGroupQuestionComponent } from './evaluation-template-group-question.component';

describe('EvaluationTemplateGroupQuestionComponent', () => {
  let component: EvaluationTemplateGroupQuestionComponent;
  let fixture: ComponentFixture<EvaluationTemplateGroupQuestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EvaluationTemplateGroupQuestionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EvaluationTemplateGroupQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
