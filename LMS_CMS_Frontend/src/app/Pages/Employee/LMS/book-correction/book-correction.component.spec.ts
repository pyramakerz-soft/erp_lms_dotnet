import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookCorrectionComponent } from './book-correction.component';

describe('BookCorrectionComponent', () => {
  let component: BookCorrectionComponent;
  let fixture: ComponentFixture<BookCorrectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookCorrectionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookCorrectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
