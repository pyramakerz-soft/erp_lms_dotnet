import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Router, ActivatedRoute } from '@angular/router';
import { BusType } from '../../../../Models/Bus/bus-type';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { Supplier } from '../../../../Models/Accounting/supplier';
import Swal from 'sweetalert2';
import { firstValueFrom } from 'rxjs';
import { AccountingTreeChart } from '../../../../Models/Accounting/accounting-tree-chart';
import { SupplierService } from '../../../../Services/Employee/Accounting/supplier.service';
import { AccountingTreeChartService } from '../../../../Services/Employee/Accounting/accounting-tree-chart.service';
import { Country } from '../../../../Models/Accounting/country';
import { CountryService } from '../../../../Services/Employee/Accounting/country.service';

@Component({
  selector: 'app-suppliers',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './suppliers.component.html',
  styleUrl: './suppliers.component.css',
})
export class SuppliersComponent {
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

  TableData: Supplier[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name', "commercialRegister", "taxCard", "address", "website", "email", "countryName"];

  Supplier: Supplier = new Supplier();

  validationErrors: { [key in keyof Supplier]?: string } = {};
  AccountNumbers: AccountingTreeChart[] = [];
  contries: Country[] = [];
  isLoading = false

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public SupplierServ: SupplierService,
    public accountServ: AccountingTreeChartService,
    public countryServ: CountryService
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
    this.GetAllAccount();
    this.GetAllCountries();
  }

  GetAllData() {
    this.TableData = []
    this.SupplierServ.Get(this.DomainName).subscribe((d) => {
      this.TableData = d;
    })
  }

  GetAllAccount() {
    this.accountServ.GetBySubAndFileLinkID(2, this.DomainName).subscribe((d) => {
      this.AccountNumbers = d;
    })
  }

  GetAllCountries() {
    this.countryServ.Get().subscribe((d) => {
      this.contries = d;
    });
  }
  Create() {
    this.mode = 'Create';
    this.Supplier = new Supplier()
    this.openModal();
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Supplier?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.SupplierServ.Delete(id, this.DomainName).subscribe((d) => {
          this.GetAllData()
        })
      }
    });
  }

  Edit(row: Supplier) {
    this.mode = 'Edit';
    this.SupplierServ.GetById(row.id, this.DomainName).subscribe((d) => {
      this.Supplier = d
    })
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

  validateNumber(event: any, field: keyof Supplier): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.Supplier[field] === 'string') {
        this.Supplier[field] = '' as never;
      }
    }
  }

  CreateOREdit() {
    if (this.isFormValid()) {
      this.isLoading = true
      if (this.mode == 'Create') {
        this.SupplierServ.Add(this.Supplier, this.DomainName).subscribe((d) => {
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
        this.SupplierServ.Edit(this.Supplier, this.DomainName).subscribe((d) => {
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
      this.closeModal()
    }
    this.GetAllData()
  }

  closeModal() {
    this.isModalVisible = false;
  }

  openModal() {
    this.isModalVisible = true;
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.Supplier) {
      if (this.Supplier.hasOwnProperty(key)) {
        const field = key as keyof Supplier;
        if (!this.Supplier[field]) {
          if (
            field == 'name' ||
            field == 'countryID' ||
            field == 'email' ||
            field == 'website' ||
            field == 'phone1' ||
            field == 'taxCard' ||
            field == 'commercialRegister' ||
            field == 'accountNumberID' ||
            field == 'address'
          ) {
            this.validationErrors[field] = `*${this.capitalizeField(
              field
            )} is required`;
            isValid = false;
          }
        }
      }
    }
    const emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
    if (this.Supplier.email && !emailPattern.test(this.Supplier.email)) {
      isValid = false;
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'Email is not valid.',
        confirmButtonColor: '#FF7519',
      });
    }

    const phoenPattern = /^0(10|11|12|15)\d{8}$/;

    if (this.Supplier.email && !phoenPattern.test(this.Supplier.phone1)) {
      isValid = false;
      Swal.fire({
        icon: 'warning',
        title: 'Warning!',
        text: 'Phone 1 is not valid.',
        confirmButtonColor: '#FF7519',
      });
    }

    if (this.Supplier.phone2) {
      if (this.Supplier.email && !phoenPattern.test(this.Supplier.phone2)) {
        isValid = false;
        Swal.fire({
          icon: 'warning',
          title: 'Warning!',
          text: 'Phone 2 is not valid.',
          confirmButtonColor: '#FF7519',
        });
      }
    }

    if (this.Supplier.phone3) {
      if (this.Supplier.email && !phoenPattern.test(this.Supplier.phone3)) {
        isValid = false;
        Swal.fire({
          icon: 'warning',
          title: 'Warning!',
          text: 'Phone 3 is not valid.',
          confirmButtonColor: '#FF7519',
        });
      }
    }

    return isValid;
  }
  capitalizeField(field: keyof Supplier): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }
  onInputValueChange(event: { field: keyof Supplier; value: any }) {
    const { field, value } = event;
    (this.Supplier as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Supplier[] = await firstValueFrom(
        this.SupplierServ.Get(this.DomainName)
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
            return fieldValue === numericValue;
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.TableData = [];
    }
  }
}
