import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { Template } from '../../../../Models/LMS/template';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EvaluationTemplateService } from '../../../../Services/Employee/LMS/evaluation-template.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { EvaluationTemplateGroups } from '../../../../Models/LMS/evaluation-template-groups';
import { EvaluationTemplateGroupService } from '../../../../Services/Employee/LMS/evaluation-template-group.service';

@Component({
  selector: 'app-evaluation-template-group',
  standalone: true,
  imports: [CommonModule , FormsModule,SearchComponent],
  templateUrl: './evaluation-template-group.component.html',
  styleUrl: './evaluation-template-group.component.css'
})
export class EvaluationTemplateGroupComponent {
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
   group: EvaluationTemplateGroups = new EvaluationTemplateGroups();
 
   validationErrors: { [key in keyof EvaluationTemplateGroups]?: string } = {};
   isLoading = false;
 
   TemplateID: number = 0;

   constructor(
     private router: Router,
     private menuService: MenuService,
     public activeRoute: ActivatedRoute,
     public account: AccountService,
     public DomainServ: DomainService,
     public EditDeleteServ: DeleteEditPermissionService,
     public ApiServ: ApiService,
     public templateServ: EvaluationTemplateService ,
     public GroupServ: EvaluationTemplateGroupService ,
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
     this.TemplateID = Number(this.activeRoute.snapshot.paramMap.get('id'));
     this.GetTemplateData();
   }
 
   GetTemplateData() {
     this.template = new Template();
     this.templateServ.GetByID(this.TemplateID,this.DomainName).subscribe((d) => {
       this.template = d;
       console.log(this.template)
     });
   }
 
   Create() {
     this.mode = 'Create';
     this.group = new EvaluationTemplateGroups();
     this.validationErrors = {};
     this.openModal();
   }
 
   Delete(id: number) {
     Swal.fire({
       title: 'Are you sure you want to delete this Group?',
       icon: 'warning',
       showCancelButton: true,
       confirmButtonColor: '#FF7519',
       cancelButtonColor: '#17253E',
       confirmButtonText: 'Delete',
       cancelButtonText: 'Cancel',
     }).then((result) => {
       if (result.isConfirmed) {
         this.GroupServ.Delete(id, this.DomainName).subscribe((d) => {
           this.GetTemplateData();
         });
       }
     });
   }
 
   Edit(row: EvaluationTemplateGroups) {
    this.validationErrors = {};
     this.mode = 'Edit';
     this.GroupServ.GetByID(row.id, this.DomainName).subscribe((d) => {
       this.group = d;
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
    this.group.evaluationTemplateID=this.TemplateID
     if (this.isFormValid()) {
       this.isLoading = true;
       if (this.mode == 'Create') {
         this.GroupServ.Add(
           this.group,
           this.DomainName
         ).subscribe(
           (d) => {
             this.GetTemplateData();
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
         this.GroupServ.Edit(
           this.group,
           this.DomainName
         ).subscribe(
           (d) => {
             this.GetTemplateData();
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
     this.GetTemplateData();
   }
 
   closeModal() {
     this.isModalVisible = false;
     this.validationErrors = {};
   }
 
   openModal() {
     this.validationErrors = {};
     this.isModalVisible = true;
   }
 
   isFormValid(): boolean {
     let isValid = true;
     for (const key in this.group) {
       if (this.group.hasOwnProperty(key)) {
         const field = key as keyof EvaluationTemplateGroups;
         if (!this.group[field]) {
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
   capitalizeField(field: keyof EvaluationTemplateGroups): string {
     return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
   }

   onInputValueChange(event: { field: keyof EvaluationTemplateGroups; value: any }) {
     const { field, value } = event;
     (this.group as any)[field] = value;
     if (value) {
       this.validationErrors[field] = '';
     }
   }
 
   async onSearchEvent(event: { key: string; value: any }) {
     this.key = event.key;
     this.value = event.value;
     try {
       const data: Template = await firstValueFrom(
        this.templateServ.GetByID(this.TemplateID,this.DomainName)
       );
       this.template = data ;
 
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

   moveToTemplate() {
    this.router.navigateByUrl('Employee/Template');
  }

  moveToQuestions(Id: number) {
    console.log(`Employee/EvaluationTemplateGroupQuestion/${Id}`)
    this.router.navigateByUrl(`Employee/EvaluationTemplateGroupQuestion/${Id}`);
  }
 }
 