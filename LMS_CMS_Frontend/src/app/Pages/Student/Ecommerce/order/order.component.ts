import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { Router } from '@angular/router';
import { ApiService } from '../../../../Services/api.service';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent {
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0;
  DomainName: string = ""; 

  orders: any[] = []

  constructor(public account: AccountService, public ApiServ: ApiService, private router: Router){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader(); 

    this.getOrders() 
  }

  goToCart() {
    this.router.navigateByUrl("Student/Ecommerce/Cart")
  } 

  getOrders() {
    
  } 
}
