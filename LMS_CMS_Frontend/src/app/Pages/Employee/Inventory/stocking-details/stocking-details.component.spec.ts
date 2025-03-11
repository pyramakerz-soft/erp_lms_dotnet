import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockingDetailsComponent } from './stocking-details.component';

describe('StockingDetailsComponent', () => {
  let component: StockingDetailsComponent;
  let fixture: ComponentFixture<StockingDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StockingDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StockingDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
