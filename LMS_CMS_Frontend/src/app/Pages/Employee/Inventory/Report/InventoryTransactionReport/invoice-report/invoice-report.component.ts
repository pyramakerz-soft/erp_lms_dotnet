import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PdfPrintComponent } from '../../../../../../Component/pdf-print/pdf-print.component';
import { Store } from '../../../../../../Models/Inventory/store';
import { StoresService } from '../../../../../../Services/Employee/Inventory/stores.service';
import { InventoryFlag } from '../../../../../../Models/Inventory/inventory-flag';
import { InventoryFlagService } from '../../../../../../Services/Employee/Inventory/inventory-flag.service';


interface Invoice {
  invoiceNumber: string;
  date: Date;
  storeName: string;
  totalAmount: number;
  status: 'Paid' | 'Pending' | 'Cancelled';
}

@Component({
  selector: 'app-invoice-report',
  standalone: true,
  imports: [FormsModule, CommonModule, PdfPrintComponent],
  templateUrl: './invoice-report.component.html',
  styleUrl: './invoice-report.component.css'
})
export class InvoiceReportComponent implements OnInit {
  dateFrom: string = '';
  dateTo: string = '';
  selectedStoreId: number = 0;
  selectedStoreName: string = '';
  stores: Store[] = [];
  invoices: Invoice[] = [];
  showTable: boolean = false;
  showPDF: boolean = false;
  isLoading: boolean = false;

  selectedFlagIds: number[] = [];
  inventoryFlags: InventoryFlag[] = [];
    filteredInventoryFlags: InventoryFlag[] = [];

  @ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;


  constructor(
    private storesService: StoresService,
    private inventoryFlagService: InventoryFlagService
  ) {}
  
  ngOnInit() {
    this.loadStores();
    this.loadInventoryFlags();

  }

  loadStores() {
    this.isLoading = true;
    this.storesService.Get(this.storesService.ApiServ.GetHeader()).subscribe({
      next: (stores) => {
        this.stores = stores;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading stores:', error);
        this.isLoading = false;
      }
    });
  }
  
  loadInventoryFlags() {
    this.isLoading = true;
    this.inventoryFlagService.GetAll(this.inventoryFlagService.ApiServ.GetHeader()).subscribe({
      next: (flags) => {
        this.inventoryFlags = flags;
        // Filter flags to only show IDs 1-8
        this.filteredInventoryFlags = this.inventoryFlags.filter(flag => 
          flag.id >= 1 && flag.id <= 8
        );
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading inventory flags:', error);
        this.isLoading = false;
      }
    });
  }
    onFlagsChange() {
    // Handle Select All (value 0)
    if (this.selectedFlagIds.includes(0)) {
      this.selectedFlagIds = this.inventoryFlags.map(flag => flag.id);
    }
    // Remove Select All option if other options are deselected
    else if (this.selectedFlagIds.length !== this.inventoryFlags.length && this.selectedFlagIds.includes(0)) {
      this.selectedFlagIds = this.selectedFlagIds.filter(id => id !== 0);
    }
  }

  viewReport() {
    if (!this.validateFilters()) return;

    this.isLoading = true;
    this.showTable = false;
    
    // Prepare filter data to send to backend
    const filterData = {
      dateFrom: this.dateFrom,
      dateTo: this.dateTo,
      storeId: this.selectedStoreId,
      flagIds: this.selectedFlagIds.filter(id => id !== 0) // Exclude Select All option
    };

    console.log('Sending filter data to backend:', filterData);
    
    // In a real implementation, you would call your API here:
    // this.invoiceService.getFilteredInvoices(filterData).subscribe(...)
    
    // For demo purposes, we'll use mock data
    this.generateMockInvoices(filterData);
  }

    private generateMockInvoices(filterData: any) {
    setTimeout(() => {
      this.selectedStoreName = this.stores.find(s => s.id === this.selectedStoreId)?.name || '';
      
      // Generate mock invoices based on filters
      this.invoices = [];
      const count = Math.floor(Math.random() * 6) + 5; // 5-10 mock invoices
      
      for (let i = 0; i < count; i++) {
        const randomFlag = this.getRandomFlag(filterData.flagIds);
        
        this.invoices.push({
          invoiceNumber: `INV-${new Date().getFullYear()}-${1000 + i}`,
          date: this.randomDate(new Date(filterData.dateFrom), new Date(filterData.dateTo)),
          storeName: this.selectedStoreName,
          totalAmount: parseFloat((Math.random() * 5000 + 100).toFixed(2)),
          status: ['Paid', 'Pending', 'Cancelled'][Math.floor(Math.random() * 3)] as any,
          // flagType: randomFlag?.enName
        });
      }
      
      this.showTable = true;
      this.isLoading = false;
    }, 1000);
  }

    private getRandomFlag(flagIds: number[]): InventoryFlag | undefined {
    if (!flagIds || flagIds.length === 0) return undefined;
    const availableFlags = this.inventoryFlags.filter(flag => flagIds.includes(flag.id));
    return availableFlags[Math.floor(Math.random() * availableFlags.length)];
  }

  private randomDate(start: Date, end: Date): Date {
    return new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));
  }

  private validateFilters(): boolean {
    const hasValidFlags = this.selectedFlagIds.length > 0 && 
                         (this.selectedFlagIds.includes(0) || 
                         this.selectedFlagIds.some(id => id !== 0));
    return !!this.dateFrom && !!this.dateTo && !!this.selectedStoreId && hasValidFlags;
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
      
      setTimeout(() => {
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