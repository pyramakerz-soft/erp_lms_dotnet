import { Component } from '@angular/core';
import { TuitionDiscountTypes } from '../../../../Models/Accounting/tuition-discount-types';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { AccountingTreeChart } from '../../../../Models/Accounting/accounting-tree-chart';
import { TuitionDiscountTypeService } from '../../../../Services/Employee/Accounting/tuition-discount-type.service';
import { firstValueFrom } from 'rxjs';
import { AccountingTreeChartService } from '../../../../Services/Employee/Accounting/accounting-tree-chart.service';

@Component({
  selector: 'app-tuition-discount-types',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './tuition-discount-types.component.html',
  styleUrl: './tuition-discount-types.component.css'
})
export class TuitionDiscountTypesComponent {
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

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: TuitionDiscountTypes[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name', 'accountNumberName'];

  tuitionDiscountTypes: TuitionDiscountTypes = new TuitionDiscountTypes();

  validationErrors: { [key in keyof TuitionDiscountTypes]?: string } = {};
  AccountNumbers: AccountingTreeChart[] = [];
  isLoading = false

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public tuitionServ: TuitionDiscountTypeService,
    public accountServ: AccountingTreeChartService,
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
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });

    this.GetAllData();
    this.GetAllAccount()
  }

  GetAllData() {
    this.TableData = []
    this.tuitionServ.Get(this.DomainName).subscribe((d) => {
      this.TableData = d;
    })

  }

  GetAllAccount() {
    this.accountServ.GetBySubAndFileLinkID(12, this.DomainName).subscribe((d) => {
      this.AccountNumbers = d;
    })
  }
  Create() {
    this.mode = 'Create';
    this.tuitionDiscountTypes = new TuitionDiscountTypes();
    this.validationErrors = {}
    this.openModal();
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Type?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.tuitionServ.Delete(id, this.DomainName).subscribe((d) => {
          this.GetAllData()
        })
      }
    });
  }

  Edit(row: TuitionDiscountTypes) {
    this.mode = 'Edit';
    this.tuitionServ.GetById(row.id, this.DomainName).subscribe((d) => {
      this.tuitionDiscountTypes = d
    })
    this.validationErrors = {}
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
    if (this.isFormValid()) {
      this.isLoading = true
      if (this.mode == 'Create') {
        this.tuitionServ.Add(this.tuitionDiscountTypes, this.DomainName).subscribe((d) => {
          this.closeModal();
          this.GetAllData()
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
        this.tuitionServ.Edit(this.tuitionDiscountTypes, this.DomainName).subscribe((d) => {
          this.closeModal();
          this.GetAllData()
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
  }

  closeModal() {
    this.validationErrors = {}
    this.isModalVisible = false;
  }

  openModal() {
    this.isModalVisible = true;
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.tuitionDiscountTypes) {
      if (this.tuitionDiscountTypes.hasOwnProperty(key)) {
        const field = key as keyof TuitionDiscountTypes;
        if (!this.tuitionDiscountTypes[field]) {
          if (
            field == 'name' ||
            field == 'accountNumberID'
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

  capitalizeField(field: keyof TuitionDiscountTypes): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }
  onInputValueChange(event: { field: keyof TuitionDiscountTypes; value: any }) {
    const { field, value } = event;
    (this.tuitionDiscountTypes as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: TuitionDiscountTypes[] = await firstValueFrom(
        this.tuitionServ.Get(this.DomainName)
      );
      this.TableData = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.TableData = this.TableData.filter((t) => {
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
      this.TableData = [];
    }
  }
}
