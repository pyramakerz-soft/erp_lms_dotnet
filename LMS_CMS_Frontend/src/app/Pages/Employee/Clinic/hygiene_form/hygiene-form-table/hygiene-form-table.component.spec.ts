import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HygieneFormTableComponent } from './hygiene-form-table.component';

describe('HygieneFormTableComponent', () => {
  let component: HygieneFormTableComponent;
  let fixture: ComponentFixture<HygieneFormTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HygieneFormTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HygieneFormTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
