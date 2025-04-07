import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PdfPrintComponent } from './pdf-print.component';

describe('PdfPrintComponent', () => {
  let component: PdfPrintComponent;
  let fixture: ComponentFixture<PdfPrintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PdfPrintComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PdfPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
