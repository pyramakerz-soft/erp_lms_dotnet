import { Component } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { RegisterationFormTestService } from '../../../../Services/Employee/Registration/registeration-form-test.service';
import { RegisterationFormTest } from '../../../../Models/Registration/registeration-form-test';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-registration-confirmation-test-details',
  standalone: true,
  imports: [CommonModule , FormsModule],
  templateUrl: './registration-confirmation-test-details.component.html',
  styleUrl: './registration-confirmation-test-details.component.css'
})
export class RegistrationConfirmationTestDetailsComponent {

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

  RegisterFormParentID: number = 0;
  Data: RegisterationFormTest[] = [];
  RegesterForm: RegisterationFormTest = new RegisterationFormTest();
  isModalVisible: boolean = false;

  StudentName: string = ""

  MarkIsEmpty : boolean=false;

  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public registrationserv: RegisterationFormTestService
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
      this.RegisterFormParentID = Number(this.activeRoute.snapshot.paramMap.get('id'))
      this.GetAllData();
    });


    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName("Registration Confirmation", items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
  }

  GetAllData() {
    this.registrationserv.GetByRegistrationParentId(this.RegisterFormParentID, this.DomainName).subscribe((d:any) => {
      console.log(d)
      this.Data = d.tests;
      this.StudentName=d.studentName
    })
  }

  Save() {
    console.log(this.RegesterForm.mark)
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

  openModal() {
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }
  moveToEmployee() {
  }

  Edit(row: RegisterationFormTest) {
    this.mode = 'Edit';
    this.RegesterForm = row;
    this.openModal();
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  View(row:RegisterationFormTest){
    this.router.navigateByUrl(`Employee/Registration Confirmation Test Answer/${row.id}/${row.registerationFormParentID}/${row.testID}`)
  }

}
