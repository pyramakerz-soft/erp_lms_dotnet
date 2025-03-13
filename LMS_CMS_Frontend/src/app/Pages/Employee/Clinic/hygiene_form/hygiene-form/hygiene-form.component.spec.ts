import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HygieneFormComponent } from './hygiene-form.component';

describe('HygieneFormComponent', () => {
  let component: HygieneFormComponent;
  let fixture: ComponentFixture<HygieneFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HygieneFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HygieneFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
