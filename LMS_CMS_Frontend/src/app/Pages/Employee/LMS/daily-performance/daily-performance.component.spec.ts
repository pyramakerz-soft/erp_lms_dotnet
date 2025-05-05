import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyPerformanceComponent } from './daily-performance.component';

describe('DailyPerformanceComponent', () => {
  let component: DailyPerformanceComponent;
  let fixture: ComponentFixture<DailyPerformanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DailyPerformanceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DailyPerformanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
