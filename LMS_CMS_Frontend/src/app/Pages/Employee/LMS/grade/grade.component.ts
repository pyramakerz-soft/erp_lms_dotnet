import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Grade } from '../../../../Models/LMS/grade';
import { Section } from '../../../../Models/LMS/section';
import { TokenData } from '../../../../Models/token-data';
import { SectionService } from '../../../../Services/Employee/LMS/section.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import Swal from 'sweetalert2';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-grade',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './grade.component.html',
  styleUrl: './grade.component.css'
})
export class GradeComponent {
  keysArray: string[] = ['id', 'name','dateFrom','dateTo'];
  key: string= "id";
  value: any = "";

  gradeData:Grade[] = []
  grade:Grade = new Grade()
  section:Section = new Section()
  editGrade:boolean = false
  validationErrors: { [key in keyof Grade]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  path: string = ""

  DomainName: string = "";
  UserID: number = 0;
  sectionId: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  isLoading = false;
    
  constructor(public account: AccountService, public sectionService: SectionService, public gradeService: GradeService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, 
    private menuService: MenuService, public activeRoute: ActivatedRoute, public router:Router){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.sectionId = Number(this.activeRoute.snapshot.paramMap.get('Id'))
    this.DomainName = String(this.activeRoute.snapshot.paramMap.get('domainName'))

    this.getSectionData()
    this.getGradeData()

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

  getSectionData(){
    this.sectionService.GetByID(this.sectionId, this.DomainName).subscribe(
      (data) => {
        this.section = data;
      }
    )
  }

  getGradeData(){
    this.gradeData=[]
    this.gradeService.GetBySectionId(this.sectionId, this.DomainName).subscribe(
      (data) => {
        this.gradeData = data;
      }
    )
  }

  GetGradeById(Id: number) {
    this.gradeService.GetByID(Id, this.DomainName).subscribe((data) => {
      this.grade = data;
    });
  }

  openModal(Id?: number) {
    if (Id) {
      this.editGrade = true;
      this.GetGradeById(Id); 
    }

    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.grade= new Grade()

    if(this.editGrade){
      this.editGrade = false
    }
    this.validationErrors = {}; 
  }
  
  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Grade[] = await firstValueFrom(this.gradeService.GetBySectionId(this.sectionId, this.DomainName));  
      this.gradeData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.gradeData = this.gradeData.filter(t => {
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
      this.gradeData = [];
    }
  }

  moveToSection(){
    this.router.navigateByUrl("Employee/Section")
  }

  capitalizeField(field: keyof Grade): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.grade) {
      if (this.grade.hasOwnProperty(key)) {
        const field = key as keyof Grade;
        if (!this.grade[field]) {
          if(field == "name" || field == "dateFrom" || field == "dateTo"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.grade.name.length > 100){
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

  onInputValueChange(event: { field: keyof Grade, value: any }) {
    const { field, value } = event;
    (this.grade as any)[field] = value;
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

    const fromDate: Date = new Date(this.grade.dateFrom); 
    const toDate: Date = new Date(this.grade.dateTo); 
    const diff: number = toDate.getTime() - fromDate.getTime();

    if(diff < 0){
      valid = false
      Swal.fire({
        title: 'From Birthdate Must Be a Date Before To Birthdate',
        icon: 'warning',
        confirmButtonColor: '#FF7519',
        confirmButtonText: 'Ok',
      });
    }

    return valid
  }

  SaveGrade(){
    this.isLoading = true;
    if(this.isFormValid()){
      this.grade.sectionID = this.sectionId
      this.checkFromToDate()
      if(this.checkFromToDate()){
        if(this.editGrade == false){
          this.gradeService.Add(this.grade, this.DomainName).subscribe(
            (result: any) => {
              this.closeModal()
              this.isLoading = false;
              this.getGradeData()
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
          this.gradeService.Edit(this.grade, this.DomainName).subscribe(
            (result: any) => {
              this.closeModal()
              this.getGradeData()
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

  deleteGrade(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this Grade?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.gradeService.Delete(id, this.DomainName).subscribe(
          (data: any) => {
            this.gradeData=[]
            this.getGradeData()
          }
        );
      }
    });
  }
}
