import { Component } from '@angular/core';
import { PagesWithRoleId } from '../../../../Models/pages-with-role-id';
import { PageService } from '../../../../Services/Employee/page.service';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { TokenData } from '../../../../Models/token-data';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-role-add-edit',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './role-add-edit.component.html',
  styleUrl: './role-add-edit.component.css'
})
export class RoleAddEditComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  DomainName: string = "";
  UserID: number = 0;
  path: string = "";
  data: PagesWithRoleId[] = []
  PagecollapseStates: boolean[][] = [];
  collapseStates: boolean[] = [];


  constructor(public pageServ: PageService, public activeRoute: ActivatedRoute, public account: AccountService, public ApiServ: ApiService, private menuService: MenuService, public EditDeleteServ: DeleteEditPermissionService) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    if (this.User_Data_After_Login.type === "employee") {
      this.DomainName = this.ApiServ.GetHeader();
      this.getAllPges();
      this.activeRoute.url.subscribe(url => {
        this.path = url[0].path
      });
    }
  }

  async getAllPges() {
    try {
      const data = await firstValueFrom(this.pageServ.Get_Pages(this.DomainName));
      console.log(data)
      this.data = data;
      this.collapseStates = new Array(this.data.length).fill(false); 
      this.PagecollapseStates = this.data.map(() => new Array(this.data[0].children.length).fill(false)); 
    } catch (error) {
      this.data = [];
      console.log('Error loading data:', error);
    }
  }
  togglePageCollapse(rowIndex: number, pageIndex: number) {
    this.PagecollapseStates[rowIndex][pageIndex] = !this.PagecollapseStates[rowIndex][pageIndex];
  }
  
  toggleCollapse(rowIndex: number) {
    this.collapseStates[rowIndex] = !this.collapseStates[rowIndex];
  }
}
