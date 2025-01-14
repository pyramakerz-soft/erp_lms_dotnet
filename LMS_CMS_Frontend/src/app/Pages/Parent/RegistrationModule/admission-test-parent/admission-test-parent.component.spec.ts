import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmissionTestParentComponent } from './admission-test-parent.component';

describe('AdmissionTestParentComponent', () => {
  let component: AdmissionTestParentComponent;
  let fixture: ComponentFixture<AdmissionTestParentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdmissionTestParentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmissionTestParentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
