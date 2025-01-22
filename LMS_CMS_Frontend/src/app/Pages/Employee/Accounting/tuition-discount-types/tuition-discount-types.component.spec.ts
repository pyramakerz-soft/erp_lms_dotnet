import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TuitionDiscountTypesComponent } from './tuition-discount-types.component';

describe('TuitionDiscountTypesComponent', () => {
  let component: TuitionDiscountTypesComponent;
  let fixture: ComponentFixture<TuitionDiscountTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TuitionDiscountTypesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TuitionDiscountTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
