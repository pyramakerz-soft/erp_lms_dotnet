import { Component } from '@angular/core';
import { StudentService } from '../../../../Services/student.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountingEmployee } from '../../../../Models/Accounting/accounting-employee';
import { AccountingTreeChart } from '../../../../Models/Accounting/accounting-tree-chart';
import { AcademicDegree } from '../../../../Models/Administrator/academic-degree';
import { Department } from '../../../../Models/Administrator/department';
import { Job } from '../../../../Models/Administrator/job';
import { JobCategories } from '../../../../Models/Administrator/job-categories';
import { Reasonsforleavingwork } from '../../../../Models/Administrator/reasonsforleavingwork';
import { Day } from '../../../../Models/day';
import { Nationality } from '../../../../Models/nationality';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { AccountingTreeChartService } from '../../../../Services/Employee/Accounting/accounting-tree-chart.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { NationalityService } from '../../../../Services/Octa/nationality.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { Student } from '../../../../Models/student';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-accounting-student-edit',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './accounting-student-edit.component.html',
  styleUrl: './accounting-student-edit.component.css'
})
export class AccountingStudentEditComponent {
  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  Data: Student = new Student();

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name', 'accountNumberName'];
  AccountNumbers: AccountingTreeChart[] = [];
  StudentId: number = 0;
  nationalities: Nationality[] = []
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
    public accountServ: AccountingTreeChartService,
    public StudentServ: StudentService,
    public NationalityServ: NationalityService,
  ) { }
  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.StudentId = Number(this.activeRoute.snapshot.paramMap.get('id'))

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });

    this.GetAllData();
    this.GetAllAccount();
    this.GetAllNationalitys();
  }

  GetAllData() {
    this.StudentServ.GetByID(this.StudentId, this.DomainName).subscribe((d: any) => {
      this.Data = d;
      this.StudentId = Number(this.activeRoute.snapshot.paramMap.get('id'))
    })
  }
  GetAllAccount() {
    this.accountServ.GetBySubAndFileLinkID(13, this.DomainName).subscribe((d) => {
      this.AccountNumbers = d;
    })
  }

  GetAllNationalitys() {
    this.NationalityServ.Get().subscribe((d) => {
      this.nationalities = d;
    });
  }


  moveToEmployee() {
    this.router.navigateByUrl(`Employee/Student Accounting`)
  }
  Save() {
    this.isLoading = true
    this.StudentServ.EditAccountingEmployee(this.Data, this.DomainName).subscribe((d) => {
      this.GetAllData();
      Swal.fire({
        icon: 'success',
        title: 'Done',
        text: 'Student Edited Succeessfully',
        confirmButtonColor: '#FF7519',
      });
      this.router.navigateByUrl(`Employee/Student Accounting`)
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

