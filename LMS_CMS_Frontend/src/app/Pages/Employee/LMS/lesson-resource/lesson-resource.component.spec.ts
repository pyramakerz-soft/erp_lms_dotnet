import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonResourceComponent } from './lesson-resource.component';

describe('LessonResourceComponent', () => {
  let component: LessonResourceComponent;
  let fixture: ComponentFixture<LessonResourceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessonResourceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonResourceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
