import { Component } from '@angular/core';
import { Violation } from '../../../../Models/Administrator/violation';
import { ViolationService } from '../../../../Services/Employee/violation.service';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { ApiService } from '../../../../Services/api.service';
import { AccountService } from '../../../../Services/account.service';
import { CommonModule, formatCurrency } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeTypeService } from '../../../../Services/Employee/employee-type.service';
import { EmployeeTypeGet } from '../../../../Models/Administrator/employee-type-get';
import { ViolationAdd } from '../../../../Models/Administrator/violation-add';
import { EmployeeTypeViolationService } from '../../../../Services/Employee/employee-type-violation.service';
import Swal from 'sweetalert2';
import { ViolationEdit } from '../../../../Models/Administrator/violation-edit';

@Component({
  selector: 'app-violation-types',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './violation-types.component.html',
  styleUrl: './violation-types.component.css'
})
export class ViolationTypesComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  DomainName: string = "";
  UserID: number = 0;
  path: string = "";

  Data: Violation[] = []
  violation: ViolationAdd = new ViolationAdd();
  
  violationId:number=0;

  isModalVisible: boolean = false;
  mode: string = "Create"

  empTypes: EmployeeTypeGet[] = []

  dropdownOpen = false;
  empTypesSelected: EmployeeTypeGet[] = [];

  constructor(public violationServ: ViolationService, public empTypeVioletionServ: EmployeeTypeViolationService, public activeRoute: ActivatedRoute, public account: AccountService, public ApiServ: ApiService, private menuService: MenuService, public EditDeleteServ: DeleteEditPermissionService, private router: Router, public empTypeServ: EmployeeTypeService) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });
    this.GetViolation();
    this.GetEmployeeType();
  }

  GetViolation() {
    this.violationServ.Get_Violations(this.DomainName).subscribe((data) => {
      this.Data = data;
    })
  }

  GetEmployeeType() {
    this.empTypeServ.Get(this.DomainName).subscribe((data) => {
      this.empTypes = data;
    });
  }
  Create() {
    this.mode="Create"
    this.openModal();

  }
  openModal() {
    this.isModalVisible = true;
  }
  Edit(row: Violation): void {
    this.mode="Edit"
    this.violation.violationId=row.id
    this.violation.violationName = this.Data.find((v) => v.id === row.id)?.name ?? '';
    this.empTypesSelected = this.Data.find((v) => v.id === row.id)?.employeeTypes ?? [];
    this.violation.employeeTypeID= this.Data.find((v) => v.id === row.id)?.employeeTypes.map(empType => empType.id) ?? [];
    this.openModal()
  }
  
  Delete(id: number): void {
    Swal.fire({
      title: 'Are you sure you want to delete this Violation?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.violationServ.Delete_Violations(id, this.DomainName).subscribe({
          next: (data) => {
            this.GetViolation();
          },
          error: (error) => {
            console.error("Error while deleting the Violation:", error);
            Swal.fire({
              title: 'Error',
              text: 'An error occurred while deleting the Violation. Please try again later.',
              icon: 'error',
              confirmButtonText: 'OK',
            });
          }
        });
      }
    });
  }

  closeModal() {
    this.isModalVisible = false;
  }
  CreateOREdit() {
    if(this.violation.violationName==""){
      Swal.fire({
        title: 'Error',
        text: 'Please Enter Violation Name.',
        icon: 'error',
        confirmButtonText: 'OK',
      });
    }
    else{
      if (this.mode == "Create") {
        this.empTypeVioletionServ.Add(this.violation, this.DomainName).subscribe(() => {
          this.GetViolation();
          this.closeModal();
        })
      }
      else if(this.mode == "Edit") {
        this.empTypeVioletionServ.Edit(this.violation, this.DomainName).subscribe(() => {
          this.GetViolation();
          this.closeModal();
        })
      }
      this.toggleDropdown();
    }
  }
  toggleDropdown(): void {
    this.dropdownOpen = !this.dropdownOpen;
  }

  selectEmployeeType(employeeType: EmployeeTypeGet): void {
    if (!this.empTypesSelected.some(e => e.id === employeeType.id)) {
      this.empTypesSelected.push(employeeType);
    }
    this.violation.employeeTypeID.push(employeeType.id)
    this.dropdownOpen = false; // Close dropdown after selection
  }

  removeSelected(id: number): void {
    this.empTypesSelected = this.empTypesSelected.filter(e => e.id !== id);
    this.violation.employeeTypeID = this.violation.employeeTypeID.filter(i => i !== id);
  }
}
