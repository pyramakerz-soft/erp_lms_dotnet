import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ActivatedRoute, Router } from '@angular/router';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { School } from '../../../../Models/school';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { InterviewTimeTableService } from '../../../../Services/Employee/Registration/interview-time-table.service';
import { InterviewTimeTable } from '../../../../Models/Registration/interview-time-table';
import Swal from 'sweetalert2';
import { firstValueFrom, lastValueFrom } from 'rxjs';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-interview-time-table',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, TranslateModule],
  templateUrl: './interview-time-table.component.html',
  styleUrl: './interview-time-table.component.css'
})
export class InterviewTimeTableComponent {
  keysArray: string[] = ['id', 'date', 'fromTime', 'toTime', 'capacity', 'reserved', 'academicYearName'];
  key: string = "id";
  value: any = "";

  interviewTimeTableData: InterviewTimeTable[] = []

  SchoolData: School[] = []
  AcademicYearData: AcademicYear[] = []

  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  AllowEdit: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDelete: boolean = false;
  AllowDeleteForOthers: boolean = false;

  path: string = ""

  interviewTimeTableByYearData: InterviewTimeTable[] = []
  interviewTimeTableBySchoolData: InterviewTimeTable[] = []
  selectedYear = 0
  selectedSchool = 0

  selectedSchoolForModal = 0

  interviewTimeTable: InterviewTimeTable = new InterviewTimeTable()
  editInterviewTimeTable: boolean = false
  validationErrors: { [key in keyof InterviewTimeTable]?: string } = {};

  schoolsForModal: School[] = []
  yearsForModal: AcademicYear[] = []

  days = ['Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday']

  isLoading = false

  constructor(public account: AccountService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService,
    private menuService: MenuService, public activeRoute: ActivatedRoute, public router: Router,
    public yearService: AcadimicYearService, public interviewTimeTableService: InterviewTimeTableService,
    public schoolService: SchoolService) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.getTimeTableData()
    this.getSchools()
    this.getYears()

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
      }
    });
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: InterviewTimeTable[] = await firstValueFrom(
        this.interviewTimeTableService.Get(this.DomainName)
      );
      this.interviewTimeTableData = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.interviewTimeTableData = this.interviewTimeTableData.filter((t) => {
          const fieldValue = t[this.key as keyof typeof t];
          if (typeof fieldValue === 'string') {
            return fieldValue.toLowerCase().includes(this.value.toLowerCase());
          }
          if (typeof fieldValue === 'number') {
            return fieldValue === numericValue;
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.interviewTimeTableData = [];
    }
  }


  openModal(InterviewId?: number) {
    if (InterviewId) {
      this.editInterviewTimeTable = true;
      this.getinterviewById(InterviewId);
    } else {
      this.days = ['Sunday',
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
        'Saturday']
    }

    this.getSchoolsForModal()

    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.interviewTimeTable = new InterviewTimeTable()
    this.schoolsForModal = []
    this.yearsForModal = []

    this.days = []

    this.selectedSchoolForModal = 0

    if (this.editInterviewTimeTable) {
      this.editInterviewTimeTable = false
    }
    this.validationErrors = {};
  }

  getTimeTableData() {
    this.interviewTimeTableData = []
    this.interviewTimeTableService.Get(this.DomainName).subscribe(
      (data) => {
        this.interviewTimeTableData = data
      }
    )
  }

  convertTo24HourFormat(time: string) {
    const [timePart, modifier] = time.split(' ');
    let [hours, minutes] = timePart.split(':').map(Number);

    if (modifier === 'PM' && hours !== 12) {
      hours += 12;
    } else if (modifier === 'AM' && hours === 12) {
      hours = 0;
    }

    return `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}`;
  }

  getinterviewById(id: number) {
    this.interviewTimeTableService.GetById(id, this.DomainName).subscribe(
      (data) => {
        this.interviewTimeTable = data
        this.interviewTimeTable.fromTime = this.convertTo24HourFormat(this.interviewTimeTable.fromTime)
        this.interviewTimeTable.toTime = this.convertTo24HourFormat(this.interviewTimeTable.toTime)
        this.getYearsForModalByID();
      }
    )
  }

  // getinterviewByYearId(id: number){
  //   this.interviewTimeTableService.GetByYearId(id, this.DomainName).subscribe(
  //     (data) => {
  //       this.interviewTimeTableByYearData = data
  //     }
  //   )
  // }

  // getinterviewBySchoolId(id: number){
  //   this.interviewTimeTableService.GetBySchoolId(id, this.DomainName).subscribe(
  //     (data) => {
  //       this.interviewTimeTableBySchoolData = data
  //     }
  //   )
  // }

  async getinterviewByYearId(id: number) {
    try {
      const data = await lastValueFrom(this.interviewTimeTableService.GetByYearId(id, this.DomainName));
      this.interviewTimeTableByYearData = data;
    } catch (error) {
      console.error('Error fetching year data:', error);
    }
  }

  async getinterviewBySchoolId(id: number) {
    try {
      const data = await lastValueFrom(this.interviewTimeTableService.GetBySchoolId(id, this.DomainName));
      this.interviewTimeTableBySchoolData = data;
    } catch (error) {
      console.error('Error fetching school data:', error);
    }
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  getSchools() {
    this.schoolService.Get(this.DomainName).subscribe(
      (data) => {
        this.SchoolData = data;
      }
    )
  }

  getSchoolsForModal() {
    this.schoolService.Get(this.DomainName).subscribe(
      (data) => {
        this.schoolsForModal = data;
      }
    )
  }

  getYears() {
    this.yearService.Get(this.DomainName).subscribe(
      (data) => {
        this.AcademicYearData = data;
      }
    )
  }

  getYearsForModal() {
    this.yearService.Get(this.DomainName).subscribe(
      (data) => {
        this.yearsForModal = data.filter((academic_year) => this.checkSchool(academic_year))
      }
    )
  }

  getYearsForModalByID() {
    this.yearService.GetByID(this.interviewTimeTable.academicYearID, this.DomainName).subscribe(
      (data) => {
        this.selectedSchoolForModal = data.schoolID
        this.getYearsForModal();
      }
    )
  }

  checkSchool(element: any) {
    return element.schoolID == this.selectedSchoolForModal
  }

  onSchoolChange(event: Event) {
    this.yearsForModal = []

    this.interviewTimeTable.academicYearID = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.selectedSchoolForModal = Number(selectedValue)
    if (this.selectedSchoolForModal) {
      this.getYearsForModal();
    }
  }

  capitalizeField(field: keyof InterviewTimeTable): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof InterviewTimeTable, value: any }) {
    const { field, value } = event;

    (this.interviewTimeTable as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  validateNumber(event: any, field: keyof InterviewTimeTable): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.interviewTimeTable[field] === 'string') {
        this.interviewTimeTable[field] = '' as never;
      }
    }
  }

  handleDays(event: Event, optionSelected: string) {
    const checkbox = event.target as HTMLInputElement;

    if (checkbox.checked) {
      this.interviewTimeTable.days.push(optionSelected)
    } else {
      this.interviewTimeTable.days = this.interviewTimeTable.days.filter(option =>
        option !== optionSelected
      );
    }
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.interviewTimeTable) {
      if (this.interviewTimeTable.hasOwnProperty(key)) {
        const field = key as keyof InterviewTimeTable;
        if (!this.interviewTimeTable[field]) {
          if (field == "academicYearID" || field == "capacity" || field == "fromTime" || field == "toTime" || ((field == "fromDate" || field == "toDate" || field == "days") && !this.editInterviewTimeTable)) {
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if (!this.editInterviewTimeTable) {
            if (this.interviewTimeTable.days.length == 0) {
              this.validationErrors["days"] = `*${this.capitalizeField("days")} is required`
              isValid = false;
            }
          }
          else {
            this.validationErrors[field] = '';
          }
        }
      }
    }
    return isValid;

  }

  isFromTimeAfterToTime(fromTime: string, toTime: string) {
    const fromTimeParts = fromTime.split(':').map(Number);
    const toTimeParts = toTime.split(':').map(Number);

    const fromTimeInMinutes = fromTimeParts[0] * 60 + fromTimeParts[1];
    const toTimeInMinutes = toTimeParts[0] * 60 + toTimeParts[1];

    return fromTimeInMinutes > toTimeInMinutes;
  }

  isFromDateAfterToDate(fromDate: string, toDate: string) {
    const from = new Date(fromDate);
    const to = new Date(toDate);

    return from > to;
  }

  Save() {
    if (this.isFormValid()) {
      const timeCheck = this.isFromTimeAfterToTime(this.interviewTimeTable.fromTime, this.interviewTimeTable.toTime);
      if (timeCheck) {
        Swal.fire({
          title: "From Time cant't be After To Time",
          icon: 'warning',
          confirmButtonColor: '#FF7519',
          confirmButtonText: 'OK',
        })
      }
      if (this.editInterviewTimeTable == false) {
        const dateCheck = this.isFromDateAfterToDate(this.interviewTimeTable.fromDate, this.interviewTimeTable.toDate);
        if (dateCheck) {
          Swal.fire({
            title: "From Date cant't be After To Date",
            icon: 'warning',
            confirmButtonColor: '#FF7519',
            confirmButtonText: 'OK',
          })
        }
        if (!timeCheck && !dateCheck) {
          this.interviewTimeTable.fromTime += ":00"
          this.interviewTimeTable.toTime += ":00"
          this.isLoading = true
          this.interviewTimeTableService.Add(this.interviewTimeTable, this.DomainName).subscribe(
            (result: any) => {
              this.closeModal()
              this.getTimeTableData()
              this.isLoading = false
            },
            error => {
              this.isLoading = false
              if (error.error == "No Dates in this Period of Time") {
                Swal.fire({
                  title: 'No Days in This Date',
                  icon: 'warning',
                  confirmButtonColor: '#FF7519',
                  confirmButtonText: 'OK',
                })
              }
              this.interviewTimeTable.fromTime = ""
              this.interviewTimeTable.toTime = ""
            }
          );
        }
      } else {
        if (!timeCheck) {
          this.interviewTimeTable.fromTime += ":00"
          this.interviewTimeTable.toTime += ":00"
          this.isLoading = true
          this.interviewTimeTableService.Edit(this.interviewTimeTable, this.DomainName).subscribe(
            (result: any) => {
              this.closeModal()
              this.getTimeTableData()
              this.isLoading = false
            },
            error => {
              this.isLoading = false
              Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Try Again Later!',
                confirmButtonText: 'Okay',
                customClass: { confirmButton: 'secondaryBg' },
              });
            }
          );
        }
      }
    }
  }

  ResetFilter() {
    this.selectedYear = 0
    this.selectedSchool = 0

    this.interviewTimeTableBySchoolData = []
    this.interviewTimeTableByYearData = []

    this.getTimeTableData()
  }

  async Search() {
    this.interviewTimeTableBySchoolData = []
    this.interviewTimeTableByYearData = []

    this.getTimeTableData()

    if (this.selectedSchool != 0) {
      await this.getinterviewBySchoolId(this.selectedSchool)
    }
    if (this.selectedYear != 0) {
      await this.getinterviewByYearId(this.selectedYear)
    }

    let filteredData = [...this.interviewTimeTableData];

    if (this.selectedSchool !== 0) {
      filteredData = filteredData.filter(item =>
        this.interviewTimeTableBySchoolData.some(schoolItem => schoolItem.id === item.id)
      );
    }

    if (this.selectedYear !== 0) {
      filteredData = filteredData.filter(item =>
        this.interviewTimeTableByYearData.some(yearItem => yearItem.id === item.id)
      );
    }

    if ((this.selectedSchool != 0 && this.interviewTimeTableBySchoolData.length == 0) ||
      (this.selectedYear != 0 && this.interviewTimeTableByYearData.length == 0)) {
      filteredData = []
    }

    this.interviewTimeTableData = filteredData;
  }

  deleteInterview(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Interview Time?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.interviewTimeTableService.Delete(id, this.DomainName).subscribe(
          (data: any) => {
            this.interviewTimeTableData = []
            this.getTimeTableData()
          }
        )
      }
    });
  }

  MoveToInterviewRegistration(id: number) {
    this.router.navigateByUrl('Employee/Interview Registration/' + id);
  }
}
