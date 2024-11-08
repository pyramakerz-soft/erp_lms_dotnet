import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../Services/account.service';
import { Login } from '../../../Models/login';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../Models/token-data';

@Component({
  selector: 'app-pyramakerz-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './pyramakerz-login.component.html',
  styleUrl: './pyramakerz-login.component.css'
})
export class PyramakerzLoginComponent {
  userInfo:Login = new Login("", "", "", "");
  User_Data_After_Login = new TokenData("", 0, 0, "", "", "", "", "")

  constructor(private router:Router, public accountService:AccountService){  }


  SignIN(){
    this.userInfo.type="pyramakerz";
      this.accountService.Login(this.userInfo).subscribe(
        (d: any) => {
          this.accountService.isAuthenticated = true;
          localStorage.setItem("token", JSON.parse(d).token);
          
          this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
  
          this.router.navigateByUrl("Pyramakerz/Home")

        },(error)=>{
          
        }
      );
    }
  }
