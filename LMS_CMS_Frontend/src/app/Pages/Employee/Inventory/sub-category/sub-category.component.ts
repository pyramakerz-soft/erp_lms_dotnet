import { Component } from '@angular/core';
import { SubCategory } from '../../../../Models/Inventory/sub-category';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Category } from '../../../../Models/Inventory/category';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { InventoryCategoryService } from '../../../../Services/Employee/Inventory/inventory-category.service';
import { InventorySubCategoriesService } from '../../../../Services/Employee/Inventory/inventory-sub-categories.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-sub-category',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './sub-category.component.html',
  styleUrl: './sub-category.component.css'
})
export class SubCategoryComponent {
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

  category: Category = new Category();
  CategoryId :number = 0;
  SubCategory :SubCategory = new SubCategory()
  validationErrors: { [key in keyof SubCategory]?: string } = {};

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService ,
    public CategoryServ :InventoryCategoryService ,
    public InventorySubCategoryServ :InventorySubCategoriesService
  ) {}
  
  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });
    this.CategoryId = Number(this.activeRoute.snapshot.paramMap.get('id'));

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
    this.GetCategoryInfo();
  }

  GetAllData() {
   this.InventorySubCategoryServ.GetByCategoryId(this.CategoryId,this.DomainName).subscribe((d)=>{
    this.TableData=d
   })
  }
  GetCategoryInfo(){
    this.CategoryServ.GetById(this.CategoryId,this.DomainName).subscribe((d)=>{
      this.category=d
    })
  }
 
  Create() {
    this.mode = 'Create';
    this.SubCategory = new SubCategory();
    this.validationErrors={}
    this.openModal();
  }

  moveToCategory(){
   this.router.navigateByUrl(`Employee/Inventory Categories`)
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this SubCategory?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
       this.InventorySubCategoryServ.Delete(id,this.DomainName).subscribe((d)=>{
        this.GetAllData()
       })
      }
    });
  }

  Edit(id:number) {
    this.mode = 'Edit';
  
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
    if (this.isFormValid()) { 
      this.SubCategory.inventoryCategoriesID = this.CategoryId 
      if (this.mode == 'Create') {
        this.InventorySubCategoryServ.Add(this.SubCategory,this.DomainName).subscribe((d)=>{
          this.GetAllData();
          this.closeModal();
        })
      }
      if (this.mode == 'Edit') {
        this.InventorySubCategoryServ.Edit(this.SubCategory,this.DomainName).subscribe((d)=>{
          this.GetAllData();
          this.closeModal();
        })
      }
    }
    this.GetAllData();
  }

  closeModal() {
    this.isModalVisible = false;
    this.GetAllData();
  }

  openModal() {
    this.isModalVisible = true;
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.category) {
      if (this.category.hasOwnProperty(key)) {
        const field = key as keyof SubCategory;
        if (!this.SubCategory[field]) {
          if (
            field == 'name' 
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
  capitalizeField(field: keyof SubCategory): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof SubCategory; value: any }) {
    const { field, value } = event;
    (this.SubCategory as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: SubCategory[] = await firstValueFrom(
        this.InventorySubCategoryServ.Get(this.DomainName)
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
            return fieldValue === numericValue;
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.TableData = [];
    }
  }
}
