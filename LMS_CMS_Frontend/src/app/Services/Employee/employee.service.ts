import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Employee } from '../../Models/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get_Employee_With_Role_Permission(Id:number){
    return this.http.get(`${this.baseUrl}/Employee/Employee_With_Role_Permission/${Id}`)
  }

  Get_Employee_By_Id(Id:number){
    return this.http.get(`${this.baseUrl}/Employee/${Id}`)
  }

  Get_Employee_By_School_Id(Id:number){
    return this.http.get(`${this.baseUrl}/Employee/Employees_In_School/${Id}`)
  }

  Add_Employee(emp:Employee){
    return this.http.post(`${this.baseUrl}/Employee`, emp, { observe: 'response' })
  }
}
