import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from '../../../../Models/Student/ECommerce/order';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service'; 
import { OrderService } from '../../../../Services/Student/order.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OrderStateService } from '../../../../Services/Employee/E-Commerce/order-state.service';
import { OrderState } from '../../../../Models/E-Commerce/order-state';  

@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './order-history.component.html',
  styleUrl: './order-history.component.css'
})
export class OrderHistoryComponent {  

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0; 
  DomainName: string = ""; 
  
  orders: Order[] = []
  orderStates: OrderState[] = []
  stateID: number = 0; 

  constructor(public account: AccountService, public ApiServ: ApiService, private router: Router, private orderrService: OrderService, private orderrStateService: OrderStateService){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.getOrders() 
    this.getOrderStates() 
  }

  getOrders(stateID?:number) {
    this.orders = []

    if(stateID){
      this.stateID = stateID
    } else{
      this.stateID = 0
    }

    if(this.stateID != 0){
      this.orderrService.getByOrderStateID(this.stateID, this.DomainName).subscribe(
        data => {
          this.orders = data
        }
      )
    } else{
      this.orderrService.getAll(this.DomainName).subscribe(
        data => {
          this.orders = data
        }
      )
    } 
  } 

  getOrderStates() {
    this.orderrStateService.Get(this.DomainName).subscribe(
      data => {
        this.orderStates = data
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
    this.router.navigate(["Employee/Order/" + id], { queryParams: { from: 'order-history' } })
  }
  
  ChangeOrderState(event: Event, orderId: number) {
    const selectedValue = (event.target as HTMLSelectElement).value; 

    let order  = new Order();
    order.id = orderId
    order.orderStateID = Number(selectedValue)

    this.orderrService.ChangeOrderState(order, this.DomainName).subscribe(
      data => {
        this.getOrders()
      }
    )
  }

  DownloadOrder(orderID: number) {
  } 
}
