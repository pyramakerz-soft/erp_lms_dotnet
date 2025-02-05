import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayableDocTypeComponent } from './payable-doc-type.component';

describe('PayableDocTypeComponent', () => {
  let component: PayableDocTypeComponent;
  let fixture: ComponentFixture<PayableDocTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PayableDocTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PayableDocTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
