import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusStudentComponent } from './bus-student.component';

describe('BusStudentComponent', () => {
  let component: BusStudentComponent;
  let fixture: ComponentFixture<BusStudentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusStudentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
