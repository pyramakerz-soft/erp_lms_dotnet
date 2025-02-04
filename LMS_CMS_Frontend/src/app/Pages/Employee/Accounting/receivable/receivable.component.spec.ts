import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceivableComponent } from './receivable.component';

describe('ReceivableComponent', () => {
  let component: ReceivableComponent;
  let fixture: ComponentFixture<ReceivableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceivableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReceivableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
