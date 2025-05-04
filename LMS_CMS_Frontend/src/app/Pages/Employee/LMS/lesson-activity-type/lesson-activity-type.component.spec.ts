import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonActivityTypeComponent } from './lesson-activity-type.component';

describe('LessonActivityTypeComponent', () => {
  let component: LessonActivityTypeComponent;
  let fixture: ComponentFixture<LessonActivityTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessonActivityTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonActivityTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
