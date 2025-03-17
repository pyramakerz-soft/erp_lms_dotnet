import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { BusType } from '../../../../Models/Bus/bus-type';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { Domain } from '../../../../Models/domain';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { ApiService } from '../../../../Services/api.service';
import { SearchComponent } from '../../../../Component/search/search.component';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-bus-types',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './bus-types.component.html',
  styleUrls: ['./bus-types.component.css'],
})
export class BusTypesComponent {
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
  busType: BusType = new BusType(0, '', 0);

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: BusType[] = [];
  DomainData: Domain[] = [];

  DomainName: string = '';
  UserID: number = 0;

  IsChoosenDomain: boolean = false;
  IsEmployee: boolean = true;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name'];

  validationErrors: { [key in keyof BusType]?: string } = {};
  isLoading = false;

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    if (this.User_Data_After_Login.type === 'employee') {
      this.IsChoosenDomain = true;
      this.DomainName = this.ApiServ.GetHeader();

      this.activeRoute.url.subscribe((url) => {
        this.path = url[0].path;
      });

      this.GetTableData();
      this.menuService.menuItemsForEmployee$.subscribe((items) => {
        const settingsPage = this.menuService.findByPageName(this.path, items);
        if (settingsPage) {
          this.AllowEdit = settingsPage.allow_Edit;
          this.AllowDelete = settingsPage.allow_Delete;
          this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
          this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
        }
      });
    } else if (this.User_Data_After_Login.type === 'octa') {
      this.GetAllDomains();
      this.IsEmployee = false;
      this.AllowEdit = true;
      this.AllowDelete = true;
    }
  }

  Create() {
    this.mode = 'add';
    this.openModal();
  }

  GetAllDomains() {
    this.DomainServ.Get().subscribe((data) => {
      this.DomainData = data;
    });
  }

  async GetTableData() {
    this.TableData = [];
    try {
      const data = await firstValueFrom(this.BusTypeServ.Get(this.DomainName));
      this.TableData = data;
    } catch (error) {
      this.TableData = [];
    }
  }

  openModal() {
    this.isModalVisible = true;
  }

  closeModal() {
    this.busType = new BusType();
    this.isModalVisible = false;
    this.validationErrors = {};
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this bus type?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.BusTypeServ.Delete(id, this.DomainName).subscribe((data) => {
          this.GetTableData();
        });
      }
    });
  }

  Edit(id: number) {
    this.mode = 'edit';
    const typeToEdit = this.TableData.find((t) => t.id === id);
    if (typeToEdit) {
      this.busType = { ...typeToEdit };
      this.openModal();
    } else {
      console.error('Type not found!');
    }
  }

  capitalizeField(field: keyof BusType): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.busType) {
      if (this.busType.hasOwnProperty(key)) {
        const field = key as keyof BusType;
        if (!this.busType[field]) {
          if (field == 'name') {
            this.validationErrors[field] = `*${this.capitalizeField(
              field
            )} is required`;
            isValid = false;
          }
        } else {
          this.validationErrors[field] = '';
        }
      }
    }
    return isValid;
  }

  onInputValueChange(event: { field: keyof BusType; value: any }) {
    const { field, value } = event;
    if (field == 'name') {
      (this.busType as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
    }
  }

  Save() {
    this.isLoading = true;
    this.BusTypeServ.Edit(this.busType, this.DomainName).subscribe(
      () => {
        this.GetTableData();
        this.closeModal();
        this.isLoading = false; // Hide spinner
        this.busType = new BusType();
      },
      (error) => {
        this.isLoading = false; // Hide spinner
      }
    );
  }

  AddNewType() {
    this.isLoading = true;
    this.BusTypeServ.Add(this.busType, this.DomainName).subscribe(
      (data) => {
        this.GetTableData();
        this.closeModal();
        this.isLoading = false; // Hide spinner
        this.busType = new BusType();
      },
      (error) => {
        this.isLoading = false; // Hide spinner
      }
    );
  }
  
  CreateOREdit() {
    if (this.isFormValid()) {
      if (this.mode === 'add') {
        this.AddNewType();
      } else if (this.mode === 'edit') {
        this.Save();
      }
    }
  }

  getBusDataByDomainId(event: Event) {
    this.IsChoosenDomain = true;
    const selectedValue: string = (event.target as HTMLSelectElement).value;
    this.DomainName = selectedValue;
    this.GetTableData();
  }

  IsAllowDelete(InsertedByID: number) {
    if (this.IsEmployee == false) {
      return true;
    }
    const IsAllow = this.EditDeleteServ.IsAllowDelete(
      InsertedByID,
      this.UserID,
      this.AllowDeleteForOthers
    );
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    if (this.IsEmployee == false) {
      return true;
    }
    const IsAllow = this.EditDeleteServ.IsAllowEdit(
      InsertedByID,
      this.UserID,
      this.AllowEditForOthers
    );
    return IsAllow;
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    await this.GetTableData();
    if (this.value != '') {
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
  }
}
