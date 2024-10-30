import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from './api.service';
import { Login } from '../Models/login';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  
   baseUrl=""
   isAuthenticated = !!localStorage.getItem("token"); 
   User: Login =new Login("", "", "", "");

  constructor(public http: HttpClient ,private router: Router , public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Login(UserInfo:Login){
    let body = {User_Name:UserInfo.user_Name ,Password:UserInfo.password , Type :UserInfo.type};
    console.log(body)
    return this.http.post(`${this.baseUrl}/Account`, body, { responseType: 'text' })
  }
   

  private CheckToken(): void {
    const token = localStorage.getItem("token");
    if (token) {
      this.isAuthenticated = true;
      this.User = jwtDecode(token);
    } else {
      this.isAuthenticated = false;
    }
  }


}
