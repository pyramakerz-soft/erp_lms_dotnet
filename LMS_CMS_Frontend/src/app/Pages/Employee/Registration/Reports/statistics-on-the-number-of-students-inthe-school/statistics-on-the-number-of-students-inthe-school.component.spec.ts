import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticsOnTheNumberOfStudentsIntheSchoolComponent } from './statistics-on-the-number-of-students-inthe-school.component';

describe('StatisticsOnTheNumberOfStudentsIntheSchoolComponent', () => {
  let component: StatisticsOnTheNumberOfStudentsIntheSchoolComponent;
  let fixture: ComponentFixture<StatisticsOnTheNumberOfStudentsIntheSchoolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StatisticsOnTheNumberOfStudentsIntheSchoolComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatisticsOnTheNumberOfStudentsIntheSchoolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
