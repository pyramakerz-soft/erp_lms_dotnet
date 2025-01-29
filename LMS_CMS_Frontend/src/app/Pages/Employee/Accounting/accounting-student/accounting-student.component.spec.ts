import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingStudentComponent } from './accounting-student.component';

describe('AccountingStudentComponent', () => {
  let component: AccountingStudentComponent;
  let fixture: ComponentFixture<AccountingStudentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountingStudentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountingStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
