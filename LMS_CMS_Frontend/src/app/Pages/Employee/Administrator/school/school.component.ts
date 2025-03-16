import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { CommonModule } from '@angular/common';
import { School } from '../../../../Models/school';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TranslateModule } from '@ngx-translate/core';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-school',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, TranslateModule],
  templateUrl: './school.component.html',
  styleUrl: './school.component.css',
})
export class SchoolComponent {
  keysArray: string[] = ['id', 'name', 'address', 'schoolTypeName'];
  key: string = 'id';
  value: any = '';

  schoolData: School[] = [];
  school: School = new School();
  editBuilding: boolean = false;
  validationErrors: { [key in keyof School]?: string } = {};

  AllowEdit: boolean = false;
  AllowEditForOthers: boolean = false;
  path: string = '';

  DomainName: string = '';
  UserID: number = 0;
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
  isLoading = false;

  constructor(
    public account: AccountService,
    public ApiServ: ApiService,
    public EditDeleteServ: DeleteEditPermissionService,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public schoolService: SchoolService,
    public router: Router
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.getSchoolData();

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });
  }

  getSchoolData() {
    this.schoolData=[]
    this.schoolService.Get(this.DomainName).subscribe((data) => {
      this.schoolData = data;
    });
  }

  GetSchoolById(schoolId: number) {
    this.schoolService
      .GetBySchoolId(schoolId, this.DomainName)
      .subscribe((data) => {
        this.school = data;
      });
  }

  openModal(schoolId: number) {
    this.GetSchoolById(schoolId);

    this.getSchoolData();

    document.getElementById('Add_Modal')?.classList.remove('hidden');
    document.getElementById('Add_Modal')?.classList.add('flex');
  }

  closeModal() {
    document.getElementById('Add_Modal')?.classList.remove('flex');
    document.getElementById('Add_Modal')?.classList.add('hidden');

    this.school = new School();

    this.validationErrors = {};
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: School[] = await firstValueFrom(
        this.schoolService.Get(this.DomainName)
      );
      this.schoolData = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.schoolData = this.schoolData.filter((t) => {
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
      this.schoolData = [];
    }
  }

  capitalizeField(field: keyof School): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof School; value: any }) {
    const { field, value } = event;

    (this.school as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(
      InsertedByID,
      this.UserID,
      this.AllowEditForOthers
    );
    return IsAllow;
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.school) {
      if (this.school.hasOwnProperty(key)) {
        const field = key as keyof School;
        if (!this.school[field]) {
          if (field == 'name') {
            this.validationErrors[field] = `*${this.capitalizeField(
              field
            )} is required`;
            isValid = false;
          }
        } else {
          if (field == 'name') {
            if (this.school.name.length > 100) {
              this.validationErrors[field] = `*${this.capitalizeField(
                field
              )} cannot be longer than 100 characters`;
              isValid = false;
            }
          } else {
            this.validationErrors[field] = '';
          }
        }
      }
    }
    return isValid;
  }

  onImageFileSelected(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      if (file.size > 25 * 1024 * 1024) {
        this.validationErrors['reportImageFile'] =
          'The file size exceeds the maximum limit of 25 MB.';
        this.school.reportImageFile = null;
        return;
      }
      if (file.type === 'image/jpeg' || file.type === 'image/png') {
        this.school.reportImageFile = file;
        this.validationErrors['reportImageFile'] = '';

        const reader = new FileReader();
        reader.readAsDataURL(file);
      } else {
        this.validationErrors['reportImageFile'] =
          'Invalid file type. Only JPEG, JPG and PNG are allowed.';
        this.school.reportImageFile = null;
        return;
      }
    }
  }

  SaveSchool() {
    this.isLoading = true;
    if (this.isFormValid()) {
      this.schoolService.Edit(this.school, this.DomainName).subscribe(
        (result: any) => {
          this.closeModal();
          this.isLoading = false;
          this.getSchoolData();
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
        }
      );
    }
  }
}
