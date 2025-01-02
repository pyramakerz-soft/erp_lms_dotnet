import { Component } from '@angular/core';
import { EmployeeGet } from '../../../../Models/Employee/employee-get';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BusCompanyService } from '../../../../Services/Employee/Bus/bus-company.service';
import { BusType } from '../../../../Models/Bus/bus-type';
import { RoleService } from '../../../../Services/Employee/role.service';
import { Role } from '../../../../Models/Administrator/role';
import { EmployeeTypeService } from '../../../../Services/Employee/employee-type.service';
import { EmployeeTypeGet } from '../../../../Models/Administrator/employee-type-get';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-employee-add-edit',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-add-edit.component.html',
  styleUrl: './employee-add-edit.component.css'
})
export class EmployeeAddEditComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  DomainName: string = "";
  UserID: number = 0;
  path: string = "";

  Data: EmployeeGet = new EmployeeGet()

  BusCompany: BusType[] = []
  Roles: Role[] = []

  empTypes: EmployeeTypeGet[] = []

  mode: string = ""

  BusCompanyId: number = 0;
  RoleId: number = 0;
  EmpType: number = 0;

  EmpId: number = 0;

  DeletedFiles: number[] = []
  constructor(public RoleServ: RoleService, public empTypeServ: EmployeeTypeService, public BusCompanyServ: BusCompanyService, public activeRoute: ActivatedRoute, public account: AccountService, public ApiServ: ApiService, private menuService: MenuService, public EditDeleteServ: DeleteEditPermissionService, private router: Router, public EmpServ: EmployeeService) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    if (this.User_Data_After_Login.type === "employee") {
      this.DomainName = this.ApiServ.GetHeader();
      this.activeRoute.url.subscribe(url => {
        this.path = url[0].path
        if (this.path == "Employee Create") {
          this.mode = "Create";

        }
        else if (this.path == "Employee Edit") {
          this.mode = "Edit";
          this.EmpId = Number(this.activeRoute.snapshot.paramMap.get('id'))
          this.EmpServ.Get_Employee_By_ID(this.EmpId, this.DomainName).subscribe(async (data) => {
            this.Data = data;
            if (data.files == null) {
              this.Data.files = []
            }
            this.Data.id = this.EmpId;
          })
        }
        this.GetBusCompany();
        this.GetRole();
        this.GetEmployeeType();

      });
    }
  }

  GetBusCompany() {
    this.BusCompanyServ.Get(this.DomainName).subscribe((data) => {
      this.BusCompany = data;
    });
  }

  GetRole() {
    this.RoleServ.Get_Roles(this.DomainName).subscribe((data) => {
      this.Roles = data;
    });
  }

  GetEmployeeType() {
    this.empTypeServ.Get(this.DomainName).subscribe((data) => {
      this.empTypes = data;
    });
  }

  onFilesSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      for (let i = 0; i < input.files.length; i++) {
        const file = input.files[i];
        this.Data.files.push(file);
      }
    }
  }

  deleteFile(id: any): void {
    const file: any = this.Data.files[id];
    this.DeletedFiles.push(file.id);
    this.Data.files.splice(id, 1);
  }

  downloadFile(file: any): void {
    if (this.mode == "Create") {
      const fileURL = URL.createObjectURL(file);
      const a = document.createElement('a');
      a.href = fileURL;
      a.download = file.name;
      a.click();
      // URL.revokeObjectURL(fileURL);
    }
    else if(this.mode=="Edit"){
      const fileURL = file.link;
      const a = document.createElement('a');
      a.href = fileURL;
      a.target = '_blank'; // Open in a new tab
      a.click();
      // URL.revokeObjectURL(fileURL);
    }
  }

  async Save() {
    if (this.Data.user_Name == "") {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'User Name cannot be empty.',
        confirmButtonColor: '#FF7519',
      });
    } else if (this.Data.en_name == "") {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'English Name cannot be empty.',
        confirmButtonColor: '#FF7519',
      });
    } else if (this.Data.password == "") {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'Password cannot be empty.',
        confirmButtonColor: '#FF7519',
      });
    } else if (this.Data.role_ID == 0) {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'Role ID must be selected.',
        confirmButtonColor: '#FF7519',
      });
    } else if (this.Data.employeeTypeID == 0) {
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'Employee Type must be selected.',
        confirmButtonColor: '#FF7519',
      });
    }
    else if (this.Data.employeeTypeID == 2 && this.Data.licenseNumber == "" || this.Data.expireDate == "") {
      if (this.Data.licenseNumber == "")
        Swal.fire({
          icon: 'warning',
          title: 'Warning!',
          text: 'licenseNumber cannot be empty.',
          confirmButtonColor: '#FF7519',
        });
      if (this.Data.expireDate == "")
        Swal.fire({
          icon: 'warning',
          title: 'Warning!',
          text: 'expireDate cannot be empty.',
          confirmButtonColor: '#FF7519',
        });
    }
    else {
      if (this.mode == "Create") {
        this.EmpServ.Add(this.Data, this.DomainName).subscribe((data) => {
          this.router.navigateByUrl("Employee/Employee")
        })
      }
      else if (this.mode == "Edit") {
        if (this.DeletedFiles.length > 0) {
          await this.DeletedFiles.forEach(id => {
            this.EmpServ.DeleteFile(id, this.DomainName).subscribe(() => {
            });
          });
        }
        this.EmpServ.Edit(this.Data, this.DomainName).subscribe((data) => {
          this.router.navigateByUrl("Employee/Employee")
        })
      }
    }
  }

  moveToEmployee() {
    this.router.navigateByUrl("Employee/Employee")
  }
  changeFileName(index: number, event: Event): void {
    const input = event.target as HTMLInputElement; // Cast EventTarget to HTMLInputElement
    const newName = input.value; // Access the value property
    const oldFile = this.Data.files[index];
    const newFile = new File([oldFile], newName, { type: oldFile.type, lastModified: oldFile.lastModified });
    this.Data.files[index] = newFile;
  }

  async convertToFile(fileObject: any): Promise<File> {
    try {
      const response = await fetch(fileObject.link);
      if (!response.ok) {
        throw new Error(`Failed to fetch file from ${fileObject.link}`);
      }
      const blob = await response.blob();
      const file = new File([blob], fileObject.name, { type: blob.type });
      return file;
    } catch (error) {
      console.error("Error converting to File:", error);
      throw error;
    }
  }

}
