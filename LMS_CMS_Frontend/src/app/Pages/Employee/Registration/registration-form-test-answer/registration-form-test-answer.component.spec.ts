import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationFormTestAnswerComponent } from './registration-form-test-answer.component';

describe('RegistrationFormTestAnswerComponent', () => {
  let component: RegistrationFormTestAnswerComponent;
  let fixture: ComponentFixture<RegistrationFormTestAnswerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistrationFormTestAnswerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistrationFormTestAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
