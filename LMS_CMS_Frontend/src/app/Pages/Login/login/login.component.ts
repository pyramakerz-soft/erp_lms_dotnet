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

  userInfo:Login =new Login(
    "", "", "");

  constructor(private router: Router , public accountService:AccountService ){  }

  SignIN(){
    console.log(this.userInfo)
    this.accountService.Login(this.userInfo).subscribe(
      (d: any) => {
        console.log(d)
        this.accountService.isAuthenticated = true;
        localStorage.setItem("token", d.token);
          this.accountService.User = jwtDecode(d);
          console.log("user :",this.accountService.User);

      },(error)=>{
        console.log(error)
      }
    );
  }
}
