import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PdfPrintComponent } from '../../../../../../Component/pdf-print/pdf-print.component';

interface Invoice {
  invoiceNumber: string;
  date: Date;
  storeName: string;
  totalAmount: number;
  status: 'Paid' | 'Pending' | 'Cancelled';
}

interface Store {
  id: number;
  name: string;
}

@Component({
  selector: 'app-invoice-report',
  standalone: true,
  imports: [FormsModule, CommonModule, PdfPrintComponent],
  templateUrl: './invoice-report.component.html',
  styleUrl: './invoice-report.component.css'
})
export class InvoiceReportComponent {
  dateFrom: string = '';
  dateTo: string = '';
  selectedStoreId: number = 0;
  selectedStoreName: string = '';
  stores: Store[] = [];
  invoices: Invoice[] = [];
  showTable: boolean = false;
  showPDF: boolean = false;

  constructor() {
    // Mock data for demonstration
    this.stores = [
      { id: 1, name: 'Main Store' },
      { id: 2, name: 'Warehouse' },
      { id: 3, name: 'Retail Outlet' }
    ];
  }

  viewReport() {
    // Mock data for demonstration - in a real app, this would come from an API
    if (this.dateFrom && this.dateTo && this.selectedStoreId) {
      this.selectedStoreName = this.stores.find(s => s.id === this.selectedStoreId)?.name || '';
      
      this.invoices = [
        {
          invoiceNumber: 'INV-2023-001',
          date: new Date('2023-01-15'),
          storeName: this.selectedStoreName,
          totalAmount: 1250.75,
          status: 'Paid'
        },
        {
          invoiceNumber: 'INV-2023-002',
          date: new Date('2023-01-18'),
          storeName: this.selectedStoreName,
          totalAmount: 899.50,
          status: 'Pending'
        },
        {
          invoiceNumber: 'INV-2023-003',
          date: new Date('2023-01-20'),
          storeName: this.selectedStoreName,
          totalAmount: 2450.00,
          status: 'Paid'
        }
      ];
      
      this.showTable = true;
    }
  }

  get invoicesForPDF() {
    return this.invoices.map(invoice => [
      invoice.invoiceNumber,
      invoice.date.toLocaleDateString(),
      invoice.storeName,
      '$' + invoice.totalAmount.toFixed(2),
      invoice.status
    ]);
  }

  print() {
    this.showPDF = true;
    setTimeout(() => {
      const printContents = document.getElementById("Data")?.innerHTML;
      if (!printContents) {
        console.error("Element not found!");
        return;
      }
  
      const printStyle = `
        <style>
          @page { size: auto; margin: 0mm; }
          body { margin: 0; }
  
          @media print {
            body > *:not(#print-container) {
              display: none !important;
            }
            #print-container {
              display: block !important;
              position: static !important;
              top: auto !important;
              left: auto !important;
              width: 100% !important;
              height: auto !important;
              background: white !important;
              box-shadow: none !important;
              margin: 0 !important;
            }
          }
        </style>
      `;
  
      const printContainer = document.createElement('div');
      printContainer.id = 'print-container';
      printContainer.innerHTML = printStyle + printContents;
  
      document.body.appendChild(printContainer);
      window.print();
      
      setTimeout(() => {``
        document.body.removeChild(printContainer);
        this.showPDF = false;
      }, 100);
    }, 500);
  }

  downloadAsPDF() {
    this.showPDF = true;
    setTimeout(() => {
      // this.pdfComponentRef.downloadPDF();
      setTimeout(() => this.showPDF = false, 2000);
    }, 500);
  }

  downloadAsExcel() {
    // This would be implemented with your reports service in a real app
    console.log('Excel download functionality would go here');
  }
}