import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentsNamesInClassComponent } from './students-names-in-class.component';

describe('StudentsNamesInClassComponent', () => {
  let component: StudentsNamesInClassComponent;
  let fixture: ComponentFixture<StudentsNamesInClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentsNamesInClassComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentsNamesInClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
