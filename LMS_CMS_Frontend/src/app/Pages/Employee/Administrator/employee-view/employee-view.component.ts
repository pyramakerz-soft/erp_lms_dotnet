import { Component } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { EmployeeGet } from '../../../../Models/Employee/employee-get';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EditPass } from '../../../../Models/Employee/edit-pass';

@Component({
  selector: 'app-employee-view',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-view.component.html',
  styleUrl: './employee-view.component.css'
})
export class EmployeeViewComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  DomainName: string = "";
  UserID: number = 0;
  path: string = "";

  Data: EmployeeGet = new EmployeeGet()
  EmpId: number = 0;

  PasswordError: string = ""; 
  isChange = false;
  password:string =""

  editpasss:EditPass=new EditPass();

  AllowEdit: boolean = false;
  AllowEditForOthers: boolean = false;


  constructor(public activeRoute: ActivatedRoute, public account: AccountService, public ApiServ: ApiService, private menuService: MenuService, public EditDeleteServ: DeleteEditPermissionService, private router: Router, public EmpServ: EmployeeService) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    if (this.User_Data_After_Login.type === "employee") {
      this.DomainName = this.ApiServ.GetHeader();
      this.activeRoute.url.subscribe(url => {
        this.path = url[0].path
        this.EmpId = Number(this.activeRoute.snapshot.paramMap.get('id'))
        this.EmpServ.Get_Employee_By_ID(this.EmpId, this.DomainName).subscribe(async (data) => {
          this.Data = data;
          if (data.files == null) {
            this.Data.files = []
          }
          this.Data.id = this.EmpId;
        })
      });
    }
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName("Employee", items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
  }

  moveToEmployee() {
    this.router.navigateByUrl("Employee/Employee")
  }
  edit() {
    this.router.navigateByUrl(`Employee/Employee Edit/${this.EmpId}`)
  }
  downloadFile(file: any): void {
    const fileURL = file.link;
    const a = document.createElement('a');
    a.href = fileURL;
    a.download = file.name || 'downloaded_file';
    a.target = '_blank';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  }

  toggleChangePassword() {
    this.isChange = !this.isChange;
  }

  UpdatePassword(){
    this.editpasss.Id=this.EmpId;
    this.editpasss.Password=this.password
   this.EmpServ.EditPassword(this.editpasss,this.DomainName).subscribe(()=>{
    this.isChange = false
    this.password = '';
   })
  }

  CancelUpdatePassword(){
    this.isChange = false
    this.password = '';
  }
  onPasswordChange() {
    this.PasswordError = "" 
  }
  IsAllowEdit() {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(this.Data.insertedByUserId, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }
}
