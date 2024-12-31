import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeAddEditComponent } from './employee-add-edit.component';

describe('EmployeeAddEditComponent', () => {
  let component: EmployeeAddEditComponent;
  let fixture: ComponentFixture<EmployeeAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeAddEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
