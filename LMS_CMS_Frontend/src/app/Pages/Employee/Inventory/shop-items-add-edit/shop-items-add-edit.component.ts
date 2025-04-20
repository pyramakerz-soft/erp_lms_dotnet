import { Component } from '@angular/core';
import { ShopItem } from '../../../../Models/Inventory/shop-item';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router'; 
import { SubCategory } from '../../../../Models/Inventory/sub-category';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service'; 
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service'; 
import { Category } from '../../../../Models/Inventory/category';
import { Grade } from '../../../../Models/LMS/grade';
import { School } from '../../../../Models/school';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { InventorySubCategoriesService } from '../../../../Services/Employee/Inventory/inventory-sub-categories.service';
import { Gender } from '../../../../Models/gender';
import { InventoryCategoryService } from '../../../../Services/Employee/Inventory/inventory-category.service';
import { GenderService } from '../../../../Services/Employee/Inventory/gender.service';
import { ShopItemService } from '../../../../Services/Employee/Inventory/shop-item.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-shop-items-add-edit',
  standalone: true,
  imports: [FormsModule, CommonModule],
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
 
  DomainName: string = '';
  UserID: number = 0;

  mode: string = ''; 

  ShopItem: ShopItem = new ShopItem();
  ShopItemId: number = 0;
  validationErrors: { [key in keyof ShopItem]?: string } = {};

  inputValue: string = '';
  colors: string[] = [];

  inputValueSize: string = '';
  Sizes: string[] = [];

  Grades: Grade[] = []
  Gender: Gender[] = []
  SchoolId: number = 0;
  Schools: School[] = []
  CategoryId: number = 0;
  Categories: Category[] = []
  InventorySubCategories: SubCategory[] = []
  gender: Gender[] = []
  isLoading=false

  constructor(
    private router: Router, 
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public SchoolServ: SchoolService,
    public GradeServ: GradeService,
    public inventorySubCategoriesService: InventorySubCategoriesService,
    public inventoryCategoryService: InventoryCategoryService,
    public genderService: GenderService,
    public shopItemService: ShopItemService
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader(); 

    this.ShopItemId = Number(this.activeRoute.snapshot.paramMap.get('id'));
    if (!this.ShopItemId) {
      this.mode = "Create"
    } else {
      this.mode = "Edit"
      this.GetShopItemData();
    }
 
    this.GetAllSchools(); 
    this.GetAllCategories();
    this.GetAllGenders();
  }

  GetShopItemData() {
    this.shopItemService.GetById(this.ShopItemId, this.DomainName).subscribe(
      data =>{
        this.ShopItem = data 
        
        this.CategoryId = this.ShopItem.inventoryCategoriesID
        if (this.CategoryId) {
          this.GetSubCategoryData(); 
        }

        this.SchoolId = this.ShopItem.schoolID
        if (this.SchoolId) {
          this.GetAllGrades(); 
        }
 
        this.ShopItem.shopItemColors.forEach((element: { name: string; }) => {
          this.colors.push(element.name)
        });

        this.ShopItem.shopItemSizes.forEach((element: { name: string; }) => {
          this.Sizes.push(element.name)
        });
      }
    )
  }

  GetSubCategoryData() {
    this.inventorySubCategoriesService.GetByCategoryId(this.CategoryId, this.DomainName).subscribe(
      data => {
        this.InventorySubCategories = data
      }
    )
  }

  GetAllCategories() {
    this.inventoryCategoryService.Get(this.DomainName).subscribe(
      data => {
        this.Categories = data
      }
    )
  }
 
  GetAllSchools() {
    this.SchoolServ.Get(this.DomainName).subscribe((d) => {
      this.Schools = d
    })
  }

  GetAllGrades() {
    this.GradeServ.GetBySchoolId(this.SchoolId, this.DomainName).subscribe((d) => {
      this.Grades = d
    })
  }

  GetAllGenders() {
    this.genderService.Get(this.DomainName).subscribe((d) => {
      this.gender = d 
    })
  }

  moveToBack() {
    this.router.navigateByUrl(`Employee/Items`)
  }

  onCategoryChange(event: Event) {
    this.InventorySubCategories = []
    this.ShopItem.inventorySubCategoriesID = 0
    const selectedValue = (event.target as HTMLSelectElement).value; 
    this.CategoryId = Number(selectedValue)
    if (this.CategoryId) {
      this.GetSubCategoryData(); 
    }
  }

  onSchoolChange(event: Event) {
    this.Grades = []
    this.ShopItem.gradeID = 0
    const selectedValue = (event.target as HTMLSelectElement).value; 
    this.SchoolId = Number(selectedValue)
    if (this.SchoolId) {
      this.GetAllGrades(); 
    }
  }
 
  validateNumber(event: any, field: keyof ShopItem): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.ShopItem[field] === 'string') {
        this.ShopItem[field] = '' as never;  
      }
    }
  }

  Save() {
    this.ShopItem.shopItemColors = []
    this.ShopItem.shopItemSizes = []
    if(this.colors.length != 0){ 
      this.colors.forEach( element => {
        this.ShopItem.shopItemColors.push(element)
      });
    }
    if(this.Sizes.length != 0){ 
      this.Sizes.forEach( element => {
        this.ShopItem.shopItemSizes.push(element)
      });
    }
    
    if (this.isFormValid()) { 
      this.isLoading=true
      if (this.mode == 'Create') {
        this.shopItemService.Add(this.ShopItem, this.DomainName).subscribe(
          data => {
            this.router.navigateByUrl(`Employee/Items`)
            this.isLoading=false
            Swal.fire({
              title: "Added Successfully!",
              icon: "success"
            });
          }, 
          err => { 
            if(err.error == "BarCode Must Be unique"){
            this.isLoading=false
              Swal.fire({
                title: "BarCode Must Be unique",
                text: "Please change the BarCode",
                icon: "error"
              });
            }
          }
        )
      }
      if (this.mode == 'Edit') {
        this.shopItemService.Edit(this.ShopItem, this.DomainName).subscribe(
          data => {
            this.router.navigateByUrl(`Employee/Items`) 
            this.isLoading=false
            Swal.fire({
              title: "Edited Successfully!",
              icon: "success"
            });
          }, 
          err => {
            this.isLoading=false
            if(err.error == "BarCode Must Be unique"){
              Swal.fire({
                title: "BarCode Must Be unique",
                text: "Please change the BarCode",
                icon: "error"
              });
            }
          }
        )
      }
    }
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.ShopItem) {
      if (this.ShopItem.hasOwnProperty(key)) {
        const field = key as keyof ShopItem;
        if (!this.ShopItem[field]) {
          if (field == 'enName'|| field == 'arName'|| field == 'enDescription'|| field == 'arDescription'|| field == 'purchasePrice'|| field == 'salesPrice' 
            || field == 'limit'|| field == 'inventorySubCategoriesID'|| field == 'schoolID') {
            this.validationErrors[field] = `*${this.capitalizeField( field )} is required`;
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
    (this.ShopItem as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  addColor() {
    if (this.inputValue.trim() !== '') {
      this.colors.push(this.inputValue.trim());
      this.inputValue = '';
    }
  }

  removeColor(index: number) {
    this.colors.splice(index, 1);
  }

  addSize() {
    if (this.inputValueSize.trim() !== '') {
      this.Sizes.push(this.inputValueSize.trim());
      this.inputValueSize = '';
    }
  }

  removeSize(index: number) {
    this.Sizes.splice(index, 1);
  }
 
  onAvailableInShopkChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.ShopItem.availableInShop = isChecked
  }

  onImageMainFileSelected(event: any) {
    const file: File = event.target.files[0];
    this.ShopItem.mainImage = ""
    if (file) {
      if (file.size > 25 * 1024 * 1024) {
        this.validationErrors['mainImageFile'] = 'The file size exceeds the maximum limit of 25 MB.';
        this.ShopItem.mainImageFile = null;
        return; 
      }
      if (file.type === 'image/jpeg' || file.type === 'image/png') {
        this.ShopItem.mainImageFile = file; 
        this.validationErrors['mainImageFile'] = ''; 

        const reader = new FileReader();
        reader.readAsDataURL(file);
      } else {
        this.validationErrors['mainImageFile'] = 'Invalid file type. Only JPEG, JPG and PNG are allowed.';
        this.ShopItem.mainImageFile = null;
        return; 
      }
    }
  }

  onImageOtherFileSelected(event: any) {
    const file: File = event.target.files[0];
    this.ShopItem.otherImage = ""
    if (file) {
      if (file.size > 25 * 1024 * 1024) {
        this.validationErrors['otherImageFile'] = 'The file size exceeds the maximum limit of 25 MB.';
        this.ShopItem.otherImageFile = null;
        return; 
      }
      if (file.type === 'image/jpeg' || file.type === 'image/png') {
        this.ShopItem.otherImageFile = file; 
        this.validationErrors['otherImageFile'] = ''; 

        const reader = new FileReader();
        reader.readAsDataURL(file);
      } else {
        this.validationErrors['otherImageFile'] = 'Invalid file type. Only JPEG, JPG and PNG are allowed.';
        this.ShopItem.otherImageFile = null;
        return; 
      }
    }
  }
}
