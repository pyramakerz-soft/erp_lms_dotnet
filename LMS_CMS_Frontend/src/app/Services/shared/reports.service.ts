import { Injectable } from '@angular/core';
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
 
  DownloadAsPDF(name:string) {
    let Element = document.getElementById('Data');
  
    if (!Element) {
      console.error("Element not found!");
      return;
    } 

    html2pdf().from(Element).set({
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
    reportHeaderOneEn: string;
    reportHeaderOneAr: string;
    reportHeaderTwoEn: string;
    reportHeaderTwoAr: string;
    reportImage?: string; // can be URL or base64
    classInfo?: string;
    studentCount?: number;
    date?: string;
    tables: {
      title: string;
      headers: string[];
      data: (string | number | boolean)[][];
    }[];
    filename?: string;
  }) {
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet("Report");

    // Optional image
    let base64Image = '';
    if (options.reportImage?.startsWith('http')) {
      base64Image = await this.getBase64ImageFromUrl(options.reportImage);
    } else if (options.reportImage) {
      base64Image = options.reportImage;
    }

    // Headers
    worksheet.mergeCells('A1:E1');
    worksheet.getCell('A1').value = options.reportHeaderOneEn;
    worksheet.getCell('A1').font = { bold: true, size: 14 };
    worksheet.getCell('A1').alignment = { horizontal: 'left' };

    worksheet.mergeCells('F1:J1');
    worksheet.getCell('F1').value = options.reportHeaderOneAr;
    worksheet.getCell('F1').font = { bold: true, size: 14 };
    worksheet.getCell('F1').alignment = { horizontal: 'right' };

    worksheet.mergeCells('A2:E2');
    worksheet.getCell('A2').value = options.reportHeaderTwoEn;
    worksheet.getCell('A2').font = { size: 12 };
    worksheet.getCell('A2').alignment = { horizontal: 'left' };

    worksheet.mergeCells('F2:J2');
    worksheet.getCell('F2').value = options.reportHeaderTwoAr;
    worksheet.getCell('F2').font = { size: 12 };
    worksheet.getCell('F2').alignment = { horizontal: 'right' };

    // Add image if available
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
    if (options.classInfo) worksheet.addRow([`Class: ${options.classInfo}`]).font = { bold: true, size: 12 };
    if (options.studentCount != null) worksheet.addRow([`Number of Students: ${options.studentCount}`]).font = { bold: true, size: 12 };
    if (options.date) worksheet.addRow([`Date: ${options.date}`]).font = { bold: true, size: 12 };
    worksheet.addRow([]);
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
      worksheet.addRow([]); // Space between tables
    }
    worksheet.columns.forEach((column) => {
      column.width = 20;
    });
    const buffer = await workbook.xlsx.writeBuffer();
    FileSaver.saveAs(new Blob([buffer]), options.filename || 'Report.xlsx');
  }
}
