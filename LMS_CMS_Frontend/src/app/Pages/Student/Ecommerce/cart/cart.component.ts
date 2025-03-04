import { Component } from '@angular/core';
import { Cart } from '../../../../Models/Student/ECommerce/cart';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { TokenData } from '../../../../Models/token-data';
import { CartService } from '../../../../Services/Student/cart.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0;
  DomainName: string = "";
    
  cart:Cart = new Cart()

  constructor(public account: AccountService, public ApiServ: ApiService, private router: Router, private cartService: CartService){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader(); 

    this.getCart() 
  }

  goToOrder() {
    this.router.navigateByUrl("Student/Ecommerce/Order")
  } 
  
  getCart(){
    this.cartService.getByStudentID(this.UserID, this.DomainName).subscribe(
      data => {
        this.cart = data
      }
    )
  }
}
