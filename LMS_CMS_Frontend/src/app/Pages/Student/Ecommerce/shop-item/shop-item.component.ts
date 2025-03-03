import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { ShopItemService } from '../../../../Services/Employee/Inventory/shop-item.service';
import { ShopItem } from '../../../../Models/Inventory/shop-item';
import { CommonModule } from '@angular/common';
import { CartShopItem } from '../../../../Models/Student/ECommerce/cart-shop-item';
import { CartShopItemService } from '../../../../Services/Student/cart-shop-item.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-shop-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './shop-item.component.html',
  styleUrl: './shop-item.component.css'
})
export class ShopItemComponent {
  ShopItemId = 0
  
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0;
  DomainName: string = "";

  shopItem: ShopItem = new ShopItem()
  cartShopItem:CartShopItem = new CartShopItem() 

  CalculatedVat: number = 0;
  selectedColor: number = 0;
  selectedSize: number = 0;
  
  constructor(public activeRoute: ActivatedRoute, public account: AccountService, public ApiServ: ApiService, private router: Router, public shopItemService:ShopItemService
    , private cartShopItemService:CartShopItemService
  ){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader(); 

    this.ShopItemId = Number(this.activeRoute.snapshot.paramMap.get('id'))

    this.getShopItem() 
  }

  getShopItem(){
    this.shopItemService.GetById(this.ShopItemId, this.DomainName).subscribe(
      data => {
        this.shopItem = data
        if(this.shopItem.vatForForeign != 0){
          this.CalculatedVat = (this.shopItem.salesPrice ? this.shopItem.salesPrice : 0) + (this.shopItem.salesPrice ? this.shopItem.salesPrice : 0) * ((this.shopItem.vatForForeign ? this.shopItem.vatForForeign : 0) / 100)
        }
      }
    )
  }

  moveToShop() {
    this.router.navigateByUrl("Student/Ecommerce/Shop")
  }

  goToCart() {
    this.router.navigateByUrl("Student/Ecommerce/Cart")
  } 

  goToOrder() {
    this.router.navigateByUrl("Student/Ecommerce/Order")
  } 

  addShopItemToCart(id: number) { 
    this.cartShopItem.studentID = this.UserID
    this.cartShopItem.quantity = 1
    this.cartShopItem.shopItemID = id
    if(this.selectedColor != 0){
      this.cartShopItem.shopItemColorID = this.selectedColor
    }
    if(this.selectedSize != 0){
      this.cartShopItem.shopItemSizeID = this.selectedSize
    }

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

  ChooseColor(id: any) {
    this.selectedColor = id
  }

  ChooseSize(id: any) {
    this.selectedSize = id
  }
      
  isValidColor(color: string): boolean {
    const s = new Option().style;
    s.color = color;
    return s.color !== '';
  }
}
