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
  selector: 'app-invoice-report-master-detailed',
  standalone: true,
  imports: [CommonModule, FormsModule, PdfPrintComponent],
  templateUrl: './invoice-report-master-detailed.component.html',
  styleUrls: ['./invoice-report-master-detailed.component.css']
})
export class InvoiceReportMasterDetailedComponent implements OnInit {
  dateFrom: string = '';
  dateTo: string = '';
selectedStoreId: number | null = null;
  stores: Store[] = [];
  transactions: InventoryMaster[] = [];
  showTable: boolean = false;
  isLoading: boolean = false;
  reportType: string = '';
  selectedFlagId: number = -1;
  selectedFlagIds: number[] = [];
  
  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  totalRecords: number = 0;

  @ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;
  showPDF = false;
  transactionsForExport: any[] = [];
  
  school = {
    reportHeaderOneEn: 'Inventory Report',
    reportHeaderTwoEn: 'Transaction Details',
    reportHeaderOneAr: 'تقرير المخزون',
    reportHeaderTwoAr: 'تفاصيل المعاملة',
    reportImage: 'assets/images/logo.png'
  };

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
  
  if (this.selectedCategoryId === null) {
    this.subCategories = [];
    // Explicitly disable the dependent dropdowns
    this.selectedSubCategoryId = null;
    this.selectedItemId = null;
  } else {
    this.subCategoryService.GetByCategoryId(this.selectedCategoryId, this.subCategoryService.ApiServ.GetHeader())
      .subscribe({
        next: (subCategories) => {
          this.subCategories = subCategories;
        },
        error: (error) => {
          console.error('Error loading subcategories:', error);
        }
      });
  }
}

onSubCategorySelected() {
  this.selectedItemId = null;
  
  if (this.selectedSubCategoryId === null) {
    this.items = [];
  } else {
    this.shopItemService.GetBySubCategory(this.selectedSubCategoryId, this.shopItemService.ApiServ.GetHeader())
      .subscribe({
        next: (items) => {
          this.items = items;
        },
        error: (error) => {
          console.error('Error loading items:', error);
        }
      });
  }
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

viewReport() {
  if (!this.validateFilters()) return;

  this.isLoading = true;
  this.showTable = false;

  const formattedDateFrom = this.formatDateForAPI(this.dateFrom);
  const formattedDateTo = this.formatDateForAPI(this.dateTo);

  this.inventoryMasterService.search(
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
        if (Array.isArray(response)) {
          this.transactions = response;
          this.totalRecords = response.length;
          this.totalPages = Math.ceil(response.length / this.pageSize);
        } else if (response?.data) {
          this.transactions = response.data;
          this.totalRecords = response.pagination?.totalRecords || response.data.length;
          this.totalPages = response.pagination?.totalPages || Math.ceil(response.data.length / this.pageSize);
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
    header: `Invoice #${t.invoiceNumber}`,
    summary: [
      { key: 'Date', value: new Date(t.date).toLocaleDateString() },
      { key: 'Store', value: t.storeName },
      { key: 'Transaction Type', value: t.flagEnName },
      { key: 'Total Amount', value: t.total },
      { key: 'All Total', value: this.totalRecords },
      { key: 'Notes', value: t.notes || 'N/A' }
    ],
    table: {
      headers: ['ID', 'Item ID', 'Name', 'Quantity', 'Price', 'Total Price', 'Notes'],
      data: t.inventoryDetails?.map(d => ({
        'ID': d.id,
        'Item ID': d.shopItemID,
        'Name': d.shopItemName || d.name || 'N/A',
        'Quantity': d.quantity,
        'Price': d.price,
        'Total Price': d.totalPrice,
        // 'Category': d.categoryId || 'N/A',
        // 'SubCategory': d.subCategoryId || 'N/A',
        'Notes': d.notes || 'N/A'
      })) || []
    }
  }));
}


getTableDataWithHeader(): any[] {
  return this.transactionsForExport.map(item => ({
    header: item.header,
    data: item.summary,
    tableHeaders: item.table.headers,
    tableData: item.table.data
  }));
}

  getInfoRows(): any[] {
    return [
      { keyEn: 'From Date: ' + this.dateFrom, valueEn: '' },
      { keyEn: 'To Date: ' + this.dateTo, valueEn: '' },
      { keyEn: 'Store: ' + this.getStoreName(), valueEn: '' }
    ];
  }

  getStoreName(): string {
    return this.stores.find(s => s.id === this.selectedStoreId)?.name || 'All Stores';
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
  return !!this.dateFrom && !!this.dateTo && this.selectedFlagIds.length > 0;
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
    if (this.transactionsForExport.length == 0) {
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

  exportExcel() {
    const data = this.transactions.flatMap(t => 
      t.inventoryDetails.map(d => ({
        'Invoice #': t.invoiceNumber,
        'Date': new Date(t.date).toLocaleDateString(),
        'Store': t.storeName,
        'Transaction Type': t.flagEnName,
        'Total Amount': t.total,
        'Item ID': d.shopItemID,
        'Quantity': d.quantity,
        'Price': d.price,
        'Total Price': d.totalPrice,
        'Notes': d.notes || 'N/A'
      }))
    );

    const worksheet = XLSX.utils.json_to_sheet(data);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Transaction Details');
    
    const dateStr = new Date().toISOString().slice(0,10);
    XLSX.writeFile(workbook, `Inventory_Transaction_Details_${dateStr}.xlsx`);
  }
}