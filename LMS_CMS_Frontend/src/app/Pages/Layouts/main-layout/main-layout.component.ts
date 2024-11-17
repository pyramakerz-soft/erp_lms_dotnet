import { Component } from '@angular/core';
import { SideMenuComponent } from '../../../Component/side-menu/side-menu.component';
import { TokenData } from '../../../Models/token-data';
import { AccountService } from '../../../Services/account.service';
import { RouterOutlet } from '@angular/router';
import { NavMenuComponent } from '../../../Component/nav-menu/nav-menu.component';
import { EmployeePermission } from '../../../Models/employee-permission';

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
  User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  constructor(public accountService: AccountService) { }

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
    this.menuItems = [
      {
        label: 'Dashboard Student', route: '#'
      }
    ]
  }
}
