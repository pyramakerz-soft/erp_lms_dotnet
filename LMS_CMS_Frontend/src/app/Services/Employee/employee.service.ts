import { HttpClient } from '@angular/common/http';
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
    return this.http.get<Employee[]>(`${this.baseUrl}/Employee/GetByTypeIdDomainId/${typeId}/${domainID}`)
  }
}
