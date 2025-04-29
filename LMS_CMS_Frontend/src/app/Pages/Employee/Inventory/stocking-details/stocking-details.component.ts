import { ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { Stocking } from '../../../../Models/Inventory/stocking';
import { StockingDetails } from '../../../../Models/Inventory/stocking-details';
import { StockingService } from '../../../../Services/Employee/Inventory/stocking.service';
import { StockingDetailsService } from '../../../../Services/Employee/Inventory/stocking-details.service';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Category } from '../../../../Models/Inventory/category';
import { ShopItem } from '../../../../Models/Inventory/shop-item';
import { Store } from '../../../../Models/Inventory/store';
import { SubCategory } from '../../../../Models/Inventory/sub-category';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { InventoryCategoryService } from '../../../../Services/Employee/Inventory/inventory-category.service';
import { InventorySubCategoriesService } from '../../../../Services/Employee/Inventory/inventory-sub-categories.service';
import { ShopItemService } from '../../../../Services/Employee/Inventory/shop-item.service';
import { StoresService } from '../../../../Services/Employee/Inventory/stores.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { InventoryMaster } from '../../../../Models/Inventory/InventoryMaster';
import { InventoryMasterService } from '../../../../Services/Employee/Inventory/inventory-master.service';
import html2pdf from 'html2pdf.js';
import { ReportsService } from '../../../../Services/shared/reports.service';
import { PdfPrintComponent } from '../../../../Component/pdf-print/pdf-print.component';
import { SearchDropdownComponent } from '../../../../Component/search-dropdown/search-dropdown.component';

@Component({
  selector: 'app-stocking-details',
  standalone: true,
  imports: [FormsModule, CommonModule,PdfPrintComponent, SearchDropdownComponent],
  templateUrl: './stocking-details.component.html',
  styleUrl: './stocking-details.component.css',
})
export class StockingDetailsComponent {
  User_Data_After_Login: TokenData = new TokenData(
    '',
    0,
    0,
    0,
    0,
    '',
    '',
    '',
    '',
    ''
  );

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  Data: Stocking = new Stocking();
  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  path: string = '';
  key: string = 'id';
  value: any = '';
  mode: string = 'Create';

  Stores: Store[] = [];
  Categories: Category[] = [];
  subCategories: SubCategory[] = [];

  SelectedCategoryId: number | null = null;
  SelectedSubCategoryId: number | null = null;
  SelectedSopItem: ShopItem | null = null;

  TableData: StockingDetails[] = [];
  Item: StockingDetails = new StockingDetails();
  // ShopItem: ShopItem = new ShopItem()
  MasterId: number = 0;
  editingRowId: any = null;
  validationErrors: { [key in keyof Stocking]?: string } = {};

  // IsOpenToAdd: boolean = false;
  IsSearchOpen: boolean = false;
  BarCode: string = '';
  HasBallance: boolean = false;
  AllItems: boolean = true;
  ShopItems: ShopItem[] = [];
  // MultiDetails: StockingDetails[] = [];
  FilteredDetails: StockingDetails[] = [];
  OriginDetails: StockingDetails[] = [];
  AddittionData: InventoryMaster = new InventoryMaster();
  DisbursementData: InventoryMaster = new InventoryMaster();

  AdditionId: number = 0;
  DisbursementId: number = 0;
  adiustmentAddition: InventoryMaster = new InventoryMaster();
  adiustmentDisbursement: InventoryMaster = new InventoryMaster();
  AllShopItems: ShopItem[] = [];
  isLoading = false;
  showPrintMenu = false;
  IsActualStockHiddenForBlankPrint: boolean = false;
  StoreAndDateSpanWhenPrint: boolean = false;
  NewDetailsWhenEdit: StockingDetails[] = [];
  DetailsToDeleted: StockingDetails[] = [];
  tableDataForPrint: any[] = []
  showPDF: boolean = false;
  @ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public EmployeeServ: EmployeeService,
    public storeServ: StoresService,
    public CategoriesServ: InventoryCategoryService,
    public SubCategoriesServ: InventorySubCategoriesService,
    public shopitemServ: ShopItemService,
    public StockingServ: StockingService,
    public StockingDetailsServ: StockingDetailsService,
    public InventoryMastrServ: InventoryMasterService,
    private cdr: ChangeDetectorRef,
    public printservice: ReportsService
  ) { }
  async ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.MasterId = Number(this.activeRoute.snapshot.paramMap.get('id'));

    await this.GetAllStores();
    if (!this.MasterId) {
      this.mode = 'Create';
      this.Data.date = new Date().toISOString().split('T')[0];
    } else {
      this.mode = 'Edit';
      this.GetTableDataByID();
      this.GetMasterInfo();
    }

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });

    this.shopitemServ.Get(this.DomainName).subscribe((d) => {
      this.AllShopItems = d;
    });
  }

  moveToMaster() {
    this.router.navigateByUrl(`Employee/Stocking`);
  }

  ////////////////////////////////////////////////////// Get Data

  GetAllStores() {
    this.storeServ.Get(this.DomainName).subscribe((d) => {
      this.Stores = d;
    });
  }

  GetMasterInfo() {
    this.StockingServ.GetById(this.MasterId, this.DomainName).subscribe((d) => {
      this.Data = d;
      this.GetCategories();
    });
  }

  onStoreChange(storeID: number) {
    this.onInputValueChange({ field: 'storeID', value: storeID });
    if (storeID) {
      this.SelectedCategoryId = null;
      this.GetCategories();
    }
  }

  GetCategories() {
    this.Categories = [];
    this.CategoriesServ.GetByStoreId(
      this.DomainName,
      this.Data.storeID
    ).subscribe(
      (d) => {
        this.Categories = d;
        this.subCategories = [];
        this.SelectedSubCategoryId = null;
      },
      (error) => {
        this.Categories = [];
        this.subCategories = [];
        this.SelectedSubCategoryId = null;
      }
    );
  }

  selectCategory(categoryId: number) {
    this.GetSubCategories(categoryId);
    this.SelectedCategoryId = categoryId;
    if (this.AllItems) {
      this.GetAllShopItems(categoryId);
    }
  }

  GetAllShopItems(categoryId: number) {
    this.StockingDetailsServ.GetCurrentStockForAllItems(
      this.Data.storeID,
      categoryId,
      this.Data.date,
      this.DomainName
    ).subscribe((d) => {
      this.ShopItems = d;
      this.FilteredDetails = this.ShopItems.map((item) => ({
        id: Date.now() + Math.floor(Math.random() * 1000),
        insertedAt: '',
        insertedByUserId: 0,
        currentStock: item.currentStock,
        actualStock: 0,
        theDifference: 0,
        shopItemID: item.id,
        stockingId: this.MasterId,
        shopItemName: item.enName,
        barCode: item.barCode,
        ItemPrice: item.purchasePrice ?? 0,
      }));
      this.OriginDetails = [...this.OriginDetails, ...this.FilteredDetails];
      if (this.mode == 'Create') {
        if (!this.Data.stockingDetails) {
          this.Data.stockingDetails = [];
        }
        this.Data.stockingDetails = this.Data.stockingDetails.concat(
          this.FilteredDetails
        );
        if (this.HasBallance) {
          this.Data.stockingDetails = this.OriginDetails.filter(
            (d) => d.currentStock != 0
          );
        }
      } else if (this.mode == 'Edit') {
        if (!this.TableData) {
          this.TableData = [];
        }
        this.TableData = this.TableData.concat(this.FilteredDetails);
        this.NewDetailsWhenEdit = this.NewDetailsWhenEdit.concat(
          this.FilteredDetails
        );
        if (this.HasBallance) {
          this.TableData = this.TableData.filter((d) => d.currentStock !== 0);
        }
      }
    });
  }

  GetSubCategories(categoryId: number) {
    this.subCategories = [];
    this.SubCategoriesServ.GetByCategoryId(
      categoryId,
      this.DomainName
    ).subscribe((d) => {
      this.subCategories = d;
    });
  }

  GetItems() {
    if (this.SelectedSubCategoryId)
      this.StockingDetailsServ.GetCurrentStockForAllItemsBySub(
        this.Data.storeID,
        this.SelectedSubCategoryId,
        this.Data.date,
        this.DomainName
      ).subscribe((d) => {
        this.ShopItems = d;
        if ((this, this.AllItems)) {
          this.FilteredDetails = this.ShopItems.map((item) => ({
            id: Date.now() + Math.floor(Math.random() * 1000),
            insertedAt: '',
            insertedByUserId: 0,
            currentStock: item.currentStock,
            actualStock: 0,
            theDifference: 0,
            shopItemID: item.id,
            stockingId: this.MasterId,
            shopItemName: item.enName,
            barCode: item.barCode,
            ItemPrice: item.purchasePrice ?? 0,
          }));
          this.OriginDetails = this.OriginDetails.concat(this.FilteredDetails);
          if (this.mode == 'Create') {
            this.Data.stockingDetails = this.Data.stockingDetails.concat(
              this.FilteredDetails
            );
            if (this.HasBallance) {
              this.Data.stockingDetails = this.OriginDetails.filter(
                (d) => d.currentStock != 0
              );
            }
          } else if (this.mode == 'Edit') {
            this.TableData = this.TableData.concat(this.FilteredDetails);
            this.NewDetailsWhenEdit = this.NewDetailsWhenEdit.concat(
              this.FilteredDetails
            );
            if (this.HasBallance) {
              this.TableData = this.OriginDetails.filter(
                (d) => d.currentStock != 0
              );
            }
          }
        }
      });
  }

  selectSubCategory(subCategoryId: number) {
    this.SelectedSubCategoryId = subCategoryId;
    this.ShopItems = [];
    this.GetItems();
  }

  selectShopItem(item: ShopItem) {
    const newItem = {
      id: Date.now() + Math.floor(Math.random() * 1000),
      insertedAt: '',
      insertedByUserId: 0,
      currentStock: item.currentStock,
      actualStock: 0,
      theDifference: 0,
      shopItemID: item.id,
      stockingId: this.MasterId,
      shopItemName: item.enName,
      barCode: item.barCode,
      ItemPrice: item.purchasePrice ?? 0,
    };
    this.OriginDetails.push(newItem);
    if (this.mode === 'Create') {
      this.Data.stockingDetails.push(newItem);
      if (this.HasBallance) {
        this.Data.stockingDetails = this.OriginDetails.filter(
          (d) => d.currentStock != 0
        );
      }
    } else if (this.mode === 'Edit') {
      this.TableData.push(newItem);
      this.NewDetailsWhenEdit.push(newItem);
      if (this.HasBallance) {
        this.TableData = this.OriginDetails.filter((d) => d.currentStock != 0);
      }
    }
  }

  SearchToggle() {
    this.IsSearchOpen = true;
    setTimeout(() => {
      const input = document.querySelector(
        'input[type="number"]'
      ) as HTMLInputElement;
      if (input) input.focus();
    }, 100);
  }

  CloseSearch() {
    this.IsSearchOpen = false;
    this.BarCode = '';
  }

  SearchOnBarCode() {
    if (!this.BarCode) return;
    this.shopitemServ.GetByBarcode(this.BarCode, this.DomainName).subscribe(
      (d) => {
        const detail: StockingDetails = {
          id: Date.now() + Math.floor(Math.random() * 1000),
          insertedAt: '',
          insertedByUserId: 0,
          currentStock: d.currentStock,
          actualStock: 0,
          theDifference: 0,
          shopItemID: d.id,
          stockingId: this.MasterId,
          shopItemName: d.arName,
          barCode: d.barCode,
          ItemPrice: d.purchasePrice ?? 0,
        };
        this.OriginDetails.push(detail);
        if (this.mode == 'Create') {
          this.Data.stockingDetails.push(detail);
          if (this.HasBallance) {
            this.Data.stockingDetails = this.OriginDetails.filter(
              (d) => d.currentStock != 0
            );
          }
        } else if (this.mode == 'Edit') {
          this.TableData.push(detail);
          this.NewDetailsWhenEdit.push(detail);
          if (this.HasBallance) {
            this.TableData = this.OriginDetails.filter(
              (d) => d.currentStock != 0
            );
          }
        }
        this.BarCode = ''; // Clear input after search
      },
      (error) => {
        Swal.fire({
          icon: 'error',
          title: 'This Item Not Exist',
          confirmButtonText: 'Okay',
          customClass: { confirmButton: 'secondaryBg' },
        });
      }
    );
  }

  async GetTableDataByID(): Promise<void> {
    return new Promise((resolve) => {
      this.StockingDetailsServ.GetBySalesId(
        this.MasterId,
        this.DomainName
      ).subscribe(
        (d) => {
          this.TableData = d;
          this.TableData = this.TableData.map((row) => ({
            ...row,
            stockingId: this.MasterId,
          }));
          this.OriginDetails = this.TableData;
          resolve();
        },
        (error) => {
          this.TableData = [];
        }
      );
    });
  }

  toggleHasBalance() {
    if (this.HasBallance == true) {
      if (this.mode == 'Create') {
        this.Data.stockingDetails = this.OriginDetails.filter(
          (s) => s.currentStock != 0
        );
      } else if (this.mode == 'Edit') {
        this.TableData = this.OriginDetails.filter((s) => s.currentStock != 0);
      }
    } else if (this.HasBallance == false) {
      if (this.mode == 'Create') {
        this.Data.stockingDetails = this.OriginDetails;
      } else if (this.mode == 'Edit') {
        this.TableData = this.OriginDetails;
      }
    }
  }
  /////////////////////////////////////////////////////// CRUD

  Save() {
    if (this.isFormValid()) {
      this.isLoading = true;
      if (this.mode == 'Create') {
        this.StockingServ.Add(this.Data, this.DomainName).subscribe(
          (d) => {
            this.MasterId = d;
            this.router.navigateByUrl(`Employee/Stocking`);
          },
          (error) => {
            this.isLoading = false;
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          }
        );
      }
      if (this.mode == 'Edit') {
        this.DetailsToDeleted = this.OriginDetails.filter(
          (originItem) =>
            this.HasBallance == true && originItem.currentStock == 0
        );
        console.log(this.DetailsToDeleted);
        this.DetailsToDeleted.forEach((element) => {
          this.StockingDetailsServ.Delete(
            element.id,
            this.DomainName
          ).subscribe((d) => { });
        });
        this.Data.stockingDetails = this.TableData;
        this.StockingDetailsServ.Edit(
          this.Data.stockingDetails,
          this.DomainName
        ).subscribe((d) => { });
        this.StockingDetailsServ.Add(
          this.NewDetailsWhenEdit,
          this.DomainName
        ).subscribe(
          (d) => { },
          (error) => { }
        );
        this.StockingServ.Edit(this.Data, this.DomainName).subscribe(
          (d) => {
            this.router.navigateByUrl(`Employee/Stocking`);
          },
          (error) => {
            this.isLoading = false;
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          }
        );
      }
    }
  }

  Edit(row: StockingDetails) {
    this.editingRowId = row.id;
  }

  Delete(row: StockingDetails) {
    if (this.mode == 'Edit') {
      Swal.fire({
        title: 'Are you sure you want to delete this Item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#FF7519',
        cancelButtonColor: '#17253E',
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
      }).then((result) => {
        if (result.isConfirmed) {
          if (!this.NewDetailsWhenEdit.find((s) => s.id == row.id)) {
            this.StockingDetailsServ.Delete(row.id, this.DomainName).subscribe(
              async (D) => {
                await this.GetTableDataByID();
              }
            );
          } else {
            this.NewDetailsWhenEdit = this.NewDetailsWhenEdit.filter(
              (s) => s.id != row.id
            );
            this.TableData = this.TableData.filter((s) => s.id != row.id);
          }
        }
      });
    } else if (this.mode == 'Create') {
      Swal.fire({
        title: 'Are you sure you want to delete this Item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#FF7519',
        cancelButtonColor: '#17253E',
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
      }).then((result) => {
        if (result.isConfirmed) {
          this.Data.stockingDetails = this.Data.stockingDetails.filter(
            (item) => item.id !== row.id
          );
        }
      });
    }
  }

  onStockChangeWhenEditRow(row: StockingDetails): void {
    row.theDifference = row.actualStock - row.currentStock;
  }

  onStockChangeWhenAddRow(row: StockingDetails): void {
    row.theDifference = row.actualStock - row.currentStock;
  }

  ///////////////////////////////////// validation fOR Master

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.Data) {
      if (this.Data.hasOwnProperty(key)) {
        const field = key as keyof Stocking;
        if (!this.Data[field]) {
          if (field == 'date') {
            this.validationErrors[field] = `*${this.capitalizeField(
              field
            )} is required`;
            isValid = false;
          }
        }
      }
    }
    if (this.mode == 'Create' && this.Data.stockingDetails.length == 0) {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'Stocking Details Is Required',
        confirmButtonColor: '#FF7519',
      });
      return false;
    }
    if (this.mode == 'Edit' && this.TableData.length == 0) {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'Stocking Details Is Required',
        confirmButtonColor: '#FF7519',
      });
      return false;
    }

    return isValid;
  }

  capitalizeField(field: keyof Stocking): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof Stocking; value: any }) {
    const { field, value } = event;
    (this.Data as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  ////////////////////////////////////////// Authorization

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(
      InsertedByID,
      this.UserID,
      this.AllowDeleteForOthers
    );
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(
      InsertedByID,
      this.UserID,
      this.AllowEditForOthers
    );
    return IsAllow;
  }

  ////////////////////////////////////////// Adjustment

  async Adjustment() {
    if (!this.isFormValid()) return;
    this.isLoading = true;
    try {
      if (this.mode === 'Create') {
        this.TableData = this.Data.stockingDetails;
        const addedData = await this.StockingServ.Add(
          this.Data,
          this.DomainName
        ).toPromise();
        this.Data.id = addedData;
        this.MasterId = addedData;
        const result = await this.StockingServ.GetById(
          this.Data.id,
          this.DomainName
        ).toPromise();
        if (result) this.Data = result;
        this.Data.additionId = await this.prepareAdjustment(
          2,
          (s) => s.theDifference > 0
        );
        this.Data.disbursementId = await this.prepareAdjustment(
          4,
          (s) => s.theDifference < 0
        );
      }
      if (this.mode === 'Edit') {
        if (this.Data.additionId != 0) {
          try {
            await this.InventoryMastrServ.Delete(
              this.Data.additionId,
              this.DomainName
            ).toPromise();
          } catch (error) {
            console.error('Error deleting additionId:', error);
          }
        }
        if (this.Data.disbursementId != 0) {
          try {
            await this.InventoryMastrServ.Delete(
              this.Data.disbursementId,
              this.DomainName
            ).toPromise();
          } catch (error) {
            console.error('Error deleting disbursementId:', error);
          }
        }
        this.Data.additionId = await this.prepareAdjustment(
          2,
          (s) => s.theDifference > 0
        );
        this.Data.disbursementId = await this.prepareAdjustment(
          4,
          (s) => s.theDifference < 0
        );
      }
      await this.StockingServ.Edit(this.Data, this.DomainName).toPromise();
      this.router.navigateByUrl(`Employee/Stocking`);
    } catch (error) {
      console.error('Unexpected error in Adjustment():', error);
    } finally {
      this.isLoading = false;
    }
  }

  private async prepareAdjustment(
    flagId: number,
    filterCondition: (item: any) => boolean
  ) {
    this.adiustmentDisbursement.date = this.Data.date;
    this.adiustmentDisbursement.storeID = this.Data.storeID;
    this.adiustmentDisbursement.flagId = flagId;
    this.adiustmentDisbursement.inventoryDetails = this.TableData.filter(
      filterCondition
    ).map((item) => {
      const foundItem = this.AllShopItems.find((s) => s.id == item.shopItemID);
      const price = foundItem?.purchasePrice ?? 0;
      const quantity = item.theDifference ?? 0;
      return {
        id: Date.now() + Math.floor(Math.random() * 1000),
        insertedAt: '',
        barCode: '',
        name: '',
        shopItemName: '',
        salesName: '',
        notes: '',
        insertedByUserId: 0,
        shopItemID: item.shopItemID,
        quantity: -1 * quantity,
        price: price,
        totalPrice: -1 * price * quantity,
        inventoryMasterId: this.MasterId,
      };
    });
    this.adiustmentDisbursement.total =
      this.adiustmentDisbursement.inventoryDetails.reduce(
        (sum, item) => sum + (item.totalPrice ?? 0),
        0
      );
    if (this.adiustmentDisbursement.inventoryDetails.length > 0) {
      const response = await this.InventoryMastrServ.Add(
        this.adiustmentDisbursement,
        this.DomainName
      ).toPromise();
      return response;
    } else return;
  }

  //////// print

  togglePrintMenu() {
    this.showPrintMenu = !this.showPrintMenu;
  }

  async selectPrintOption(type: string) {
    this.showPrintMenu = false;
    this.StoreAndDateSpanWhenPrint = true;
    switch (type) {
      case 'Blank':
        await this.Blank();
        break;
      case 'Differences':
        await this.Differences();
        break;
      case 'Print':
        await this.Print();
        break;
      case 'Receipt':
        await this.Receipt();
        break;
    }
    this.StoreAndDateSpanWhenPrint = false; 
  }
  
  async Blank() {
    const backupTableData = JSON.parse(JSON.stringify(this.TableData)); // 🛡 Deep copy
    this.TableData.forEach((row) => {
      row.actualStock = "";
      row.theDifference = "";
    });
    this.cdr.detectChanges();
    return new Promise<void>((resolve) => {
      setTimeout(async () => {
        await this.Print();
        this.TableData = backupTableData; // ✅ Safe, not modified
        this.cdr.detectChanges();
        resolve();
      }, 300);
    });
  }

  async Differences() {
    const backupTableData = JSON.parse(JSON.stringify(this.TableData)); // 🛡 Deep copy
    this.TableData = this.TableData.filter((f) => f.theDifference != 0);
    this.cdr.detectChanges();
    return new Promise<void>((resolve) => {
      setTimeout(async () => {
        await this.Print();
        this.TableData = backupTableData;
        this.cdr.detectChanges();
        resolve();
      }, 300);
    });
  }

  async Print() {
    await this.formateData()
    this.showPDF = true;
    setTimeout(() => {
      const printContents = document.getElementById("Data")?.innerHTML;
      if (!printContents) {
        console.error("Element not found!");
        return;
      }
      // Create a print-specific stylesheet
      const printStyle = `
      <style>
      @page { size: auto; margin: 0mm; }
      body { 
            margin: 0; 
          }
  
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
  
  async DownloadAsPDF() {
    this.showPDF = true;
    await this.formateData()
    setTimeout(() => {
      this.pdfComponentRef.downloadPDF();
      setTimeout(() => this.showPDF = false, 2000);
    }, 500);
  }

  formateData() {
    const sourceData = this.mode === "Create" ? this.Data?.stockingDetails ?? [] : this.TableData ?? [];
    this.tableDataForPrint = sourceData.map((row) => {
      return {
        BarCode: row.barCode || '',
        Item_Id: row.shopItemID || '',
        Item_Name: row.shopItemName || '',
        Current_Stock: row.currentStock || '',
        Actual_Stock: row.actualStock || '',
        The_Difference: row.theDifference || '',
      };
    });
  }
  
  async DownloadAsExcel() {
    const tableHeaders = [
      'Bar Code',
      'Item ID',
      'Item Name',
      'Current Stock',
      'Actual Stock',
      'The Difference',
    ];

    const tableData = (
      this.mode === 'Create' ? this.Data.stockingDetails : this.TableData || []
    ).map((row) => {
      return [
        row.barCode || '',
        row.shopItemID || '',
        row.shopItemName || '',
        row.currentStock ?? 0,
        this.IsActualStockHiddenForBlankPrint ? '' : row.actualStock ?? '',
        this.IsActualStockHiddenForBlankPrint ? '' : row.theDifference ?? '',
      ];
    });

    const tables = [
      {
        title: 'Inventory Details',
        headers: tableHeaders,
        data: tableData,
      },
    ];

    await this.printservice.generateExcelReport({
      filename: 'Inventory.xlsx',
      mainHeader: {
        en: 'Inventory Report',
        ar: 'تقرير المخزون',
      },
      subHeaders: [
        {
          en: 'Generated on: ' + new Date().toLocaleString(),
          ar: 'تاريخ الإنشاء: ' + new Date().toLocaleString(),
        },
      ],
      tables: tables,
    });
  }

  async Receipt() {
    await this.formateData();
    this.showPDF = true;
  
    setTimeout(() => {
      const printContents = document.getElementById("Data")?.innerHTML;
      if (!printContents) {
        console.error("Element not found!");
        return;
      }
  
      const printStyle = `
        <style>
        @page { 
          size: 80mm 200mm; /* Width: 80mm like POS printer, Height: auto */
          margin: 5mm;
        }
        body {
          margin: 0;
          padding: 0;
        }
        @media print {
          body > *:not(#print-container) {
            display: none !important;
          }
          #print-container {
            display: block !important;
            position: static !important;
            width: 80mm; /* Receipt width */
            min-height: 200mm; /* You can adjust */
            background: white !important;
            box-shadow: none !important;
            margin: 0 auto !important;
            padding: 5mm;
            font-size: 12px;
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

}


  