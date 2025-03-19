import { Component } from '@angular/core';
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

@Component({
  selector: 'app-stocking-details',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './stocking-details.component.html',
  styleUrl: './stocking-details.component.css'
})
export class StockingDetailsComponent {

  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

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
  mode: string = "Create"

  Stores: Store[] = []
  Categories: Category[] = []
  subCategories: SubCategory[] = []

  SelectedCategoryId: number | null = null;
  SelectedSubCategoryId: number | null = null;
  SelectedSopItem: ShopItem | null = null;

  TableData: StockingDetails[] = []
  Item: StockingDetails = new StockingDetails()
  // ShopItem: ShopItem = new ShopItem()
  MasterId: number = 0;
  editingRowId: any = null;
  validationErrors: { [key in keyof Stocking]?: string } = {};

  IsOpenToAdd: boolean = false
  IsSearchOpen: boolean = false
  BarCode: number = 0
  HasBallance: boolean = false
  AllItems: boolean = true
  ShopItems: ShopItem[] = []
  FilteredShopItems: ShopItem[] = []
  MultiDetails: StockingDetails[] = []
  FilteredDetails: StockingDetails[] = []
  AddittionData: InventoryMaster = new InventoryMaster()
  DisbursementData: InventoryMaster = new InventoryMaster()

  AdditionId: number = 0
  DisbursementId: number = 0
  adiustmentAddition: InventoryMaster = new InventoryMaster()
  adiustmentDisbursement: InventoryMaster = new InventoryMaster()
  AllShopItems: ShopItem[] = []

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
    public StockingDetailsServ: StockingDetailsService ,
    public InventoryMastrServ : InventoryMasterService
  ) { }
  async ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.MasterId = Number(this.activeRoute.snapshot.paramMap.get('id'))

    await this.GetAllStores()
    if (!this.MasterId) {
      this.mode = "Create"
      this.Data.date = new Date().toISOString().split('T')[0];
    } else {
      this.mode = "Edit"
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
      this.AllShopItems = d
    })
  }

  moveToMaster() {
    this.router.navigateByUrl(`Employee/Stocking`)
  }

  ////////////////////////////////////////////////////// Get Data

  GetAllStores() {
    this.storeServ.Get(this.DomainName).subscribe((d) => {
      this.Stores = d
    })
  }

  GetMasterInfo() {
    this.StockingServ.GetById(this.MasterId, this.DomainName).subscribe((d) => {
      this.Data = d
    })
  }

  onStoreChange(storeID: number) {
    this.onInputValueChange({ field: 'storeID', value: storeID });
    if (storeID) {
      this.GetCategories();
    }
  }

  GetCategories() {
    this.CategoriesServ.GetByStoreId(this.DomainName, this.Data.storeID).subscribe((d) => {
      this.Categories = d
      this.subCategories = [];
      this.SelectedSubCategoryId = null;
    }, (error) => {
      this.Categories = []
      this.subCategories = [];
      this.SelectedSubCategoryId = null;
    })
  }

  selectCategory(categoryId: number) {
    this.SelectedCategoryId = categoryId;
    this.GetSubCategories();
    this.GetAllShopItems();
  }

  GetAllShopItems() {
    if (this.SelectedCategoryId) {
      this.StockingDetailsServ.GetCurrentStockForAllItems(
        this.Data.storeID,
        this.SelectedCategoryId,
        this.Data.date,
        this.DomainName
      ).subscribe((d) => {
        this.ShopItems = d
        this.FilteredShopItems = d;
        this.MultiDetails = this.FilteredShopItems.map(item => ({
          id: Date.now() + Math.floor(Math.random() * 1000), // it is random for edit and delete only 
          insertedAt: "",
          insertedByUserId: 0,
          currentStock: item.currentStock,
          actualStock: 0,
          theDifference: 0,
          shopItemID: item.id,
          stockingId: this.MasterId,
          shopItemName: item.enName,
          barCode: item.barCode,
          ItemPrice: item.purchasePrice ?? 0
        }));
        this.FilteredDetails = this.MultiDetails
        if (this.HasBallance == true) {
          this.FilteredDetails = this.MultiDetails.filter(f => f.currentStock > 0)
        }
      });
    }
  }

  GetSubCategories() {
    if (this.SelectedCategoryId)
      this.SubCategoriesServ.GetByCategoryId(this.SelectedCategoryId, this.DomainName)
        .subscribe(d => {
          this.subCategories = d;
          this.SelectedSubCategoryId = null;
        });
  }

  selectSubCategory(subCategoryId: number) {
    this.SelectedSubCategoryId = subCategoryId;
    this.FilteredShopItems = this.ShopItems.filter(s => s.inventorySubCategoriesID == subCategoryId)
    this.FilteredDetails = this.FilteredShopItems
      .map(item => ({
        id: Date.now() + Math.floor(Math.random() * 1000),
        insertedAt: "",
        insertedByUserId: 0,
        currentStock: item.currentStock,
        actualStock: 0,
        theDifference: 0,
        shopItemID: item.id,
        stockingId: this.MasterId,
        shopItemName: item.enName,
        barCode: item.barCode,
        ItemPrice: item.purchasePrice ?? 0

      }));

    this.SelectedSopItem = null;

    if (this.HasBallance == true) {
      this.FilteredDetails = this.FilteredDetails.filter(f => f.currentStock > 0)
      this.FilteredShopItems = this.FilteredShopItems.filter(f => f.currentStock > 0)
    }
  }

  selectShopItem(item: ShopItem) {
    this.FilteredDetails = this.MultiDetails.filter(s => s.shopItemID == item.id)
    this.SelectedSopItem = item;
  }

  async GetTableDataByID(): Promise<void> {
    return new Promise((resolve) => {
      this.StockingDetailsServ.GetBySalesId(this.MasterId, this.DomainName).subscribe((d) => {
        this.TableData = d;
        this.TableData = this.TableData.map(row => ({
          ...row,
          stockingId: this.MasterId
        }));
        resolve();
      },
        (error) => {
          this.TableData = []
        });
    });
  }

  toggleHasBalance() {
    if (this.HasBallance == true) {
      this.FilteredDetails = this.FilteredDetails.filter(f => f.currentStock > 0)
      this.FilteredShopItems = this.FilteredShopItems.filter(s => s.currentStock > 0)
    }
    else if (this.SelectedSopItem != null) {
      this.FilteredDetails = this.MultiDetails.filter(d => d.shopItemID == this.SelectedSopItem!.id)
    }
    else if (this.SelectedSubCategoryId != null) {
      this.FilteredShopItems = this.ShopItems.filter(s => s.inventorySubCategoriesID == this.SelectedSubCategoryId)
      this.FilteredDetails = this.FilteredShopItems
        .map(item => ({
          id: Date.now() + Math.floor(Math.random() * 1000),
          insertedAt: "",
          insertedByUserId: 0,
          currentStock: item.currentStock,
          actualStock: 0,
          theDifference: 0,
          shopItemID: item.id,
          stockingId: this.MasterId,
          shopItemName: item.enName,
          barCode: item.barCode,
          ItemPrice: item.purchasePrice ?? 0

        }));
    }
    else if (this.SelectedCategoryId != null) {
      this.FilteredDetails = this.ShopItems.map(item => ({
        id: Date.now() + Math.floor(Math.random() * 1000), // it is random for edit and delete only 
        insertedAt: "",
        insertedByUserId: 0,
        currentStock: item.currentStock,
        actualStock: 0,
        theDifference: 0,
        shopItemID: item.id,
        stockingId: this.MasterId,
        shopItemName: item.enName,
        barCode: item.barCode,
        ItemPrice: item.purchasePrice ?? 0

      }));
    }
    else {
      this.FilteredDetails = []
    }
  }

  /////////////////////////////////////////////////////// CRUD

  AddDetail() {
    if (this.Data.storeID != 0) {
      this.SelectedCategoryId = null;
      this.SelectedSubCategoryId = null;
      this.SelectedSopItem = null;
      this.IsOpenToAdd = true;
      this.Item = new StockingDetails()
      // this.ShopItem = new ShopItem()
      this.GetCategories()
    }
    else {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'You Should Choose Store First',
        confirmButtonColor: '#FF7519',
      });
    }
  }

  Save() {
    console.log(this.Data)
    if (this.isFormValid()) {
      if (this.mode == "Create") {
        console.log(this.Data)
        this.StockingServ.Add(this.Data, this.DomainName).subscribe((d) => {
          this.MasterId = d
          this.router.navigateByUrl(`Employee/Stocking`)
        })
      }
      if (this.mode == "Edit") {
        this.StockingServ.Edit(this.Data, this.DomainName).subscribe((d) => {
          this.router.navigateByUrl(`Employee/Stocking`)
        })
      }
    }
  }

  Edit(row: StockingDetails) {
    this.editingRowId = row.id;
  }

  Delete(row: StockingDetails) {
    console.log(this.Data.stockingDetails)
    console.log(row)
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
          this.StockingDetailsServ.Delete(row.id, this.DomainName).subscribe((D) => {
            this.GetTableDataByID();
          })
        }
      });
    }
    else if (this.mode == 'Create') {
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
          this.Data.stockingDetails = this.Data.stockingDetails.filter(item => item.id !== row.id);
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
      console.log(this.Item)
      this.StockingDetailsServ.Add(this.Item, this.DomainName).subscribe(async (d) => {
        await this.GetTableDataByID();
      });
    }
    this.IsOpenToAdd = false;
    this.Item = new StockingDetails();
    this.editingRowId = null;
    // this.ShopItem = new ShopItem();
  }

  CancelAdd() {
    this.IsOpenToAdd = false
  }

  SaveEdit(row: StockingDetails) {
    this.editingRowId = null;
    // this.Item.shopItemID = this.ShopItem.id
    if (this.mode == 'Create') {
    } else if (this.mode == 'Edit') {
      this.StockingDetailsServ.Edit(row, this.DomainName).subscribe(async (d) => {
        await this.GetTableDataByID();
        this.StockingServ.Edit(this.Data, this.DomainName).subscribe((d) => {
        })
      })
    }
  }

  onStockChangeWhenEditRow(row: StockingDetails): void {
    row.theDifference = row.actualStock - row.currentStock
  }

  onStockChangeWhenAddRow(row: StockingDetails): void {
    row.theDifference = row.actualStock - row.currentStock
  }

  CancelNewRow(index: number) {
    this.FilteredDetails.splice(index, 1);
    if (this.FilteredDetails.length === 0) {
      this.IsOpenToAdd = false;
    }
  }

  SaveNewRow(index: number) {
    if (this.mode == "Create") {
      const removedRow = this.FilteredDetails.splice(index, 1)[0]; // Get the first element from the spliced array
      if (removedRow) {
        this.Data.stockingDetails.push(removedRow);
      }
    }
    else if (this.mode == "Edit") {
      const removedRow = this.FilteredDetails.splice(index, 1)[0];
      console.log(removedRow)
      if (removedRow) {
        this.TableData.push(removedRow);
        this.StockingDetailsServ.Add(removedRow, this.DomainName).subscribe(async (d) => {
          await this.GetTableDataByID();
        });
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
          if (
            field == 'date'
          ) {
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

  Adjustment() {
    if (this.mode == "Edit") {
      console.log(this.TableData);

      /// Addition
      this.adiustmentAddition.date = this.Data.date;
      this.adiustmentAddition.storeID = this.Data.storeID;
      this.adiustmentAddition.flagId = 2;
      this.adiustmentAddition.inventoryDetails = this.TableData.filter(s=>s.theDifference>0).map(item => {
        const foundItem = this.AllShopItems.find(s => s.id == item.shopItemID);
        const price = foundItem?.purchasePrice ?? 0; 
        const quantity = item.theDifference ?? 0; 
        return {
          id: Date.now() + Math.floor(Math.random() * 1000),
          insertedAt: "",
          barCode: "",
          name: "",
          shopItemName: "",
          salesName: "",
          notes: "",
          insertedByUserId: 0,
          shopItemID: item.shopItemID,
          quantity: quantity,
          price: price,
          totalPrice: price * quantity, 
          inventoryMasterId: this.MasterId,
        };
      });
      this.adiustmentAddition.total = this.adiustmentAddition.inventoryDetails.reduce((sum, item) => sum + (item.totalPrice ?? 0), 0);
      this.InventoryMastrServ.Add(this.adiustmentAddition ,this.DomainName).subscribe((d)=>{
        this.AdditionId=d
        console.log(d)
      })
      console.log(this.adiustmentAddition);

      /// Disbursement

      this.adiustmentDisbursement.date = this.Data.date;
      this.adiustmentDisbursement.storeID = this.Data.storeID;
      this.adiustmentDisbursement.flagId = 2;
      this.adiustmentDisbursement.inventoryDetails = this.TableData.filter(s=>s.theDifference<0).map(item => {
        const foundItem = this.AllShopItems.find(s => s.id == item.shopItemID);
        const price = foundItem?.purchasePrice ?? 0; 
        const quantity = item.theDifference ?? 0; 
        return {
          id: Date.now() + Math.floor(Math.random() * 1000),
          insertedAt: "",
          barCode: "",
          name: "",
          shopItemName: "",
          salesName: "",
          notes: "",
          insertedByUserId: 0,
          shopItemID: item.shopItemID,
          quantity: quantity,
          price: price,
          totalPrice: price * quantity, 
          inventoryMasterId: this.MasterId,
        };
      });
      this.adiustmentDisbursement.total = this.adiustmentDisbursement.inventoryDetails.reduce((sum, item) => sum + (item.totalPrice ?? 0), 0);
      this.InventoryMastrServ.Add(this.adiustmentDisbursement ,this.DomainName).subscribe((d)=>{
        this.AdditionId=d
        console.log(d)
      })
      console.log(this.adiustmentDisbursement);

    }
  }

  //////// print
  
}