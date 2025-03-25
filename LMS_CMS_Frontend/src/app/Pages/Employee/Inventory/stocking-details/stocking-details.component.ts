import { ChangeDetectorRef, Component } from '@angular/core';
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

@Component({
  selector: 'app-stocking-details',
  standalone: true,
  imports: [FormsModule, CommonModule],
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

  IsOpenToAdd: boolean = false;
  IsSearchOpen: boolean = false;
  BarCode: string = "";
  HasBallance: boolean = false;
  AllItems: boolean = true;
  ShopItems: ShopItem[] = [];
  FilteredShopItems: ShopItem[] = [];
  MultiDetails: StockingDetails[] = [];
  FilteredDetails: StockingDetails[] = [];
  AddittionData: InventoryMaster = new InventoryMaster();
  DisbursementData: InventoryMaster = new InventoryMaster();

  AdditionId: number = 0;
  DisbursementId: number = 0;
  adiustmentAddition: InventoryMaster = new InventoryMaster();
  adiustmentDisbursement: InventoryMaster = new InventoryMaster();
  AllShopItems: ShopItem[] = [];
  isLoading = false;
  showPrintMenu = false;
  IsActualStockHiddenForBlankPrint: boolean = false

  SelectedCategoryIds: number[] = [];
  SelectedSubCategoryIds: number[] = [];
  SelectedSopItems: ShopItem[] = [];


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
    private cdr: ChangeDetectorRef
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
    });
  }

  onStoreChange(storeID: number) {
    this.onInputValueChange({ field: 'storeID', value: storeID });
    if (storeID) {
      this.GetCategories();
    }
  }

  GetCategories() {
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
    const index = this.SelectedCategoryIds.indexOf(categoryId);
    if (index > -1) {
      this.SelectedCategoryIds.splice(index, 1);
      this.UnSelectCategory(categoryId)
    } else {
      this.SelectedCategoryIds.push(categoryId);
      this.GetSubCategories(categoryId);
      this.GetAllShopItems(categoryId);  // Load items for each
      console.log("addFilteredShopItems", this.FilteredShopItems)
    }
  }

  UnSelectCategory(CategoryId: number) {
    this.subCategories = this.subCategories.filter(s => s.inventoryCategoriesID != CategoryId);

    const remainingSubCategoryIds = this.subCategories.map(sc => sc.id);
    this.FilteredShopItems = this.FilteredShopItems.filter(item =>
      remainingSubCategoryIds.includes(item.inventorySubCategoriesID)
    );
    this.SelectedSopItems = this.FilteredShopItems
    const remainingFilteredDetailsIds = this.FilteredShopItems.map(sc => sc.id);
    this.FilteredDetails = this.FilteredDetails.filter(item =>
      remainingFilteredDetailsIds.includes(item.shopItemID)
    );
    console.log("FilteredShopItems", this.FilteredShopItems)
  }

  GetAllShopItems(categoryId: number) {
    this.StockingDetailsServ.GetCurrentStockForAllItems(
      this.Data.storeID,
      categoryId,
      this.Data.date,
      this.DomainName
    ).subscribe((d) => {
      console.log(d)
      const newItems = d.filter(item => !this.ShopItems.some(existing => existing.id === item.id));
      this.ShopItems = [...this.ShopItems, ...newItems];
      this.SelectedSopItems = this.ShopItems
      this.FilteredShopItems = this.ShopItems;
      const newDetails = newItems.map((item) => ({
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
      const uniqueDetails = newDetails.filter(detail =>
        !this.MultiDetails.some(existing => existing.shopItemID === detail.shopItemID)
      );
      this.MultiDetails = [...this.MultiDetails, ...uniqueDetails];
      this.FilteredDetails = this.MultiDetails
      if (this.HasBallance) {
        this.FilteredDetails = this.MultiDetails.filter((f) => f.currentStock != 0);
      }
    });
  }

  GetSubCategories(categoryId: number) {
    this.SubCategoriesServ.GetByCategoryId(categoryId, this.DomainName).subscribe((d) => {
      const newSubCats = d.filter(cat => !this.subCategories.some(existing => existing.id === cat.id));
      this.subCategories = [...this.subCategories, ...newSubCats];
      this.SelectedSubCategoryIds = this.subCategories.map(s => s.id)
      console.log(d)
    });
  }

  selectSubCategory(subCategoryId: number) {
    const index = this.SelectedSubCategoryIds.indexOf(subCategoryId);
    if (index > -1) {
      this.SelectedSubCategoryIds.splice(index, 1);

    } else {
      this.SelectedSubCategoryIds.push(subCategoryId);
    }
    this.SelectedSopItems = this.ShopItems.filter(
      (s) => this.SelectedSubCategoryIds.includes(s.inventorySubCategoriesID)
    );
    this.FilteredDetails = this.SelectedSopItems.map((item) => ({
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
    if (this.HasBallance) {
      this.FilteredDetails = this.FilteredDetails.filter((f) => f.currentStock != 0);
    }
  }

  selectShopItem(item: ShopItem) {
    const exists = this.SelectedSopItems.find((i) => i.id === item.id);
    if (exists) {
      this.SelectedSopItems = this.SelectedSopItems.filter((i) => i.id !== item.id);
      this.SelectedSubCategoryIds.filter(s => s != item.inventorySubCategoriesID)
    } else {
      this.SelectedSopItems.push(item);
      if (!this.SelectedSubCategoryIds.includes(item.inventorySubCategoriesID)) {
        this.SelectedSubCategoryIds.push(item.inventorySubCategoriesID)
      }
    }
    this.FilteredDetails = this.MultiDetails.filter(
      (s) => this.SelectedSopItems.some((i) => i.id === s.shopItemID)
    );
    if (this.HasBallance) {
      this.FilteredDetails = this.FilteredDetails.filter((f) => f.currentStock != 0);
    }
  }

  SearchToggle() {
    this.IsSearchOpen = true;
    setTimeout(() => {
      const input = document.querySelector('input[type="number"]') as HTMLInputElement;
      if (input) input.focus();
    }, 100);
  }

  CloseSearch() {
    this.IsSearchOpen = false;
    this.BarCode = '';
  }

  SearchOnBarCode() {
    if (!this.BarCode) return;
    this.shopitemServ.GetByBarcode(this.BarCode, this.DomainName).subscribe(d => {
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
      const exists = this.FilteredDetails.some(x => x.shopItemID === d.id);
      if (!exists) this.FilteredDetails.push(detail);
      this.BarCode = ''; // Clear input after search
    }, (error) => {
      Swal.fire({
        icon: 'error',
        title: 'This Item Not Exist',
        confirmButtonText: 'Okay',
        customClass: { confirmButton: 'secondaryBg' },
      });
    });
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
      this.FilteredDetails = this.FilteredDetails.filter(
        (f) => f.currentStock != 0
      );
    } else if (this.HasBallance == false) {
      this.FilteredDetails = this.MultiDetails.filter(
        (s) => this.SelectedSopItems.some((i) => i.id === s.shopItemID)
      );
    }
  }

  /////////////////////////////////////////////////////// CRUD

  AddDetail() {
    if (this.Data.storeID != 0) {
      this.SelectedCategoryId = null;
      this.SelectedSubCategoryId = null;
      this.SelectedSopItem = null;
      this.IsOpenToAdd = true;
      this.Item = new StockingDetails();
      // this.ShopItem = new ShopItem()
      this.GetCategories();
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'You Should Choose Store First',
        confirmButtonColor: '#FF7519',
      });
    }
  }

  Save() {
    if (this.isFormValid()) {
      if (this.mode == 'Create') {
        this.StockingServ.Add(this.Data, this.DomainName).subscribe((d) => {
          this.MasterId = d;
          this.router.navigateByUrl(`Employee/Stocking`);
        });
      }
      if (this.mode == 'Edit') {

        this.StockingServ.Edit(this.Data, this.DomainName).subscribe((d) => {
          this.router.navigateByUrl(`Employee/Stocking`);
        });
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
          this.StockingDetailsServ.Delete(row.id, this.DomainName).subscribe(
            (D) => {
              this.GetTableDataByID();
            }
          );
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

  async SaveRow() {
    // this.Item.shopItemID = this.ShopItem.id;
    if (this.mode == 'Create') {
      if (!this.Data.stockingDetails) {
        this.Data.stockingDetails = [];
      }
      this.Data.stockingDetails.push(this.Item);
    }
    if (this.mode == 'Edit') {
      this.Item.stockingId = this.MasterId;
      this.StockingDetailsServ.Add(this.Item, this.DomainName).subscribe(
        async (d) => {
          await this.GetTableDataByID();
        }
      );
    }
    this.IsOpenToAdd = false;
    this.Item = new StockingDetails();
    this.editingRowId = null;
    // this.ShopItem = new ShopItem();
  }

  CancelAdd() {
    this.IsOpenToAdd = false;
  }

  SaveEdit(row: StockingDetails) {
    this.editingRowId = null;
    // this.Item.shopItemID = this.ShopItem.id
    if (this.mode == 'Create') {
    } else if (this.mode == 'Edit') {
      this.StockingDetailsServ.Edit(row, this.DomainName).subscribe(
        async (d) => {
          await this.GetTableDataByID();
          this.StockingServ.Edit(this.Data, this.DomainName).subscribe(
            (d) => { }
          );
        }
      );
    }
  }

  onStockChangeWhenEditRow(row: StockingDetails): void {
    row.theDifference = row.actualStock - row.currentStock;
  }

  onStockChangeWhenAddRow(row: StockingDetails): void {
    row.theDifference = row.actualStock - row.currentStock;
  }

  CancelNewRow(index: number) {
    this.FilteredDetails.splice(index, 1);
    if (this.FilteredDetails.length === 0) {
      this.IsOpenToAdd = false;
    }
  }

  SaveNewRow(index: number) {
    if (this.mode == 'Create') {
      const removedRow = this.FilteredDetails.splice(index, 1)[0]; // Get the first element from the spliced array
      if (removedRow) {
        this.Data.stockingDetails.push(removedRow);
      }
    } else if (this.mode == 'Edit') {
      const removedRow = this.FilteredDetails.splice(index, 1)[0];
      if (removedRow) {
        this.TableData.push(removedRow);
        this.StockingDetailsServ.Add(removedRow, this.DomainName).subscribe(
          async (d) => {
            await this.GetTableDataByID();
          }
        );
      }
    }
    if (this.FilteredDetails.length === 0) {
      this.IsOpenToAdd = false;
    }
    // this.IsOpenToAdd = false;
    this.editingRowId = null;
    // this.SelectedCategoryId= null
    // this.SelectedSubCategoryId= null
    // this.FilteredDetails=[]
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
        const addedData = await this.StockingServ.Add(this.Data, this.DomainName).toPromise();
        this.Data.id = addedData;
        this.MasterId = addedData;
        const result = await this.StockingServ.GetById(this.Data.id, this.DomainName).toPromise();
        if (result) this.Data = result;
        this.Data.additionId = await this.prepareAdjustment(2, (s) => s.theDifference > 0);
        this.Data.disbursementId = await this.prepareAdjustment(4, (s) => s.theDifference < 0);
      }
      if (this.mode === 'Edit') {
        if (this.Data.additionId != 0) {
          try {
            await this.InventoryMastrServ.Delete(this.Data.additionId, this.DomainName).toPromise();
          } catch (error) {
            console.error("Error deleting additionId:", error);
          }
        }
        if (this.Data.disbursementId != 0) {
          try {
            await this.InventoryMastrServ.Delete(this.Data.disbursementId, this.DomainName).toPromise();
          } catch (error) {
            console.error("Error deleting disbursementId:", error);
          }
        }
        this.Data.additionId = await this.prepareAdjustment(2, (s) => s.theDifference > 0);
        this.Data.disbursementId = await this.prepareAdjustment(4, (s) => s.theDifference < 0);
      }
      await this.StockingServ.Edit(this.Data, this.DomainName).toPromise();
      this.router.navigateByUrl(`Employee/Stocking`);
    } catch (error) {
      console.error('Unexpected error in Adjustment():', error);
    } finally {
      this.isLoading = false;
    }
  }

  private async prepareAdjustment(flagId: number, filterCondition: (item: any) => boolean) {

    this.adiustmentDisbursement.date = this.Data.date;
    this.adiustmentDisbursement.storeID = this.Data.storeID;
    this.adiustmentDisbursement.flagId = flagId;
    this.adiustmentDisbursement.inventoryDetails = this.TableData.filter(filterCondition).map((item) => {
      const foundItem = this.AllShopItems.find(
        (s) => s.id == item.shopItemID
      );
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
      const response = await this.InventoryMastrServ.Add(this.adiustmentDisbursement, this.DomainName).toPromise();
      return response;
    }
    else return
  }

  //////// print

  togglePrintMenu() {
    this.showPrintMenu = !this.showPrintMenu;
  }

  selectPrintOption(type: string) {
    this.showPrintMenu = false;
    console.log('Selected Print Type:', type);
    // Call different print logic based on type
    switch (type) {
      case 'Blank':
        this.Blank();
        break;
      case 'Differences':
        this.Differences();
        break;
      case 'Print':
        this.Print();
        break;
    }
  }

  Blank() {
    this.IsActualStockHiddenForBlankPrint = true
    this.cdr.detectChanges();
    setTimeout(async () => {
      await this.Print();
      this.IsActualStockHiddenForBlankPrint = false
      this.cdr.detectChanges(); // Update view again
    }, 300);
  }

  async Differences() {
    const backupFilteredDetails = [...this.FilteredDetails];
    const backupTableData = [...this.TableData];
    if (this.mode === "Create") {
      this.FilteredDetails = this.FilteredDetails.filter(f => f.theDifference != 0);
    } else if (this.mode === "Edit") {
      this.TableData = this.TableData.filter(f => f.theDifference != 0);
    }
    this.cdr.detectChanges();
    setTimeout(async () => {
      await this.Print();
      this.FilteredDetails = backupFilteredDetails;
      if (this.mode === "Create") {
      } else if (this.mode === "Edit") {
        this.TableData = backupTableData;
      }
      this.cdr.detectChanges(); // Update view again
    }, 300);
  }

  Print() {
    const sections = document.querySelectorAll('.print-section');
    if (!sections.length) {
      console.error('No print sections found!');
      return;
    }
    let combinedHTML = '';
    sections.forEach(el => {
      combinedHTML += el.outerHTML;
    });
    const tempContainer = document.createElement('div');
    tempContainer.innerHTML = combinedHTML;
    tempContainer.style.padding = '20px';
    tempContainer.classList.add('pdf-scale');
    document.body.appendChild(tempContainer);
    setTimeout(() => {
      html2pdf()
        .from(tempContainer)
        .set({
          margin: 5,
          filename: 'Stocking.pdf',
          image: { type: 'jpeg', quality: 0.98 },
          html2canvas: { scale: 2, useCORS: true },
          jsPDF: { orientation: 'landscape', unit: 'mm', format: 'a4' }
        })
        .save()
        .then(() => {
          document.body.removeChild(tempContainer);
        });
    }, 500);
  }

  getStoreNameById(id: number): string {
    const store = this.Stores?.find(s => s.id === id);
    return store ? store.name : '—';
  }
}



