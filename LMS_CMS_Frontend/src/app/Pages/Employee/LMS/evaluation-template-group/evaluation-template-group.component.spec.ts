import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluationTemplateGroupComponent } from './evaluation-template-group.component';

describe('EvaluationTemplateGroupComponent', () => {
  let component: EvaluationTemplateGroupComponent;
  let fixture: ComponentFixture<EvaluationTemplateGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EvaluationTemplateGroupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EvaluationTemplateGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
