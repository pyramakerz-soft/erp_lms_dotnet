import { Component } from '@angular/core';
import { SideMenuComponent } from '../../../Component/side-menu/side-menu.component';
import { TokenData } from '../../../Models/token-data';
import { AccountService } from '../../../Services/account.service';
import { RouterOutlet } from '@angular/router';
import { NavMenuComponent } from '../../../Component/nav-menu/nav-menu.component';
import { RoleDetailsService } from '../../../Services/Employee/role-details.service';
import { CommonModule } from '@angular/common';
import { PagesWithRoleId } from '../../../Models/pages-with-role-id';
import { MenuService } from '../../../Services/shared/menu.service';
import { NewTokenService } from '../../../Services/shared/new-token.service';
import { routes } from '../../../app.routes';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [SideMenuComponent, RouterOutlet, NavMenuComponent, CommonModule],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  menuItems: { label: string; route?: string; icon?:string; subItems?: { label: string; route: string; icon?:string }[] }[] = [];
  menuItemsForEmployee?: PagesWithRoleId[];

  User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  constructor(public accountService: AccountService, public roleDetailsService: RoleDetailsService ,private menuService: MenuService ,private communicationService: NewTokenService) { }

  async ngOnInit() {
   await this.GetInfo();
    this.communicationService.action$.subscribe(async (state) => {
      await this.GetInfo();

    });
  }

  async GetInfo(){
    this.User_Data_After_Login = this.accountService.Get_Data_Form_Token() 

    if (this.User_Data_After_Login.type == "employee") {
      await this.Get_Pages_With_RoleID()
    } else if (this.User_Data_After_Login.type == "student") {
      this.menuItems = [
        {
          label: 'Dashboard Student', route: '#', icon: 'Dashboard'
        },
        {
          label: 'ECommerce', subItems: [
            {
              label: 'Shop', route: 'Ecommerce/Shop'
            }
          ], icon: 'E-Commerce'
        }
      ]
    } else if (this.User_Data_After_Login.type == "parent") {
      this.menuItems = [
        {
          label: 'Dashboard Parent', route: '#', icon: 'Dashboard'
        },
        {
          label: 'Registrations', subItems: [
            {
              label: 'Registration Form', route: 'Registration Form'
            },
            {
              label: 'Admission Test', route: 'Admission Test'
            },
            {
              label: 'Interview Registration', route: 'Interview Registration'
            }
          ], icon: 'Registration'
        }
      ]
    }
    else if (this.User_Data_After_Login.type == "octa") {
      this.menuItems = [
        {
          label: 'Administrator', subItems:[
            {
              label: "Domains", route: "Domains"
            },
            {
              label: "School Types", route: "School Types"
            },
            {
              label: "School", route: "School"
            },
            {
              label: "Account", route: "Account"
            }
          ], icon: "Administrator"
        }
      ]
    }
  }
 
  Get_Pages_With_RoleID() {
    this.roleDetailsService.Get_Pages_With_RoleID(this.User_Data_After_Login.role).subscribe(
      (data:any) => {
        this.menuItemsForEmployee = data 
        this.menuService.updateMenuItemsForEmployee(this.menuItemsForEmployee);
      } ,(error)=>{
        this.menuItemsForEmployee = [];
      });
  }

  // Get_All_With_Group_By() {
  //   this.roleDetailsService.Get_All_Pages().subscribe(
  //     (data:any) => {
  //       this.menuItemsForEmployee = data
  //       this.menuService.updateMenuItemsForEmployee(this.menuItemsForEmployee);
  //     } ,(error)=>{
  //       this.menuItemsForEmployee = [];
  //     });
  // }
}
