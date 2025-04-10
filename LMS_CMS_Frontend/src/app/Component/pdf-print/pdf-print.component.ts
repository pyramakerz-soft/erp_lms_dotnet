import { CommonModule } from '@angular/common';
import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import html2pdf from 'html2pdf.js';

@Component({
  selector: 'app-pdf-print',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pdf-print.component.html',
  styleUrl: './pdf-print.component.css'
})
export class PdfPrintComponent {

  @Input() school: any;
  @Input() tableHeaders: string[] | null = null;
  @Input() tableData: any[] | null = null;
  @Input() tableDataWithHeader: any[] | null = null;
  @Input() fileName: string = 'report';
  @Input() Title: string = '';
  @Input() infoRows: {
    keyEn?: string;
    valueEn?: string | number | null;
    keyAr?: string;
    valueAr?: string | number | null;
  }[] = [];
  @Input() PsNotes: {
    EnNote?: string| number | null;
    ArNote?: string| number | null;
  }[] = [];
  @ViewChild('printContainer') printContainer!: ElementRef;

  ngAfterViewInit(): void {
    setTimeout(() => this.printPDF(), 100);
  }

  printPDF() {
    const opt = {
      margin: 0.5,
      filename: `${this.fileName}.pdf`,
      image: { type: 'jpeg', quality: 0.98 },
      html2canvas: { scale: 2 },
      jsPDF: { unit: 'in', format: 'a4', orientation: 'portrait' }
    };
    html2pdf().from(this.printContainer.nativeElement).set(opt).save();
  }
}