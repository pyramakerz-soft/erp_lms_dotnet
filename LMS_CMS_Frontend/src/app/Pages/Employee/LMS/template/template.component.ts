import { Component } from '@angular/core';
import { Template } from '../../../../Models/LMS/template';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { SubjectCategory } from '../../../../Models/LMS/subject-category';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { firstValueFrom } from 'rxjs';
import { BusCategoryService } from '../../../../Services/Employee/Bus/bus-category.service';
import { DomainService } from '../../../../Services/Employee/domain.service';

@Component({
  selector: 'app-template',
  standalone: true,
  imports: [],
  templateUrl: './template.component.html',
  styleUrl: './template.component.css'
})
export class TemplateComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  template: Template = new Template();

  AllowEdit: boolean = true;
  AllowDelete: boolean = true;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: Template[] = []

  DomainName: string = "";
  UserID: number = 0;

  IsChoosenDomain: boolean = false;
  IsEmployee: boolean = true;

  isModalVisible: boolean = false;
  mode: string = "";

  path: string = ""
  key: string = "id";
  value: any = "";
  keysArray: string[] = ['id', 'name'];

  validationErrors: { [key in keyof Template]?: string } = {};
  isLoading = false;


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
    } 
  }

  Create() {
    this.mode = "add";
    this.openModal();
  }

  async GetTableData() {
    this.TableData = []
    try {
      // const data = await firstValueFrom();
      // this.TableData = data;
    } catch (error) {
      this.TableData = [];
    }
  }

  openModal() {
    this.isModalVisible = true;
  }

  closeModal() {
    this.template = new Template()
    this.isModalVisible = false;
    this.validationErrors = {};
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Template?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {

      }
    });
  }

  Edit(id: number) {
    this.mode = "edit";
    const typeToEdit = this.TableData.find((t) => t.id === id);
    if (typeToEdit) {
      this.template = { ...typeToEdit };
      this.openModal();
    } else {
      console.error("Type not found!");
    }
  }

  capitalizeField(field: keyof Template): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.template) {
      if (this.template.hasOwnProperty(key)) {
        const field = key as keyof Template;
        if (!this.template[field]) {
          if (
            field == "en_name" ||
            field == "ar_name" ||
            field == "weight" ||
            field == "afterCount"
          ) {
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

  onInputValueChange(event: { field: keyof Template, value: any }) {
    const { field, value } = event;
    if (
        field == "en_name" ||
        field == "ar_name" ||
        field == "weight" ||
        field == "afterCount"
    ) {
      (this.template as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
    }
  }

  CreateOREdit() {
    if (this.isFormValid()) {
      if (this.mode === "add") {
       
      }
      else if (this.mode === "edit") {

      }
    }
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

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    await this.GetTableData();
    if (this.value != "") {
      const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);

      this.TableData = this.TableData.filter(t => {
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
