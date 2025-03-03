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

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css'
})
export class ShopComponent { 
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0;
  DomainName: string = "";
  
  InventoryCategory:Category[] = []
  InventorySubCategory:SubCategory[] = []
  ShopItem:ShopItem[] = []
  selectedInventoryCategory = 0
  selectedInventorySubCategory = 0
  
  CurrentPage:number = 1
  PageSize:number = 9
  TotalPages:number = 1
  TotalRecords:number = 0

  cartShopItem:CartShopItem = new CartShopItem()

  constructor(public inventoryCategoryService:InventoryCategoryService, public inventorySubCategoryService:InventorySubCategoriesService, 
    public account: AccountService, public ApiServ: ApiService, public shopItemService:ShopItemService, private router: Router, private cartShopItemService:CartShopItemService){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader(); 

    this.getInventoryCategory() 
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
    this.shopItemService.GetBySubCategoryIDWithGenderAndGrade(this.selectedInventorySubCategory, this.CurrentPage, this.PageSize, this.DomainName).subscribe(
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
    this.cartShopItem.studentID = this.UserID
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
    this.router.navigateByUrl("Student/Ecommerce/ShopItem/" + id)
  }

  goToCart() {
    this.router.navigateByUrl("Student/Ecommerce/Cart")
  } 

  goToOrder() {
    this.router.navigateByUrl("Student/Ecommerce/Order")
  } 
}
