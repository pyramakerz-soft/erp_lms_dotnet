import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Store } from '../../../../../../Models/Inventory/store';
import { InventoryMaster } from '../../../../../../Models/Inventory/InventoryMaster';
import { InventoryMasterService } from '../../../../../../Services/Employee/Inventory/inventory-master.service';
import { StoresService } from '../../../../../../Services/Employee/Inventory/stores.service';
import * as XLSX from 'xlsx';
import { PdfPrintComponent } from '../../../../../../Component/pdf-print/pdf-print.component';
import { InventoryCategoryService } from '../../../../../../Services/Employee/Inventory/inventory-category.service';
import { InventorySubCategoriesService } from '../../../../../../Services/Employee/Inventory/inventory-sub-categories.service';
import { ShopItemService } from '../../../../../../Services/Employee/Inventory/shop-item.service';


interface FlagOption {
  id: number;
  name: string;
}

@Component({
  selector: 'app-inventory-transaction-report',
  standalone: true,
  imports: [CommonModule, FormsModule, PdfPrintComponent],
  templateUrl: './invoice-report-master.component.html',
  styleUrl: './invoice-report-master.component.css'
})
export class InventoryTransactionReportComponent implements OnInit {

  
getTableDataWithHeader(): any[] {
  return this.transactions.map(transaction => ({
    header: `Invoice #${transaction.invoiceNumber} - ${transaction.storeName} - ${new Date(transaction.date).toLocaleDateString()}`,
    data: [
      { key: 'Transaction Type', value: transaction.flagEnName },
      { key: 'Total Amount', value: transaction.total },
      { key: 'Payment Type', value: transaction.isCash ? 'Cash' : transaction.isVisa ? 'Visa' : 'Other' },
      { key: 'Notes', value: transaction.notes || 'N/A' }
    ],
    details: {
      headers: ['Item ID', 'Quantity', 'Price', 'Total Price', 'Notes'],
      data: transaction.inventoryDetails.map(detail => ({
        'Item ID': detail.id,
        'Quantity': detail.quantity,
        'Price': detail.price,
        'Total Price': detail.totalPrice,
        'Notes': detail.notes || 'N/A'
      }))
    }
  }));
}
  dateFrom: string = '';
  dateTo: string = '';
  selectedStoreId: number |null = null;
  stores: Store[] = [];
  transactions: InventoryMaster[] = [];
  showTable: boolean = false;
  isLoading: boolean = false;
  reportType: string = '';
  selectedFlagId: number = -1; // single selected id
selectedFlagIds: number[] = []; // array to send to backend

  @ViewChild(PdfPrintComponent) pdfPrintComponent!: PdfPrintComponent;

  showPDF = false;
        transactionsForExport: any[] = [];
school = {
  reportHeaderOneEn: 'Inventory Report',
  reportHeaderTwoEn: 'Transaction Summary',
  reportHeaderOneAr: 'تقرير المخزون',
  reportHeaderTwoAr: 'ملخص المعاملات',
  reportImage: 'assets/images/logo.png' 
};

@ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;

  
getStoreName(): string {
  return this.stores.find(s => s.id === this.selectedStoreId)?.name || 'All Stores';
}

getInfoRows(): any[] {
  return [
    { keyEn: 'From Date: ' + this.dateFrom },
    { keyEn: 'To Date: ' + this.dateTo },
    { keyEn: 'Store: ' + this.getStoreName() },
    // { keyEn: 'Transaction Types: ' + this.getSelectedFlagNames() }
  ];
}
  availableFlags: { [key: string]: FlagOption[] } = {
    inventory: [
      { id: 1, name: 'Opening Balances' },
      { id: 2, name: 'Addition' },
      { id: 3, name: 'Addition Adjustment' },
      { id: 4, name: 'Disbursement' },
      { id: 5, name: 'Disbursement Adjustment' },
      { id: 6, name: 'Gifts' },
      { id: 7, name: 'Damaged' },
      { id: 8, name: 'Transfer to Warehouse' }
    ],
    sales: [
      { id: 11, name: 'Sales' },
      { id: 12, name: 'Sales Returns' }
    ],
    purchase: [
      { id: 9, name: 'Purchases' },
      { id: 10, name: 'Purchase Returns' },
      { id: 13, name: 'Purchase Order' }
    ]
  };

  currentFlags: FlagOption[] = [];
  
  
  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  totalRecords: number = 0;
  selectedCategoryId: number | null = null;
  selectedSubCategoryId: number | null = null;
  selectedItemId: number | null = null;
  categories: any[] = [];
  subCategories: any[] = [];
  items: any[] = [];
  constructor(
    private route: ActivatedRoute,
    private inventoryMasterService: InventoryMasterService,
    private storesService: StoresService,
    private categoryService: InventoryCategoryService,
    private subCategoryService: InventorySubCategoriesService,
    private shopItemService: ShopItemService
  ) {}

ngOnInit() {
    this.route.data.subscribe(data => {
      this.reportType = data['reportType'];
      this.currentFlags = this.availableFlags[this.reportType];
      this.selectedFlagIds = this.currentFlags.map(flag => flag.id);
    });
    this.loadStores();
    this.loadCategories();
    this.selectedStoreId = null; // Initialize with null for "Select All"
}
    loadCategories() {
    this.categoryService.Get(this.categoryService.ApiServ.GetHeader()).subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (error) => {
        console.error('Error loading categories:', error);
      }
    });
  }

 onCategorySelected() {
    this.selectedSubCategoryId = null;
    this.selectedItemId = null;
    this.items = [];
    
    if (this.selectedCategoryId) {
      this.subCategoryService.GetByCategoryId(this.selectedCategoryId, this.subCategoryService.ApiServ.GetHeader())
        .subscribe({
          next: (subCategories) => {
            this.subCategories = subCategories;
          },
          error: (error) => {
            console.error('Error loading subcategories:', error);
          }
        });
    } else {
      this.subCategories = [];
    }
  }

  onSubCategorySelected() {
    this.selectedItemId = null;
    
    if (this.selectedSubCategoryId) {
      this.shopItemService.GetBySubCategory(this.selectedSubCategoryId, this.shopItemService.ApiServ.GetHeader())
        .subscribe({
          next: (items) => {
            this.items = items;
          },
          error: (error) => {
            console.error('Error loading items:', error);
          }
        });
    } else {
      this.items = [];
    }
  }


  // selectedFlagId: number = 0; 


  onFlagSelected() {
    this.selectedFlagIds = [];
    if (this.selectedFlagId == -1) {
      this.selectedFlagIds = this.getAllFlagsForReportType();
    } else {
      this.selectedFlagIds = [this.selectedFlagId];
    }
  }

  getAllFlagsForReportType(): number[] {
    if (this.reportType === 'inventory') {
      return [1,2,3,4,5,6,7,8];
    }
    if (this.reportType === 'sales') {
      return [11,12];
    }
    if (this.reportType === 'purchase') {
      return [9,10,13];
    }
    return [];
  }

//  onFlagsChange() {
  
//   if (this.selectedFlagIds.includes(0) && !this.selectedFlagIds.includes(0)) {
    
//     this.selectedFlagIds = [0, ...this.currentFlags.map(flag => flag.id)];
//   }
  
//   else if (!this.selectedFlagIds.includes(0) && this.selectedFlagIds.includes(0)) {
    
//     this.selectedFlagIds = [];
//   }
  
//   else if (this.selectedFlagIds.length === this.currentFlags.length && !this.selectedFlagIds.includes(0)) {
    
//     this.selectedFlagIds = [0, ...this.selectedFlagIds];
//   }
  
//   else if (this.selectedFlagIds.includes(0) && this.selectedFlagIds.length < this.currentFlags.length + 1) {
    
//     this.selectedFlagIds = this.selectedFlagIds.filter(id => id !== 0);
//   }
// }

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


viewReport() {
  if (!this.validateFilters()) return;

  this.isLoading = true;
  this.showTable = false;

  const formattedDateFrom = this.formatDateForAPI(this.dateFrom);
  const formattedDateTo = this.formatDateForAPI(this.dateTo);

  this.inventoryMasterService.searchInvoice(
    this.inventoryMasterService.ApiServ.GetHeader(),
    this.selectedStoreId,
    formattedDateFrom,
    formattedDateTo,
    this.selectedFlagIds,
    this.selectedCategoryId,
    this.selectedSubCategoryId,
    this.selectedItemId,
    this.currentPage,
    this.pageSize
  ).subscribe({
    next: (response: any) => {
      if (response?.data) {
        this.transactions = response.data;
        this.totalRecords = response.allTotal || response.data.length;
        this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
      } else {
        this.transactions = [];
      }

      this.prepareExportData();
      this.showTable = true;
      this.isLoading = false;
    },
    error: (error) => {
      console.error('Error loading transactions:', error);
      this.transactions = [];
      this.showTable = true;
      this.isLoading = false;
    }
  });
}

private prepareExportData(): void {
  this.transactionsForExport = this.transactions.map(t => ({
    'Invoice #': t.invoiceNumber,
    'Date': new Date(t.date).toLocaleDateString(),
    'Store': t.storeName,
    'Total Amount': t.total,
    'Transaction Type': t.flagEnName,
    'Notes': t.notes || 'N/A'
  }));
}

private formatDateForAPI(dateString: string): string {
  if (!dateString) return '';
  
  const date = new Date(dateString);
  if (isNaN(date.getTime())) {
    console.error('Invalid date:', dateString);
    return '';
  }

  // Format as DD/MM/YYYY (what backend expects)
  const day = date.getDate().toString().padStart(2, '0');
  const month = (date.getMonth() + 1).toString().padStart(2, '0');
  const year = date.getFullYear();
  console.log(`${day}/${month}/${year}`)
  return `${day}/${month}/${year}`;
}

private validateFilters(): boolean {
  return !!this.dateFrom && !!this.dateTo && 
         this.selectedStoreId !== undefined && // Changed from null to undefined check
         this.selectedFlagIds.length > 0;
}



  changePage(page: number) {
    this.currentPage = page;
    this.viewReport();
  }

DownloadAsPDF() {
  if (this.transactionsForExport.length === 0) {
    alert('No data to export!');
    return;
  }
  
  this.showPDF = true;
  setTimeout(() => {
    this.pdfComponentRef.downloadPDF();
    setTimeout(() => this.showPDF = false, 2000);
  }, 500);
}

Print() {
  if (this.transactionsForExport.length === 0) {
    alert('No data to print!');
    return;
  }
  
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
          body > *:not(#print-container) { display: none !important; }
          #print-container {
            display: block !important;
            position: static !important;
            width: 100% !important;
            height: auto !important;
            background: white !important;
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



// DownloadAsPDF() {
//   this.showPDF = true;
//   console.log(this.transactionsForExport);
//   setTimeout(() => {
//     this.pdfComponentRef.downloadPDF();
//     setTimeout(() => this.showPDF = false, 2000);
//   }, 500);
// }

getSelectedFlagNames(): string {
  if (this.selectedFlagIds.includes(20)) {
    return 'All Types';
  }
  return this.currentFlags
    .filter(flag => this.selectedFlagIds.includes(flag.id))
    .map(flag => flag.name)
    .join(', ');
}


printReport() {
  const data = {
    reportHeaderOneEn: 'Inventory Report',
    reportHeaderTwoEn: 'Transaction Summary',
    reportHeaderOneAr: 'تقرير المخزون',
    reportHeaderTwoAr: 'ملخص المعاملات',
    reportImage: '' // Update with your logo
  };

  const tableHeaders = ['ID', 'Invoice #', 'Date', 'Store', 'Transaction Type', 'Total Amount', 'Notes'];
  
  const tableData = this.transactions.map(t => ({
    'ID': t.id,
    'Invoice #': t.invoiceNumber,
    'Date': t.date,
    'Store': t.storeName,
    'Transaction Type': t.flagEnName,
    'Total Amount': t.total,
    'Notes': t.notes
  }));

  const infoRows = [
    { keyEn: 'From Date', valueEn: this.dateFrom },
    { keyEn: 'To Date', valueEn: this.dateTo },
    { keyEn: 'Store', valueEn: this.stores.find(s => s.id === this.selectedStoreId)?.name || 'All Stores' }
  ];

  const pdfComponent = new PdfPrintComponent();
  pdfComponent.school = data;
  pdfComponent.Title = `${this.reportType} Transaction Report`;
  pdfComponent.tableHeaders = tableHeaders;
  pdfComponent.tableData = tableData;
  pdfComponent.infoRows = infoRows;
  
  setTimeout(() => {
    pdfComponent.print();
  });
}


exportExcel() {
  const worksheet = XLSX.utils.json_to_sheet(
    this.transactions.map(t => ({
      'Invoice #': t.invoiceNumber,
      'Date': new Date(t.date).toLocaleDateString(),
      'Store': t.storeName,
      'Transaction Type': t.flagEnName,
      'Total Amount': t.total,
      'Payment Type': t.isCash ? 'Cash' : t.isVisa ? 'Visa' : 'Other',
      'Cash Amount': t.cashAmount,
      'Visa Amount': t.visaAmount,
      'Remaining Amount': t.remaining,
      'Notes': t.notes || 'N/A'
    }))
  );
  
  const workbook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(workbook, worksheet, 'Transactions');
  
  const dateStr = new Date().toISOString().slice(0,10);
  XLSX.writeFile(workbook, `Inventory_Transactions_${dateStr}.xlsx`);
}
}