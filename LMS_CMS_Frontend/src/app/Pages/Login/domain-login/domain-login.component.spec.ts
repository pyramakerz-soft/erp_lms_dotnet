import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DomainLoginComponent } from './domain-login.component';

describe('DomainLoginComponent', () => {
  let component: DomainLoginComponent;
  let fixture: ComponentFixture<DomainLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DomainLoginComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DomainLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
