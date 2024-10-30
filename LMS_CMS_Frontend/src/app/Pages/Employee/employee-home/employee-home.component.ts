import { Component } from '@angular/core';
import { EmployeeService } from '../../../Services/Employee/employee.service';
import { AccountService } from '../../../Services/account.service';
import { TokenData } from '../../../Models/token-data';

@Component({
  selector: 'app-employee-home',
  standalone: true,
  imports: [],
  templateUrl: './employee-home.component.html',
  styleUrl: './employee-home.component.css'
})
export class EmployeeHomeComponent {
  Employee_With_Permission = null
  User_Data_After_Login = new TokenData("", 0, "", "", "", "", "", "")

  constructor(public employeeService:EmployeeService, public accountService:AccountService){}

  ngOnInit(){
    this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
    this.Get_Employee_With_Role_Permission()
  }

  Get_Employee_With_Role_Permission(){
    this.employeeService.Get_Employee_With_Role_Permission(this.User_Data_After_Login.id).subscribe(
      (d: any) => {
        this.Employee_With_Permission = d
        console.log(this.Employee_With_Permission)
      },(error)=>{
        console.log(error)
      }
    )
  }
}
