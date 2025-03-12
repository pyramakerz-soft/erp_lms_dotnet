import { Component } from '@angular/core';
import { InventoryCategoryService } from '../../../../Services/Employee/Inventory/inventory-category.service';
import { Category } from '../../../../Models/Inventory/category';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { CommonModule } from '@angular/common';
import { SubCategory } from '../../../../Models/Inventory/sub-category';
import { InventorySubCategoriesService } from '../../../../Services/Employee/Inventory/inventory-sub-categories.service';
import { ShopItemService } from '../../../../Services/Employee/Inventory/shop-item.service';
import { ShopItem } from '../../../../Models/Inventory/shop-item';
import { Router } from '@angular/router';
import { CartShopItemService } from '../../../../Services/Student/cart-shop-item.service';
import { CartShopItem } from '../../../../Models/Student/ECommerce/cart-shop-item';
import Swal from 'sweetalert2';
import { EmployeeStudentService } from '../../../../Services/Employee/Accounting/employee-student.service';
import { EmplyeeStudent } from '../../../../Models/Accounting/emplyee-student';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css'
})
export class ShopComponent { 
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0;
  StuID: number = 0;
  emplyeeStudent: EmplyeeStudent[] = [];
  DomainName: string = "";
  
  InventoryCategory:Category[] = []
  InventorySubCategory:SubCategory[] = []
  ShopItem:ShopItem[] = []
  selectedInventoryCategory = 0
  selectedInventorySubCategory = 0
  
  CurrentPage:number = 1
  PageSize:number = 1

  TotalPages:number = 1
  TotalRecords:number = 0
  
  cartShopItem:CartShopItem = new CartShopItem()
 
  searchQuery: string = '';
   
  constructor(public inventoryCategoryService:InventoryCategoryService, public inventorySubCategoryService:InventorySubCategoriesService, public employeeStudentService:EmployeeStudentService,
    public account: AccountService, public ApiServ: ApiService, public shopItemService:ShopItemService, private router: Router, private cartShopItemService:CartShopItemService){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader(); 

    if(this.User_Data_After_Login.type == 'employee'){
      this.getStudents()
    }

    if(this.User_Data_After_Login.type == 'student'){
      this.StuID = this.UserID
    }

    this.getInventoryCategory() 
  }

  getStudents(){
    this.employeeStudentService.Get(this.UserID, this.DomainName).subscribe(
      data => {
        this.emplyeeStudent = data
      }
    )
  }

  getInventoryCategory(){
    this.inventoryCategoryService.Get(this.DomainName).subscribe(
      data => {
        this.InventoryCategory = data 
      }
    )
  }

  getInventorySubCategory(){
    this.inventorySubCategoryService.GetByCategoryId(this.selectedInventoryCategory, this.DomainName).subscribe(
      data => { 
        this.InventorySubCategory = data 
      }
    )
  }

  getSubCategories(id: number) { 
    this.InventorySubCategory = []
    this.ShopItem = []
    this.selectedInventorySubCategory = 0
    this.selectedInventoryCategory = id
    this.getInventorySubCategory()
  }

  getShopPagination(){
    this.shopItemService.GetBySubCategoryIDWithGenderAndGrade(this.selectedInventorySubCategory, this.CurrentPage, this.PageSize, this.DomainName, this.searchQuery).subscribe(
      data => { 
        this.CurrentPage = data.pagination.currentPage
        this.PageSize = data.pagination.pageSize
        this.TotalPages = data.pagination.totalPages
        this.TotalRecords = data.pagination.totalRecords 
        this.ShopItem = data.data 
      }
    )
  }
  
  getShopItems(id: number) { 
    this.ShopItem = []
    this.selectedInventorySubCategory = id
    this.getShopPagination()
  }
  
  addShopItemToCart(id: number) { 
    this.cartShopItem.studentID = this.StuID
    this.cartShopItem.quantity = 1
    this.cartShopItem.shopItemID = id

    this.cartShopItemService.Add(this.cartShopItem, this.DomainName).subscribe(
      data => {
        Swal.fire({
          title: "Added Successfully!",
          icon: "success"
        }).then((result) => {
          this.goToCart();
        }); 
      }
    )
  }

  onAddToCartClick(event: MouseEvent, itemId: number, limit: number|null) {
    event.stopPropagation(); 
    if (limit && limit > 0) {
        this.addShopItemToCart(itemId);
    }
  }

  changeCurrentPage(currentPage:number){
    this.CurrentPage = currentPage
    this.getShopPagination() 
  }

  goToShopItem(id: number) {  
    if(this.User_Data_After_Login.type == "employee"){
      this.router.navigateByUrl("Employee/ShopItem/" + id)
    } else{
      this.router.navigateByUrl("Student/Ecommerce/ShopItem/" + id)
    }
  }

  goToCart() {
    if(this.User_Data_After_Login.type == "employee"){
      this.router.navigateByUrl("Employee/Cart")
    } else{
      this.router.navigateByUrl("Student/Ecommerce/Cart")
    }
  } 

  goToOrder() {
    if(this.User_Data_After_Login.type == "employee"){
      this.router.navigateByUrl("Employee/Order")
    } else{
      this.router.navigateByUrl("Student/Ecommerce/Order")
    }
  }
  
  searchReports(): void {
    this.ShopItem = []
    this.CurrentPage = 1; 
    this.getShopPagination();
  }
}
