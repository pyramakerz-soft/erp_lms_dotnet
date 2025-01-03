import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusStatusComponent } from './bus-status.component';

describe('BusStatusComponent', () => {
  let component: BusStatusComponent;
  let fixture: ComponentFixture<BusStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusStatusComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
