import { Injectable } from '@angular/core';
// import saveAs from 'file-saver';
import FileSaver from 'file-saver';
import saveAs from 'file-saver';
import html2pdf from 'html2pdf.js'; 
import html2canvas from 'html2canvas';
import * as ExcelJS from 'exceljs'

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor() { }
 
  DownloadAsPDF(name: string) {
    const elements = document.querySelectorAll('.print-area');
    
    if (!elements || elements.length === 0) {
      console.error("No elements found!");
      return;
    }
  
    const container = document.createElement('div');
  
    elements.forEach(el => {
      container.appendChild(el.cloneNode(true));
    });
  
    html2pdf().from(container).set({
      margin: 10,
      filename: `${name}.pdf`,
      image: { type: 'jpeg', quality: 0.98 },
      html2canvas: { scale: 3, useCORS: true, allowTaint: true },
      jsPDF: { orientation: 'portrait', unit: 'mm', format: 'a4' }
    }).save();
  }
  
  PrintPDF(name:string) {
    let Element = document.getElementById('Data');

    if (!Element) {
        console.error("Element not found!");
        return;
    }

    let printWindow = window.open('', '', 'width=800,height=600');
    if (!printWindow) {
        console.error("Failed to open print window.");
        return;
    }
 
    let styles = Array.from(document.styleSheets)
        .map(styleSheet => {
            try {
                return Array.from(styleSheet.cssRules)
                    .map(rule => rule.cssText)
                    .join("\n");
            } catch (e) {
                return "";
            }
        })
        .join("\n");

    printWindow.document.write(`
        <html>
        <head>
            <title>${name}</title>
            <style>${styles}</style>  <!-- Injects all styles -->
        </head>
        <body>
            ${Element.outerHTML}
            <script>
                window.onload = function() {
                    window.print();
                    window.onafterprint = function() { window.close(); };
                };
            </script>
        </body>
        </html>
    `);

    printWindow.document.close();
  }

  async getBase64ImageFromUrl(imageUrl: string): Promise<string> {
    const response = await fetch(imageUrl);
    const blob = await response.blob();
  
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onloadend = () => resolve(reader.result as string);
      reader.onerror = reject;
      reader.readAsDataURL(blob);
    });
  }

  async generateExcelReport(options: {
    mainHeader?: { en: string; ar: string };
    subHeaders?: { en: string; ar: string }[];
    infoRows?: { key: string; value: string | number | boolean }[]; // 👈 NEW
    reportImage?: string;
    tables: {
      title: string;
      headers: string[];
      data: (string | number | boolean)[][];
    }[];
    filename?: string;
  }) {
    console.log(options)
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet("Report");
  
    function getExcelColumnLetter(colIndex: number): string {
      let temp = '';
      while (colIndex > 0) {
        let remainder = (colIndex - 1) % 26;
        temp = String.fromCharCode(65 + remainder) + temp;
        colIndex = Math.floor((colIndex - 1) / 26);
      }
      return temp;
    }
  
    let base64Image = '';
    if (options.reportImage?.startsWith('http')) {
      base64Image = await this.getBase64ImageFromUrl(options.reportImage);
    } else if (options.reportImage) {
      base64Image = options.reportImage;
    }
  
    const colCount = options.tables?.[0]?.headers.length || 5;
    const enEnd = getExcelColumnLetter(colCount);
    const arStart = getExcelColumnLetter(colCount + 1);
    const arEnd = getExcelColumnLetter(colCount * 2);
  
    // Main header
    worksheet.mergeCells(`A1:${enEnd}1`);
    worksheet.getCell('A1').value = options.mainHeader?.en;
    worksheet.getCell('A1').font = { bold: true, size: 16 };
    worksheet.getCell('A1').alignment = { horizontal: 'left' };
  
    worksheet.mergeCells(`${arStart}1:${arEnd}1`);
    worksheet.getCell(`${arStart}1`).value = options.mainHeader?.ar;
    worksheet.getCell(`${arStart}1`).font = { bold: true, size: 16 };
    worksheet.getCell(`${arStart}1`).alignment = { horizontal: 'right' };
  
    // Sub headers
    options.subHeaders?.forEach((header, i) => {
      const row = i + 2;
      worksheet.mergeCells(`A${row}:${enEnd}${row}`);
      worksheet.getCell(`A${row}`).value = header.en;
      worksheet.getCell(`A${row}`).font = { size: 12 };
      worksheet.getCell(`A${row}`).alignment = { horizontal: 'left' };
  
      worksheet.mergeCells(`${arStart}${row}:${arEnd}${row}`);
      worksheet.getCell(`${arStart}${row}`).value = header.ar;
      worksheet.getCell(`${arStart}${row}`).font = { size: 12 };
      worksheet.getCell(`${arStart}${row}`).alignment = { horizontal: 'right' };
    });
  
    const headerOffset = (options.subHeaders?.length || 0) + 2;
  
    if (base64Image) {
      const imageId = workbook.addImage({
        base64: base64Image.split(',')[1],
        extension: 'png',
      });
  
      worksheet.addImage(imageId, {
        tl: { col: 4, row: 0 },
        ext: { width: 100, height: 50 },
      });
    }
  
    worksheet.addRow([]);
  
    // Info rows (dynamic)
    options.infoRows?.forEach(({ key, value }) => {
      worksheet.addRow([`${key}: ${value}`]).font = { bold: true, size: 12 };
    });
  
    worksheet.addRow([]);
  
    // Tables
    for (const table of options.tables) {
      worksheet.addRow([table.title]).font = { bold: true, size: 13 };
  
      const headerRow = worksheet.addRow(table.headers);
      headerRow.font = { bold: true, color: { argb: 'FFFFFF' } };
      headerRow.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: '4F81BD' } };
      headerRow.eachCell((cell) => {
        cell.border = { bottom: { style: 'thin' } };
      });
  
      table.data.forEach((row) => {
        worksheet.addRow(row);
      });
  
      worksheet.addRow([]);
    }
  
    worksheet.columns.forEach((col) => {
      col.width = 20;
    });
  
    const buffer = await workbook.xlsx.writeBuffer();
    FileSaver.saveAs(new Blob([buffer]), options.filename || 'Report.xlsx');
  }
}
