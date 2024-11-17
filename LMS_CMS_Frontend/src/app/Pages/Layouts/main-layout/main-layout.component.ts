import { Component } from '@angular/core';
import { SideMenuComponent } from '../../../Component/side-menu/side-menu.component';
import { TokenData } from '../../../Models/token-data';
import { AccountService } from '../../../Services/account.service';
import { RouterOutlet } from '@angular/router';
import { NavMenuComponent } from '../../../Component/nav-menu/nav-menu.component';
import { RoleDetailsService } from '../../../Services/Employee/role-details.service';
import { CommonModule } from '@angular/common';
import { PagesWithRoleId } from '../../../Models/pages-with-role-id';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [SideMenuComponent, RouterOutlet, NavMenuComponent, CommonModule],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  menuItems: { label: string; route?: string; subItems?: { label: string; route: string }[] }[] = [];
  menuItemsForEmployee?: PagesWithRoleId[];

  User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  constructor(public accountService: AccountService, public roleDetailsService: RoleDetailsService) { }

  async ngOnInit() {
    this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
    if (this.User_Data_After_Login.type == "employee") {
      await this.Get_Pages_With_RoleID()
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

  Get_Pages_With_RoleID() {
    this.roleDetailsService.Get_Pages_With_RoleID(this.User_Data_After_Login.role).subscribe(
      (data:any) => {
        this.menuItemsForEmployee = data
        console.log(this.menuItemsForEmployee)
      } 
    )
    this.menuItems = [
      {
        label: 'Dashboard Student', route: '#'
      }
    ]
  }
}
