import { Component } from '@angular/core';
import { Job } from '../../../../Models/Administrator/job';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { JobService } from '../../../../Services/Employee/Administration/job.service';
import { firstValueFrom } from 'rxjs';
import { JobCategoriesService } from '../../../../Services/Employee/Administration/job-categories.service';
import { JobCategories } from '../../../../Models/Administrator/job-categories';

@Component({
  selector: 'app-job',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './job.component.html',
  styleUrl: './job.component.css',
})
export class JobComponent {
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

  TableData: Job[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name'];

  job: Job = new Job();
  JobCategoryID: number = 0;
  validationErrors: { [key in keyof Job]?: string } = {};
  Category: JobCategories = new JobCategories();

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
    public jobServ: JobService,
    public JobCategoryServ: JobCategoriesService
  ) {}
  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });
    this.JobCategoryID = Number(this.activeRoute.snapshot.paramMap.get('id'));
    this.GetJobCategoryInfo();
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
    this.jobServ
      .GetByCtegoty(this.JobCategoryID, this.DomainName)
      .subscribe((d) => {
        this.TableData = d;
      });
  }

  GetJobCategoryInfo() {
    this.JobCategoryServ.GetById(this.JobCategoryID, this.DomainName).subscribe(
      (d) => {
        this.Category = d;
      }
    );
  }

  Create() {
    this.mode = 'Create';
    this.job = new Job();
    this.openModal();
    this.validationErrors = {};
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Job ?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.jobServ.Delete(id, this.DomainName).subscribe((d) => {
          this.GetAllData();
        });
      }
    });
  }

  Edit(rowId:number) {
    this.mode = 'Edit';
    this.jobServ.GetById(rowId, this.DomainName).subscribe((d) => {
      this.job = d;
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
    this.job.JobCategoryId = this.JobCategoryID;
    this.isLoading = true;
    if (this.isFormValid()) {
      if (this.mode == 'Create') {
        this.jobServ.Add(this.job, this.DomainName).subscribe(
          (d) => {
            this.TableData = d;
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
        this.jobServ.Edit(this.job, this.DomainName).subscribe(
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
    for (const key in this.job) {
      if (this.job.hasOwnProperty(key)) {
        const field = key as keyof Job;
        if (!this.job[field]) {
          if (field == 'name' || field == 'JobCategoryId') {
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
  capitalizeField(field: keyof Job): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }
  onInputValueChange(event: { field: keyof Job; value: any }) {
    const { field, value } = event;
    (this.job as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Job[] = await firstValueFrom(
        this.jobServ.GetByCtegoty(this.JobCategoryID, this.DomainName)
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
  moveToBack() {
    this.router.navigateByUrl(`Employee/Job Category`);
  }
}
