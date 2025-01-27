import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingStudentEditComponent } from './accounting-student-edit.component';

describe('AccountingStudentEditComponent', () => {
  let component: AccountingStudentEditComponent;
  let fixture: ComponentFixture<AccountingStudentEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountingStudentEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountingStudentEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
