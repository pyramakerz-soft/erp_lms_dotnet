import { Component } from '@angular/core';
import { InstallmentDeductionDetail } from '../../../../Models/Accounting/installment-deduction-detail';
import { TokenData } from '../../../../Models/token-data';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { InstallmentDeductionMaster } from '../../../../Models/Accounting/installment-deduction-master';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../../../Models/Employee/employee';
import { Student } from '../../../../Models/student';
import { StudentService } from '../../../../Services/student.service';
import { InstallmentDeductionDetailService } from '../../../../Services/Employee/Accounting/installment-deduction-detail.service';
import { InstallmentDeductionMasterService } from '../../../../Services/Employee/Accounting/installment-deduction-master.service';
import { TuitionFeesType } from '../../../../Models/Accounting/tuition-fees-type';
import { TuitionFeesTypeService } from '../../../../Services/Employee/Accounting/tuition-fees-type.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-installment-deduction-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './installment-deduction-detail.component.html',
  styleUrl: './installment-deduction-detail.component.css'
})
export class InstallmentDeductionDetailComponent {
  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  Data: InstallmentDeductionMaster = new InstallmentDeductionMaster();

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name', 'accountNumberName'];
  mode: string = "Create"

  employees: Employee[] = []
  students: Student[] = []
  FeesType: TuitionFeesType[] = []

  TableData: InstallmentDeductionDetail[] = []
  Detail: InstallmentDeductionDetail = new InstallmentDeductionDetail()
  MasterId: number = 0;
  editingRowId: any = 0;

  IsOpenToAdd: boolean = false
  isLoading = false

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
    public installmentDeductionDetailServ: InstallmentDeductionDetailService,
    public installmentDeductionMasterServ: InstallmentDeductionMasterService,
    public TuitionFeesTypeServ: TuitionFeesTypeService
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
    this.GetAllEmployees()
    this.GetAllStudents()
    this.GetAllTuitionFeesType()
  }

  moveToMaster() {
    this.router.navigateByUrl(`Employee/Installment Deduction`)
  }

  Save() {
    this.isLoading = true
    if (this.mode == "Create") {
      this.installmentDeductionMasterServ.Add(this.Data, this.DomainName).subscribe((d) => {
        this.MasterId = d
        this.isLoading = false

        Swal.fire({
          icon: 'success',
          title: 'Done',
          text: 'Done Succeessfully',
          confirmButtonColor: '#FF7519',
        });
        
        this.router.navigateByUrl(`Employee/Installment Deduction Details/Edit/${this.MasterId}`)
      },
        err => {
          this.isLoading = false
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
      this.installmentDeductionMasterServ.Edit(this.Data, this.DomainName).subscribe((d) => {
        this.GetMasterInfo()
        this.isLoading = false
      },
        err => {
          this.isLoading = false
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

  GetAllEmployees() {
    this.EmployeeServ.Get_Employees(this.DomainName).subscribe((d) => {
      this.employees = d
    })
  }

  GetAllStudents() {
    this.StudentServ.GetAll(this.DomainName).subscribe((d) => {
      this.students = d
    })
  }

  GetMasterInfo() {
    this.installmentDeductionMasterServ.GetById(this.MasterId, this.DomainName).subscribe((d) => {
      this.Data = d
    })
  }

  GetTableDataByID() {
    this.installmentDeductionDetailServ.GetByMasterId(this.MasterId, this.DomainName).subscribe((d) => {
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
      title: 'Are you sure you want to delete this Installment Deduction Details?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.installmentDeductionDetailServ.Delete(id, this.DomainName).subscribe((D) => {
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

  GetAllTuitionFeesType() {
    this.TuitionFeesTypeServ.Get(this.DomainName).subscribe((d) => {
      this.FeesType = d
    })
  }

  SaveRow() {
    this.Detail.installmentDeductionMasterID = this.MasterId
    this.installmentDeductionDetailServ.Add(this.Detail, this.DomainName).subscribe((d) => {
      this.GetTableDataByID();
    })
    this.IsOpenToAdd = false
    this.Detail = new InstallmentDeductionDetail()
  }

  CancelAdd() {
    this.IsOpenToAdd = false
  }

  SaveEdit(row: InstallmentDeductionDetail) {
    this.editingRowId = null;
    this.installmentDeductionDetailServ.Edit(row, this.DomainName).subscribe((d) => {
      this.GetTableDataByID();
    })
  }
}
