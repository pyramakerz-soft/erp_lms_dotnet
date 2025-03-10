import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockingComponent } from './stocking.component';

describe('StockingComponent', () => {
  let component: StockingComponent;
  let fixture: ComponentFixture<StockingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StockingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StockingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
