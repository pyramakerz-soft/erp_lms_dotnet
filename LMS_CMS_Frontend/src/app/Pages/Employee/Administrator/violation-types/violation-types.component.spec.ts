import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViolationTypesComponent } from './violation-types.component';

describe('ViolationTypesComponent', () => {
  let component: ViolationTypesComponent;
  let fixture: ComponentFixture<ViolationTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViolationTypesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViolationTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
