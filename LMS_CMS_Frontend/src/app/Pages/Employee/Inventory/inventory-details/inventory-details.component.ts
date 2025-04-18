import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Bank } from '../../../../Models/Accounting/bank';
import { Saves } from '../../../../Models/Accounting/saves';
import { Category } from '../../../../Models/Inventory/category';
import { InventoryMaster } from '../../../../Models/Inventory/InventoryMaster';
import { InventoryDetails } from '../../../../Models/Inventory/InventoryDetails';
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
import { ShopItemService } from '../../../../Services/Employee/Inventory/shop-item.service';
import { StoresService } from '../../../../Services/Employee/Inventory/stores.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { StudentService } from '../../../../Services/student.service';
import { InventoryDetailsService } from '../../../../Services/Employee/Inventory/inventory-details.service';
import { InventoryMasterService } from '../../../../Services/Employee/Inventory/inventory-master.service';
import { Supplier } from '../../../../Models/Accounting/supplier';
import { SupplierService } from '../../../../Services/Employee/Accounting/supplier.service';
import { InventoryFlagService } from '../../../../Services/Employee/Inventory/inventory-flag.service';
import { InventoryFlag } from '../../../../Models/Inventory/inventory-flag';
import { firstValueFrom } from 'rxjs';
import { PdfPrintComponent } from '../../../../Component/pdf-print/pdf-print.component';
import { ReportsService } from '../../../../Services/shared/reports.service';

@Component({
  selector: 'app-inventory-details',
  standalone: true,
  imports: [FormsModule, CommonModule,PdfPrintComponent],
  templateUrl: './inventory-details.component.html',
  styleUrl: './inventory-details.component.css'
})
export class InventoryDetailsComponent {

  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  Data: InventoryMaster = new InventoryMaster();
  FlagId: number = 0
  IsPriceEditable: boolean = false;
  IsRemainingCashVisa: boolean = false;
  IsPriceChanged: boolean = false;
  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name', 'accountNumberName'];
  mode: string = "Create"

  students: Student[] = []
  Originsstudents: Student[] = []
  Suppliers: Supplier[] = []
  OriginsSuppliers: Supplier[] = []
  StoresForTitle: Store[] = []
  Stores: Store[] = []
  Saves: Saves[] = []
  Banks: Bank[] = []
  Categories: Category[] = []
  subCategories: SubCategory[] = []
  ShopItems: ShopItem[] = []
  InventoryFlag: InventoryFlag = new InventoryFlag()

  SelectedCategoryId: number | null = null;
  SelectedSubCategoryId: number | null = null;
  SelectedSopItem: ShopItem | null = null;

  TableData: InventoryDetails[] = []
  NewDetailsWhenEdit: InventoryDetails[] = []
  Item: InventoryDetails = new InventoryDetails()
  ShopItem: ShopItem = new ShopItem()
  MasterId: number = 0;
  editingRowId: any = 0;
  validationErrors: { [key in keyof InventoryMaster]?: string } = {};

  IsOpenToAdd: boolean = false

  isLoading = false

  EditedShopItems: ShopItem[] = []
  showPDF: boolean = false
  @ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;
  showTable: boolean = false

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
    public salesItemServ: InventoryDetailsService,
    public salesServ: InventoryMasterService,
    public storeServ: StoresService,
    public SaveServ: SaveService,
    public bankServ: BankService,
    public CategoriesServ: InventoryCategoryService,
    public SubCategoriesServ: InventorySubCategoriesService,
    public shopitemServ: ShopItemService,
    public SupplierServ: SupplierService,
    public InventoryFlagServ: InventoryFlagService ,
    public printservice : ReportsService 
  ) { }
  async ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.MasterId = Number(this.activeRoute.snapshot.paramMap.get('id'))
    this.FlagId = Number(this.activeRoute.snapshot.paramMap.get('FlagId'))
    this.Data.flagId = Number(this.activeRoute.snapshot.paramMap.get('FlagId'))
    if (!this.MasterId) {
      this.mode = "Create"
      this.Data.date = new Date().toISOString().split('T')[0];
    } else {
      this.mode = "Edit"
      this.GetTableDataByID();
      this.GetMasterInfo();
      if (this.Data.saveID == null) {
        this.Data.saveID = 0
      }
      if (this.Data.bankID == null) {
        this.Data.bankID = 0
      }

    }

    if (this.FlagId == 8 || this.FlagId == 9 || this.FlagId == 10 || this.FlagId == 11 || this.FlagId == 12 || this.FlagId == 13) {
      this.IsRemainingCashVisa = true
    }

    if (this.FlagId == 9 && this.mode == 'Create' || this.FlagId == 13 && this.mode == 'Create') { //CanEditPrice
      this.IsPriceEditable = true
    }

    if (this.FlagId == 8) {

    } else if (this.FlagId == 9 || this.FlagId == 10 || this.FlagId == 13) {
      this.GetAllSuppliers()
    } else if (this.FlagId == 11 || this.FlagId == 12) {
      await this.GetAllStudents()
    }

    await this.GetAllStores()
    await this.GetAllSaves()
    await this.GetAllBanks()
    this.GetInventoryFlagInfo()


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
    this.router.navigateByUrl(`Employee/${this.InventoryFlag.enName}`)
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
      this.Originsstudents = d
    })
  }

  GetAllStores() {
    this.storeServ.Get(this.DomainName).subscribe((d) => {
      this.Stores = d
      this.StoresForTitle = d
    })
  }

  FilterStudent(){
    // this.students=this.Originsstudents.filter(s=>s.)
  }

  FilterSupplier(){

  }

  GetAllSuppliers() {
    this.SupplierServ.Get(this.DomainName).subscribe((d) => {
      this.Suppliers = d
      this.OriginsSuppliers =d
    })
  }

  GetMasterInfo() {
    this.salesServ.GetById(this.MasterId, this.DomainName).subscribe((d) => {
      this.Data = d
      this.GetCategories()
    })
  }

  GetCategories() {
    this.Categories = []
    this.CategoriesServ.GetByStoreId(this.DomainName, this.Data.storeID).subscribe((d) => {
      this.Categories = d
    })
  }

  GetInventoryFlagInfo() {
    this.InventoryFlagServ.GetById(this.FlagId, this.DomainName).subscribe((d) => {
      this.InventoryFlag = d
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

  async selectShopItem(item: ShopItem) {
    this.SelectedSopItem = item;
    this.ShopItem = item;
    this.Item.id = Date.now();
    if (this.FlagId === 11 || this.FlagId === 12) {
      this.Item.price = item.salesPrice ?? 0;
    } else {
      this.Item.price = item.purchasePrice ?? 0;
    }
    this.Item.shopItemID = item.id;
    this.Item.shopItemName = item.enName;
    this.Item.barCode = item.barCode;
    this.Item.quantity = 1
    this.Item.totalPrice = this.Item.price
    await this.SaveRow();
  }

  async GetTableDataByID(): Promise<void> {
    return new Promise((resolve) => {
      this.salesItemServ.GetBySalesId(this.MasterId, this.DomainName).subscribe((d) => {
        this.TableData = d;
        this.Data.inventoryDetails = d
        resolve();
      },
        (error) => {
          this.TableData = []
        });
    });
  }

  /////////////////////////////////////////////////////// CRUD
  AddDetail() {
    if (this.Data.storeID != 0) {
      this.SelectedCategoryId = null;
      this.SelectedSubCategoryId = null;
      this.SelectedSopItem = null;
      this.IsOpenToAdd = true;
      this.Item = new InventoryDetails()
      this.ShopItem = new ShopItem()
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
  async Save() {
    if (this.isFormValid()) {
      this.isLoading = true
      await this.SaveRow()
      if (this.mode == "Create") {
        this.salesServ.Add(this.Data, this.DomainName).subscribe((d) => {
          this.MasterId = d
          if (this.EditedShopItems.length > 0) {
            this.EditedShopItems.forEach(element => {
              if (element.id !== 0) {
                this.shopitemServ.Edit(element, this.DomainName).subscribe({
                  next: (res) => {
                  },
                  error: (err) => {
                    console.error("Error updating item:", err);
                  }
                });
              }
            });
          }
          this.router.navigateByUrl(`Employee/${this.InventoryFlag.enName}`)
        }, (error) => {
          this.isLoading = false;
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Try Again Later!',
            confirmButtonText: 'Okay',
            customClass: { confirmButton: 'secondaryBg' },
          });
        })
      }
      if (this.mode == "Edit") {
        this.Data.inventoryDetails = this.TableData
        this.salesItemServ.Edit(this.Data.inventoryDetails, this.DomainName).subscribe((d) => { })
        this.salesItemServ.Add(this.NewDetailsWhenEdit, this.DomainName).subscribe((d => { }), (error) => {
        })
        this.salesServ.Edit(this.Data, this.DomainName).subscribe((d) => {
          this.router.navigateByUrl(`Employee/${this.InventoryFlag.enName}`)
        }, (error) => {
          this.isLoading = false;
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Try Again Later!',
            confirmButtonText: 'Okay',
            customClass: { confirmButton: 'secondaryBg' },
          });
        })
      }
    }
  }

  Edit(row: InventoryDetails) {
    this.editingRowId = row.id;
  }

  EditPrice(row:InventoryDetails) {
    row.totalPrice=row.quantity * row.price
    this.shopitemServ.GetById(row.shopItemID,this.DomainName).subscribe((d)=>{
      this.ShopItem=d
      this.ShopItem.purchasePrice=row.price
      this.EditedShopItems.push(this.ShopItem)
    })
    this.CalculateTotalPrice()
    this.IsPriceChanged = true;
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

  Delete(row: InventoryDetails) {
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
          if (!this.NewDetailsWhenEdit.find(s => s.id == row.id)) {
            this.salesItemServ.Delete(row.id, this.DomainName).subscribe(async (D) => {
              await this.GetTableDataByID();
            })
          }
          else {
            this.NewDetailsWhenEdit = this.NewDetailsWhenEdit.filter(s => s.id != row.id)
            this.TableData = this.TableData.filter(s => s.id != row.id)
          }
          this.TotalandRemainingCalculate()
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
    // this.Item.shopItemID = this.ShopItem.id;
    if (this.mode === 'Create') {
      if (this.FlagId === 9 || this.FlagId === 13) {
        await firstValueFrom(this.shopitemServ.Edit(this.ShopItem, this.DomainName));
      }
      if (!this.Data.inventoryDetails) {
        this.Data.inventoryDetails = [];
      }
      this.Data.inventoryDetails.push(this.Item);
    }
    if (this.mode === 'Edit') {
      this.Item.inventoryMasterId = this.MasterId;
      if (!this.NewDetailsWhenEdit) {
        this.NewDetailsWhenEdit = [];
      }
      this.NewDetailsWhenEdit.push(this.Item)
      this.TableData.push(this.Item)
      await this.GetMasterInfo();
      await firstValueFrom(this.salesServ.Edit(this.Data, this.DomainName));
    }
    this.TotalandRemainingCalculate();
    this.Item = new InventoryDetails();
    this.editingRowId = null;
    this.ShopItem = new ShopItem();
  }

  CancelAdd() {
    this.IsOpenToAdd = false
    this.TotalandRemainingCalculate()
  }

  ConvertToPurcase() {
    this.Data.flagId = 9
    this.Data.isEditInvoiceNumber = true
    this.Data.date = new Date().toISOString().split('T')[0];
    this.salesServ.Edit(this.Data, this.DomainName).subscribe((d) => {
      this.router.navigateByUrl(`Employee/Purchases`)
    })
  }

  onImageFileSelected(event: any) {
    const files: FileList = event.target.files;

    if (this.mode === "Create") {
      this.Data.attachment = this.Data.attachment || [];
      Array.from(files).forEach(file => this.Data.attachment.push(file));
    }
    if (this.mode === "Edit") {
      if (!this.Data.NewAttachments) {
        this.Data.NewAttachments = [];
      }
      Array.from(files).forEach(file => this.Data.NewAttachments.push(file));
    }
  }

  openFile(file: any) {
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
  async CalculateTotalPrice(row?: InventoryDetails) {
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
        const field = key as keyof InventoryMaster;
        if (!this.Data[field]) {
          if (
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
    if (this.FlagId == 8 && this.Data.storeToTransformId == 0) {
      this.validationErrors['storeToTransformId'] = 'Store Is Required'
      return false;
    }
    if (this.FlagId == 9 || this.FlagId == 10 || this.FlagId == 13) {
      if (this.Data.supplierId == 0) {
        this.validationErrors['supplierId'] = 'Supplier Is Required'
        return false;
      }
    }
    if (this.FlagId == 11 || this.FlagId == 12) {
      if (this.Data.studentID == 0) {
        this.validationErrors['studentID'] = 'Student Is Required'
        return false;
      }
    }
    if (this.Data.isCash == true && this.Data.saveID == 0 || this.Data.isCash == true && this.Data.saveID == null) {
      this.validationErrors['saveID'] = 'Safe Is Required'
      return false;
    }
    if (this.Data.isVisa == true && this.Data.bankID == 0 || this.Data.isVisa == true && this.Data.bankID == null) {
      this.validationErrors['bankID'] = 'Bank Is Required'
      return false;
    }
    return isValid;
  }
  capitalizeField(field: keyof InventoryMaster): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof InventoryMaster; value: any }) {
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

  //////////////////////////// Print ////////////////////////////////

  Print() {
    const elements = document.querySelectorAll('.print-area');
    const printContent = Array.from(elements).map(el => el.outerHTML).join('');
  
    const iframe = document.createElement('iframe');
    iframe.style.position = 'fixed';
    iframe.style.right = '0';
    iframe.style.bottom = '0';
    iframe.style.width = '0';
    iframe.style.height = '0';
    iframe.style.border = '0';
    document.body.appendChild(iframe);
  
    const doc = iframe.contentWindow?.document;
    if (doc) {
      doc.open();
      doc.write(`
        <html>
          <head>
            <title>Print</title>
            <link rel="stylesheet" href="styles.css"> <!-- optional -->
            <style>
              /* Copy any critical styles from your app here */
              body {
                font-family: Arial, sans-serif;
                padding: 20px;
              }
            </style>
          </head>
          <body>
            ${printContent}
          </body>
        </html>
      `);
      doc.close();
  
      iframe.contentWindow?.focus();
      iframe.contentWindow?.print();
  
      // Remove iframe after printing
      setTimeout(() => {
        document.body.removeChild(iframe);
      }, 1000);
    }
  }

  DownloadAsPDF() {
   this.printservice.DownloadAsPDF("Inventory");
  }

  async DownloadAsExcel() {
    const tableHeaders = [
      'Bar Code',
      'Item ID',
      'Item Name',
      'Quantity',
      'Price',
      'Total Price',
      'Notes'
    ];
  
    const tableData = (this.mode === 'Create' ? this.Data.inventoryDetails : this.TableData || []).map(row => {
      return [
        row.barCode || '',
        row.shopItemID || '',
        row.shopItemName || '',
        row.quantity || 0,
        row.price || 0,
        row.totalPrice || 0,
        row.notes || ''
      ];
    });
  
    const tables = [{
      title: 'Inventory Details',
      headers: tableHeaders,
      data: tableData
    }];
  
    await this.printservice.generateExcelReport({
      filename: "Inventory.xlsx",
      mainHeader: {
        en: 'Inventory Report',
        ar: 'تقرير المخزون'
      },
      subHeaders: [
        { en: 'Generated on: ' + new Date().toLocaleString(), ar: 'تاريخ الإنشاء: ' + new Date().toLocaleString() }
      ],
      tables: tables
    });
  }
  
}
