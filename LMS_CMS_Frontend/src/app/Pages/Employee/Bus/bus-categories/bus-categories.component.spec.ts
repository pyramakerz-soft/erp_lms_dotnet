import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusCategoriesComponent } from './bus-categories.component';

describe('BusCategoriesComponent', () => {
  let component: BusCategoriesComponent;
  let fixture: ComponentFixture<BusCategoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusCategoriesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
