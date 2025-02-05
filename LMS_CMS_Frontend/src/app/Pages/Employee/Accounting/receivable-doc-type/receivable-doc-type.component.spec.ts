import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceivableDocTypeComponent } from './receivable-doc-type.component';

describe('ReceivableDocTypeComponent', () => {
  let component: ReceivableDocTypeComponent;
  let fixture: ComponentFixture<ReceivableDocTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceivableDocTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReceivableDocTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
