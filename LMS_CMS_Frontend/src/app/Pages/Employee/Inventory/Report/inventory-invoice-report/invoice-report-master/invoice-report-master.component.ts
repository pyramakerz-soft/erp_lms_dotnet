// inventory-transaction-report.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Store } from '../../../../../../Models/Inventory/store';
import { InventoryMaster } from '../../../../../../Models/Inventory/InventoryMaster';
import { InventoryMasterService } from '../../../../../../Services/Employee/Inventory/inventory-master.service';
import { StoresService } from '../../../../../../Services/Employee/Inventory/stores.service';

interface FlagOption {
  id: number;
  name: string;
}

@Component({
  selector: 'app-inventory-transaction-report',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './invoice-report-master.component.html',
  styleUrl: './invoice-report-master.component.css'
})
export class InventoryTransactionReportComponent implements OnInit {
  dateFrom: string = '';
  dateTo: string = '';
  selectedStoreId: number = 0;
  selectedFlagIds: number[] = [];
  stores: Store[] = [];
  transactions: InventoryMaster[] = [];
  showTable: boolean = false;
  isLoading: boolean = false;
  reportType: string = '';
  
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
  
  // Pagination
  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  totalRecords: number = 0;

  constructor(
    private route: ActivatedRoute,
    private inventoryMasterService: InventoryMasterService,
    private storesService: StoresService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.reportType = data['reportType'];
      this.currentFlags = this.availableFlags[this.reportType];
      this.selectedFlagIds = this.currentFlags.map(flag => flag.id);
    });
    this.loadStores();
  }

 onFlagsChange() {
  // If "Select All" (0) is selected and wasn't selected before
  if (this.selectedFlagIds.includes(0) && !this.selectedFlagIds.includes(0)) {
    // Select all available flags for this report type
    this.selectedFlagIds = [0, ...this.currentFlags.map(flag => flag.id)];
  }
  // If "Select All" is deselected
  else if (!this.selectedFlagIds.includes(0) && this.selectedFlagIds.includes(0)) {
    // Remove all flags
    this.selectedFlagIds = [];
  }
  // If manually selecting all flags
  else if (this.selectedFlagIds.length === this.currentFlags.length && !this.selectedFlagIds.includes(0)) {
    // Add the "Select All" option
    this.selectedFlagIds = [0, ...this.selectedFlagIds];
  }
  // If deselecting some flags while "Select All" is selected
  else if (this.selectedFlagIds.includes(0) && this.selectedFlagIds.length < this.currentFlags.length + 1) {
    // Remove "Select All" option
    this.selectedFlagIds = this.selectedFlagIds.filter(id => id !== 0);
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

viewReport() {
  if (!this.validateFilters()) return;

  this.isLoading = true;
  this.showTable = false;
  
  // Get flags to fetch (excluding the "Select All" option 0)
  const flagsToFetch = this.selectedFlagIds.includes(0) 
    ? this.currentFlags.map(flag => flag.id)
    : this.selectedFlagIds.filter(id => id !== 0);

  // Format dates as DD/MM
  const formattedDateFrom = this.formatDateForAPI(this.dateFrom);
  const formattedDateTo = this.formatDateForAPI(this.dateTo);

  this.inventoryMasterService.search(
    this.inventoryMasterService.ApiServ.GetHeader(),
    this.selectedStoreId,
    formattedDateFrom,
    formattedDateTo,
    flagsToFetch,
    this.currentPage,
    this.pageSize
  ).subscribe({
    next: (response: any) => {
      // Handle response as before
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

  private formatDateForAPI(dateString: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    return `${day}/${month}`;
  }

  private validateFilters(): boolean {
    const hasValidFlags = this.selectedFlagIds.length > 0 && 
                         (this.selectedFlagIds.includes(0) || 
                         this.selectedFlagIds.some(id => id !== 0));
    return !!this.dateFrom && !!this.dateTo && !!this.selectedStoreId && hasValidFlags;
  }

  changePage(page: number) {
    this.currentPage = page;
    this.viewReport();
  }
}