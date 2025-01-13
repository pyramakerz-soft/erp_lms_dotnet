import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationConfirmationTestDetailsComponent } from './registration-confirmation-test-details.component';

describe('RegistrationConfirmationTestDetailsComponent', () => {
  let component: RegistrationConfirmationTestDetailsComponent;
  let fixture: ComponentFixture<RegistrationConfirmationTestDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistrationConfirmationTestDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistrationConfirmationTestDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
