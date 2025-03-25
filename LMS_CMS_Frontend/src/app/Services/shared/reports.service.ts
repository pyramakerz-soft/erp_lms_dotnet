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
}
