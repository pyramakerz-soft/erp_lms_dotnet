import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingEntriesDetailsComponent } from './accounting-entries-details.component';

describe('AccountingEntriesDetailsComponent', () => {
  let component: AccountingEntriesDetailsComponent;
  let fixture: ComponentFixture<AccountingEntriesDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountingEntriesDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountingEntriesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
