import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonResourcesTypeComponent } from './lesson-resources-type.component';

describe('LessonResourcesTypeComponent', () => {
  let component: LessonResourcesTypeComponent;
  let fixture: ComponentFixture<LessonResourcesTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LessonResourcesTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonResourcesTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
