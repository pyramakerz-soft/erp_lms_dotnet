import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingEntriesComponent } from './accounting-entries.component';

describe('AccountingEntriesComponent', () => {
  let component: AccountingEntriesComponent;
  let fixture: ComponentFixture<AccountingEntriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountingEntriesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountingEntriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
