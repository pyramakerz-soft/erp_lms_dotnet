import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from './api.service';
import { Login } from '../Models/login';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';
import { TokenData } from '../Models/token-data';
import { EmployeeService } from './Employee/employee.service';
import { LogOutService } from './shared/log-out.service';
import { ParentService } from './parent.service';
import { StudentService } from './student.service';
import { NavMenuComponent } from '../Component/nav-menu/nav-menu.component';


@Injectable({
  providedIn: 'root'
})

export class AccountService {
  
  baseUrl=""
  baseUrlOcta=""

  header=""

  isAuthenticated = !!localStorage.getItem("current_token");

  constructor(public http: HttpClient ,private router: Router , public ApiServ:ApiService, 
    public employeeService:EmployeeService, public studentService:StudentService, public parentService:ParentService, 
    public logOutService:LogOutService){  
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

      if(User_Data_After_Login.type == 'employee'){
        this.employeeService.Get_Employee_By_ID(User_Data_After_Login.id, this.header).subscribe(
          data => {  
            if(User_Data_After_Login.user_Name != data.user_Name || User_Data_After_Login.role != data.role_ID){ 
              this.logOutService.logOut()  
            } 
          }
        )
      } else if(User_Data_After_Login.type == 'parent'){
        this.parentService.GetByID(User_Data_After_Login.id, this.header).subscribe(
          data => { 
            if(User_Data_After_Login.user_Name != data.user_Name){
              this.logOutService.logOut() 
            } 
          }
        )
      } else if(User_Data_After_Login.type == 'student'){
        this.studentService.GetByID(User_Data_After_Login.id, this.header).subscribe(
          data => { 
            if(User_Data_After_Login.user_Name != data.user_Name){
              this.logOutService.logOut() 
            } 
          }
        )
      }
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
