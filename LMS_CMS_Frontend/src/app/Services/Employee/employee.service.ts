import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Employee } from '../../Models/Employee/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  baseUrl=""
  header=""
  
  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
    this.header = ApiServ.GetHeader();
  }

  GetWithTypeId(typeId: number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Employee[]>(`${this.baseUrl}/Employee/GetByTypeId/${typeId}`, { headers })
  }
  
  GetByID(empID: number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Employee[]>(`${this.baseUrl}/Employee/${empID}`, { headers })
  }
}
