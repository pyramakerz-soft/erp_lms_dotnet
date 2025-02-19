import { Component } from '@angular/core';
import { ShopItem } from '../../../../Models/Inventory/shop-item';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { SearchComponent } from '../../../../Component/search/search.component';
import { SubCategory } from '../../../../Models/Inventory/sub-category';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { Category } from '../../../../Models/Inventory/category';
import { Grade } from '../../../../Models/LMS/grade';
import { School } from '../../../../Models/school';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';

@Component({
  selector: 'app-shop-items-add-edit',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './shop-items-add-edit.component.html',
  styleUrl: './shop-items-add-edit.component.css'
})
export class ShopItemsAddEditComponent {
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

  TableData: SubCategory[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name'];

  Data: ShopItem = new ShopItem();
  ShopItemId: number = 0;
  validationErrors: { [key in keyof ShopItem]?: string } = {};

  inputValue: string = '';
  colors: string[] = [];

  inputValueSize: string = '';
  Sizes: string[] = [];

  CategoryId: number = 0;
  Categories: Category[] = []
  SubCategories: SubCategory[] = []
  Grades: Grade[] = []
  Gender: SubCategory[] = []
  Schools: School[] = []

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public SchoolServ: SchoolService,
    public GradeServ: GradeService
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.ShopItemId = Number(this.activeRoute.snapshot.paramMap.get('id'));
    if (!this.ShopItemId) {
      this.mode = "Create"
    } else {
      this.mode = "Edit"
    }

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
    this.GetAllSchools();
    this.GetAllGrades();
  }

  GetAllData() {

  }

  moveToBack() {
    this.router.navigateByUrl(`Employee/Shop Items`)
  }

  Save() {
    if (this.isFormValid()) {
      if (this.mode == 'Create') {

      }
      if (this.mode == 'Edit') {

      }
    }
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.Data) {
      if (this.Data.hasOwnProperty(key)) {
        const field = key as keyof ShopItem;
        if (!this.Data[field]) {
          if (
            field == 'enName' ||
            field == 'arName' ||
            field == 'enDescription' ||
            field == 'arDescription' ||
            field == 'purchasePrice' ||
            field == 'salesPrice'

          ) {
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
  capitalizeField(field: keyof ShopItem): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof ShopItem; value: any }) {
    const { field, value } = event;
    (this.Data as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  addOption() {
    if (this.inputValue.trim() !== '') {
      this.colors.push(this.inputValue.trim());
      this.inputValue = '';
    }
  }

  removeOption(index: number) {
    this.colors.splice(index, 1);
  }

  addSize() {
    if (this.inputValueSize.trim() !== '') {
      this.Sizes.push(this.inputValueSize.trim());
      this.inputValueSize = '';
    }
  }

  removeSixe(index: number) {
    this.Sizes.splice(index, 1);
  }

  GetAllSchools() {
    this.SchoolServ.Get(this.DomainName).subscribe((d) => {
      this.Schools = d
    })
  }

  GetAllGrades() {
    this.GradeServ.Get(this.DomainName).subscribe((d) => {
      this.Grades = d
    })
  }

  onIsExceptionChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.Data.availableInShop = isChecked
  }

  // onImageFileSelected(event: any) {
  //   const file: File = event.target.files[0];
    
  //   if (file) {
  //     if (file.size > 25 * 1024 * 1024) {
  //       this.validationErrors['MainImage'] = 'The file size exceeds the maximum limit of 25 MB.';
  //       this.Data.MainImageFile = null;
  //       return; 
  //     }
  //     if (file.type === 'image/jpeg' || file.type === 'image/png') {
  //       this.Data.MainImageFile = file; 
  //       this.validationErrors['MainImage'] = ''; 

  //       const reader = new FileReader();
  //       reader.readAsDataURL(file);
  //     } else {
  //       this.validationErrors['MainImage'] = 'Invalid file type. Only JPEG, JPG and PNG are allowed.';
  //       this.Data.MainImageFile = null;
  //       return; 
  //     }
  //   }
  // }
}
