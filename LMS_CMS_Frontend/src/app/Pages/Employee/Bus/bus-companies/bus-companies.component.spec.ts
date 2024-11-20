import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusCompaniesComponent } from './bus-companies.component';

describe('BusCompaniesComponent', () => {
  let component: BusCompaniesComponent;
  let fixture: ComponentFixture<BusCompaniesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusCompaniesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusCompaniesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
