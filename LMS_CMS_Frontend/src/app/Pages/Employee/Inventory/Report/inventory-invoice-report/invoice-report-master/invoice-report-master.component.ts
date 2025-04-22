import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../../../Component/search/search.component';
import { InventoryFlag } from '../../../../../../Models/Inventory/inventory-flag';
import { InventoryMaster } from '../../../../../../Models/Inventory/InventoryMaster';
import { Store } from '../../../../../../Models/Inventory/store';
import { TokenData } from '../../../../../../Models/token-data';
import { AccountService } from '../../../../../../Services/account.service';
import { ApiService } from '../../../../../../Services/api.service';
import { DomainService } from '../../../../../../Services/Employee/domain.service';
import { InventoryFlagService } from '../../../../../../Services/Employee/Inventory/inventory-flag.service';
import { InventoryMasterService } from '../../../../../../Services/Employee/Inventory/inventory-master.service';
import { StoresService } from '../../../../../../Services/Employee/Inventory/stores.service';
import { DeleteEditPermissionService } from '../../../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../../../Services/shared/menu.service';


@Component({
  selector: 'app-inventory-report',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './invoice-report-master.component.html',
  styleUrl: './invoice-report-master.component.css'
})
export class InventoryReportComponent implements OnInit {
  // User and permission data
  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  // Table data
  TableData: InventoryMaster[] = [];
  DomainName: string = '';
  UserID: number = 0;

  // Filter properties
  dateFrom: string = '';
  dateTo: string = '';
  selectedStoreId: number = 0;
  stores: Store[] = [];
  inventoryFlag: InventoryFlag = new InventoryFlag();
  FlagId: number = 0;

  // Pagination
  CurrentPage: number = 1;
  PageSize: number = 10;
  TotalPages: number = 1;
  TotalRecords: number = 0;
  isDeleting: boolean = false;

  // Search
  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'storeName', 'invoiceNumber', 'date', 'amount'];

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public inventoryMasterService: InventoryMasterService,
    private inventoryFlagService: InventoryFlagService,
    private storesService: StoresService
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });
    this.FlagId = this.activeRoute.snapshot.data['id'];
    
    this.loadPermissions();
    this.loadStores();
    this.GetAllData(this.CurrentPage, this.PageSize);
  }

  private loadPermissions() {
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });
  }

  private loadStores() {
    this.storesService.Get(this.DomainName).subscribe({
      next: (stores) => {
        this.stores = stores;
      },
      error: (error) => {
        console.error('Error loading stores:', error);
      }
    });
  }

  // Filter methods
  applyFilters() {
    this.CurrentPage = 1;
    this.GetAllData(this.CurrentPage, this.PageSize);
  }

  resetFilters() {
    this.dateFrom = '';
    this.dateTo = '';
    this.selectedStoreId = 0;
    this.applyFilters();
  }

  // Data methods
  GetAllData(pageNumber: number, pageSize: number) {
    const filters = {
      dateFrom: this.dateFrom,
      dateTo: this.dateTo,
      storeId: this.selectedStoreId
    };

    this.inventoryMasterService.GetWithFilters(
      this.DomainName,
      this.FlagId,
      pageNumber,
      pageSize,
      filters
    ).subscribe(
      (data) => {
        this.CurrentPage = data.pagination.currentPage;
        this.PageSize = data.pagination.pageSize;
        this.TotalPages = data.pagination.totalPages;
        this.TotalRecords = data.pagination.totalRecords;
        this.TableData = data.data;
        this.inventoryFlag = data.inventoryFlag;
      },
      (error) => {
        if (error.status == 404) {
          if (this.TotalRecords != 0) {
            let lastPage = this.TotalRecords / this.PageSize;
            if (lastPage >= 1) {
              if (this.isDeleting) {
                this.CurrentPage = Math.floor(lastPage);
                this.isDeleting = false;
              } else {
                this.CurrentPage = Math.ceil(lastPage);
              }
              this.GetAllData(this.CurrentPage, this.PageSize);
            }
          }
        }
        this.TableData = [];
      }
    );
  }

  // CRUD methods
  Create() {
    this.router.navigateByUrl(`Employee/${this.inventoryFlag.enName} Item/${this.FlagId}`);
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this record?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.inventoryMasterService.Delete(id, this.DomainName).subscribe(() => {
          this.GetAllData(this.CurrentPage, this.PageSize);
        });
      }
    });
  }

  Edit(row: InventoryMaster) {
    this.router.navigateByUrl(`Employee/${this.inventoryFlag.enName} Item/Edit/${row.flagId}/${row.id}`);
  }

  View(id: number) {
    this.router.navigateByUrl(`Employee/${this.inventoryFlag.enName} Item/View/${id}`);
  }

  // Permission methods
  IsAllowDelete(InsertedByID: number) {
    return this.EditDeleteServ.IsAllowDelete(
      InsertedByID,
      this.UserID,
      this.AllowDeleteForOthers
    );
  }

  IsAllowEdit(InsertedByID: number) {
    return this.EditDeleteServ.IsAllowEdit(
      InsertedByID,
      this.UserID,
      this.AllowEditForOthers
    );
  }

  // Search methods
  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: any = await firstValueFrom(
        this.inventoryMasterService.Get(this.DomainName, this.FlagId, this.CurrentPage, this.PageSize)
      );
      this.TableData = data.data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.TableData = this.TableData.filter((t) => {
          const fieldValue = t[this.key as keyof typeof t];
          if (typeof fieldValue === 'string') {
            return fieldValue.toLowerCase().includes(this.value.toLowerCase());
          }
          if (typeof fieldValue === 'number') {
            return fieldValue === numericValue;
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.TableData = [];
    }
  }

  // Pagination methods
  changeCurrentPage(currentPage: number) {
    this.CurrentPage = currentPage;
    this.GetAllData(this.CurrentPage, this.PageSize);
  }

  validatePageSize(event: any) {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
    }
  }

  validateNumber(event: any): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      this.PageSize = 0;
    }
  }
}