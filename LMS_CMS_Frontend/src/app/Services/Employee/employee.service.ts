import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get_Employee_With_Role_Permission(Id:string){
    return this.http.get(`${this.baseUrl}/Employee/Employee_With_Role_Permission/${Id}`)
  }
}
