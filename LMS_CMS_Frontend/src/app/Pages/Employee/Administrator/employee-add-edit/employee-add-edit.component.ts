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
  validationErrors: { [key in keyof EmployeeGet]?: string } = {};
  emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/; 
  DeletedFiles: number[] = []
  SelectedFiles :File [] = []
  isLoading = false;

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
        } else if (this.path == "Employee Edit") {
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
        this.SelectedFiles.push(file);
      }
    } 
    input.value = '';
  }

  deleteFile(id: any): void {
    const file: any = this.Data.files[id];
    this.DeletedFiles.push(file.id);
    this.Data.files.splice(id, 1);
  } 
  
  deleteFileFromSelectedFile(file: File): void { 
    const index = this.SelectedFiles.indexOf(file);
    if (index !== -1) {
      this.SelectedFiles.splice(index, 1);
    } 
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
    else if (this.mode == "Edit") {
      const fileURL = file.link;
      const a = document.createElement('a');
      a.href = fileURL;
      a.target = '_blank'; // Open in a new tab
      a.click();
      // URL.revokeObjectURL(fileURL);
    }
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.Data) {
      if (this.Data.hasOwnProperty(key)) {
        const field = key as keyof EmployeeGet;
        if (!this.Data[field]) {
          if (field == 'user_Name' || field == 'en_name' || field == 'password' || field == 'role_ID' || field == 'employeeTypeID' || field == 'email') {
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`;
            isValid = false;
          }
        }
      }
    }

    if (this.Data.employeeTypeID == 2) {
      if (this.Data.licenseNumber == "") {
        this.validationErrors["licenseNumber"] = `*License Number is required`;
        isValid = false;
      }
      if (this.Data.expireDate == "") {
        this.validationErrors["expireDate"] = `*Expire Data is required`;
        isValid = false;
      }
    }

    if (this.Data.email && !this.emailPattern.test(this.Data.email)) {
      this.validationErrors["email"] = `*Email is not valid`;
      isValid = false;
    } 
    
    return isValid;
  }

  capitalizeField(field: keyof EmployeeGet): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof EmployeeGet; value: any }) {
    const { field, value } = event;
    (this.Data as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  validateNumber(event: any, field: keyof EmployeeGet): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.Data[field] === 'string') {
        this.Data[field] = '' as never;
      }
    }
  }

  async Save() {
    if (this.isFormValid()) { 
      this.isLoading = true;
      for (let i = 0; i < this.SelectedFiles.length; i++) {
        this.Data.files.push(this.SelectedFiles[i]);
      }
      if (this.mode == "Create") {
        return this.EmpServ.Add(this.Data, this.DomainName).toPromise().then(
          (data) => {
            Swal.fire({
              icon: 'success',
              title: 'Done',
              text: 'Employee Added Succeessfully',
              confirmButtonColor: '#FF7519',
            });
            this.moveToEmployee();
            this.isLoading = false;
            return true;
          },
          (error) => {   
            switch(true) {
              case error.error == 'This User Name Already Exist':
                Swal.fire({
                  icon: 'error',
                  title: 'Error',
                  text: error.error || 'An unexpected error occurred',
                  confirmButtonColor: '#FF7519',
                });
                break;
              
              case error.error.errors?.Password !== undefined:
                Swal.fire({
                  icon: 'error',
                  title: 'Error',
                  text: error.error.errors.Password[0] || 'An unexpected error occurred',
                  confirmButtonColor: '#FF7519',
                });
                break;
              
              case error.error === "This Email Already Exist":
                Swal.fire({
                  icon: 'error',
                  title: 'Error',
                  text: error.error || 'An unexpected error occurred',
                  confirmButtonColor: '#FF7519',
                });
                break;
              
              default:
                Swal.fire({
                  icon: 'error',
                  title: 'Error',
                  text: error.error.errors || 'An unexpected error occurred',
                  confirmButtonColor: '#FF7519',
                });
                break;
            }
            this.isLoading = false;
            return false;
          }
        );
      } else if (this.mode == "Edit") {
        if (this.DeletedFiles.length > 0) {
          for (const id of this.DeletedFiles) {
            await this.EmpServ.DeleteFile(id, this.DomainName).toPromise();
          }
        }
        return this.EmpServ.Edit(this.Data, this.DomainName).toPromise().then(
          (data) => {
            Swal.fire({
              icon: 'success',
              title: 'Done',
              text: 'Employee Edited Succeessfully',
              confirmButtonColor: '#FF7519',
            });
            this.moveToEmployee();
            this.isLoading = false;
            return true;
          },
          (error) => { 
            this.isLoading = false;
            switch(true) { 
              case error.error == 'This User Name Already Exist':
                Swal.fire({
                  icon: 'error',
                  title: 'Error',
                  text: error.error || 'An unexpected error occurred',
                  confirmButtonColor: '#FF7519',
                });
                break;
                
              case error.error === "This Email Already Exist":
                Swal.fire({
                  icon: 'error',
                  title: 'Error',
                  text: error.error || 'An unexpected error occurred',
                  confirmButtonColor: '#FF7519',
                });
                break;
              
              default:
                Swal.fire({
                  icon: 'error',
                  title: 'Error',
                  text: error.error.errors || 'An unexpected error occurred',
                  confirmButtonColor: '#FF7519',
                });
                break;
            }
            return false;
          }
        );
      }
    }

    return Promise.resolve(true); // Default resolve if all logic completes
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
}
