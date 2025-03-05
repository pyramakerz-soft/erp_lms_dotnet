import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { TokenData } from '../../../../Models/token-data';
import { CartService } from '../../../../Services/Student/cart.service';
import { Cart } from '../../../../Models/Student/ECommerce/cart';
import { OrderService } from '../../../../Services/Student/order.service';
import Swal from 'sweetalert2';
import { Order } from '../../../../Models/Student/ECommerce/order';

@Component({
  selector: 'app-order-items',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-items.component.html',
  styleUrl: './order-items.component.css'
})
export class OrderItemsComponent {
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0;
  DomainName: string = "";
  
  orderID: number = 0;

  order:Order = new Order()
  cart:Cart = new Cart()
  totalSalesPrices: number = 0;
  totalVat: number = 0;
  
  constructor(public account: AccountService, public ApiServ: ApiService, private router: Router, public cartService:CartService, public orderService:OrderService, public activeRoute: ActivatedRoute){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader(); 
    this.orderID = Number(this.activeRoute.snapshot.paramMap.get('id'))

    this.getCartData() 
    this.getOrderById()
  }

  goToCart() {
    if(this.User_Data_After_Login.type == 'employee'){
      this.router.navigateByUrl("Employee/Cart")
    } else{
      this.router.navigateByUrl("Student/Ecommerce/Cart")
    } 
  } 

  moveToOrders(){
    if(this.User_Data_After_Login.type == 'employee'){
      this.router.navigateByUrl("Employee/Order")
    } else{
      this.router.navigateByUrl("Student/Ecommerce/Order")
    } 
  }

  getCartData(){
    this.cartService.getByOrderID(this.orderID, this.DomainName).subscribe(
      data => {
        this.cart = data
        this.cart.cart_ShopItems.forEach(element => {
          this.totalSalesPrices = this.totalSalesPrices + (element.salesPrice * element.quantity) 
          this.totalVat = this.totalVat + (element.salesPrice * element.quantity) * (element.vatForForeign /100)
        });  
      }
    )
  }

  getOrderById(){
    this.orderService.getByID(this.orderID, this.DomainName).subscribe(
      data => {
        this.order = data
      }
    )
  }

  CancelOrder(){
    Swal.fire({
      title: 'Are you sure you want to cancel this Order?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Yes',
      cancelButtonText: 'No',
    }).then((result) => {
      if (result.isConfirmed) { 
        this.orderService.cancelOrder(this.orderID,this.DomainName).subscribe(
          data => {
            this.moveToOrders()
          }
        )
      }
    });
  }
}
