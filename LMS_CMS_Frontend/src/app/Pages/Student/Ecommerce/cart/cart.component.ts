import { Component } from '@angular/core';
import { Cart } from '../../../../Models/Student/ECommerce/cart';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { TokenData } from '../../../../Models/token-data';
import { CartService } from '../../../../Services/Student/cart.service';
import { OrderService } from '../../../../Services/Student/order.service';
import Swal from 'sweetalert2';
import { CartShopItemService } from '../../../../Services/Student/cart-shop-item.service';
import { CartShopItem } from '../../../../Models/Student/ECommerce/cart-shop-item'; 
import { EmplyeeStudent } from '../../../../Models/Accounting/emplyee-student';
import { EmployeeStudentService } from '../../../../Services/Employee/Accounting/employee-student.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0;
  StuID: number = 0;
  emplyeeStudent: EmplyeeStudent[] = [];
  DomainName: string = "";
    
  cart:Cart = new Cart()
  totalSalesPrices: number = 0;
  totalVat: number = 0;

  cartShopItem:CartShopItem = new CartShopItem()

  filteredCartShopItem: CartShopItem[] = [] 
  searchTerm: string = '';
  
  constructor(public account: AccountService, public ApiServ: ApiService, public activeRoute: ActivatedRoute, public employeeStudentService:EmployeeStudentService,
    private router: Router, private cartService: CartService, 
    private orderService: OrderService, public cartShopItemService:CartShopItemService){}
  
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

    this.getCart() 
  }

  getStudents(){
    this.employeeStudentService.Get(this.UserID, this.DomainName).subscribe(
      data => {
        this.emplyeeStudent = data
      }
    )
  }

  goToOrder() {
    if(this.User_Data_After_Login.type == 'employee'){
      this.router.navigateByUrl("Employee/Order")
    } else{
      this.router.navigateByUrl("Student/Ecommerce/Order")
    }
  } 
  
  getCart(){
    this.totalVat = 0
    this.totalSalesPrices = 0
    this.cartService.getByStudentID(this.StuID, this.DomainName).subscribe(
      data => {
        this.cart = data 
        this.cart.cart_ShopItems.forEach(element => {
          this.totalVat = this.totalVat + (element.salesPrice *(element.vatForForeign / 100))
          this.totalSalesPrices = this.totalSalesPrices + element.salesPrice * element.quantity
        });
        this.filterCartItems();
      }
    )
  }
  
  ProceedToBuy() { 
    Swal.fire({
      title: 'Are you sure you want to Proceed To Buy?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Yes',
      cancelButtonText: 'No',
    }).then((result) => {
      if (result.isConfirmed) { 
        this.orderService.ConfirmOrder(this.cart.id, this.DomainName).subscribe(
          data =>{
            this.goToOrder()
          }
        )
      }
    }); 
  }
  
  RemoveCartShopItem(id: number) {
    Swal.fire({
      title: 'Are you sure you want to Remove this item?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Yes',
      cancelButtonText: 'No',
    }).then((result) => {
      if (result.isConfirmed) { 
        this.cartShopItemService.RemoveItemFromCart(id, this.DomainName).subscribe(
          data => {
            this.getCart()
          }
        )
      }
    });  
  }

  ChangeQuantity(cartShopitemID:number, quantity:number){
    this.cartShopItem.cartID = this.cart.id
    this.cartShopItem.quantity = quantity
    this.cartShopItem.id = cartShopitemID
    this.cartShopItemService.ChangeQuantity(this.cartShopItem, this.DomainName).subscribe(
      data => {
        this.getCart()
      }
    )
  }

  filterCartItems() {
    if (this.searchTerm.trim() === '') {
      this.filteredCartShopItem = [...this.cart.cart_ShopItems];  
    } else {
      this.filteredCartShopItem = this.cart.cart_ShopItems.filter(item =>
        item.shopItemEnName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        (item.shopItemColorName && item.shopItemColorName.toLowerCase().includes(this.searchTerm.toLowerCase())) ||
        (item.shopItemSizeName && item.shopItemSizeName.toLowerCase().includes(this.searchTerm.toLowerCase()))
      );
    }
  }
}
