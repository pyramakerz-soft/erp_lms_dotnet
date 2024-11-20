import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusPrintNameTagComponent } from './bus-print-name-tag.component';

describe('BusPrintNameTagComponent', () => {
  let component: BusPrintNameTagComponent;
  let fixture: ComponentFixture<BusPrintNameTagComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusPrintNameTagComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusPrintNameTagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
