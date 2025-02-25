import { Component } from '@angular/core';
import { Sales } from '../../../../Models/Inventory/sales';
import { SalesItem } from '../../../../Models/Inventory/sales-item';
import { SalesService } from '../../../../Services/Employee/Inventory/sales.service';
import { SalesItemService } from '../../../../Services/Employee/Inventory/sales-item.service';
import { Router, ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { Student } from '../../../../Models/student';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { StudentService } from '../../../../Services/student.service';
import { Store } from '../../../../Models/Inventory/store';
import { StoresService } from '../../../../Services/Employee/Inventory/stores.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { SaveService } from '../../../../Services/Employee/Accounting/save.service';
import { Saves } from '../../../../Models/Accounting/saves';
import { BankService } from '../../../../Services/Employee/Accounting/bank.service';
import { Bank } from '../../../../Models/Accounting/bank';
import { InventoryCategoryService } from '../../../../Services/Employee/Inventory/inventory-category.service';
import { Category } from '../../../../Models/Inventory/category';
import { SubCategory } from '../../../../Models/Inventory/sub-category';
import { ShopItem } from '../../../../Models/Inventory/shop-item';
import { SubjectCategoryService } from '../../../../Services/Employee/LMS/subject-category.service';
import { InventorySubCategoriesService } from '../../../../Services/Employee/Inventory/inventory-sub-categories.service';
import { ShopItemService } from '../../../../Services/Employee/Inventory/shop-item.service';

@Component({
  selector: 'app-sales-item',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './sales-item.component.html',
  styleUrl: './sales-item.component.css'
})
export class SalesItemComponent {
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

  Save() {
    this.Data.flagId = 1;
    console.log("data", this.Data)
    if(this.isFormValid()){
    if (this.mode == "Create") {
      this.salesServ.Add(this.Data, this.DomainName).subscribe((d) => {
        console.log(d)
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
    this.Item.price = this.ShopItem.salesPrice
  }

  GetTableDataByID() {
    this.salesItemServ.GetBySalesId(this.MasterId, this.DomainName).subscribe((d) => {
      this.TableData = d;
    })
  }

  AddDetail() {
    this.IsOpenToAdd = true
    this.GetCategories()
  }

  Edit(id: number) {
    this.editingRowId = id
  }

  CalculateTotalPrice(Quantity:number ){
    this.Item.totalPrice=Quantity*this.Item.price
    this.TotalandRemainingCalculate()
  }

  Delete(id: number) {
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
        this.salesItemServ.Delete(id, this.DomainName).subscribe((D) => {
          this.GetTableDataByID();
        })
      }
    });
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

  SaveRow() {
    this.Data.total = +this.Data.total + +this.Item.totalPrice;
    this.Data.remaining = +this.Data.total - (+this.Data.cashAmount + +this.Data.visaAmount)
    this.Item.shopItemID = this.ShopItem.id
    if(this.mode=='Create'){
      if (!this.Data.inventoryDetails) {
        this.Data.inventoryDetails = [];
      }
      this.Data.inventoryDetails.push(this.Item)
    }
    if (this.mode == 'Edit') {
      this.Item.inventoryMasterId = this.MasterId
      this.salesItemServ.Add(this.Item, this.DomainName).subscribe((d) => {
        this.GetTableDataByID();
      })

    }
    this.IsOpenToAdd = false
    this.Item = new SalesItem()
  }

  CancelAdd() {
    this.IsOpenToAdd = false
  }

  SaveEdit(row: SalesItem) {
    this.editingRowId = null;
    this.Data.total = +this.Data.total + +this.Item.totalPrice;
    this.Data.remaining = +this.Data.total - (+this.Data.cashAmount + +this.Data.visaAmount)
    this.Item.shopItemID = this.ShopItem.id
    this.salesItemServ.Edit(row, this.DomainName).subscribe((d) => {
      this.GetTableDataByID();
    })
  }

  TotalandRemainingCalculate(){
    this.Data.total = +this.Data.total + +this.Item.totalPrice;
    this.Data.remaining = +this.Data.total - (+this.Data.cashAmount + +this.Data.visaAmount)
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
    console.log(this.Data.inventoryDetails , isValid)
    if (this.mode=='Create'&&this.Data.inventoryDetails.length==0 ) {
       Swal.fire({
                icon: 'warning',
                title: 'Warning!',
                text: 'SalesItems Is Required',
                confirmButtonColor: '#FF7519',
              });
              return false;
    }
    if (this.mode=='Edit'&&this.TableData.length==0 ) {
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

  openFile(file: any) {
    console.log(file);
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

}
