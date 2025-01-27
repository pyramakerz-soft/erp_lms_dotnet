import { Component } from '@angular/core';
import { AccountingEmployee } from '../../../../Models/Accounting/accounting-employee';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountingTreeChart } from '../../../../Models/Accounting/accounting-tree-chart';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { AccountingTreeChartService } from '../../../../Services/Employee/Accounting/accounting-tree-chart.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { NationalityService } from '../../../../Services/Octa/nationality.service';
import { Nationality } from '../../../../Models/nationality';
import { DepartmentService } from '../../../../Services/Employee/Administration/department.service';
import { Department } from '../../../../Models/Administrator/department';
import { Job } from '../../../../Models/Administrator/job';
import { JobService } from '../../../../Services/Employee/Administration/job.service';
import { AcademicDegreeService } from '../../../../Services/Employee/Administration/academic-degree.service';
import { AcademicDegree } from '../../../../Models/Administrator/academic-degree';
import { DaysService } from '../../../../Services/Octa/days.service';
import { Day } from '../../../../Models/day';
import { Reasonsforleavingwork } from '../../../../Models/Administrator/reasonsforleavingwork';
import { ReasonsforleavingworkService } from '../../../../Services/Employee/Administration/reasonsforleavingwork.service';
import { JobCategoriesService } from '../../../../Services/Employee/Administration/job-categories.service';
import { JobCategories } from '../../../../Models/Administrator/job-categories';

@Component({
  selector: 'app-accounting-employee-edit',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './accounting-employee-edit.component.html',
  styleUrl: './accounting-employee-edit.component.css'
})
export class AccountingEmployeeEditComponent {
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

  Data: AccountingEmployee = new AccountingEmployee();

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name', 'accountNumberName'];
  AccountNumbers:AccountingTreeChart[]=[];
  
  EmployeeId:number =1;

  nationalities:Nationality[]=[]
  departments:Department[]=[]
  Jobs:Job[]=[]
  academicDegree:AcademicDegree[]=[]
  days:Day[]=[]
  Reasons:Reasonsforleavingwork[]=[]
  JobCategories:JobCategories[]=[]
  JobCategoryId:number=0;
  EndDate:boolean=false;
  selectedDays: { id: number; name: string }[] = [];

  isDropdownOpen = false;

  attendanceTime = {
    hours: '',
    minutes: '',
    period: 'AM'
  };
  departureTime = {
    hour: '',
    minute: '',
    periods: 'AM'
  };

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService ,
    public EmployeeServ: EmployeeService,
    public accountServ:AccountingTreeChartService ,
    public employeeServ:EmployeeService,
    public NationalityServ :NationalityService ,
    public DepartmentServ :DepartmentService ,
    public JobServ :JobService,
    public AcademicDegreeServ:AcademicDegreeService ,
    public DaysServ :DaysService,
    public ReasonsServ:ReasonsforleavingworkService  ,
    public jobCategoryServ :JobCategoriesService
  ) {}
  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
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

    this.GetAllData();
    this.GetAllAccount();
    this.GetAllNationalitys();
    this.GetAllReasons();
    this.GetAllDepartment();
    this.GetAllAcademicDegrees();
    this.GetAllDays();
    this.GetAllJobCategories();

  }

  GetAllData() {
   this.employeeServ.GetAcountingEmployee(this.EmployeeId,this.DomainName).subscribe((d:any)=>{
    this.Data=d;
    this.JobCategoryId=this.Data.jobCategoryId;
    this.EmployeeId = Number(this.activeRoute.snapshot.paramMap.get('id'))
    this.GetAllJobs()
    this.selectedDays=this.days
    this.selectedDays = this.days.filter(day => this.Data.days.includes(day.id));
    this.parseDepartureTime(this.Data.departureTime);
    this.parseAttendanceTime(this.Data.attendanceTime);
   })
  }
  GetAllAccount(){
    this.accountServ.GetBySubAndFileLinkID(10,this.DomainName).subscribe((d)=>{
      this.AccountNumbers=d;
    })
  }

  GetAllDepartment(){
    this.DepartmentServ.Get(this.DomainName).subscribe((d)=>{
      this.departments=d;
    })
  }
  GetAllJobs(){
    this.Jobs=[]
    this.JobServ.GetByCtegoty(this.JobCategoryId,this.DomainName).subscribe((d)=>{
      this.Jobs=d;
    });
  }

  GetAllJobCategories(){
    this.jobCategoryServ.Get(this.DomainName).subscribe((d)=>{
      this.JobCategories=d;
    });
  }
  GetAllNationalitys(){
    this.NationalityServ.Get().subscribe((d)=>{
      this.nationalities=d;
    });
  }
  GetAllAcademicDegrees(){
    this.AcademicDegreeServ.Get(this.DomainName).subscribe((d)=>{
      this.academicDegree=d;
    });
  }
  GetAllDays(){
    this.DaysServ.Get(this.DomainName).subscribe((d)=>{
      this.days=d;
    });
  }

  
  GetAllReasons(){
    this.ReasonsServ.Get(this.DomainName).subscribe((d)=>{
      this.Reasons=d;
    });
  }
  moveToEmployee(){
    this.router.navigateByUrl(`Employee/Employee Accounting`)
  }
  Save(){
    this.getFormattedTime()
   this.employeeServ.EditAccountingEmployee(this.Data,this.DomainName).subscribe((d)=>{
    this.GetAllData();
    this.router.navigateByUrl(`Employee/Employee Accounting`)
   });
  }
 
  onIsActiveChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.EndDate = isChecked
  }
  onHasAttendanceChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.Data.hasAttendance = isChecked
  }

  selectDay(day: { id: number; name: string }) {
    if (!this.selectedDays.some((selected) => selected.id === day.id)) {
      this.selectedDays.push(day);
      this.Data.days.push(day.id);
    }

    // Keep dropdown open for multi-select
  }

  removeDay(dayId: number) {
    this.selectedDays = this.selectedDays.filter((day) => day.id !== dayId);
    this.Data.days = this.Data.days.filter((id) => id!== dayId);
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  getFormattedTime() {
    const { hours, minutes, period } = this.attendanceTime;
    if (hours && minutes && period) {
      this.Data.attendanceTime= `${hours}:${minutes} ${period}`;
    }

    const { hour, minute, periods } = this.departureTime;
    if (hour && minute && periods) {
      this.Data.departureTime= `${hours}:${minutes} ${period}`;
    }

  }
  parseDepartureTime(departureTimeString: string) {
    const [time, period] = departureTimeString.split(' ');
    const [hour, minute] = time.split(':');
  
    this.departureTime = {
      hour: hour || '',
      minute: minute || '',
      periods: period || 'AM'
    };
  }
  parseAttendanceTime(attendanceTimeString: string) {
    const [time, period] = attendanceTimeString.split(' ');
    const [hour, minute] = time.split(':');
    this.attendanceTime = {
      hours: hour || '',
      minutes: minute || '',
      period: period || 'AM'
    };
  }
  OnSelectJobCategory(){
    this.JobCategoryId=this.Data.jobCategoryId;
    this.GetAllJobs();
  }
}

