import { Component } from '@angular/core';
import { Role } from '../../../../Models/Administrator/role';
import { RoleService } from '../../../../Services/Employee/role.service';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './role.component.html',
  styleUrl: './role.component.css'
})
export class RoleComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  TableData: Role[] = [];

  DomainName: string = "";
  UserID: number = 0;
  path: string = "";

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  constructor(public roleserv: RoleService, public activeRoute: ActivatedRoute, public account: AccountService, public ApiServ: ApiService, private menuService: MenuService, public EditDeleteServ: DeleteEditPermissionService, private router: Router) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    if (this.User_Data_After_Login.type === "employee") {
      this.DomainName = this.ApiServ.GetHeader();

      this.activeRoute.url.subscribe(url => {
        this.path = url[0].path
      });
    }

    this.getAllRoles();
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
  }
  async getAllRoles() {
    try {
      const data = await firstValueFrom(this.roleserv.Get_Roles(this.DomainName));
      this.TableData = data;
    } catch (error) {
      this.TableData = [];
      console.log('Error loading data:', error);
    }
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  Edit(id: number) {
    this.router.navigateByUrl(`Employee/Role Edit/${id}`);
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Role?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.roleserv.Delete(id, this.DomainName).subscribe({
          next: (response) => {
            this.getAllRoles();
          },
          error: (error) => {
            const errorMessage = error?.error || 'An unexpected error occurred.';
          },
        });
      }
    });
  }

  Create() {
    this.router.navigateByUrl("Employee/Role Create")
  }
  
}
