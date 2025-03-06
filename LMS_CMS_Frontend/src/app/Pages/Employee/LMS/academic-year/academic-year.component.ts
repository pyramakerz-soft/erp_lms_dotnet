import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { CommonModule } from '@angular/common';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { School } from '../../../../Models/school';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import Swal from 'sweetalert2';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-academic-year',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './academic-year.component.html',
  styleUrl: './academic-year.component.css'
})
export class AcademicYearComponent {
  keysArray: string[] = ['id', 'name','dateFrom','dateTo','schoolName','isActive'];
  key: string= "id";
  value: any = "";

  academicYearData:AcademicYear[] = []
  academicYear:AcademicYear = new AcademicYear()
  editAcademicYear:boolean = false
  validationErrors: { [key in keyof AcademicYear]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  path: string = ""

  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  Schools: School[] = []

  constructor(public account: AccountService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, 
      private menuService: MenuService, public activeRoute: ActivatedRoute, public schoolService: SchoolService, public router:Router,
      public acadimicYearServicea:AcadimicYearService){}
      
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.getAcademicYearData()

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

  openModal(Id?: number) {
    if (Id) {
      this.editAcademicYear = true;
      this.getAcademicYearById(Id); 
    }
     
    this.getSchoolData()

    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.academicYear= new AcademicYear()
    this.Schools = [] 

    if(this.editAcademicYear){
      this.editAcademicYear = false
    }
    this.validationErrors = {}; 
  }

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: AcademicYear[] = await firstValueFrom(this.acadimicYearServicea.Get(this.DomainName));  
      this.academicYearData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.academicYearData = this.academicYearData.filter(t => {
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
      this.academicYearData = [];
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

  getAcademicYearData(){
    this.acadimicYearServicea.Get(this.DomainName).subscribe(
      (data) => {
        this.academicYearData = data;
      }
    )
  }

  getAcademicYearById(id:number){
    this.acadimicYearServicea.GetByID(id, this.DomainName).subscribe(
      (data) => {
        this.academicYear = data;
      }
    )
  }

  getSchoolData(){
    this.schoolService.Get(this.DomainName).subscribe(
      (data) => {
        this.Schools = data;
      }
    )
  }

  capitalizeField(field: keyof AcademicYear): string {
      return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.academicYear) {
      if (this.academicYear.hasOwnProperty(key)) {
        const field = key as keyof AcademicYear;
        if (!this.academicYear[field]) {
          if(field == "name" || field == "schoolID" || field == "dateFrom" || field == "dateTo"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.academicYear.name.length > 100){
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

  onInputValueChange(event: { field: keyof AcademicYear, value: any }) {
    const { field, value } = event;
    
    (this.academicYear as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  onIsActiveChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.academicYear.isActive = isChecked
  }

  checkFromToDate(){
    let valid = true

    const fromDate: Date = new Date(this.academicYear.dateFrom); 
    const toDate: Date = new Date(this.academicYear.dateTo); 
    const diff: number = toDate.getTime() - fromDate.getTime();

    if(diff < 0){
      valid = false
      Swal.fire({
        title: 'From Date Must Be a Date Before To Date',
        icon: 'warning',
        confirmButtonColor: '#FF7519',
        confirmButtonText: 'Ok',
      });
    }

    return valid
  }

  Save(){
    if(this.isFormValid()){
      if(this.checkFromToDate()){
        if(this.editAcademicYear == false){
          this.acadimicYearServicea.Add(this.academicYear, this.DomainName).subscribe(
            (result: any) => {
              this.closeModal()
              this.getAcademicYearData()
            },
            error => {
            }
          );
        } else{
          this.acadimicYearServicea.Edit(this.academicYear, this.DomainName).subscribe(
            (result: any) => {
              this.closeModal()
              this.getAcademicYearData()
            },
            error => {
            }
          );
        }  
      }
    }
  } 

  deleteAcademicYear(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this Academic Year?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.acadimicYearServicea.Delete(id, this.DomainName).subscribe(
          (data: any) => {
            this.academicYearData=[]
            this.getAcademicYearData()
          }
        );
      }
    });
  }

  moveToAcademicYearView(Id:number){
    this.router.navigateByUrl('Employee/Semester/' + this.DomainName + '/' + Id);
  } 
}
