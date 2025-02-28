import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateHygieneFormComponent } from './create-hygiene-form.component';

describe('CreateHygieneFormComponent', () => {
  let component: CreateHygieneFormComponent;
  let fixture: ComponentFixture<CreateHygieneFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateHygieneFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateHygieneFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
