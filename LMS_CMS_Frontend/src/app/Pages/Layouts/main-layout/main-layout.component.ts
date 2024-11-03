import { Component } from '@angular/core';
import { SideMenuComponent } from '../../../Component/side-menu/side-menu.component';
import { TokenData } from '../../../Models/token-data';
import { EmployeeService } from '../../../Services/Employee/employee.service';
import { AccountService } from '../../../Services/account.service';
import { RouterOutlet } from '@angular/router';
import { NavMenuComponent } from '../../../Component/nav-menu/nav-menu.component';
import { EmployeePermission } from '../../../Models/employee-permission';
import { GetDataFromLayoutService } from '../../../Services/Employee/get-data-from-layout.service';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [SideMenuComponent, RouterOutlet, NavMenuComponent],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  menuItems: { label: string; route?: string; subItems?: { label: string; route: string }[] }[] = [];

  Employee_With_Permission = new EmployeePermission(0, "", "", [])
  User_Data_After_Login = new TokenData("", 0, 0, "", "", "", "", "")

  constructor(public employeeService: EmployeeService, public accountService: AccountService, public getDataService: GetDataFromLayoutService) { }

  async ngOnInit() {
    this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
    if (this.User_Data_After_Login.type == "employee") {
      await this.Get_Employee_With_Role_Permission()
    } else if (this.User_Data_After_Login.type == "student") {
      this.menuItems = [
        {
          label: 'Dashboard Student', route: '#'
        }
      ]
    } else if (this.User_Data_After_Login.type == "parent") {
      this.menuItems = [
        {
          label: 'Dashboard Parent', route: '#'
        }
      ]
    }
  }

   Get_Employee_With_Role_Permission() {
    this.employeeService.Get_Employee_With_Role_Permission(this.User_Data_After_Login.id).subscribe(
      async (d: any) => {
        this.Employee_With_Permission = d[0].employee
       await this.getDataService.setData(this.Employee_With_Permission)
        this.Employee_With_Permission.roles.forEach((role) => {
          role.modules.forEach((module) => {
            let moduleName = module.moduleName;
            module.masterPermissions.forEach((master) => {
              let masterName = master.masterPermissionName
              let moduleMenuItem = this.menuItems.find(item => item.label === moduleName);
              let RouteName = `${moduleName.replace(" ", "-")}/${masterName.replace(" ", "-")}`
              if (moduleMenuItem) {
                if (!moduleMenuItem.subItems?.some(sub => sub.label === masterName)) {
                  moduleMenuItem.subItems?.push({ label: masterName, route: RouteName });
                }
              } else {
                this.menuItems.push({
                  label: moduleName,
                  subItems: [{ label: masterName, route: RouteName }]
                });
              }
            })
          })
        })
      }, (error) => {
        console.log(error)
      }
    )
  }
}
