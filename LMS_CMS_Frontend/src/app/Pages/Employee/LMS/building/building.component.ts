import { Component } from '@angular/core';
import { Building } from '../../../../Models/LMS/building';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { BuildingService } from '../../../../Services/Employee/LMS/building.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { School } from '../../../../Models/school';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-building',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './building.component.html',
  styleUrl: './building.component.css',
})
export class BuildingComponent {
  keysArray: string[] = ['id', 'name', 'schoolName'];
  key: string = 'id';
  value: any = '';

  buildingData: Building[] = [];
  building: Building = new Building();
  editBuilding: boolean = false;
  validationErrors: { [key in keyof Building]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
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

  Schools: School[] = [];

  constructor(
    public account: AccountService,
    public buildingService: BuildingService,
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

    this.getBuildingData();

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });
  }

  getBuildingData() {
    this.buildingData = [];
    this.buildingService.Get(this.DomainName).subscribe((data) => {
      this.buildingData = data;
    });
  }

  getSchoolData() {
    this.schoolService.Get(this.DomainName).subscribe((data) => { 
      this.Schools = data;
    });
  }

  GetBuildingById(buildingId: number) {
    this.buildingService
      .GetByID(buildingId, this.DomainName)
      .subscribe((data) => {
        this.building = data;
      });
  }

  openModal(buildingId?: number) {
    if (buildingId) {
      this.editBuilding = true;
      this.GetBuildingById(buildingId);
    }

    this.getSchoolData();

    document.getElementById('Add_Modal')?.classList.remove('hidden');
    document.getElementById('Add_Modal')?.classList.add('flex');
  }

  closeModal() {
    document.getElementById('Add_Modal')?.classList.remove('flex');
    document.getElementById('Add_Modal')?.classList.add('hidden');

    this.building = new Building();
    this.Schools = [];

    if (this.editBuilding) {
      this.editBuilding = false;
    }
    this.validationErrors = {};
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Building[] = await firstValueFrom(
        this.buildingService.Get(this.DomainName)
      );
      this.buildingData = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.buildingData = this.buildingData.filter((t) => {
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
      this.buildingData = [];
    }
  }

  capitalizeField(field: keyof Building): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.building) {
      if (this.building.hasOwnProperty(key)) {
        const field = key as keyof Building;
        if (!this.building[field]) {
          if (field == 'name' || field == 'schoolID') {
            this.validationErrors[field] = `*${this.capitalizeField(
              field
            )} is required`;
            isValid = false;
          }
        } else {
          if (field == 'name') {
            if (this.building.name.length > 100) {
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

  onInputValueChange(event: { field: keyof Building; value: any }) {
    const { field, value } = event;

    (this.building as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
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

  SaveBuilding() {
    if (this.isFormValid()) {
      this.isLoading = true;
      if (this.editBuilding == false) {
        this.buildingService.Add(this.building, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal();
            this.isLoading = false;
            this.getBuildingData();
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
      } else {
        this.buildingService.Edit(this.building, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal();
            this.isLoading = false;
            this.getBuildingData();
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

  deleteBuilding(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Building?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.buildingService
          .Delete(id, this.DomainName)
          .subscribe((data: any) => {
            this.buildingData = [];
            this.getBuildingData();
          });
      }
    });
  }

  moveToFloors(Id: number) {
    this.router.navigateByUrl('Employee/Floor/' + this.DomainName + '/' + Id);
  }
}
