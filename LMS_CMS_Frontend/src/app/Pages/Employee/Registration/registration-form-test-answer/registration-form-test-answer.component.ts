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
import { TranslateModule } from '@ngx-translate/core';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-registration-form-test-answer',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule],
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

  isModalVisible: boolean = false;
  TestId: number = 0
  RegisterFormParentID: number = 0
  RegisterFormID: number = 0
  RegisterFormState: number = 0

  TestName: string = ""
  MarkIsEmpty : boolean=false;

  RegesterForm: RegisterationFormTest = new RegisterationFormTest();
  isLoading=false
  
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
        this.RegisterFormParentID = Number(params.get('Pid')); 
        this.TestId = Number(params.get('Tid')); 
        this.RegisterFormID = Number(params.get('Rid')); 

        this.RegesterForm.registerationFormParentID=this.RegisterFormParentID;
        this.RegesterForm.testID=this.TestId;
        this.RegesterForm.id=this.RegisterFormID;
      });
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName("Registration Confirmation Test", items);
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
    this.Data=[]
    this.registerServ.GetByRegistrationParentId(this.RegisterFormParentID, this.TestId, this.DomainName).subscribe((d: any) => {
      this.RegesterForm.mark=d.mark
      this.RegesterForm.visibleToParent=d.isVisibleToParent
      this.RegesterForm.stateID=d.State
      this.Data = d.questionWithAnswer;
      this.TestName = d.testName
    })
  }

  moveToEmployee() {
    this.router.navigateByUrl(`Employee/Registration Confirmation Test/${this.RegisterFormParentID}`)
  }

  openModal() {
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  AddDegree() {  
    this.openModal(); 
  }

  Save() { 
    this.RegesterForm.registerationFormParentID=this.RegisterFormParentID;
    this.RegesterForm.testID=this.TestId;
    this.RegesterForm.id=this.RegisterFormID;
    if(!this.RegesterForm.mark){
      this.MarkIsEmpty=true
    }
    else{
      this.isLoading=true
      this.registrationserv.Edit(this.RegesterForm, this.DomainName).subscribe(() => {
        this.GetAllData();
        this.closeModal();
        this.isLoading=false
      },
      error => {
        this.isLoading=false
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Try Again Later!',
          confirmButtonText: 'Okay',
          customClass: { confirmButton: 'secondaryBg' },
        });
      })
    }
  }
 
  validateNumber(event: any, field: keyof RegisterationFormTest): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.RegesterForm[field] === 'string') {
        this.RegesterForm[field] = '' as never;  
      }
    }
  }
}
