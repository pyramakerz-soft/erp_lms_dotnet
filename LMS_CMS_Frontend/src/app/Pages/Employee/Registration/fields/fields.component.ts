import { Component } from '@angular/core';
import { RegistrationCategory } from '../../../../Models/Registration/registration-category';
import { Field } from '../../../../Models/Registration/field';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../../../Services/api.service';
import { AccountService } from '../../../../Services/account.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FieldOption } from '../../../../Models/Registration/field-option';
import { FieldType } from '../../../../Models/Registration/field-type';
import { RegistrationCategoryService } from '../../../../Services/Employee/Registration/registration-category.service';
import { FieldsService } from '../../../../Services/Employee/Registration/fields.service';
import Swal from 'sweetalert2';
import { FieldTypeService } from '../../../../Services/Employee/Registration/field-type.service';
import { FieldAddEdit } from '../../../../Models/Registration/field-add-edit';
import { firstValueFrom } from 'rxjs';
import { SearchComponent } from '../../../../Component/search/search.component';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-fields',
  standalone: true,
  imports: [CommonModule, FormsModule, SearchComponent, TranslateModule],
  templateUrl: './fields.component.html',
  styleUrl: './fields.component.css',
})
export class FieldsComponent {
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

  Category: RegistrationCategory = new RegistrationCategory();
  Data: Field[] = [];

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  mode: string = 'Create';

  isModalVisible: boolean = false;
  field: FieldAddEdit = new FieldAddEdit();

  CategoryId: number = 0;

  options: string[] = [];
  inputValue: string = '';
  fieldTypes: FieldType[] = [];

  key: string = 'id';
  value: any = '';
  keysArray: string[] = [
    'id',
    'arName',
    'enName',
    'orderInForm',
    'isMandatory',
    'fieldTypeName',
  ];

  validationErrors: { [key in keyof Field]?: string } = {};
  isLoading = false;

  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public CategoryServ: RegistrationCategoryService,
    public fieldServ: FieldsService,
    public fieldTypeServ: FieldTypeService
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
      this.CategoryId = Number(this.activeRoute.snapshot.paramMap.get('id'));
      this.field.registrationCategoryID = this.CategoryId;
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });

    this.GetAllData();
    this.GetFieldType();
    this.GetCategoryData();
  }

  moveToEmployee() {
    this.router.navigateByUrl('Employee/Registration Form Field');
  }

  GetAllData() {
    this.Data = []
    this.fieldServ.GetByID(this.CategoryId, this.DomainName).subscribe((d) => {
      this.Data = d;
    });
  }
  GetCategoryData() {
    this.CategoryServ.GetByID(this.CategoryId, this.DomainName).subscribe(
      (d) => {
        this.Category = d;
      }
    );
  }

  GetFieldType() {
    this.fieldTypeServ.Get(this.DomainName).subscribe((d) => {
      this.fieldTypes = d;
    });
  }

  Create() {
    this.mode = 'Create';
    this.field = new FieldAddEdit();
    this.options = [];
    this.openModal();
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Field?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.fieldServ.Delete(id, this.DomainName).subscribe((data: any) => {
          this.GetAllData();
        });
      }
    });
  }

  Edit(row: Field) {
    this.mode = 'Edit';
    this.field = row as unknown as FieldAddEdit;
    this.options = row.options.map((option) => option.name);
    this.openModal();
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(
      InsertedByID,
      this.UserID,
      this.AllowDeleteForOthers
    );
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(
      InsertedByID,
      this.UserID,
      this.AllowEditForOthers
    );
    return IsAllow;
  }

  CreateOREdit() {
    this.field.registrationCategoryID = this.CategoryId;
    if (this) this.field.options = this.options;
    if (this.isFormValid()) {
      this.isLoading = true
      if (this.mode == 'Create') {
        this.fieldServ.Add(this.field, this.DomainName).subscribe(() => {
          this.GetAllData();
          this.closeModal();
          this.isLoading = false
        }, (error) => {
          this.isLoading = false;
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Try Again Later!',
            confirmButtonText: 'Okay',
            customClass: { confirmButton: 'secondaryBg' },
          });
        });
      }
      if (this.mode == 'Edit') {
        this.fieldServ.Edit(this.field, this.DomainName).subscribe(() => {
          this.GetAllData();
          this.closeModal();
          this.isLoading = false
        }, (error) => {
          this.isLoading = false;
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Try Again Later!',
            confirmButtonText: 'Okay',
            customClass: { confirmButton: 'secondaryBg' },
          });
        });
      }
    }
  }
  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.Category) {
      if (this.field.hasOwnProperty(key)) {
        const field = key as keyof FieldAddEdit;
        if (!this.field[field]) {
          if (
            field == 'arName' ||
            field == 'enName' ||
            field == 'orderInForm' ||
            field == 'fieldTypeID' ||
            field == 'registrationCategoryID'
          ) {
            this.validationErrors[field] = `*${this.capitalizeField(
              field
            )} is required`;
            isValid = false;
          }
        }
      }
    }
    if (this.field.fieldTypeID == 0) {
      this.validationErrors['fieldTypeID'] = `*${this.capitalizeField(
        'fieldTypeID'
      )} is required`;
      isValid = false;
    }
    if ((this.field.fieldTypeID == 5 || this.field.fieldTypeID == 4 || this.field.fieldTypeID == 7) && this.field.options.length == 0) {
      this.validationErrors['options'] = `*${this.capitalizeField(
        'options'
      )} is required`;
      isValid = false;
    }
    return isValid;
  }

  capitalizeField(field: keyof FieldAddEdit): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof FieldAddEdit; value: any }) {
    const { field, value } = event;
    (this.field as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
    if(field == 'fieldTypeID'){
      this.validationErrors['options'] = ''
    }
  }

  closeModal() {
    this.isModalVisible = false;
    this.inputValue = ''
    this.validationErrors = {}
  }

  openModal() {
    this.isModalVisible = true;
  }

  addOption() {
    if (this.inputValue.trim() !== '') {
      this.options.push(this.inputValue.trim());
      this.inputValue = '';
    }
  }

  removeOption(index: number) {
    this.options.splice(index, 1);
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Field[] = await firstValueFrom(
        this.fieldServ.GetByID(this.CategoryId, this.DomainName)
      );
      this.Data = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.Data = this.Data.filter((t) => {
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

  validateNumber(event: any, field: keyof FieldAddEdit): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.field[field] === 'string') {
        this.field[field] = '' as never;
      }
    }
  }
}
