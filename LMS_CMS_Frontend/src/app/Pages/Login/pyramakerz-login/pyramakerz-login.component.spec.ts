import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PyramakerzLoginComponent } from './pyramakerz-login.component';

describe('PyramakerzLoginComponent', () => {
  let component: PyramakerzLoginComponent;
  let fixture: ComponentFixture<PyramakerzLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PyramakerzLoginComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PyramakerzLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
