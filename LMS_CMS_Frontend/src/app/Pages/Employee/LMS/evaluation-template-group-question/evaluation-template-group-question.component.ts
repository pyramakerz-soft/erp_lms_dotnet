import { Component } from '@angular/core';
import { EvaluationTemplateGroupQuestion } from '../../../../Models/LMS/evaluation-template-group-question';
import { EvaluationTemplateGroups } from '../../../../Models/LMS/evaluation-template-groups';
import { EvaluationTemplateGroupQuestionService } from '../../../../Services/Employee/LMS/evaluation-template-group-question.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EvaluationTemplateGroupService } from '../../../../Services/Employee/LMS/evaluation-template-group.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';

@Component({
  selector: 'app-evaluation-template-group-question',
  standalone: true,
  imports: [CommonModule , FormsModule,SearchComponent],
  templateUrl: './evaluation-template-group-question.component.html',
  styleUrl: './evaluation-template-group-question.component.css'
})
export class EvaluationTemplateGroupQuestionComponent {
 User_Data_After_Login: TokenData = new TokenData('', 0,0,0, 0,'','','','', '');
 
   AllowEdit: boolean = false;
   AllowDelete: boolean = false;
   AllowEditForOthers: boolean = false;
   AllowDeleteForOthers: boolean = false;
 
   DomainName: string = '';
   UserID: number = 0;
 
   isModalVisible: boolean = false;
   mode: string = '';
 
   path: string = '';
   key: string = 'id';
   value: any = '';
   keysArray: string[] = ['id', 'name'];
 
   question: EvaluationTemplateGroupQuestion = new EvaluationTemplateGroupQuestion();
   group: EvaluationTemplateGroups = new EvaluationTemplateGroups();
 
   validationErrors: { [key in keyof EvaluationTemplateGroupQuestion]?: string } = {};
   isLoading = false;
 
   GroupId: number = 0;

   constructor(
     private router: Router,
     private menuService: MenuService,
     public activeRoute: ActivatedRoute,
     public account: AccountService,
     public DomainServ: DomainService,
     public EditDeleteServ: DeleteEditPermissionService,
     public ApiServ: ApiService,
     public questionsServ: EvaluationTemplateGroupQuestionService ,
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
     this.GroupId = Number(this.activeRoute.snapshot.paramMap.get('id'));
     this.GetGroupData();
   }
 
   GetGroupData() {
     this.group = new EvaluationTemplateGroups();
     this.GroupServ.GetByID(this.GroupId,this.DomainName).subscribe((d) => {
       this.group = d;
     });
   }
 
   Create() {
     this.mode = 'Create';
     this.question = new EvaluationTemplateGroupQuestion();
     this.validationErrors = {};
     this.openModal();
   }
 
   Delete(id: number) {
     Swal.fire({
       title: 'Are you sure you want to delete this Question?',
       icon: 'warning',
       showCancelButton: true,
       confirmButtonColor: '#FF7519',
       cancelButtonColor: '#17253E',
       confirmButtonText: 'Delete',
       cancelButtonText: 'Cancel',
     }).then((result) => {
       if (result.isConfirmed) {
         this.questionsServ.Delete(id, this.DomainName).subscribe((d) => {
           this.GetGroupData();
         });
       }
     });
   }
 
   Edit(row: EvaluationTemplateGroupQuestion) {
     this.mode = 'Edit';
     this.questionsServ.GetByID(row.id, this.DomainName).subscribe((d) => {
       this.question = d;
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
    this.question.evaluationTemplateGroupID=this.GroupId
     if (this.isFormValid()) {
       this.isLoading = true;
       if (this.mode == 'Create') {
         this.questionsServ.Add(
           this.question,
           this.DomainName
         ).subscribe(
           (d) => {
             this.GetGroupData();
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
         this.questionsServ.Edit(
           this.question,
           this.DomainName
         ).subscribe(
           (d) => {
             this.GetGroupData();
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
     this.GetGroupData();
   }
 
   closeModal() {
     this.isModalVisible = false;
   }
 
   openModal() {
    this.validationErrors = {};
     this.isModalVisible = true;
   }
 
   isFormValid(): boolean {
     let isValid = true;
     for (const key in this.question) {
       if (this.question.hasOwnProperty(key)) {
         const field = key as keyof EvaluationTemplateGroupQuestion;
         if (!this.question[field]) {
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
   capitalizeField(field: keyof EvaluationTemplateGroupQuestion): string {
     return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
   }

   onInputValueChange(event: { field: keyof EvaluationTemplateGroupQuestion; value: any }) {
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
       const data: EvaluationTemplateGroups = await firstValueFrom(
        this.GroupServ.GetByID(this.GroupId,this.DomainName)
       );
       this.group = data ;
 
       if (this.value !== '') {
         const numericValue = isNaN(Number(this.value))
           ? this.value
           : parseInt(this.value, 10);
 
         this.group.evaluationTemplateGroupQuestions = this.group.evaluationTemplateGroupQuestions.filter((t) => {
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
       this.group.evaluationTemplateGroupQuestions = [];
     }
   }

   moveToGroup() {
    this.router.navigateByUrl('Employee/EvaluationTemplateGroup/'+this.GroupId);
  }
 }
 