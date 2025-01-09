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
  question: Question = new Question();

  testId: number = 0;

  QuestionTypes:QuestionType[]=[]

  options:string[]=[]
  NewOption:string=""


  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
      this.testId = Number(this.activeRoute.snapshot.paramMap.get('id'))
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });

    this.GetAllData();
  }

  moveToEmployee() {

  }

  GetAllData() {

  }
  GetQuestionType(){

  }

  Create() {
    this.mode = 'Create';
    this.question = new Question();
    this.openModal();
  }

  Delete(id: number) {

  }

  Edit(row: Question) {
    this.mode = 'Edie';
    this.question = row;
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

  }

  closeModal() {
    this.isModalVisible = false;
  }

  openModal() {
    this.isModalVisible = true;
  }

  onInputValueChange() {

  }

  AddOption(){
    this.options.push(this.NewOption);
    this.NewOption=''
  }
}
