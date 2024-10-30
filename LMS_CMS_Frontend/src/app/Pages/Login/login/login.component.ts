import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../Services/account.service';
import { Login } from '../../../Models/login';
import { jwtDecode } from 'jwt-decode';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  userInfo:Login =new Login("", "", "", "");

  constructor(private router:Router, public accountService:AccountService){  }

  SignIN(){
    this.accountService.Login(this.userInfo).subscribe(
      (d: any) => {
        this.accountService.isAuthenticated = true;
        localStorage.setItem("token", d.token);
        this.accountService.User = jwtDecode(d);
        if(this.accountService.User.type == "parent"){
          console.log("hehe")
          this.router.navigateByUrl("/ParentHome")
        } else if(this.accountService.User.type == "student"){
          this.router.navigateByUrl("/StudentHome")
        } else if(this.accountService.User.type == "employee"){
          this.router.navigateByUrl("/EmployeeHome")
        }
      },(error)=>{
        console.log(error)
      }
    );
  }
}
