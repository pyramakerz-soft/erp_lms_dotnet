import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EmployeeGet } from '../../../../Models/Employee/employee-get';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { TokenData } from '../../../../Models/token-data';
import { EmployeeTypeService } from '../../../../Services/Employee/employee-type.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  DomainName: string = "";
  UserID: number = 0;
  path: string = "";

  TableData: EmployeeGet[] = []

  constructor(public activeRoute: ActivatedRoute, public account: AccountService, public ApiServ: ApiService, private menuService: MenuService, public EditDeleteServ: DeleteEditPermissionService, private router: Router, public EmpServ: EmployeeService) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    if (this.User_Data_After_Login.type === "employee") {
      this.DomainName = this.ApiServ.GetHeader();
      this.activeRoute.url.subscribe(url => {
        this.path = url[0].path
        this.GetEmployee();

      });
    }
  }
  GetEmployee() {
    this.EmpServ.Get_Employees(this.DomainName).subscribe((data) => {
      console.log("fdf", data)
      this.TableData = data
    })
  }
}
