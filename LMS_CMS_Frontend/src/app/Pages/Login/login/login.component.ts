import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../Services/account.service';
import { Login } from '../../../Models/login';
import { jwtDecode } from 'jwt-decode';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../Models/token-data';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  userInfo:Login = new Login("", "", "", "");
  User_Data_After_Login = new TokenData("", 0, "", "", "", "", "", "")

  constructor(private router:Router, public accountService:AccountService){  }

  SignIN(){
    this.accountService.Login(this.userInfo).subscribe(
      (d: any) => {
        this.accountService.isAuthenticated = true;
        localStorage.setItem("token", JSON.parse(d).token);
        
        this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()

        if(this.User_Data_After_Login.type == "parent"){
          this.router.navigateByUrl("/ParentHome")
        } else if(this.User_Data_After_Login.type == "student"){
          this.router.navigateByUrl("/StudentHome")
        } else if(this.User_Data_After_Login.type == "employee"){
          this.router.navigateByUrl("/EmployeeHome")
        }
      },(error)=>{
        console.log(error)
      }
    );
  }
}
