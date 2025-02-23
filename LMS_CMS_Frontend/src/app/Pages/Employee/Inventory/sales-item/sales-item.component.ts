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

  TableData: SalesItem[] = []
  Item: SalesItem = new SalesItem()
  MasterId: number = 0;
  editingRowId: any = 0;

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
    public bankServ: BankService
  ) { }
  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.MasterId = Number(this.activeRoute.snapshot.paramMap.get('id'))

    if (!this.MasterId) {
      this.mode = "Create"
    } else {
      this.GetTableDataByID();
      this.GetMasterInfo();
    }

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
      if (url[1].path == "View") {
        this.mode = "View"
      } else if (url[1].path == "Edit") {
        this.mode = "Edit"
      }
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });

    if (this.mode == "Create") {

    }
    this.GetAllStudents()
    this.GetAllStores()
    this.GetAllSaves()
    this.GetAllBanks()
  }

  moveToMaster() {
    this.router.navigateByUrl(`Employee/Sales`)
  }

  Save() {
    this.Data.flagId = 1;
    console.log(this.Data)
    if (this.mode == "Create") {
      this.salesServ.Add(this.Data, this.DomainName).subscribe((d) => {
        this.MasterId = d
        this.router.navigateByUrl(`Employee/Sales Item/Edit/${this.MasterId}`)
      })
    }
    if (this.mode == "Edit") {
      this.salesServ.Edit(this.Data, this.DomainName).subscribe((d) => {
        this.GetMasterInfo()
      })
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

  GetTableDataByID() {
    this.salesItemServ.GetBySalesId(this.MasterId, this.DomainName).subscribe((d) => {
      this.TableData = d;
    })
  }

  AddDetail() {
    this.IsOpenToAdd = true
  }

  Edit(id: number) {
    this.editingRowId = id
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
    this.Item.salesID = this.MasterId
    this.salesItemServ.Add(this.Item, this.DomainName).subscribe((d) => {
      this.GetTableDataByID();
    })
    this.IsOpenToAdd = false
    this.Item = new SalesItem()
  }

  CancelAdd() {
    this.IsOpenToAdd = false
  }

  SaveEdit(row: SalesItem) {
    this.editingRowId = null;
    this.salesItemServ.Edit(row, this.DomainName).subscribe((d) => {
      this.GetTableDataByID();
    })
  }


  onImageFileSelected(event: any) {
    console.log(this.mode)
    const file: File = event.target.files[0];
    if(this.mode=="Create"){
      this.Data.attachment.push(file)
    }
    if (this.mode === "Edit") {
      console.log(this.Data);
      if (!this.Data.NewAttachments) {
        this.Data.NewAttachments = [];
      }
      this.Data.NewAttachments.push(file);
    }

  }

  openFile(fileUrl: any) {
    // window.open(fileUrl, '_blank');
  }

}
