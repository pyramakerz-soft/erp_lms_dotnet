import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PerformanceTypeComponent } from './performance-type.component';

describe('PerformanceTypeComponent', () => {
  let component: PerformanceTypeComponent;
  let fixture: ComponentFixture<PerformanceTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PerformanceTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PerformanceTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
