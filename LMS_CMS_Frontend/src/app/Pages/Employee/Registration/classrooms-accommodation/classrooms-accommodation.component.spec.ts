import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomsAccommodationComponent } from './classrooms-accommodation.component';

describe('ClassroomsAccommodationComponent', () => {
  let component: ClassroomsAccommodationComponent;
  let fixture: ComponentFixture<ClassroomsAccommodationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClassroomsAccommodationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassroomsAccommodationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
