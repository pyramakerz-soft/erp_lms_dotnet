import { Component } from '@angular/core';
import { RegistrationCategory } from '../../../../Models/Registration/registration-category';
import { Field } from '../../../../Models/Registration/field';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../../../Services/api.service';
import { AccountService } from '../../../../Services/account.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FieldOption } from '../../../../Models/Registration/field-option';
import { FieldType } from '../../../../Models/Registration/field-type';

@Component({
  selector: 'app-fields',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './fields.component.html',
  styleUrl: './fields.component.css'
})
export class FieldsComponent {

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

  DomainName: string = '';
  UserID: number = 0;
  path: string = '';

  Category: RegistrationCategory = new RegistrationCategory();
  Data: Field[] = []

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  mode: string = 'Create'

  isModalVisible: boolean = false;
  field: Field = new Field();

  CategoryId:number=0;

  options:string[]=[];

  inputValue: string = '';

  fieldTypes:FieldType[]=[]

  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
      this.CategoryId = Number(this.activeRoute.snapshot.paramMap.get('id'))
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });

    this.GetAllData();
  }

  moveToEmployee() {
    this.router.navigateByUrl("Employee/RegistrationFormField")
  }

  GetAllData() {

  }

  Create() {
    this.mode = 'Create';
    this.field = new Field();
    this.openModal();
  }

  Delete(id: number) {

  }

  Edit(row: Field) {
    this.mode = 'Edie';
    this.field = row;
    this.openModal();
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  CreateOREdit() {

  }

  closeModal() {
    this.isModalVisible = false;
  }

  openModal() {
    this.isModalVisible = true;
  }

  addOption() {
    if (this.inputValue.trim() !== '') {
      this.options.push(this.inputValue.trim()); // Trim whitespace and add to array
      this.inputValue = ''; // Clear input field
    }
  }

  // Remove an option by index
  removeOption(index: number) {
    this.options.splice(index, 1); // Remove the option from array
  }

  onInputValueChange(){
    
  }
}
