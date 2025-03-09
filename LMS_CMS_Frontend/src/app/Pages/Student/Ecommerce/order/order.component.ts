import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { Router } from '@angular/router';
import { ApiService } from '../../../../Services/api.service';
import { Order } from '../../../../Models/Student/ECommerce/order';
import { OrderService } from '../../../../Services/Student/order.service';
import { EmployeeStudentService } from '../../../../Services/Employee/Accounting/employee-student.service';
import { EmplyeeStudent } from '../../../../Models/Accounting/emplyee-student';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent { 
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0;
  StuID: number = 0;
  emplyeeStudent: EmplyeeStudent[] = [];
  DomainName: string = ""; 

  orders: Order[] = []

  constructor(public account: AccountService, public ApiServ: ApiService, private router: Router, public employeeStudentService:EmployeeStudentService, private orderrService: OrderService){}
  
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

    this.getOrders() 
  }

  getStudents(){
    this.employeeStudentService.Get(this.UserID, this.DomainName).subscribe(
      data => {
        this.emplyeeStudent = data
      }
    )
  }

  goToCart() {
    if(this.User_Data_After_Login.type == 'employee'){
      this.router.navigateByUrl("Employee/Cart")
    } else{
      this.router.navigateByUrl("Student/Ecommerce/Cart")
    }
  } 

  getOrders() {
    this.orderrService.getByStudentID(this.StuID, this.DomainName).subscribe(
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
    if(this.User_Data_After_Login.type == 'employee'){
      this.router.navigateByUrl("Employee/Order/" + id)
    } else{
      this.router.navigateByUrl("Student/Ecommerce/Order/" + id)
    }
  }
  
  DownloadOrder(id:number) {  
    if(this.User_Data_After_Login.type == 'employee'){ 
      this.router.navigate(['Employee/Order', id], { queryParams: { download: 'true' } }); 

    } else{ 
      this.router.navigate(['Student/Ecommerce/Order', id], { queryParams: { download: 'true' } }); 
    }
  }
} 
