import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { SubjectCategory } from '../../../../Models/LMS/subject-category';
import Swal from 'sweetalert2';
import { SubjectCategoryService } from '../../../../Services/Employee/LMS/subject-category.service';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { ActivatedRoute } from '@angular/router';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-subject-category',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './subject-category.component.html',
  styleUrl: './subject-category.component.css'
})
export class SubjectCategoryComponent {
  keysArray: string[] = ['id', 'name'];
  key: string= "id";
  value: any = "";

  subjectCategoryData:SubjectCategory[] = []
  subjectCategory:SubjectCategory = new SubjectCategory()
  editSubjectCategory:boolean = false
  validationErrors: { [key in keyof SubjectCategory]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  path: string = ""

  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  isLoading = false;
  
  constructor(public account: AccountService, public subjectCategoryService: SubjectCategoryService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, public activeRoute: ActivatedRoute, private menuService: MenuService){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.getSubjectCategoryData()

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

  getSubjectCategoryData(){
    this.subjectCategoryData=[]
    this.subjectCategoryService.Get(this.DomainName).subscribe(
      (data) => {
        this.subjectCategoryData = data;
      }
    )
  }

  GetSubjectCategoryById(accountId: number) {
    this.subjectCategoryService.GetByID(accountId, this.DomainName).subscribe((data) => {
      this.subjectCategory = data;
    });
  }

  openModal(accountId?: number) {
    if (accountId) {
      this.editSubjectCategory = true;
      this.GetSubjectCategoryById(accountId); 
    }
    
    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.subjectCategory= new SubjectCategory()

    if(this.editSubjectCategory){
      this.editSubjectCategory = false
    }
    this.validationErrors = {}; 
  }
  
  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: SubjectCategory[] = await firstValueFrom(this.subjectCategoryService.Get(this.DomainName));  
      this.subjectCategoryData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.subjectCategoryData = this.subjectCategoryData.filter(t => {
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
      this.subjectCategoryData = [];
    }
  }

  capitalizeField(field: keyof SubjectCategory): string {
      return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.subjectCategory) {
      if (this.subjectCategory.hasOwnProperty(key)) {
        const field = key as keyof SubjectCategory;
        if (!this.subjectCategory[field]) {
          if(field == "name"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.subjectCategory.name.length > 100){
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

  onInputValueChange(event: { field: keyof SubjectCategory, value: any }) {
    const { field, value } = event;
    if (field == "name" ) {
      (this.subjectCategory as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
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

  SaveSubjectCategory(){
    if(this.isFormValid()){
      this.isLoading = true;
      if(this.editSubjectCategory == false){
        this.subjectCategoryService.Add(this.subjectCategory, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.isLoading = false;
            this.getSubjectCategoryData()
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
        this.subjectCategoryService.Edit(this.subjectCategory, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.getSubjectCategoryData()
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

  deleteSubjectCategory(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this Subject Category?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.subjectCategoryService.Delete(id, this.DomainName).subscribe(
          (data: any) => {
            this.subjectCategoryData=[]
            this.getSubjectCategoryData()
          }
        );
      }
    });
  }
}
