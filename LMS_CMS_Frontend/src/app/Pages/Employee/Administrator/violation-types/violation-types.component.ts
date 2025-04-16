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
import { firstValueFrom } from 'rxjs';
import { SearchComponent } from '../../../../Component/search/search.component';

@Component({
  selector: 'app-violation-types',
  standalone: true,
  imports: [CommonModule, FormsModule , SearchComponent],
  templateUrl: './violation-types.component.html',
  styleUrl: './violation-types.component.css',
})
export class ViolationTypesComponent {
  User_Data_After_Login: TokenData = new TokenData(
    '',
    0,
    0,
    0,
    0,
    '',
    '',
    '',
    '',
    ''
  );

  DomainName: string = '';
  UserID: number = 0;
  path: string = '';

  Data: Violation[] = [];
  violation: ViolationAdd = new ViolationAdd();

  violationId: number = 0;

  isModalVisible: boolean = false;
  mode: string = 'Create';

  empTypes: EmployeeTypeGet[] = [];

  dropdownOpen = false;
  empTypesSelected: EmployeeTypeGet[] = [];

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  keysArray: string[] = ['id', 'name'];
  key: string= "id";
  value: any = "";
  isLoading=false

  constructor(
    public violationServ: ViolationService,
    public empTypeVioletionServ: EmployeeTypeViolationService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public empTypeServ: EmployeeTypeService
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });
    this.GetViolation();
    this.GetEmployeeType();
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

  GetViolation() {
    this.Data = []
    this.violationServ.Get_Violations(this.DomainName).subscribe((data) => {
      this.Data = data;
    });
  }

  GetEmployeeType() {
    this.empTypeServ.Get(this.DomainName).subscribe((data) => {
      this.empTypes = data;
    });
  }
  Create() {
    this.mode = 'Create';
    this.violation = new ViolationAdd();
    this.dropdownOpen = false;
    this.openModal();
    this.empTypesSelected = [];
  }
  openModal() {
    this.isModalVisible = true;
  }
  Edit(row: Violation): void {
    this.mode = 'Edit';
    this.violation.violationId = row.id;
    this.violation.violationName =
      this.Data.find((v) => v.id === row.id)?.name ?? '';
    this.empTypesSelected =
      this.Data.find((v) => v.id === row.id)?.employeeTypes ?? [];
    this.violation.employeeTypeID =
      this.Data.find((v) => v.id === row.id)?.employeeTypes.map(
        (empType) => empType.id
      ) ?? [];
    this.openModal();
    this.dropdownOpen = false;
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
            console.error('Error while deleting the Violation:', error);
            Swal.fire({
              title: 'Error',
              text: 'An error occurred while deleting the Violation. Please try again later.',
              icon: 'error',
              confirmButtonText: 'OK',
            });
          },
        });
      }
    });
  }

  closeModal() {
    this.isModalVisible = false;
  }
  CreateOREdit() {
    if (this.violation.violationName == '') {
      Swal.fire({
        title: 'Error',
        text: 'Please Enter Violation Name.',
        icon: 'error',
        confirmButtonText: 'OK',
      });
    } else {
      this.isLoading=true
      if (this.mode == 'Create') {
        this.empTypeVioletionServ
          .Add(this.violation, this.DomainName)
          .subscribe({
            next: (response) => {
              this.GetViolation();
              this.closeModal();
             this.isLoading=false
            },
            error: (error) => {
             this.isLoading=false
              const errorMessage =
                error?.error || 'An unexpected error occurred';
              Swal.fire({
                icon: 'error',
                title: 'Error',
                confirmButtonColor: '#FF7519',
                text: errorMessage,
              });
            },
          });
      } else if (this.mode == 'Edit') {
        this.empTypeVioletionServ
          .Edit(this.violation, this.DomainName)
          .subscribe({
            next: (response) => {
              this.GetViolation();
              this.closeModal();
              this.isLoading=false
            },
            error: (error) => {
              this.isLoading=false
              const errorMessage =
                error?.error || 'An unexpected error occurred';
              Swal.fire({
                icon: 'error',
                title: 'Error',
                confirmButtonColor: '#FF7519',
                text: errorMessage,
              });
            },
          });
      }
    }
  }

  toggleDropdown(): void {
    this.dropdownOpen = !this.dropdownOpen;
  }

  selectEmployeeType(employeeType: EmployeeTypeGet): void {
    if (!this.empTypesSelected.some((e) => e.id === employeeType.id)) {
      this.empTypesSelected.push(employeeType);
    }
    this.violation.employeeTypeID.push(employeeType.id);
    this.dropdownOpen = false; // Close dropdown after selection
  }

  removeSelected(id: number): void {
    this.empTypesSelected = this.empTypesSelected.filter((e) => e.id !== id);
    this.violation.employeeTypeID = this.violation.employeeTypeID.filter(
      (i) => i !== id
    );
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Violation[] = await firstValueFrom( this.violationServ.Get_Violations(this.DomainName));  
      this.Data = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.Data = this.Data.filter(t => {
          const fieldValue = t[this.key as keyof typeof t];
          if (typeof fieldValue === 'string') {
            return fieldValue.toLowerCase().includes(this.value.toLowerCase());
          }
          if (typeof fieldValue === 'number') {
            return fieldValue.toString().includes(numericValue.toString())
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.Data = [];
    }
  }

}
