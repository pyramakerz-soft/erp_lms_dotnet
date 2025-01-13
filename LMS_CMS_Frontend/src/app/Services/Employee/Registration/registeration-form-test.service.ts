import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { RegisterationFormTest } from '../../../Models/Registration/registeration-form-test';

@Injectable({
  providedIn: 'root'
})
export class RegisterationFormTestService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  GetByRegistrationParentId(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegisterationFormTest[]>(`${this.baseUrl}/RegisterationFormTest/${id}`, { headers })
  }
  Edit(registerationFormTest: RegisterationFormTest, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/RegisterationFormTest`, registerationFormTest, { headers });
  }

  GetByRegistrationParentIdForParent(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegisterationFormTest[]>(`${this.baseUrl}/RegisterationFormTest/ForParent/${id}`, { headers })
  }

}
