import { Component } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { Test } from '../../../../Models/Registration/test';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { School } from '../../../../Models/school';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { Grade } from '../../../../Models/LMS/grade';
import { Subject } from '../../../../Models/LMS/subject';

@Component({
  selector: 'app-admission-test',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './admission-test.component.html',
  styleUrl: './admission-test.component.css'
})
export class AdmissionTestComponent {

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

  Data: Test[] = [];
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  mode: string = 'Create'

  isModalVisible: boolean = false;

  test: Test = new Test();

  Schools:School[]=[];
  AcadenicYears:AcademicYear[]=[]
  Grades:Grade[]=[]
  Subjects:Subject[] = []

  SchoolId:number=0;
  
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

  }

  Create() {
    this.mode = 'Create';
    this.test = new Test();
    this.openModal();
  }

  Delete(id: number) {

  }

  Edit(row: Test) {
    this.mode = 'Edie';
    this.test = row;
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

  view(id: number) {

  }

  onInputValueChange(){
    
  }
}
