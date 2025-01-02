import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SemesterViewComponent } from './semester-view.component';

describe('SemesterViewComponent', () => {
  let component: SemesterViewComponent;
  let fixture: ComponentFixture<SemesterViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SemesterViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SemesterViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
