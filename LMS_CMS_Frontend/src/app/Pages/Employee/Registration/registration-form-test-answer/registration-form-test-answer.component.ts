import { Component } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { RegisterationFormTestAnswer } from '../../../../Models/Registration/registeration-form-test-answer';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { RegistrationCategoryService } from '../../../../Services/Employee/Registration/registration-category.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { RegisterationFormTestAnswerService } from '../../../../Services/Employee/Registration/registeration-form-test-answer.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegisterationFormTest } from '../../../../Models/Registration/registeration-form-test';
import { RegisterationFormTestService } from '../../../../Services/Employee/Registration/registeration-form-test.service';

@Component({
  selector: 'app-registration-form-test-answer',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './registration-form-test-answer.component.html',
  styleUrl: './registration-form-test-answer.component.css'
})
export class RegistrationFormTestAnswerComponent {
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

  Data: RegisterationFormTestAnswer[] = [];
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  mode: string = 'Create'

  isModalVisible: boolean = false;
  Tid: number = 0
  Pid: number = 0
  Rid: number = 0

  TestName: string = ""
  MarkIsEmpty : boolean=false;

  RegesterForm: RegisterationFormTest = new RegisterationFormTest();
  
  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public registerServ: RegisterationFormTestAnswerService,
    public registrationserv: RegisterationFormTestService
    
  ) { }

  ngOnInit() {

    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
      this.activeRoute.paramMap.subscribe((params) => {
        this.Pid = Number(params.get('Pid')); // Retrieve and convert Pid to a number
        this.Tid = Number(params.get('Tid')); // Retrieve and convert Tid to a number
        this.Rid = Number(params.get('Rid')); // Retrieve and convert Tid to a number
      });
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

  GetAllData() {
    this.registerServ.GetByRegistrationParentId(this.Pid, this.Tid, this.DomainName).subscribe((d: any) => {
      this.Data = d.questionWithAnswer;
      this.TestName = d.testName
      console.log(this.Data)
    })
  }

  moveToEmployee() {
    this.router.navigateByUrl(`Employee/Registration Confirmation Test/${this.Tid}`)
  }

  openModal() {
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  AddDegree() {
    this.RegesterForm=new RegisterationFormTest()
    this.openModal();
  
  }

  Save() {
    this.RegesterForm.testID=this.Tid;
    this.RegesterForm.registerationFormParentID=this.Pid;
    this.RegesterForm.testID=this.Tid;
    this.RegesterForm.id=this.Rid;
    console.log(this.RegesterForm)
    if(!this.RegesterForm.mark){
      this.MarkIsEmpty=true
    }
    else{
      this.registrationserv.Edit(this.RegesterForm, this.DomainName).subscribe(() => {
        this.GetAllData();
        this.closeModal();
      })
    }
  }

}
