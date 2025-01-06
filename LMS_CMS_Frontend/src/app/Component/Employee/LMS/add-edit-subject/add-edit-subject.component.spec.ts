import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditSubjectComponent } from './add-edit-subject.component';

describe('AddEditSubjectComponent', () => {
  let component: AddEditSubjectComponent;
  let fixture: ComponentFixture<AddEditSubjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditSubjectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditSubjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
