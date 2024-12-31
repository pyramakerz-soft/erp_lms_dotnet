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

@Component({
  selector: 'app-employee-add-edit',
  standalone: true,
  imports: [CommonModule,FormsModule],
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
    Roles:Role[]=[]

    empTypes:EmployeeTypeGet[]=[]
    
  
    constructor(public RoleServ :RoleService ,public empTypeServ:EmployeeTypeService , public BusCompanyServ: BusCompanyService ,public activeRoute: ActivatedRoute, public account: AccountService, public ApiServ: ApiService, private menuService: MenuService, public EditDeleteServ: DeleteEditPermissionService, private router: Router, public EmpServ: EmployeeService) { }
  
    ngOnInit() {
      this.User_Data_After_Login = this.account.Get_Data_Form_Token();
      this.UserID = this.User_Data_After_Login.id;
      if (this.User_Data_After_Login.type === "employee") {
        this.DomainName = this.ApiServ.GetHeader();
        this.activeRoute.url.subscribe(url => {
          this.path = url[0].path
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
        this.Roles=data;
      });
    }

    GetEmployeeType() {
        this.empTypeServ.Get(this.DomainName).subscribe((data) => {
          this.empTypes=data;
        });
    }

    onFilesSelected(event: Event): void {
      const input = event.target as HTMLInputElement;
      if (input.files) {
        for (let i = 0; i < input.files.length; i++) {
          const file = input.files[i];
          this.Data.files.push(file);
        }
        console.log('Selected Files:', this.Data.files);
      }
    }

    deleteFile(index: number): void {
      this.Data.files.splice(index, 1); 
      console.log('Updated Files:', this.Data.files);
    }

    downloadFile(file: File): void {
      const fileURL = URL.createObjectURL(file);
      const a = document.createElement('a');
      a.href = fileURL;
      a.download = file.name; 
      a.click();
      URL.revokeObjectURL(fileURL); 
    }

    Save(){
      console.log(this.Data)
      this.EmpServ.Add(this.Data , this.DomainName).subscribe((data)=>{
        console.log(data)
      })
    }

    changeFileName(index: number, event: Event): void {
      const input = event.target as HTMLInputElement; // Cast EventTarget to HTMLInputElement
      const newName = input.value; // Access the value property
      const oldFile = this.Data.files[index];
      const newFile = new File([oldFile], newName, { type: oldFile.type, lastModified: oldFile.lastModified });
      this.Data.files[index] = newFile;
    }
}
