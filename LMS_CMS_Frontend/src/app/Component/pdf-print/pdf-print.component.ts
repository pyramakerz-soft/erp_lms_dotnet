import { CommonModule } from '@angular/common';
import { OnChanges, Component, ElementRef, Input, SimpleChanges, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import html2pdf from 'html2pdf.js';


// Add this interface at the top of the file
interface TableSection {
  header: string;
  data: { key: string; value: any }[];
  tableHeaders: string[];
  tableData: any[];
}

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
  @Input() tableDataWithHeaderArray: TableSection[] | null = null;
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
    EnNote?: string | number | null;
    ArNote?: string | number | null;
  }[] = [];
  @ViewChild('printContainer') printContainer!: ElementRef;
  tableChunks: { headers: string[], data: any[] }[] = [];
  preservedColumns: string = "";
  @Input() autoDownload: boolean = false;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tableHeaders'] || changes['tableData']) {
      if (this.tableHeaders)
        this.preservedColumns = this.tableHeaders[0];
      this.splitTableGenerically();
    }
  }
  print() {
  const printContents = this.printContainer.nativeElement.innerHTML;
  
  // Create a print-specific stylesheet
  const printStyle = `
    <style>
      @page { size: auto; margin: 0mm; }
      body { margin: 0;
      }
      .print-content {
        padding: 20px;
      }
      @media print {
        body * {
        }
        .print-overlay,
        .print-overlay * {
          visibility: visible;
          position: static;
          background: none;
        }
        .print-content {
        }
      }
    </style>
  `;

  // Create print overlay
  const printOverlay = document.createElement('div');
  printOverlay.className = 'print-overlay';
  printOverlay.innerHTML = `
    <div class="print-content">
      ${printContents}
    </div>
  `;

  // Add to body
  document.body.appendChild(printOverlay);
  document.body.insertAdjacentHTML('beforeend', printStyle);

  // Print and clean up
  setTimeout(() => {
    window.print();
    
    setTimeout(() => {
      document.body.removeChild(printOverlay);
      const styles = document.querySelectorAll('style[data-print-style]');
      styles.forEach(style => document.body.removeChild(style));
    }, 100);
  }, 100);
}

  // ngAfterViewInit(): void {
  //   if (this.school?.reportImage?.startsWith('http')) {
  //     this.convertImgToBase64URL(this.school.reportImage).then((base64Img) => {
  //       this.school.reportImage = base64Img;
  //       setTimeout(() => this.printPDF(), 100);
  //     });
  //   } else {
  //     setTimeout(() => this.printPDF(), 100);
  //   }
  // }

  // ngAfterViewInit(): void {
  //   if (this.autoDownload) {
  //     this.convertImgToBase64URL(this.school.reportImage).then((base64Img) => {
  //       this.school.reportImage = base64Img;
  //       setTimeout(() => this.printPDF(), 100);
  //     });
  //   }
  // }

  downloadPDF() {
    if (this.school?.reportImage?.startsWith('http')) {
      this.convertImgToBase64URL(this.school.reportImage).then((base64Img) => {
        this.school.reportImage = base64Img;
        console.log("gf",this.school.reportImage)
        setTimeout(() => this.printPDF(), 100);
      });
    } else {
      setTimeout(() => this.printPDF(), 100);
      console.log("gf",this.school.reportImage)
    }
  }
  
  convertImgToBase64URL(url: string): Promise<string> {
    return new Promise((resolve) => {
      const img = new Image();
      img.crossOrigin = 'anonymous'; // Important for CORS
      img.onload = () => {
        const canvas = document.createElement('canvas');
        canvas.width = img.width;
        canvas.height = img.height;
        const ctx = canvas.getContext('2d');
        if (!ctx) return resolve('');
        ctx.drawImage(img, 0, 0);
        try {
          const dataURL = canvas.toDataURL('image/png');
          resolve(dataURL);
        } catch (e) {
          console.error('toDataURL failed:', e);
          resolve('');
        }
      };
      img.onerror = (e) => {
        console.error('Failed to load image', e);
        resolve('');
      };
      img.src = url;
    });
  }
  
  estimateHeaderWidth(header: string): number {
    // Rough estimate: 10–12px per character
    return header.length * 10;
  }

  splitTableGenerically(maxTotalWidth: number = 600) {
    this.tableChunks = [];
  
    if (!this.tableHeaders || !this.tableData) return;
  
    const preserved = this.preservedColumns;
    const headers = [...this.tableHeaders];
    let i = 0;
  
    while (i < headers.length) {
      let widthSum = 0;
      let headersSlice: string[] = [];
  
      while (i < headers.length) {
        const currentHeader = headers[i];
        const headerWidth = this.estimateHeaderWidth(currentHeader);
  
        if (widthSum + headerWidth > maxTotalWidth) break;
  
        headersSlice.push(currentHeader);
        widthSum += headerWidth;
        i++;
      }
      // Add preserved column to every chunk (if not already included)
      const finalHeaders = headersSlice.includes(preserved)
        ? headersSlice
        : [preserved, ...headersSlice];
  
      const dataChunk = this.tableData.map(row => {
        const newRow: any = {};
        finalHeaders.forEach(header => {
          newRow[header] = row[header] ?? '-';
        });
        return newRow;
      });
  
      this.tableChunks.push({ headers: finalHeaders, data: dataChunk });
    }
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