import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MenuService } from '../../../Services/shared/menu.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../Models/token-data';
import { AccountService } from '../../../Services/account.service';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {

  constructor(private router: Router ,private menuService: MenuService ,public account:AccountService) {}
  User_Data_After_Login :TokenData =new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  AllowEdit:boolean=false;
  AllowDelete:boolean=false;
  
  InsertedByUserId:number=5;
  LoggedInUserId:number=5;

  
  ngOnInit() {

    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.InsertedByUserId=this.User_Data_After_Login.id;
    
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      console.log(items)
      const settingsPage = this.menuService.findByPageName('Settings', items);
      // this.AllowDelete=(settingsPage.allow_Delete && InsertedByUserId==this.LoggedInUserId)||(settingsPage.allow_Delete_For_Others && this.InsertedByUserId!=this.LoggedInUserId);
      this.AllowEdit=settingsPage.allow_Edit;
      this.AllowDelete=settingsPage.allow_Delete;


    });
  }
}
