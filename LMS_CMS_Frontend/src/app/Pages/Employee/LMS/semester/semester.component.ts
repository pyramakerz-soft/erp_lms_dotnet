import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Semester } from '../../../../Models/LMS/semester';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { TokenData } from '../../../../Models/token-data';
import { SemesterService } from '../../../../Services/Employee/LMS/semester.service';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import Swal from 'sweetalert2';
import { School } from '../../../../Models/school';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-semester',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './semester.component.html',
  styleUrl: './semester.component.css'
})
export class SemesterComponent {
  keysArray: string[] = ['id', 'name','dateFrom','dateTo','academicYearName'];
  key: string= "id";
  value: any = "";

  semesterData:Semester[] = []
  semester:Semester = new Semester()
  academicYear:AcademicYear = new AcademicYear()
  editSemester:boolean = false
  validationErrors: { [key in keyof Semester]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  path: string = ""

  DomainName: string = "";
  UserID: number = 0;
  academicYearId: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  isLoading = false;
  
  constructor(public account: AccountService, public semesterService: SemesterService, public acadimicYearService: AcadimicYearService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, 
    private menuService: MenuService, public activeRoute: ActivatedRoute, public router:Router){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.academicYearId = Number(this.activeRoute.snapshot.paramMap.get('Id'))
    this.DomainName = String(this.activeRoute.snapshot.paramMap.get('domainName'))

    this.getAcademicYearData()
    this.getSemesterData()

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
  }

  getAcademicYearData(){
    this.acadimicYearService.GetByID(this.academicYearId, this.DomainName).subscribe(
      (data) => {
        this.academicYear = data;
      }
    )
  }

  getSemesterData(){
    this.semesterData=[]
    this.semesterService.GetByAcademicYearId(this.academicYearId, this.DomainName).subscribe(
      (data) => {
        this.semesterData = data;
      }
    )
  }

  GetSemesterById(Id: number) {
    this.semesterService.GetByID(Id, this.DomainName).subscribe((data) => {
      this.semester = data;
    });
  }

  openModal(Id?: number) {
    if (Id) {
      this.editSemester = true;
      this.GetSemesterById(Id); 
    }

    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.semester= new Semester()

    if(this.editSemester){
      this.editSemester = false
    }
    this.validationErrors = {}; 
  } 

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Semester[] = await firstValueFrom(this.semesterService.GetByAcademicYearId(this.academicYearId, this.DomainName));  
      this.semesterData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.semesterData = this.semesterData.filter(t => {
          const fieldValue = t[this.key as keyof typeof t];
          if (typeof fieldValue === 'string') {
            return fieldValue.toLowerCase().includes(this.value.toLowerCase());
          }
          if (typeof fieldValue === 'number') {
            return fieldValue.toString().includes(numericValue.toString())
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.semesterData = [];
    }
  }

  moveToAcademicYear(){
    this.router.navigateByUrl("Employee/Academic Year")
  }

  MoveToSemesterView(id:number){
    this.router.navigateByUrl('Employee/SemesterView/' + this.DomainName + '/' + id)
  }

  capitalizeField(field: keyof Semester): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.semester) {
      if (this.semester.hasOwnProperty(key)) {
        const field = key as keyof Semester;
        if (!this.semester[field]) {
          if(field == "name" || field == "dateFrom" || field == "dateTo"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.semester.name.length > 100){
              this.validationErrors[field] = `*${this.capitalizeField(field)} cannot be longer than 100 characters`
              isValid = false;
            }
          } else{
            this.validationErrors[field] = '';
          }
        }
      }
    }
    return isValid;
  }

  onInputValueChange(event: { field: keyof Semester, value: any }) {
    const { field, value } = event;
    (this.semester as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
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

  checkFromToDate(){
    let valid = true

    const fromDate: Date = new Date(this.semester.dateFrom); 
    const toDate: Date = new Date(this.semester.dateTo); 
    const diff: number = toDate.getTime() - fromDate.getTime();

    if(diff < 0){
      valid = false
      Swal.fire({
        title: 'From Date Must Be a Date Before To Date',
        icon: 'warning',
        confirmButtonColor: '#FF7519',
        confirmButtonText: 'Ok',
      });
      this.isLoading = false;
    }

    return valid
  }

  SaveSemester(){
    if(this.isFormValid()){
      this.isLoading = true;
      this.semester.academicYearID = this.academicYearId
      if(this.checkFromToDate()){
        if(this.editSemester == false){
          this.semesterService.Add(this.semester, this.DomainName).subscribe(
            (result: any) => {
              this.closeModal()
              this.isLoading = false;
              this.getSemesterData()
            },
            error => {
              this.isLoading = false;
              Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Try Again Later!',
                confirmButtonText: 'Okay',
                customClass: { confirmButton: 'secondaryBg' },
              });
            }
          );
        } else{
          this.semesterService.Edit(this.semester, this.DomainName).subscribe(
            (result: any) => {
              this.closeModal()
              this.getSemesterData()
              this.isLoading = false;
            },
            error => {
              this.isLoading = false;
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

  deleteSemester(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this Semester?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.semesterService.Delete(id, this.DomainName).subscribe(
          (data: any) => {
            this.semesterData=[]
            this.getSemesterData()
          }
        );
      }
    });
  }
}
