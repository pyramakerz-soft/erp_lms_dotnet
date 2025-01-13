import { Component } from '@angular/core';
import { Test } from '../../../../Models/Registration/test';
import { Question } from '../../../../Models/Registration/question';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuestionType } from '../../../../Models/Registration/question-type';
import { TestService } from '../../../../Services/Employee/Registration/test.service';
import { QuestionService } from '../../../../Services/Employee/Registration/question.service';
import { QuestionTypeService } from '../../../../Services/Employee/Registration/question-type.service';
import { QuestionAddEdit } from '../../../../Models/Registration/question-add-edit';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-questions',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './questions.component.html',
  styleUrl: './questions.component.css'
})
export class QuestionsComponent {

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

  DomainName: string = '';
  UserID: number = 0;
  path: string = '';

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  mode: string = 'Create'

  isModalVisible: boolean = false;
  Data: Question[] = []
  test: Test = new Test();
  question: QuestionAddEdit = new QuestionAddEdit();
  testId: number = 0;
  QuestionTypes:QuestionType[]=[]

  options:string[]=[]
  NewOption:string=""

  validationErrors: { [key in keyof Question]?: string } = {};


  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public testServ: TestService,
    public QuestionServ :QuestionService,
    public QuestionTypeServ :QuestionTypeService,
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
      this.testId = Number(this.activeRoute.snapshot.paramMap.get('id'))
      this.getTestInfo()
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName("Admission Test", items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
    this.GetAllData();
    this.GetQuestionType();
  }

  moveToEmployee() {

  }

  GetAllData() {
    this.QuestionServ.GetByTestID(this.testId,this.DomainName).subscribe((d:any)=>{
      console.log(d)
      this.Data=d
    })
  }
  GetQuestionType(){
   this.QuestionTypeServ.Get(this.DomainName).subscribe((d)=>{
    this.QuestionTypes=d
   })
  }
  getTestInfo(){
    this.testServ.GetByID(this.testId,this.DomainName).subscribe((d)=>{
      this.test=d
    })
  }

  Create() {
    this.mode = 'Create';
    this.question = new QuestionAddEdit();
    this.options=[]
    this.openModal();
  }

  Delete(id: number) {
  Swal.fire({
          title: 'Are you sure you want to delete this question?',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#FF7519',
          cancelButtonColor: '#17253E',
          confirmButtonText: 'Delete',
          cancelButtonText: 'Cancel'
        }).then((result) => {
          if (result.isConfirmed) {
            this.QuestionServ.Delete(id, this.DomainName).subscribe(
              (data: any) => {
                this.GetAllData();
              }
            );
          }
        });
  }

  Edit(row: Question) {
    this.mode = 'Edit';
    this.question = row as unknown as QuestionAddEdit;
    this.options = row.options.map(option => option.name); 
    console.log(this.question)
    this.openModal();
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  CreateOREdit() {
    this.question.options=this.options
    this.question.testID=this.testId
    if(this.isFormValid()){
    console.log(this.question)
     if(this.mode=="Create"){
      this.QuestionServ.Add(this.question,this.DomainName).subscribe(()=>{
        this.GetAllData();
       this.closeModal()
      })
     } if(this.mode=="Edit"){
      this.QuestionServ.Edit(this.question,this.DomainName).subscribe(()=>{
        this.GetAllData();
        this.closeModal();
      })
    }
  }
  }

  closeModal() {
    this.isModalVisible = false;
  }

  CorrectAnswer(option :string){
    this.question.correctAnswerName=option;
  }

  openModal() {
    this.isModalVisible = true;
  }

  AddOption(){
    this.options.push(this.NewOption);
    this.NewOption=''
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.question) {
      if (this.question.hasOwnProperty(key)) {
        const field = key as keyof QuestionAddEdit;
        if (!this.question[field]) {
          if(field == "description" || field == "questionTypeID" || field == "testID"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } 
      }
    }
    if(this.question.questionTypeID==1||this.question.questionTypeID){
      if(this.question.options.length==0){
        this.validationErrors["options"] = `*${this.capitalizeField("options")} is required`
      }
      if(this.question.correctAnswerName==""){
        this.validationErrors["correctAnswerName"] = `*${this.capitalizeField("correctAnswerName")} is required`
      }
    }
    return isValid;
  }
  capitalizeField(field: keyof QuestionAddEdit): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }
  onInputValueChange(event: { field: keyof QuestionAddEdit, value: any }) {
    const { field, value } = event;
    (this.question as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    } else {
      this.validationErrors[field] = `*${this.capitalizeField(field)} is required`;
    }
  }

}
