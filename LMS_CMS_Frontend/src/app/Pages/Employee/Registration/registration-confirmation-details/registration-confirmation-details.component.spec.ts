import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationConfirmationDetailsComponent } from './registration-confirmation-details.component';

describe('RegistrationConfirmationDetailsComponent', () => {
  let component: RegistrationConfirmationDetailsComponent;
  let fixture: ComponentFixture<RegistrationConfirmationDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistrationConfirmationDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistrationConfirmationDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
