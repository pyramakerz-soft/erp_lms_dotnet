import { Component } from '@angular/core';
import { Reasonsforleavingwork } from '../../../../Models/Administrator/reasonsforleavingwork';
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
import { ReasonsforleavingworkService } from '../../../../Services/Employee/Administration/reasonsforleavingwork.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-reasonsforleavingwork',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './reasonsforleavingwork.component.html',
  styleUrl: './reasonsforleavingwork.component.css',
})
export class ReasonsforleavingworkComponent {
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

  TableData: Reasonsforleavingwork[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name'];

  reasonsforleavingwork: Reasonsforleavingwork = new Reasonsforleavingwork();

  validationErrors: { [key in keyof Reasonsforleavingwork]?: string } = {};
  isLoading = false;

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public reasonServ: ReasonsforleavingworkService
  ) {}
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
  }

  GetAllData() {
    this.TableData = [];
    this.reasonServ.Get(this.DomainName).subscribe((d) => {
      this.TableData = d;
    });
  }

  Create() {
    this.mode = 'Create';
    this.reasonsforleavingwork = new Reasonsforleavingwork();
    this.validationErrors = {};
    this.openModal();
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Reason?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.reasonServ.Delete(id, this.DomainName).subscribe((d) => {
          this.GetAllData();
        });
      }
    });
  }

  Edit(row: Reasonsforleavingwork) {
    this.mode = 'Edit';
    this.reasonServ.GetById(row.id, this.DomainName).subscribe((d) => {
      this.reasonsforleavingwork = d;
    });
    this.validationErrors = {};
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
      this.isLoading = true;
      if (this.mode == 'Create') {
        this.reasonServ
          .Add(this.reasonsforleavingwork, this.DomainName)
          .subscribe(
            (d) => {
              this.GetAllData();
              this.isLoading = false;
            },
            (error) => {
              this.isLoading = false;
              Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Try Again Later!',
                confirmButtonText: 'Okay',
                customClass: { confirmButton: 'secondaryBg' },
              });
              return false;
            }
          );
      }
      if (this.mode == 'Edit') {
        this.reasonServ
          .Edit(this.reasonsforleavingwork, this.DomainName)
          .subscribe(
            (d) => {
              this.GetAllData();
              this.isLoading = false;
            },
            (error) => {
              this.isLoading = false;
              Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Try Again Later!',
                confirmButtonText: 'Okay',
                customClass: { confirmButton: 'secondaryBg' },
              });
              return false;
            }
          );
      }
      this.closeModal();
    } 
  }

  closeModal() {
    this.isModalVisible = false;
  }

  openModal() {
    this.isModalVisible = true;
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.reasonsforleavingwork) {
      if (this.reasonsforleavingwork.hasOwnProperty(key)) {
        const field = key as keyof Reasonsforleavingwork;
        if (!this.reasonsforleavingwork[field]) {
          if (field == 'name') {
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
  capitalizeField(field: keyof Reasonsforleavingwork): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }
  onInputValueChange(event: {
    field: keyof Reasonsforleavingwork;
    value: any;
  }) {
    const { field, value } = event;
    (this.reasonsforleavingwork as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Reasonsforleavingwork[] = await firstValueFrom(
        this.reasonServ.Get(this.DomainName)
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
