import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Bank } from '../../../../Models/Accounting/bank';
import { Saves } from '../../../../Models/Accounting/saves';
import { Category } from '../../../../Models/Inventory/category';
import { Sales } from '../../../../Models/Inventory/sales';
import { SalesItem } from '../../../../Models/Inventory/sales-item';
import { ShopItem } from '../../../../Models/Inventory/shop-item';
import { Store } from '../../../../Models/Inventory/store';
import { SubCategory } from '../../../../Models/Inventory/sub-category';
import { Student } from '../../../../Models/student';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BankService } from '../../../../Services/Employee/Accounting/bank.service';
import { SaveService } from '../../../../Services/Employee/Accounting/save.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { InventoryCategoryService } from '../../../../Services/Employee/Inventory/inventory-category.service';
import { InventorySubCategoriesService } from '../../../../Services/Employee/Inventory/inventory-sub-categories.service';
import { SalesItemService } from '../../../../Services/Employee/Inventory/sales-item.service';
import { SalesService } from '../../../../Services/Employee/Inventory/sales.service';
import { ShopItemService } from '../../../../Services/Employee/Inventory/shop-item.service';
import { StoresService } from '../../../../Services/Employee/Inventory/stores.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { StudentService } from '../../../../Services/student.service';

@Component({
  selector: 'app-inventory-details',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './inventory-details.component.html',
  styleUrl: './inventory-details.component.css'
})
export class InventoryDetailsComponent {

  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  Data: Sales = new Sales();

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name', 'accountNumberName'];
  mode: string = "Create"

  students: Student[] = []
  Stores: Store[] = []
  Saves: Saves[] = []
  Banks: Bank[] = []
  Categories: Category[] = []
  subCategories: SubCategory[] = []
  ShopItems: ShopItem[] = []

  SelectedCategoryId: number | null = null;
  SelectedSubCategoryId: number | null = null;
  SelectedSopItem: ShopItem | null = null;

  TableData: SalesItem[] = []
  Item: SalesItem = new SalesItem()
  ShopItem: ShopItem = new ShopItem()
  MasterId: number = 0;
  editingRowId: any = 0;
  validationErrors: { [key in keyof Sales]?: string } = {};

  IsOpenToAdd: boolean = false

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public EmployeeServ: EmployeeService,
    public StudentServ: StudentService,
    public salesItemServ: SalesItemService,
    public salesServ: SalesService,
    public storeServ: StoresService,
    public SaveServ: SaveService,
    public bankServ: BankService,
    public CategoriesServ: InventoryCategoryService,
    public SubCategoriesServ: InventorySubCategoriesService,
    public shopitemServ: ShopItemService,
  ) { }
  async ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.MasterId = Number(this.activeRoute.snapshot.paramMap.get('id'))

    await this.GetAllStudents()
    await this.GetAllStores()
    await this.GetAllSaves()
    await this.GetAllBanks()

    if (!this.MasterId) {
      this.mode = "Create"
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
  }

  moveToMaster() {
    this.router.navigateByUrl(`Employee/Sales`)
  }

  ////////////////////////////////////////////////////// Get Data
  GetAllSaves() {
    this.SaveServ.Get(this.DomainName).subscribe((d) => {
      this.Saves = d
    })
  }

  GetAllBanks() {
    this.bankServ.Get(this.DomainName).subscribe((d) => {
      this.Banks = d
    })
  }

  GetAllStudents() {
    this.StudentServ.GetAll(this.DomainName).subscribe((d) => {
      this.students = d
    })
  }

  GetAllStores() {
    this.storeServ.Get(this.DomainName).subscribe((d) => {
      this.Stores = d
    })
  }

  GetMasterInfo() {
    this.salesServ.GetById(this.MasterId, this.DomainName).subscribe((d) => {
      this.Data = d
    })
  }

  GetCategories() {
    this.CategoriesServ.Get(this.DomainName).subscribe((d) => {
      this.Categories = d
    })
  }

  selectCategory(categoryId: number) {
    this.SelectedCategoryId = categoryId;
    this.GetSubCategories();
  }

  GetSubCategories() {
    if (this.SelectedCategoryId)
      this.SubCategoriesServ.GetByCategoryId(this.SelectedCategoryId, this.DomainName)
        .subscribe(d => {
          this.subCategories = d;
          this.ShopItems = []; // Clear items when category changes
          this.SelectedSubCategoryId = null;
        });
  }

  selectSubCategory(subCategoryId: number) {
    this.SelectedSubCategoryId = subCategoryId;
    this.ShopItems = []
    this.GetItems();
  }

  GetItems() {
    if (this.SelectedSubCategoryId)
      this.shopitemServ.GetBySubCategory(this.SelectedSubCategoryId, this.DomainName)
        .subscribe(d => {
          this.ShopItems = d;
        });
  }

  selectShopItem(item: ShopItem) {
    this.SelectedSopItem = item;
    this.ShopItem = item
    this.Item.id = Date.now();  // it is random for edit and delete only 
    this.Item.price = this.ShopItem.salesPrice ?? 0
    this.Item.shopItemID = this.ShopItem.id
    this.Item.shopItemName = this.ShopItem.enName
    this.Item.barCode = this.ShopItem.barCode
  }

  async GetTableDataByID(): Promise<void> {
    return new Promise((resolve) => {
      this.salesItemServ.GetBySalesId(this.MasterId, this.DomainName).subscribe((d) => {
        this.TableData = d;
        resolve();
      }, 
      (error) => { 
        this.TableData=[]
      });
    });
  }

  /////////////////////////////////////////////////////// CRUD
  AddDetail() {
    this.SelectedCategoryId = null;
    this.SelectedSubCategoryId = null;
    this.SelectedSopItem = null;
    this.IsOpenToAdd = true;
    this.Item = new SalesItem()
    this.ShopItem = new ShopItem()
    this.GetCategories()
  }
  Save() {
    this.Data.flagId = 1;
    if (this.isFormValid()) {
      if (this.mode == "Create") {
        this.salesServ.Add(this.Data, this.DomainName).subscribe((d) => {
          this.MasterId = d
          this.router.navigateByUrl(`Employee/Sales`)
        })
      }
      if (this.mode == "Edit") {
        this.salesServ.Edit(this.Data, this.DomainName).subscribe((d) => {
          this.router.navigateByUrl(`Employee/Sales`)
        })
      }
    }
  }

  Edit(row: SalesItem) {
    this.editingRowId = row.id;
  }

  handleCashChange(isChecked: boolean): void {
    if (!isChecked) {
      this.Data.cashAmount = 0;
      this.Data.saveID = 0; // Optional, if you want to clear the save selection too
    }
    this.TotalandRemainingCalculate();
  }
  
  handleVisaChange(isChecked: boolean): void {
    if (!isChecked) {
      this.Data.visaAmount = 0;
      this.Data.bankID = 0; // Optional, if you want to clear the bank selection too
    }
    this.TotalandRemainingCalculate();
  }

  Delete(row: SalesItem) {
    if (this.mode == 'Edit') {
      Swal.fire({
        title: 'Are you sure you want to delete this Sales Item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#FF7519',
        cancelButtonColor: '#17253E',
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
      }).then((result) => {
        if (result.isConfirmed) {
          this.salesItemServ.Delete(row.id, this.DomainName).subscribe((D) => {
            this.GetTableDataByID();
            this.TotalandRemainingCalculate()
          })
        }
      });
    }
    else if (this.mode == 'Create') {
      Swal.fire({
        title: 'Are you sure you want to delete this Sales Item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#FF7519',
        cancelButtonColor: '#17253E',
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
      }).then((result) => {
        if (result.isConfirmed) {
          this.Data.inventoryDetails = this.Data.inventoryDetails.filter(item => item.id !== row.id);
          this.TotalandRemainingCalculate()
        }
      });
    }
  }

  DeleteWhenCreate(img: File) {
    this.Data.attachment = this.Data.attachment.filter(i => i != img)
  }

  DeleteWhenEdit(img: File) {
    this.Data.NewAttachments = this.Data.NewAttachments.filter(i => i != img)
  }

  DeleteExistedImg(img: string) {
    if (!this.Data.DeletedAttachments) {
      this.Data.DeletedAttachments = [];
    }
    this.Data.DeletedAttachments.push(img)
    this.Data.attachments = this.Data.attachments.filter(i => i != img)
  }


  async SaveRow() {
    this.Item.shopItemID = this.ShopItem.id;
    if (this.mode == 'Create') {
      if (!this.Data.inventoryDetails) {
        this.Data.inventoryDetails = [];
      }
      this.Data.inventoryDetails.push(this.Item);
      this.TotalandRemainingCalculate();
    }
    if (this.mode == 'Edit') {
      this.Item.inventoryMasterId = this.MasterId;
      this.salesItemServ.Add(this.Item, this.DomainName).subscribe(async (d) => {
        await this.GetTableDataByID(); 
        await this.TotalandRemainingCalculate(); 
        this.salesServ.Edit(this.Data, this.DomainName).subscribe((d) => {});
      });
    }
    this.IsOpenToAdd = false;
    this.Item = new SalesItem();
    this.editingRowId = null;
    this.ShopItem = new ShopItem();
  }

  CancelAdd() {
    this.IsOpenToAdd = false
    this.TotalandRemainingCalculate()
  }

  SaveEdit(row: SalesItem) {
    this.editingRowId = null;
    row.totalPrice = row.quantity * row.price
    this.Item.shopItemID = this.ShopItem.id
    if (this.mode == 'Create') {
      this.TotalandRemainingCalculate()
    } else if (this.mode == 'Edit') {
      this.salesItemServ.Edit(row, this.DomainName).subscribe(async (d) => {
        await this.GetTableDataByID();
        await this.TotalandRemainingCalculate()
        this.salesServ.Edit(this.Data, this.DomainName).subscribe((d) => {
        })
      })
    }
  }

  onImageFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (this.mode == "Create") {
      this.Data.attachment.push(file)
    }
    if (this.mode === "Edit") {
      if (!this.Data.NewAttachments) {
        this.Data.NewAttachments = [];
      }
      this.Data.NewAttachments.push(file);
    }

  }

  openFile(file: any) {  // open image if it file or url 
    if (typeof file === 'string') {
      window.open(file, '_blank');
    } else if (file instanceof File) {
      const fileUrl = URL.createObjectURL(file);
      window.open(fileUrl, '_blank');
      setTimeout(() => URL.revokeObjectURL(fileUrl), 10000);
    } else {
      console.warn('Unknown file type:', file);
    }
  }

  ////////////////////// Calculate Total and Remaining 
  async CalculateTotalPrice(row?: SalesItem) {
    await this.TotalandRemainingCalculate()
    if (this.mode == 'Create') {
      if (row == null) {
        this.Item.totalPrice = +this.Item.quantity * this.Item.price
        this.Data.total = +this.Data.total + +this.Item.totalPrice
        this.Data.remaining = +this.Data.total - (+this.Data.cashAmount + +this.Data.visaAmount)
      }
      else {
        row.totalPrice = row.quantity * row.price
        this.TotalandRemainingCalculate()
      }
    }
    else if (this.mode == 'Edit') {
      if (row == null) {
        this.Item.totalPrice = this.Item.quantity * this.Item.price
        this.Data.total = +this.Data.total + +this.Item.totalPrice
        this.Data.remaining = +this.Data.total - (+this.Data.cashAmount + +this.Data.visaAmount)
      }
      else {
        row.totalPrice = row.quantity * row.price
        this.TotalandRemainingCalculate()
       }
    }
  }

  async TotalandRemainingCalculate(): Promise<void> {
    return new Promise((resolve) => {
      if (this.mode == 'Create') {
        this.Data.cashAmount = this.Data.cashAmount || 0;
        this.Data.visaAmount = this.Data.visaAmount || 0;
        this.Data.total = this.Data.inventoryDetails.reduce((sum, item) => sum + (item.totalPrice || 0), 0);
        this.Data.remaining = +this.Data.total - (+this.Data.cashAmount + +this.Data.visaAmount);
      } else if (this.mode == 'Edit') {
        this.Data.cashAmount = this.Data.cashAmount || 0;
        this.Data.visaAmount = this.Data.visaAmount || 0;
        this.Data.total = this.TableData.reduce((sum, item) => sum + (item.totalPrice || 0), 0);
        this.Data.remaining = +this.Data.total - (+this.Data.cashAmount + +this.Data.visaAmount);
        this.salesServ.Edit(this.Data, this.DomainName).subscribe((d) => {
        })
      }
      resolve(); 
    });
  }

  ///////////////////////////////////// validation fOR Master
  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.Data) {
      if (this.Data.hasOwnProperty(key)) {
        const field = key as keyof Sales;
        if (!this.Data[field]) {
          if (
            field == 'studentID' ||
            field == 'storeID' ||
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
    if (this.mode == 'Create' && this.Data.inventoryDetails.length == 0) {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'SalesItems Is Required',
        confirmButtonColor: '#FF7519',
      });
      return false;
    }
    if (this.mode == 'Edit' && this.TableData.length == 0) {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'SalesItems Is Required',
        confirmButtonColor: '#FF7519',
      });
      return false;
    }
    return isValid;
  }
  capitalizeField(field: keyof Sales): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof Sales; value: any }) {
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
}
