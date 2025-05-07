import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentLessonLiveComponent } from './student-lesson-live.component';

describe('StudentLessonLiveComponent', () => {
  let component: StudentLessonLiveComponent;
  let fixture: ComponentFixture<StudentLessonLiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentLessonLiveComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentLessonLiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
