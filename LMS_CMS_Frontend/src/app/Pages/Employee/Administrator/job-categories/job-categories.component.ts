import { Component } from '@angular/core';
import { JobCategories } from '../../../../Models/Administrator/job-categories';
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
import { JobCategoriesService } from '../../../../Services/Employee/Administration/job-categories.service';
import { firstValueFrom } from 'rxjs';
import { Job } from '../../../../Models/Administrator/job';

@Component({
  selector: 'app-job-categories',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './job-categories.component.html',
  styleUrl: './job-categories.component.css',
})
export class JobCategoriesComponent {
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

  TableData: JobCategories[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name'];

  jobCategories: JobCategories = new JobCategories();

  validationErrors: { [key in keyof JobCategories]?: string } = {};
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
    public JobCategoryServ: JobCategoriesService
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
    this.JobCategoryServ.Get(this.DomainName).subscribe((d) => {
      this.TableData = d;
    });
  }

  Create() {
    this.mode = 'Create';
    this.jobCategories = new JobCategories();
    this.validationErrors = {};
    this.openModal();
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Job Category?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.JobCategoryServ.Delete(id, this.DomainName).subscribe((d) => {
          this.GetAllData();
        });
      }
    });
  }

  Edit(row: JobCategories) {
    this.mode = 'Edit';
    this.JobCategoryServ.GetById(row.id, this.DomainName).subscribe((d) => {
      this.jobCategories = d;
    });
    this.openModal();
  }

  IsAllowDelete(InsertedByID: number) {
    console.log(this.TableData)
    console.log(InsertedByID)
    console.log(this.UserID)
    console.log(this.AllowDeleteForOthers)
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
        this.JobCategoryServ.Add(this.jobCategories, this.DomainName).subscribe(
          (d) => {
            this.GetAllData();
            this.closeModal();
            this.isLoading = false;
          },
          (error) => {
            this.isLoading = false;
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: error.error || 'An unexpected error occurred',
              confirmButtonColor: '#FF7519',
            });
            return false;
          }
        );
      }
      if (this.mode == 'Edit') {
        this.JobCategoryServ.Edit(
          this.jobCategories,
          this.DomainName
        ).subscribe(
          (d) => {
            this.GetAllData();
            this.closeModal();
            this.isLoading = false;
          },
          (error) => {
            this.isLoading = false;
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: error.error || 'An unexpected error occurred',
              confirmButtonColor: '#FF7519',
            });
            return false;
          }
        );
      }
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
    for (const key in this.jobCategories) {
      if (this.jobCategories.hasOwnProperty(key)) {
        const field = key as keyof JobCategories;
        if (!this.jobCategories[field]) {
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
  capitalizeField(field: keyof JobCategories): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }
  onInputValueChange(event: { field: keyof JobCategories; value: any }) {
    const { field, value } = event;
    (this.jobCategories as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: JobCategories[] = await firstValueFrom(
        this.JobCategoryServ.Get(this.DomainName)
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
  view(id: number) {
    this.router.navigateByUrl(`Employee/Job/${id}`);
  }
}
