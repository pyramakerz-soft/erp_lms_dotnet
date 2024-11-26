import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../Services/account.service';
import { Login } from '../../../Models/login';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../Models/token-data';
import { jwtDecode } from 'jwt-decode';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-pyramakerz-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pyramakerz-login.component.html',
  styleUrl: './pyramakerz-login.component.css'
})
export class PyramakerzLoginComponent {

  userInfo: Login = new Login("", "", "", "");
  User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  userNameError: string = "";
  passwordError: string = "";
  somthingError: string = "";

  constructor(private router: Router, public accountService: AccountService) { }


  SignIN() {
    this.userInfo.type = "pyramakerz";
    this.accountService.Login(this.userInfo).subscribe(
      (d: any) => {

        localStorage.removeItem("GoToLogin");
        localStorage.setItem("GoToLogin", "false");
        localStorage.removeItem("current_token");
        let count = localStorage.getItem("count")

        this.accountService.isAuthenticated = true;
        const token = JSON.parse(d).token;
        let add = true;

        for (let i = 0; i < localStorage.length; i++) {
          const key = localStorage.key(i);
          const value = localStorage.getItem(key || '');
          if (key && value && key.includes('token') && key != "current_token") {
            let decodedToken1: TokenData = jwtDecode(token);
            let decodedToken2: TokenData = jwtDecode(value);
            if (decodedToken1.user_Name === decodedToken2.user_Name && decodedToken1.type === decodedToken2.type)
              add = false;
          }
        }

        localStorage.setItem("current_token", token);

        if (add == true) {
          if (count === null) {
            localStorage.setItem("count", "1");
            localStorage.setItem("token 1", token);
          } else {
            let countNum = parseInt(count) + 1;
            localStorage.setItem("count", countNum.toString());
            let T = localStorage.getItem("token " + countNum)
            if (T != null) {
              let i = countNum + 1;
              let Continue = true;
              while (Continue) {
                let T2 = localStorage.getItem("token " + i);
                if (T2 == null) {
                  localStorage.setItem("token " + i, token);
                  Continue = false;
                }
                i++;
              }
            }
            else {
              localStorage.setItem("token " + countNum, token);
            }
          }
        }

        this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
        this.router.navigateByUrl("Pyramakerz/Home")
        
      }, (error) => {
        if (error.error === "UserName or Password is Invalid") {
          this.somthingError = "UserName or Password is Invalid"
        }
        if (error.status == 400) {
          this.somthingError = "Username, Password or Type maybe wrong"
        }
        else {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Try Again Later!',
            confirmButtonText: 'Okay',
            customClass: {
              confirmButton: 'secondaryBg' // Add your custom class here
            }
          });
        }
      }
    );
  }
}
