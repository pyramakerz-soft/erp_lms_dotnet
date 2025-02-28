import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReuseTableComponent } from './reuse-table.component';

describe('ReuseTableComponent', () => {
  let component: ReuseTableComponent;
  let fixture: ComponentFixture<ReuseTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReuseTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReuseTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
