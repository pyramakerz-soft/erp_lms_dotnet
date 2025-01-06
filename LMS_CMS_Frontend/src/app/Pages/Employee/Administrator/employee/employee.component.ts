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
import Swal from 'sweetalert2';

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

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

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
        this.menuService.menuItemsForEmployee$.subscribe((items) => {
          console.log(items)
          const settingsPage = this.menuService.findByPageName(this.path, items);
          if (settingsPage) {
            console.log(settingsPage)
            this.AllowEdit = settingsPage.allow_Edit;
            this.AllowDelete = settingsPage.allow_Delete;
            this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
            this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
          }
        });

      });
    }
  }
  GetEmployee() {
    this.EmpServ.Get_Employees(this.DomainName).subscribe((data) => {
      this.TableData = data
    })
  }

  Create() {
    this.router.navigateByUrl("Employee/Employee Create")
  }

  Edit(id: number) {
    this.router.navigateByUrl(`Employee/Employee Edit/${id}`)

  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Role?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire({
          title: 'Deleting...',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });
  
        this.EmpServ.Delete(id, this.DomainName).subscribe({
          next: () => {
            Swal.fire({
              icon: 'success',
              title: 'Deleted!',
              text: 'The role has been deleted successfully.',
              confirmButtonColor: '#FF7519',
            });
            this.GetEmployee(); 
          },
          error: (error) => {
            const errorMessage = error?.error || 'An unexpected error occurred.';
            Swal.fire({
              icon: 'error',
              title: 'Error!',
              text: errorMessage,
              confirmButtonColor: '#FF7519',
            });
          },
        });
      }
    });
  }

  view(id:number){
    this.router.navigateByUrl(`Employee/Employee Details/${id}`)

  }
  
  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }
  
}
