import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HygieneTypesComponent } from './hygiene-types.component';

describe('HygieneTypesComponent', () => {
  let component: HygieneTypesComponent;
  let fixture: ComponentFixture<HygieneTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HygieneTypesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HygieneTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
