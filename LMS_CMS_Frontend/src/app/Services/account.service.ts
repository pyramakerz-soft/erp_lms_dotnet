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
  baseUrlOcta=""

  header=""

  isAuthenticated = !!localStorage.getItem("current_token");

  constructor(public http: HttpClient ,private router: Router , public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
    this.baseUrlOcta=ApiServ.BaseUrlOcta
    this.header = ApiServ.GetHeader();

  }

  Login(UserInfo: Login) {
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Content-Type', 'application/json');
  
    return this.http.post(`${this.baseUrl}/Account`, UserInfo, {
      headers: headers,
      responseType: 'text',
    });
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

  LoginOcta(UserInfo: Login) {
    const headers = new HttpHeaders()
    .set('Content-Type', 'application/json');
    return this.http.post(`${this.baseUrlOcta}/OctaAccount`, UserInfo, {
      headers: headers,
      responseType: 'text',
    });
  }

}
