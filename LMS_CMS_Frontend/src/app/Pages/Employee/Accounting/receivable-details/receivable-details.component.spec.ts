import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceivableDetailsComponent } from './receivable-details.component';

describe('ReceivableDetailsComponent', () => {
  let component: ReceivableDetailsComponent;
  let fixture: ComponentFixture<ReceivableDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceivableDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReceivableDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
