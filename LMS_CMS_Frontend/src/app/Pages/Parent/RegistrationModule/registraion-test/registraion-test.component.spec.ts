import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistraionTestComponent } from './registraion-test.component';

describe('RegistraionTestComponent', () => {
  let component: RegistraionTestComponent;
  let fixture: ComponentFixture<RegistraionTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistraionTestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistraionTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
