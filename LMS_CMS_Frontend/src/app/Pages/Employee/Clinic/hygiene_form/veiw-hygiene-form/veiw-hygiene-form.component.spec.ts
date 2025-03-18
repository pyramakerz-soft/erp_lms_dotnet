import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VeiwHygieneFormComponent } from './veiw-hygiene-form.component';

describe('VeiwHygieneFormComponent', () => {
  let component: VeiwHygieneFormComponent;
  let fixture: ComponentFixture<VeiwHygieneFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VeiwHygieneFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VeiwHygieneFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
