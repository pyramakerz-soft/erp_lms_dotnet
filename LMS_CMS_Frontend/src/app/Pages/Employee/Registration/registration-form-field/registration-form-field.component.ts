import { Component } from '@angular/core';
import { RegistrationCategory } from '../../../../Models/Registration/registration-category';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegistrationCategoryService } from '../../../../Services/Employee/Registration/registration-category.service';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { firstValueFrom } from 'rxjs';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-registration-form-field',
  standalone: true,
  imports: [CommonModule, FormsModule, SearchComponent, TranslateModule],
  templateUrl: './registration-form-field.component.html',
  styleUrl: './registration-form-field.component.css'
})
export class RegistrationFormFieldComponent {

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

  Data: RegistrationCategory[] = [];
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  mode: string = 'Create'

  isModalVisible: boolean = false;

  Category: RegistrationCategory = new RegistrationCategory();

  key: string = "id";
  value: any = "";
  keysArray: string[] = ['id', 'arName', 'enName', 'orderInForm'];

  validationErrors: { [key in keyof RegistrationCategory]?: string } = {};
  isLoading = false

  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public CategoryServ: RegistrationCategoryService
  ) { }

  ngOnInit() {

    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });

    this.GetAllData();
  }

  GetAllData() {
    this.Data = []
    this.CategoryServ.Get(this.DomainName).subscribe((d) => {
      this.Data = d;
    })
  }

  Create() {
    this.mode = 'Create';
    this.Category = new RegistrationCategory(); 
    this.openModal(); 
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Form Field Category?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.CategoryServ.Delete(id, this.DomainName).subscribe(
          (data: any) => {
            this.GetAllData();
          }
        );
      }
    });
  }

  Edit(row: RegistrationCategory) {
    this.mode = 'Edit';
    this.CategoryServ.GetByID(row.id, this.DomainName).subscribe((d) => {
      this.Category = d
    })
    this.openModal();
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  CreateOREdit() {
    this.Category.registrationFormId = 1;
    if (this.isFormValid()) {
      this.isLoading = true
      if (this.mode == "Create") {
        this.CategoryServ.Add(this.Category, this.DomainName).subscribe(() => {
          this.GetAllData();
          this.closeModal()
          this.isLoading = false
        },
          error => {
            this.isLoading = false
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          })
      } if (this.mode == "Edit") {
        this.CategoryServ.Edit(this.Category, this.DomainName).subscribe(() => {
          this.GetAllData();
          this.closeModal();
          this.isLoading = false
        },
          error => {
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
  }

  closeModal() {
    this.isModalVisible = false;
  }

  openModal() { 
    this.isModalVisible = true;
  }

  view(id: number) {
    this.router.navigateByUrl(`Employee/Registration Form Field/${id}`)
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.Category) {
      if (this.Category.hasOwnProperty(key)) {
        const field = key as keyof RegistrationCategory;
        if (!this.Category[field]) {
          if (field == "arName" || field == "enName" || field == "orderInForm") {
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        }
      }
    }
    return isValid;
  }
  capitalizeField(field: keyof RegistrationCategory): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }
  onInputValueChange(event: { field: keyof RegistrationCategory, value: any }) {
    const { field, value } = event;
    (this.Category as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: RegistrationCategory[] = await firstValueFrom(this.CategoryServ.Get(this.DomainName));
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

  validateNumber(event: any, field: keyof RegistrationCategory): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.Category[field] === 'string') {
        this.Category[field] = '' as never;
      }
    }
  }
}
