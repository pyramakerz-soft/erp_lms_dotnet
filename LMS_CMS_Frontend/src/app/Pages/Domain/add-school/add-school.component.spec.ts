import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSchoolComponent } from './add-school.component';

describe('AddSchoolComponent', () => {
  let component: AddSchoolComponent;
  let fixture: ComponentFixture<AddSchoolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddSchoolComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddSchoolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
