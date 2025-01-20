import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewRegistrationComponent } from './interview-registration.component';

describe('InterviewRegistrationComponent', () => {
  let component: InterviewRegistrationComponent;
  let fixture: ComponentFixture<InterviewRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InterviewRegistrationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InterviewRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
