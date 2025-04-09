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
  @Input() infoRows: {
    keyEn: string;
    valueEn: string | number | null;
    keyAr?: string;
    valueAr?: string | number | null;
  }[] = [];
  @ViewChild('printContainer') printContainer!: ElementRef;
  readonly maxPrintableColumns = 6;

  ngAfterViewInit(): void {
    if (this.tableData && this.tableHeaders) {
      this.smartSplitTableData(); // dynamically handles based on width
    }
    if (this.tableDataWithHeader) {
      this.reSplitWideChunksIfNeeded(); // optional: split further if any section too wide
    }
    setTimeout(() => this.printPDF(), 100);
  }
  
  private estimateColumnsPerPage(): number {
    const printableWidthPx = 960; // A4 in landscape ≈ 11in * 96dpi
    const averageColumnWidthPx = 120;
    return Math.floor(printableWidthPx / averageColumnWidthPx); // ~8 columns
  }

  private normalizeKey(header: string): string {
    return header.trim().toLowerCase().replace(/\s+/g, '').replace(/[^a-zA-Z0-9]/g, '');
  }
  
  private smartSplitTableData(): void {
    if (!this.tableHeaders || !this.tableData) return;
  
    const colsPerPage = this.estimateColumnsPerPage();
    const sections = [];
  
    for (let i = 0; i < this.tableHeaders.length; i += colsPerPage) {
      const headersSlice = this.tableHeaders.slice(i, i + colsPerPage);
  
      const dataSlice = this.tableData.map(row => {
        const rowSlice: Record<string, any> = {};
        headersSlice.forEach(header => {
          const key = this.normalizeKey(header);
          rowSlice[header] = row[key] ?? 'N/A';
        });
        return rowSlice;
      });
  
      sections.push({
        header: `Columns ${i + 1} - ${Math.min(i + colsPerPage, this.tableHeaders.length)}`,
        headers: headersSlice,
        data: dataSlice
      });
    }
  
    this.tableDataWithHeader = sections;
    this.tableData = null; // Don't print raw wide table
  }

  private reSplitWideChunksIfNeeded(): void {
    if (!this.tableDataWithHeader) return;
  
    const colsPerPage = this.estimateColumnsPerPage();
    const reSplitSections: any[] = [];
  
    for (const section of this.tableDataWithHeader) {
      const { headers, data, header } = section;
  
      if (headers.length <= colsPerPage) {
        // No need to split — add as-is
        reSplitSections.push(section);
      } else {
        // Re-split this section
        for (let i = 0; i < headers.length; i += colsPerPage) {
          const headersSlice = headers.slice(i, i + colsPerPage);
          const dataSlice = data.map((row: any) => {
            const rowSlice: Record<string, any> = {};
            headersSlice.forEach((header: string | number) => {
              rowSlice[header] = row[header] ?? 'N/A';
            });
            return rowSlice;
          });
  
          reSplitSections.push({
            header: `${header} (Cols ${i + 1}–${Math.min(i + colsPerPage, headers.length)})`,
            headers: headersSlice,
            data: dataSlice
          });
        }
      }
    }
  
    this.tableDataWithHeader = reSplitSections;
  }
  
  printPDF() {
    const opt = {
      margin: 0.2,
      filename: `${this.fileName}.pdf`,
      image: { type: 'jpeg', quality: 0.98 },
      html2canvas: { scale: 1 }, // You can try 1.5 if too large
      jsPDF: { unit: 'in', format: 'a4', orientation: 'landscape' } // 👈 Landscape mode
    };
    html2pdf().from(this.printContainer.nativeElement).set(opt).save();
  }
}
