import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeesActivationComponent } from './fees-activation.component';

describe('FeesActivationComponent', () => {
  let component: FeesActivationComponent;
  let fixture: ComponentFixture<FeesActivationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FeesActivationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FeesActivationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
