import { Component } from '@angular/core';
import { Floor } from '../../../../Models/LMS/floor';
import { Building } from '../../../../Models/LMS/building';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { BuildingService } from '../../../../Services/Employee/LMS/building.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FloorService } from '../../../../Services/Employee/LMS/floor.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { EmployeeGet } from '../../../../Models/Employee/employee-get';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import Swal from 'sweetalert2';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-floor',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './floor.component.html',
  styleUrl: './floor.component.css',
})
export class FloorComponent {
  keysArray: string[] = ['id', 'name', 'floorMonitorName'];
  key: string = 'id';
  value: any = '';

  monitorrData: EmployeeGet[] = [];
  floorData: Floor[] = [];
  floor: Floor = new Floor();
  building: Building = new Building();
  editFloor: boolean = false;
  validationErrors: { [key in keyof Floor]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  path: string = '';

  DomainName: string = '';
  UserID: number = 0;
  buildingId: number = 0;
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
    public buildingService: BuildingService,
    public ApiServ: ApiService,
    public EditDeleteServ: DeleteEditPermissionService,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public floorService: FloorService,
    public employeeService: EmployeeService,
    public router: Router
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.buildingId = Number(this.activeRoute.snapshot.paramMap.get('Id'));
    this.DomainName = String(
      this.activeRoute.snapshot.paramMap.get('domainName')
    );

    this.getBuildingData();
    this.getFloorData();

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
    this.buildingService
      .GetByID(this.buildingId, this.DomainName)
      .subscribe((data) => {
        this.building = data;
      });
  }

  getFloorData() {
    this.floorData = [];
    this.floorService
      .GetByBuildingId(this.buildingId, this.DomainName)
      .subscribe((data) => {
        this.floorData = data;
      });
  }

  getMonitorData() {
    this.employeeService.Get_Employees(this.DomainName).subscribe((data) => {
      this.monitorrData = data;
    });
  }

  GetFloorById(Id: number) {
    this.floorService.GetByID(Id, this.DomainName).subscribe((data) => {
      this.floor = data;
    });
  }

  openModal(floorId?: number) {
    if (floorId) {
      this.editFloor = true;
      this.GetFloorById(floorId);
    }

    this.getMonitorData();

    document.getElementById('Add_Modal')?.classList.remove('hidden');
    document.getElementById('Add_Modal')?.classList.add('flex');
  }

  closeModal() {
    document.getElementById('Add_Modal')?.classList.remove('flex');
    document.getElementById('Add_Modal')?.classList.add('hidden');

    this.floor = new Floor();
    this.monitorrData = [];

    if (this.editFloor) {
      this.editFloor = false;
    }
    this.validationErrors = {};
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Floor[] = await firstValueFrom(
        this.floorService.GetByBuildingId(this.buildingId, this.DomainName)
      );
      this.floorData = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.floorData = this.floorData.filter((t) => {
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
      this.floorData = [];
    }
  }

  capitalizeField(field: keyof Floor): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  moveToBuilding() {
    this.router.navigateByUrl('Employee/Building');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.building) {
      if (this.building.hasOwnProperty(key)) {
        const field = key as keyof Floor;
        if (!this.floor[field]) {
          if (field == 'name' || field == 'floorMonitorID') {
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

  onInputValueChange(event: { field: keyof Floor; value: any }) {
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

  SaveFloor() {
    if (this.isFormValid()) {
      this.isLoading = true;
      this.floor.buildingID = this.buildingId;
      if (this.editFloor == false) {
        this.floorService.Add(this.floor, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal();
            this.getFloorData();
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
          }
        );
      } else {
        this.floorService.Edit(this.floor, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal();
            this.getFloorData();
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
          }
        );
      }
    }
  }

  deleteFloor(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Floor?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.floorService.Delete(id, this.DomainName).subscribe((data: any) => {
          this.floorData = [];
          this.getFloorData();
        });
      }
    });
  }
}
