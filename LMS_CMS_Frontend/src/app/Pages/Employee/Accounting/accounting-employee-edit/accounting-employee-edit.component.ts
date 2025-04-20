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
import { EmplyeeStudent } from '../../../../Models/Accounting/emplyee-student';
import { EmployeeStudentService } from '../../../../Services/Employee/Accounting/employee-student.service';
import { StudentService } from '../../../../Services/student.service';
import Swal from 'sweetalert2';
import { Student } from '../../../../Models/student';

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
  AccountNumbers: AccountingTreeChart[] = [];

  EmployeeId: number = 0;

  nationalities: Nationality[] = []
  departments: Department[] = []
  Jobs: Job[] = []
  academicDegree: AcademicDegree[] = []
  days: Day[] = []
  Reasons: Reasonsforleavingwork[] = []
  JobCategories: JobCategories[] = []
  JobCategoryId: number = 0;
  EndDate: boolean = false;
  emplyeeStudent: EmplyeeStudent = new EmplyeeStudent();
  TableData: EmplyeeStudent[] = [];
  Student: Student = new Student();
  NationalID: string = "";

  selectedDays: { id: number; name: string }[] = [];
  isLoading = false

  isDropdownOpen = false;

  attendanceTime = {
    hours: '',
    minutes: '',
    periods: 'AM'
  };
  departureTime = {
    hour: '',
    minute: '',
    period: 'AM'
  };

  validationErrors: { [key in keyof AccountingEmployee]?: string } = {};

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
    public employeeServ: EmployeeService,
    public NationalityServ: NationalityService,
    public DepartmentServ: DepartmentService,
    public JobServ: JobService,
    public AcademicDegreeServ: AcademicDegreeService,
    public DaysServ: DaysService,
    public ReasonsServ: ReasonsforleavingworkService,
    public jobCategoryServ: JobCategoriesService,
    public EmplyeeStudentServ: EmployeeStudentService,
    public StudentServ: StudentService,
  ) { }
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
    this.EmployeeId = Number(this.activeRoute.snapshot.paramMap.get('id'))
    this.GetAllDays();
    this.GetAllData();
    this.GetAllAccount();
    this.GetAllNationalitys();
    this.GetAllReasons();
    this.GetAllDepartment();
    this.GetAllAcademicDegrees();
    this.GetAllJobCategories();
  }

  GetAllData() {
    this.employeeServ.GetAcountingEmployee(this.EmployeeId, this.DomainName).subscribe((d: any) => {
      this.Data = d;
      console.log(this.Data)
      this.JobCategoryId = this.Data.jobCategoryId;
      this.GetAllJobs()
      this.selectedDays = this.days
      this.selectedDays = this.days.filter(day => this.Data.days.includes(day.id));
      this.parseDepartureTime(this.Data.departureTime);
      this.parseAttendanceTime(this.Data.attendanceTime);
      if(this.Data.dateOfLeavingWork!=""){
        this.EndDate=true
      }

    })
    this.EmplyeeStudentServ.Get(this.EmployeeId, this.DomainName).subscribe((d) => {
      this.TableData = d
    })

  }

  GetAllAccount() {
    this.accountServ.GetBySubAndFileLinkID(10, this.DomainName).subscribe((d) => {
      this.AccountNumbers = d;
    })
  }

  GetAllDepartment() {
    this.DepartmentServ.Get(this.DomainName).subscribe((d) => {
      this.departments = d;
    })
  }

  GetAllJobs() {
    this.Jobs = []
    this.JobServ.GetByCtegoty(this.JobCategoryId, this.DomainName).subscribe((d) => {
      this.Jobs = d;
    });
  }

  GetAllJobCategories() {
    this.jobCategoryServ.Get(this.DomainName).subscribe((d) => {
      this.JobCategories = d;
    });
  }

  GetAllNationalitys() {
    this.NationalityServ.Get().subscribe((d) => {
      this.nationalities = d;
    });
  }

  GetAllAcademicDegrees() {
    this.AcademicDegreeServ.Get(this.DomainName).subscribe((d) => {
      this.academicDegree = d;
    });
  }

  GetAllDays() {
    this.DaysServ.Get(this.DomainName).subscribe((d) => {
      this.days = d;
    });
  }


  GetAllReasons() {
    this.ReasonsServ.Get(this.DomainName).subscribe((d) => {
      this.Reasons = d;
    });
  }

  moveToEmployee() {
    this.router.navigateByUrl(`Employee/Employee Accounting`)
  }

  Save() {
    if(this.isFormValid()){
      console.log("s",this.Data)
      this.getFormattedTime()
      this.isLoading = true
      this.employeeServ.EditAccountingEmployee(this.Data, this.DomainName).subscribe((d) => {
        this.GetAllData();
        this.router.navigateByUrl(`Employee/Employee Accounting`)
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
  
      if (!Array.isArray(this.Data.days)) {
        this.Data.days = [];
      }
  
      this.Data.days.push(day.id);
    }
  }
  
  removeDay(dayId: number) {
    this.selectedDays = this.selectedDays.filter((day) => day.id !== dayId);
    this.Data.days = this.Data.days.filter((id) => id !== dayId);
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  getFormattedTime() {
  console.log(this.attendanceTime ,this.departureTime)

    const { hours, minutes, periods } = this.attendanceTime;
    if (hours && minutes && periods) {
      this.Data.attendanceTime = `${hours}:${minutes} ${periods}`;
    }

    const { hour, minute, period } = this.departureTime;
    if (hour && minute && period) {
      this.Data.departureTime = `${hour}:${minute} ${period}`;
    }
  console.log(this.Data.attendanceTime ,this.Data.departureTime)
  }

  parseDepartureTime(departureTimeString: string) {
    const [time, period] = departureTimeString.split(' ');
    const [hour, minute] = time.split(':');

    this.departureTime = {
      hour: hour || '',
      minute: minute || '',
      period: period || 'AM'
    };
  }

  parseAttendanceTime(attendanceTimeString: string) {
    const [time, period] = attendanceTimeString.split(' ');
    const [hour, minute] = time.split(':');
    this.attendanceTime = {
      hours: hour || '',
      minutes: minute || '',
      periods: period || 'AM'
    };
  }

  OnSelectJobCategory() {
    this.JobCategoryId = this.Data.jobCategoryId;
    this.GetAllJobs();
  }

  SelectChild(nationalId: string) {
    this.Student = new Student()
    this.emplyeeStudent = new EmplyeeStudent()
    this.StudentServ.GetByNationalID(nationalId, this.DomainName).subscribe((d) => {
      this.Student = d
      this.emplyeeStudent.studentID = d.id
      this.emplyeeStudent.employeeID = this.UserID
    })
  }


  CreateOREdit() {
    if (this.emplyeeStudent.studentID != 0) {
      var EmployeeStudent = new EmplyeeStudent()
      EmployeeStudent.studentName = this.Student.user_Name;
      EmployeeStudent.studentID = this.emplyeeStudent.id;
      this.TableData.push(EmployeeStudent)
      this.Data.students.push(this.emplyeeStudent.studentID)
      this.closeModal()
    }
    else {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'There is no student with this National Id',
        confirmButtonColor: '#FF7519',
      });
    }
  }

  closeModal() {
    this.isModalVisible = false;
  }

  openModal() {
    this.isModalVisible = true;
  }

  Create() {
    this.mode = 'Create';
    this.Student = new Student()
    this.NationalID = ""
    this.emplyeeStudent = new EmplyeeStudent()
    this.openModal();
  }

  DeleteChild(id: number) {
    this.TableData = this.TableData.filter((student) => student.id !== id);
    this.Data.students = this.Data.students.filter((id) => id !== id);
  }

  validateNumber(event: any, field?: keyof AccountingEmployee): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (field) {
        if (typeof this.Data[field] === 'string') {
          this.Data[field] = '' as never;
        }
      }
    }
  }
  capitalizeField(field: keyof AccountingEmployee): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }
  onInputValueChange(event: { field: keyof AccountingEmployee; value: any }) {
    const { field, value } = event;
    (this.Data as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.Data) {
      if (this.Data.hasOwnProperty(key)) {
        const field = key as keyof AccountingEmployee;
        if (!this.Data[field]) {
          if (
            field == 'user_Name' || 
            field == 'departureTime' 
          ) {
            this.validationErrors[field] = `*${this.capitalizeField(
              field
            )} is required`;
            isValid = false;
          }
        }
        if(this.Data.departureTime==this.Data.attendanceTime){
          this.validationErrors['departureTime'] = 'Attendance Time and Departure Time cannot be the same.';
          isValid = false;
        }
      }
    }
    return isValid;
  }
}

