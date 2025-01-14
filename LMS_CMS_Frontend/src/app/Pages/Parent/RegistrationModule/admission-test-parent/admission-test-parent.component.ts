import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { RegistrationCategory } from '../../../../Models/Registration/registration-category';
import { Test } from '../../../../Models/Registration/test';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { RegistrationCategoryService } from '../../../../Services/Employee/Registration/registration-category.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TestService } from '../../../../Services/Employee/Registration/test.service';
import { RegisterationFormParentService } from '../../../../Services/Employee/Registration/registeration-form-parent.service';
import { RegisterationFormParent } from '../../../../Models/Registration/registeration-form-parent';
import { RegisterationFormTestService } from '../../../../Services/Employee/Registration/registeration-form-test.service';
import { RegisterationFormTest } from '../../../../Models/Registration/registeration-form-test';

@Component({
  selector: 'app-admission-test-parent',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './admission-test-parent.component.html',
  styleUrl: './admission-test-parent.component.css'
})
export class AdmissionTestParentComponent {
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

  Data: RegisterationFormTest[] = [];
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  mode: string = 'Create'

  isModalVisible: boolean = false;

  Category: RegistrationCategory = new RegistrationCategory();

  validationErrors: { [key in keyof RegistrationCategory]?: string } = {};
  RegesterFormParentID:number=0;
  Students:RegisterationFormParent[]=[]
 

  
  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public testServ: TestService,
    public RegisterationFormParentServ :RegisterationFormParentService,
    public registrationserv: RegisterationFormTestService
  ) { }

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
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });

    this.GetAllStudents();
  }

  GetAllStudents() {
    this.RegisterationFormParentServ.GetByParentId(this.UserID ,this.DomainName).subscribe((d)=>{
      this.Students=d;
    })
  }

  GetAllData() {
    this.registrationserv.GetByRegistrationParentIdForParent(this.RegesterFormParentID, this.DomainName).subscribe((d:any) => {
      console.log(d)
      this.Data = d.tests;
    })
  }

  SelectStudent(event: Event) {
    const selectedId = Number((event.target as HTMLSelectElement).value); 
    this.RegesterFormParentID = selectedId; 
    this.GetAllData();
    console.log('Selected Student ID:', selectedId);
  }
  View(row:RegisterationFormTest){
    this.router.navigateByUrl(`Parent/Test/${row.id}/${row.registerationFormParentID}/${row.testID}`)
  }
}
