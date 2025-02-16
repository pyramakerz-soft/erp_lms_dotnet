import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopItemsAddEditComponent } from './shop-items-add-edit.component';

describe('ShopItemsAddEditComponent', () => {
  let component: ShopItemsAddEditComponent;
  let fixture: ComponentFixture<ShopItemsAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShopItemsAddEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShopItemsAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
