import { CommonModule } from '@angular/common';
import { OnChanges ,Component, ElementRef, Input, SimpleChanges, ViewChild } from '@angular/core';
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
  tableChunks: { headers: string[], data: any[] }[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tableHeaders'] || changes['tableData']) {
      this.splitTableGenerically();
    }
  }

  ngAfterViewInit(): void {
    setTimeout(() => this.printPDF(), 100);
  }
  splitTableGenerically(maxColsPerTable: number = 8) {
    this.tableChunks = [];
  
    if (!this.tableHeaders || !this.tableData) return;
  
    for (let i = 0; i < this.tableHeaders.length; i += maxColsPerTable) {
      const headersSlice = this.tableHeaders.slice(i, i + maxColsPerTable);
  
      const dataChunk = this.tableData.map(row => {
        const newRow: any = {};
        headersSlice.forEach(header => {
          newRow[header] = row[header];
        });
        return newRow;
      });
      
      this.tableChunks.push({ headers: headersSlice, data: dataChunk });
    }
    console.log(this.tableChunks)
    console.log(this.tableData)
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