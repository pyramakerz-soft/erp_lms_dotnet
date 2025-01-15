import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewTimeTableComponent } from './interview-time-table.component';

describe('InterviewTimeTableComponent', () => {
  let component: InterviewTimeTableComponent;
  let fixture: ComponentFixture<InterviewTimeTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InterviewTimeTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InterviewTimeTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
