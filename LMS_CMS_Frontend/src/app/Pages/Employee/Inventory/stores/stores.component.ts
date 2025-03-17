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
import { StoresService } from '../../../../Services/Employee/Inventory/stores.service';
import { InventoryCategoryService } from '../../../../Services/Employee/Inventory/inventory-category.service';
import { StoreAdd } from '../../../../Models/Inventory/store-add';
import { firstValueFrom } from 'rxjs';

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
  store: StoreAdd = new StoreAdd();

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
  isLoading = false

  constructor(
    public violationServ: ViolationService,
    public empTypeVioletionServ: EmployeeTypeViolationService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public StoresServ: StoresService,
    public CategoryServ: InventoryCategoryService
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });
    this.GetAllData();
    this.GetAllCategories();
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
    this.TableData = []
    this.StoresServ.Get(this.DomainName).subscribe((d) => {
      this.TableData = d
    })
  }

  GetAllCategories() {
    this.CategoryServ.Get(this.DomainName).subscribe((d) => {
      this.Categories = d
    })
  }

  Create() {
    this.mode = 'Create';
    this.store = new StoreAdd();
    this.dropdownOpen = false;
    this.openModal();
    this.CategoriesSelected = [];
  }

  openModal() {
    this.isModalVisible = true;
  }

  Edit(row: Store): void {
    this.mode = 'Edit';
    this.store.id = row.id;
    this.store.name = row.name;
    this.store.categoriesIds = row.storeCategories.map(s => s.id);
    this.CategoriesSelected = row.storeCategories
    this.openModal();
    this.dropdownOpen = false;
  }

  selectCategory(category: Category) {
    if (!this.CategoriesSelected.some((e) => e.id === category.id)) {
      this.CategoriesSelected.push(category);
    }
    this.store.categoriesIds.push(category.id);
    this.dropdownOpen = false;
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
        this.StoresServ.Delete(id, this.DomainName).subscribe((d) => {
          this.GetAllData()
        })
      }
    });
  }

  closeModal() {
    this.isModalVisible = false;
  }

  CreateOREdit() {
    if (this.isFormValid()) {
      this.isLoading = true
      if (this.mode == 'Create') {
        this.StoresServ.Add(this.store, this.DomainName).subscribe((d) => {
          this.GetAllData();
          this.closeModal();
          this.isLoading = false
        },
          err => {
            this.isLoading = false
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          })
      }
      if (this.mode == 'Edit') {
        this.StoresServ.Edit(this.store, this.DomainName).subscribe((d) => {
          this.GetAllData();
          this.closeModal();
          this.isLoading = false
        },
          err => {
            this.isLoading = false
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          })
      }
    }
    this.GetAllData();
  }

  toggleDropdown(): void {
    this.dropdownOpen = !this.dropdownOpen;
  }


  removeSelected(id: number): void {
    this.CategoriesSelected = this.CategoriesSelected.filter((e) => e.id !== id);
    this.store.categoriesIds = this.store.categoriesIds.filter(
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
      const data: Store[] = await firstValueFrom(this.StoresServ.Get(this.DomainName));
      this.TableData = data || [];

      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);

        this.TableData = this.TableData.filter(t => {
          const fieldValue = t[this.key as keyof typeof t];
          if (typeof fieldValue === 'string') {
            return fieldValue.toLowerCase().includes(this.value.toLowerCase());
          }
          if (typeof fieldValue === 'number') {
            return fieldValue === numericValue;
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.TableData = [];
    }
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.store) {
      if (this.store.hasOwnProperty(key)) {
        const field = key as keyof StoreAdd;
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
