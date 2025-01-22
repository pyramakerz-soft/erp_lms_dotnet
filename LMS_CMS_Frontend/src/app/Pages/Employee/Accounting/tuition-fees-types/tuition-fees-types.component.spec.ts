import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TuitionFeesTypesComponent } from './tuition-fees-types.component';

describe('TuitionFeesTypesComponent', () => {
  let component: TuitionFeesTypesComponent;
  let fixture: ComponentFixture<TuitionFeesTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TuitionFeesTypesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TuitionFeesTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
