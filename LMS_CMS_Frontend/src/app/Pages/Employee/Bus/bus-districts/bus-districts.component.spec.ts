import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusDistrictsComponent } from './bus-districts.component';

describe('BusDistrictsComponent', () => {
  let component: BusDistrictsComponent;
  let fixture: ComponentFixture<BusDistrictsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusDistrictsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusDistrictsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
