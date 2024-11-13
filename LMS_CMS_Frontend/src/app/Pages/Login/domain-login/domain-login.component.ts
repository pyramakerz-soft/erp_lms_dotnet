import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../Services/account.service';
import { Login } from '../../../Models/login';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../Models/token-data';

@Component({
  selector: 'app-domain-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './domain-login.component.html',
  styleUrl: './domain-login.component.css'
})
export class DomainLoginComponent {
  
  userInfo:Login = new Login("", "", "", "");
  User_Data_After_Login = new TokenData("", 0, 0, "", "", "", "", "")

  constructor(private router:Router, public accountService:AccountService){  }


  SignIN(){
    this.userInfo.type="domain";
      this.accountService.Login(this.userInfo).subscribe(
        (d: any) => {
          this.accountService.isAuthenticated = true;
          localStorage.setItem("current_token", JSON.parse(d).token);
          this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
          
          localStorage.setItem(this.User_Data_After_Login.user_Name +"token", JSON.parse(d).token);
          this.router.navigateByUrl("Domain/Home")

        },(error)=>{
          
        }
      );
    }
  }
