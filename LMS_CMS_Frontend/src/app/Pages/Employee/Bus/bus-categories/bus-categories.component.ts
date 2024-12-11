import { CommonModule, DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../../Models/token-data';
import { BusType } from '../../../../Models/Bus/bus-type';
import { Domain } from '../../../../Models/domain';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuService } from '../../../../Services/shared/menu.service';
import { AccountService } from '../../../../Services/account.service';
import { BusCategoryService } from '../../../../Services/Employee/Bus/bus-category.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { ApiService } from '../../../../Services/api.service';
import { SearchComponent } from '../../../../Component/search/search.component';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-bus-categories',
  standalone: true,
  imports: [CommonModule, FormsModule, SearchComponent],
  templateUrl: './bus-categories.component.html',
  styleUrl: './bus-categories.component.css'
})
export class BusCategoriesComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  busCategory: BusType = new BusType();

  AllowEdit: boolean = true;
  AllowDelete: boolean = true;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: BusType[] = []
  DomainData: Domain[] = []

  DomainName: string = "";
  UserID: number = 0;

  IsChoosenDomain: boolean = false;
  IsEmployee: boolean = true;
 
  isModalVisible: boolean = false;
  mode: string = "";

  path: string = ""
  key: keyof BusType = "id";
  value: any = "";
  keysArray: string[] = ['id', 'name'];

  validationErrors: { [key in keyof BusType]?: string } = {};

  constructor(private router: Router, private menuService: MenuService, public activeRoute: ActivatedRoute, public account: AccountService, public BusTypeServ: BusCategoryService, public DomainServ: DomainService, public EditDeleteServ: DeleteEditPermissionService, public ApiServ: ApiService) { }

  ngOnInit() {

    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    if (this.User_Data_After_Login.type === "employee") {
      this.IsChoosenDomain = true;
      this.DomainName = this.ApiServ.GetHeader();

      this.activeRoute.url.subscribe(url => {
        this.path = url[0].path
      });

      this.GetTableData();
      this.menuService.menuItemsForEmployee$.subscribe((items) => {
        const settingsPage = this.menuService.findByPageName(this.path, items);
        if (settingsPage) {
          this.AllowEdit = settingsPage.allow_Edit;
          this.AllowDelete = settingsPage.allow_Delete;
          this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
          this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
        }
      });
    } else if (this.User_Data_After_Login.type === "octa") {
      this.GetAllDomains();
      this.IsEmployee = false;
      this.AllowEdit = true;
      this.AllowDelete = true;
    }
  }

  Create() {
    this.mode = "add";
    this.openModal();
  }

  GetAllDomains() {
    this.DomainServ.Get().subscribe((data) => {
      this.DomainData = data;
    })
  }

  async GetTableData() {
    try {
      const data = await firstValueFrom(this.BusTypeServ.Get(this.DomainName));
      this.TableData = data;
    } catch (error) {
      this.TableData = [];
      console.log('Error loading data:', error);
    }
  }

  openModal() {
    this.isModalVisible = true;
  }

  closeModal() {
    this.busCategory = new BusType()
    this.isModalVisible = false;
    this.validationErrors = {};
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this bus category?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.BusTypeServ.Delete(id, this.DomainName).subscribe((data) => {
          this.GetTableData();
        }
        );
      }
    });
  }

  Edit(id: number) {
    this.mode = "edit";
    const typeToEdit = this.TableData.find((t) => t.id === id);
    if (typeToEdit) {
      this.busCategory = { ...typeToEdit };
      this.openModal();
    } else {
      console.error("Type not found!");
    }
  }

  capitalizeField(field: keyof BusType): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.busCategory) {
      if (this.busCategory.hasOwnProperty(key)) {
        const field = key as keyof BusType;
        if (!this.busCategory[field]) {
          if(field == "name"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          this.validationErrors[field] = '';
        }
      }
    }
    return isValid;
  }

  onInputValueChange(event: { field: keyof BusType, value: any }) {
    const { field, value } = event;
    if (field == "name") {
      (this.busCategory as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
    }
  }

  Save() {
    this.BusTypeServ.Edit(this.busCategory, this.DomainName).subscribe(() => {
      this.GetTableData();
      this.closeModal();
      this.busCategory = new BusType()
    })
  }

  AddNewType() {
    this.BusTypeServ.Add(this.busCategory, this.DomainName).subscribe((data) => {
      this.GetTableData();
      this.closeModal();
      this.busCategory = new BusType()
    });
  }

  CreateOREdit() {
    if(this.isFormValid()){
      if (this.mode === "add") {
        this.AddNewType();
      }
      else if (this.mode === "edit") {
        this.Save();
      }
    }
  }

  getBusDataByDomainName(event: Event) {
    this.IsChoosenDomain = true;
    const selectedValue: string = ((event.target as HTMLSelectElement).value);
    this.DomainName = selectedValue;
    this.GetTableData();
  }

  IsAllowDelete(InsertedByID: number) {
    if (this.IsEmployee == false) { return true; }
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    if (this.IsEmployee == false) { return true; }
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  async onSearchEvent(event: { key: keyof BusType, value: any }) {
    this.key = event.key;
    this.value = event.value;
    console.log('Search by:', this.key, this.value);
    await this.GetTableData();
    if (this.value != "") {
      const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);

      this.TableData = this.TableData.filter(t => {
        const fieldValue = t[this.key];
        if (typeof fieldValue === 'string') {
          return fieldValue.toLowerCase().includes(this.value.toLowerCase());
        }
        if (typeof t[this.key] === 'number') {
          return t[this.key] === numericValue;
        }
        return t[this.key] == this.value;
      });
    }
  }
}
