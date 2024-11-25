import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Employee } from '../../Models/Employee/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  GetWithTypeIdDomainID(typeId: number, domainID: number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<Employee[]>(`${this.baseUrl}/Employee/GetByTypeIdDomainId/${typeId}/${domainID}`, { headers })
  }
  
  GetByID(empID: number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<Employee[]>(`${this.baseUrl}/Employee/${empID}`, { headers })
  }
}
