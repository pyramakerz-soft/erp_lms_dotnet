import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { EmployeeTypeGet } from '../../Models/Administrator/employee-type-get';
import { ViolationAdd } from '../../Models/Administrator/violation-add';

@Injectable({
  providedIn: 'root'
})
export class EmployeeTypeService {

  baseUrl=""
  header=""
  
  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
    this.header = ApiServ.GetHeader();
  }

  Get(DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<EmployeeTypeGet[]>(`${this.baseUrl}/EmployeeType`, { headers })
    
  }

  
  
}
