import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusRestrictsComponent } from './bus-restricts.component';

describe('BusRestrictsComponent', () => {
  let component: BusRestrictsComponent;
  let fixture: ComponentFixture<BusRestrictsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusRestrictsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusRestrictsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
