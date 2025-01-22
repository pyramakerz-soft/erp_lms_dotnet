import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DebitsComponent } from './debits.component';

describe('DebitsComponent', () => {
  let component: DebitsComponent;
  let fixture: ComponentFixture<DebitsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DebitsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DebitsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
