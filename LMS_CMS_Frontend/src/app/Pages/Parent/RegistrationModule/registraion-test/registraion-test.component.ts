import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterationFormTest } from '../../../../Models/Registration/registeration-form-test';
import { RegisterationFormTestAnswer } from '../../../../Models/Registration/registeration-form-test-answer';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { RegisterationFormTestAnswerService } from '../../../../Services/Employee/Registration/registeration-form-test-answer.service';
import { RegisterationFormTestService } from '../../../../Services/Employee/Registration/registeration-form-test.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-registraion-test',
  standalone: true,
  imports: [CommonModule ,FormsModule],
  templateUrl: './registraion-test.component.html',
  styleUrl: './registraion-test.component.css',
})
export class RegistraionTestComponent {
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

  mode: string = 'Create';

  isModalVisible: boolean = false;
  Tid: number = 0;
  Pid: number = 0;
  Rid: number = 0;

  TestName: string = '';
  Mark:number = 0;
  Mode : string = 'degree' ;
  MarkIsEmpty: boolean = false;

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
  ) {}

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
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });

    this.GetAllData();
  }

  GetAllData() {
    this.registerServ
      .GetByRegistrationParentId(this.Pid, this.Tid, this.DomainName)
      .subscribe((d: any) => {
        console.log(d)
        this.Data = d.questionWithAnswer;
        this.TestName = d.testName;
        this.Mark =d.mark;
        console.log(this.Data);
      });
  }
  moveToEmployee() {
  }
}
