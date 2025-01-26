import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingEmployeeEditComponent } from './accounting-employee-edit.component';

describe('AccountingEmployeeEditComponent', () => {
  let component: AccountingEmployeeEditComponent;
  let fixture: ComponentFixture<AccountingEmployeeEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountingEmployeeEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountingEmployeeEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
