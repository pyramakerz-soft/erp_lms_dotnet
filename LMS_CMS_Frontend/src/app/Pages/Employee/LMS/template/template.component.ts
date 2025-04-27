import { Component } from '@angular/core';
import { Template } from '../../../../Models/LMS/template';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { SubjectCategory } from '../../../../Models/LMS/subject-category';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { firstValueFrom } from 'rxjs';
import { BusCategoryService } from '../../../../Services/Employee/Bus/bus-category.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EvaluationTemplateService } from '../../../../Services/Employee/LMS/evaluation-template.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';

@Component({
  selector: 'app-template',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './template.component.html',
  styleUrl: './template.component.css'
})
export class TemplateComponent {

  User_Data_After_Login: TokenData = new TokenData('', 0,0,0, 0,'','','','', '');
 
   AllowEdit: boolean = false;
   AllowDelete: boolean = false;
   AllowEditForOthers: boolean = false;
   AllowDeleteForOthers: boolean = false;
 
   TableData: Template[] = [];
 
   DomainName: string = '';
   UserID: number = 0;
 
   isModalVisible: boolean = false;
   mode: string = '';
 
   path: string = '';
   key: string = 'id';
   value: any = '';
   keysArray: string[] = ['id', 'name'];
 
   template: Template = new Template();
 
   validationErrors: { [key in keyof Template]?: string } = {};
   isLoading = false;
 
   constructor(
     private router: Router,
     private menuService: MenuService,
     public activeRoute: ActivatedRoute,
     public account: AccountService,
     public DomainServ: DomainService,
     public EditDeleteServ: DeleteEditPermissionService,
     public ApiServ: ApiService,
     public templateServ: EvaluationTemplateService
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
   }
 
   GetAllData() {
     this.TableData = [];
     this.templateServ.Get(this.DomainName).subscribe((d) => {
       this.TableData = d;
       console.log(this.TableData)
     });
   }
 
   Create() {
     this.mode = 'Create';
     this.template = new Template();
     this.validationErrors = {};
     this.openModal();
   }
 
   Delete(id: number) {
     Swal.fire({
       title: 'Are you sure you want to delete this Academic Degree?',
       icon: 'warning',
       showCancelButton: true,
       confirmButtonColor: '#FF7519',
       cancelButtonColor: '#17253E',
       confirmButtonText: 'Delete',
       cancelButtonText: 'Cancel',
     }).then((result) => {
       if (result.isConfirmed) {
         this.templateServ.Delete(id, this.DomainName).subscribe((d) => {
           this.GetAllData();
         });
       }
     });
   }
 
   Edit(row: Template) {
     this.mode = 'Edit';
     this.templateServ.GetByID(row.id, this.DomainName).subscribe((d) => {
       this.template = d;
     });
     this.openModal();
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
 
   CreateOREdit() {
     if (this.isFormValid()) {
       this.isLoading = true;
       if (this.mode == 'Create') {
         this.templateServ.Add(
           this.template,
           this.DomainName
         ).subscribe(
           (d) => {
             this.GetAllData();
             this.isLoading = false;
             this.closeModal();
           },
           (error) => {
             this.isLoading = false; // Hide spinner
             Swal.fire({
               icon: 'error',
               title: 'Oops...',
               text: 'Try Again Later!',
               confirmButtonText: 'Okay',
               customClass: { confirmButton: 'secondaryBg' }
             });
           }
         );
       }
       if (this.mode == 'Edit') {
         this.templateServ.Edit(
           this.template,
           this.DomainName
         ).subscribe(
           (d) => {
             this.GetAllData();
             this.isLoading = false;
             this.closeModal();
           },
           (error) => {
             this.isLoading = false; // Hide spinner
             Swal.fire({
               icon: 'error',
               title: 'Oops...',
               text: 'Try Again Later!',
               confirmButtonText: 'Okay',
               customClass: { confirmButton: 'secondaryBg' }
             });
           }
         );
       }
     }
     this.GetAllData();
   }
 
   closeModal() {
     this.isModalVisible = false;
   }
 
   openModal() {
     this.isModalVisible = true;
   }
 
   isFormValid(): boolean {
     let isValid = true;
     for (const key in this.template) {
       if (this.template.hasOwnProperty(key)) {
         const field = key as keyof Template;
         if (!this.template[field]) {
           if (
                field == 'englishTitle' ||
                field == 'arabicTitle' 
              ) {
             this.validationErrors[field] = `*${this.capitalizeField(
               field
             )} is required`;
             isValid = false;
           }
         }
       }
     }
     return isValid;
   }
   capitalizeField(field: keyof Template): string {
     return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
   }
   onInputValueChange(event: { field: keyof Template; value: any }) {
     const { field, value } = event;
     (this.template as any)[field] = value;
     if (value) {
       this.validationErrors[field] = '';
     }
   }
 
   async onSearchEvent(event: { key: string; value: any }) {
     this.key = event.key;
     this.value = event.value;
     try {
       const data: Template[] = await firstValueFrom(
         this.templateServ.Get(this.DomainName)
       );
       this.TableData = data || [];
 
       if (this.value !== '') {
         const numericValue = isNaN(Number(this.value))
           ? this.value
           : parseInt(this.value, 10);
 
         this.TableData = this.TableData.filter((t) => {
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
       this.TableData = [];
     }
   }
 }
 