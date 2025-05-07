import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonActivityComponent } from './lesson-activity.component';

describe('LessonActivityComponent', () => {
  let component: LessonActivityComponent;
  let fixture: ComponentFixture<LessonActivityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessonActivityComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonActivityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
