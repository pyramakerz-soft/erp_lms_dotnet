import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopItemsComponent } from './shop-items.component';

describe('ShopItemsComponent', () => {
  let component: ShopItemsComponent;
  let fixture: ComponentFixture<ShopItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShopItemsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShopItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
