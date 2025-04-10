import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentInformationComponent } from './student-information.component';

describe('StudentInformationComponent', () => {
  let component: StudentInformationComponent;
  let fixture: ComponentFixture<StudentInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentInformationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
