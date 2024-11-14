import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from './api.service';
import { Login } from '../Models/login';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';
import { TokenData } from '../Models/token-data';


@Injectable({
  providedIn: 'root'
})

export class AccountService {
  
  baseUrl=""
  isAuthenticated = !!localStorage.getItem("current_token");

  constructor(public http: HttpClient ,private router: Router , public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Login(UserInfo:Login){
    return this.http.post(`${this.baseUrl}/Account`, UserInfo, { responseType: 'text' })
  }

  Get_Data_Form_Token(){
    let User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
    let token = localStorage.getItem("current_token")
    if(token){
      User_Data_After_Login = jwtDecode(token)
      return User_Data_After_Login
    } else{
      return User_Data_After_Login
    }
  }

  

  SignOut(){
    let User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
    User_Data_After_Login = this.Get_Data_Form_Token()
    let User_Type = User_Data_After_Login.type

    this.isAuthenticated = false;
    localStorage.removeItem("token");
    if(User_Type=="pyramakerz"){
      this.router.navigateByUrl("Pyramakerz/login");
    }else if(User_Type=="domain"){
      this.router.navigateByUrl("Domain/login");
    }else{
      this.router.navigateByUrl("");
    }
  }
}
