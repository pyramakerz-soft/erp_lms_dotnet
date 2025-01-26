import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingEmployeeComponent } from './accounting-employee.component';

describe('AccountingEmployeeComponent', () => {
  let component: AccountingEmployeeComponent;
  let fixture: ComponentFixture<AccountingEmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountingEmployeeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountingEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
