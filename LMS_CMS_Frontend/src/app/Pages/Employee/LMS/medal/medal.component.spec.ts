import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedalComponent } from './medal.component';

describe('MedalComponent', () => {
  let component: MedalComponent;
  let fixture: ComponentFixture<MedalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
