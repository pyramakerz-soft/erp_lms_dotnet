import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmissionTestComponent } from './admission-test.component';

describe('AdmissionTestComponent', () => {
  let component: AdmissionTestComponent;
  let fixture: ComponentFixture<AdmissionTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdmissionTestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmissionTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
