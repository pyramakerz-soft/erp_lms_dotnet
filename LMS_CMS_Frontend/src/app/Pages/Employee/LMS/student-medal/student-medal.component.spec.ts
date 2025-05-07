import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentMedalComponent } from './student-medal.component';

describe('StudentMedalComponent', () => {
  let component: StudentMedalComponent;
  let fixture: ComponentFixture<StudentMedalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentMedalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentMedalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
