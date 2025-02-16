import { Component } from '@angular/core';
import { Store } from '../../../../Models/Inventory/store';
import { Category } from '../../../../Models/Inventory/category';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { EmployeeTypeGet } from '../../../../Models/Administrator/employee-type-get';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { EmployeeTypeViolationService } from '../../../../Services/Employee/employee-type-violation.service';
import { ViolationService } from '../../../../Services/Employee/violation.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';

@Component({
  selector: 'app-stores',
  standalone: true,
  imports: [CommonModule, FormsModule, SearchComponent],
  templateUrl: './stores.component.html',
  styleUrl: './stores.component.css'
})
export class StoresComponent {
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

  TableData: Store[] = [];
  store: Store = new Store();

  isModalVisible: boolean = false;
  mode: string = 'Create';

  Categories: Category[] = [];

  dropdownOpen = false;
  CategoriesSelected: Category[] = [];

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  keysArray: string[] = ['id', 'name'];
  key: string = "id";
  value: any = "";

  validationErrors: { [key in keyof Store]?: string } = {};

  constructor(
    public violationServ: ViolationService,
    public empTypeVioletionServ: EmployeeTypeViolationService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });
    this.GetAllData();
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

  GetAllData() {

  }

  Create() {
    this.mode = 'Create';
    this.store = new Store();
    this.dropdownOpen = false;
    this.openModal();
    this.CategoriesSelected = [];
  }

  openModal() {
    this.isModalVisible = true;
  }

  Edit(id: number): void {
    this.mode = 'Edit';
  }

  selectCategory(category:Category){

  }

  Delete(id: number): void {
    Swal.fire({
      title: 'Are you sure you want to delete this Store?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {

      }
    });
  }

  closeModal() {
    this.isModalVisible = false;
  }

  CreateOREdit() {

  }

  toggleDropdown(): void {
    this.dropdownOpen = !this.dropdownOpen;
  }

  select(employeeType: EmployeeTypeGet): void {

  }

  removeSelected(id: number): void {

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
    // this.key = event.key;
    // this.value = event.value;
    // try {
    //   const data: Violation[] = await firstValueFrom( this.violationServ.Get_Violations(this.DomainName));  
    //   this.Data = data || [];

    //   if (this.value !== "") {
    //     const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);

    //     this.Data = this.Data.filter(t => {
    //       const fieldValue = t[this.key as keyof typeof t];
    //       if (typeof fieldValue === 'string') {
    //         return fieldValue.toLowerCase().includes(this.value.toLowerCase());
    //       }
    //       if (typeof fieldValue === 'number') {
    //         return fieldValue === numericValue;
    //       }
    //       return fieldValue == this.value;
    //     });
    //   }
    // } catch (error) {
    //   this.Data = [];
    //   console.log('Error fetching data:', error);
    // }
  }

  isFormValid(): boolean {
      let isValid = true;
      for (const key in this.store) {
        if (this.store.hasOwnProperty(key)) {
          const field = key as keyof Store;
          if (!this.store[field]) {
            if (
              field == 'name' 
            ) {
              this.validationErrors[field] = `*${this.capitalizeField(
                field
              )} is required`;
              isValid = false;
            }
          }
        }
      }
      return isValid;
    }
    capitalizeField(field: keyof Store): string {
      return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
    }
  
    onInputValueChange(event: { field: keyof Store; value: any }) {
      const { field, value } = event;
      (this.store as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
    }
}
