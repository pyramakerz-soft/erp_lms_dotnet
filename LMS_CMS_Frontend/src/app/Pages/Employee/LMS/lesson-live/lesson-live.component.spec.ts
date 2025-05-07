import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonLiveComponent } from './lesson-live.component';

describe('LessonLiveComponent', () => {
  let component: LessonLiveComponent;
  let fixture: ComponentFixture<LessonLiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessonLiveComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonLiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
