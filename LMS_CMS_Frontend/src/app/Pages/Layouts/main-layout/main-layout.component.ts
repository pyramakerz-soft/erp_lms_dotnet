import { Component } from '@angular/core';
import { SideMenuComponent } from '../../../Component/side-menu/side-menu.component';
import { TokenData } from '../../../Models/token-data';
import { EmployeeService } from '../../../Services/Employee/employee.service';
import { AccountService } from '../../../Services/account.service';
import { RouterOutlet } from '@angular/router';
import { NavMenuComponent } from '../../../Component/nav-menu/nav-menu.component';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [SideMenuComponent, RouterOutlet, NavMenuComponent],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  menuItems: { label: string; route?: string; subItems?: { label: string; route: string }[] }[] = [];

  Employee_With_Permission = null
  User_Data_After_Login = new TokenData("", 0, "", "", "", "", "", "")

  constructor(public employeeService:EmployeeService, public accountService:AccountService){}

  ngOnInit(){
    this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
    if(this.User_Data_After_Login.type == "employee"){
      this.Get_Employee_With_Role_Permission()
    } else if(this.User_Data_After_Login.type == "student"){
      this.menuItems = [
        {
          label: 'Dashboard Student', route: '#'
        }
      ]
    } else if(this.User_Data_After_Login.type == "parent"){
      this.menuItems = [
        {
          label: 'Dashboard Parent', route: '#'
        }
      ]
    }
  }

  Get_Employee_With_Role_Permission(){
    this.employeeService.Get_Employee_With_Role_Permission(this.User_Data_After_Login.id).subscribe(
      (d: any) => {
        this.Employee_With_Permission = d
        d.roles.forEach((role: any) => {
          role.detailedPermissions.forEach((DetailedPermission: any) => {
            let masterName = DetailedPermission.masterPermission.name;
            let permissionLabel = DetailedPermission.name;

            let masterMenuItem = this.menuItems.find(item => item.label === masterName); // If Master ALready Exists

            if (masterMenuItem) {
              if (!masterMenuItem.subItems?.some(sub => sub.label === permissionLabel)) {
                masterMenuItem.subItems?.push({ label: permissionLabel, route: '#' });
              }
            } else {
              this.menuItems.push({
                label: masterName,
                subItems: [{ label: permissionLabel, route: '#' }]
              });
            }
          })
        });
      },(error)=>{
        console.log(error)
      }
    )
  }
}
