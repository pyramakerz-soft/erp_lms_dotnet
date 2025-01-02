import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { ViolationAdd } from '../../Models/Administrator/violation-add';
import { ViolationEdit } from '../../Models/Administrator/violation-edit';

@Injectable({
  providedIn: 'root'
})
export class EmployeeTypeViolationService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
    this.header = ApiServ.GetHeader();
  }

  Add(violation:ViolationAdd,DomainName?: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${token}`)
      .set('domain-name', this.header)
      .set('Content-Type', 'application/json');

    return this.http.post<ViolationAdd>(`${this.baseUrl}/EmployeeTypeViolation`,violation, { headers })

  }

  Edit(violation:ViolationAdd,DomainName?: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${token}`)
      .set('domain-name', this.header)
      .set('Content-Type', 'application/json');

    return this.http.put<ViolationAdd>(`${this.baseUrl}/EmployeeTypeViolation`,violation, { headers })

  }
}
