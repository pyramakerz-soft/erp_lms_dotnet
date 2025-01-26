import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingItemComponent } from './accounting-item.component';

describe('AccountingItemComponent', () => {
  let component: AccountingItemComponent;
  let fixture: ComponentFixture<AccountingItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountingItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountingItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
