import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingEntriesDocTypeComponent } from './accounting-entries-doc-type.component';

describe('AccountingEntriesDocTypeComponent', () => {
  let component: AccountingEntriesDocTypeComponent;
  let fixture: ComponentFixture<AccountingEntriesDocTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountingEntriesDocTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountingEntriesDocTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
