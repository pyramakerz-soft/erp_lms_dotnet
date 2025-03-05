import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { Router } from '@angular/router';
import { ApiService } from '../../../../Services/api.service';
import { Order } from '../../../../Models/Student/ECommerce/order';
import { OrderService } from '../../../../Services/Student/order.service';

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

  orders: Order[] = []

  constructor(public account: AccountService, public ApiServ: ApiService, private router: Router, private orderrService: OrderService){}
  
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
    this.orderrService.getByStudentID(this.UserID, this.DomainName).subscribe(
      data => {
        this.orders = data
      }
    )
  } 

  formatDate(dateString: string) {
    const date = new Date(dateString);
    const options: Intl.DateTimeFormatOptions = {
        month: 'short',
        day: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        hour12: true
    };

    const formattedDate = date.toLocaleDateString('en-US', options)

    return `${formattedDate}`;
  }

  goToOrderItems(id: number) {
    this.router.navigateByUrl("Student/Ecommerce/Order/" + id)
  }
  
  DownloadOrder() {
    
  }
}
