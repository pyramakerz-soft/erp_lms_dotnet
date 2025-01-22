import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReasonsforleavingworkComponent } from './reasonsforleavingwork.component';

describe('ReasonsforleavingworkComponent', () => {
  let component: ReasonsforleavingworkComponent;
  let fixture: ComponentFixture<ReasonsforleavingworkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReasonsforleavingworkComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReasonsforleavingworkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
