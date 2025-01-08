import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationFormFieldComponent } from './registration-form-field.component';

describe('RegistrationFormFieldComponent', () => {
  let component: RegistrationFormFieldComponent;
  let fixture: ComponentFixture<RegistrationFormFieldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistrationFormFieldComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistrationFormFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
