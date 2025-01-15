import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { RegisterationFormParent } from '../../../Models/Registration/registeration-form-parent';

@Injectable({
  providedIn: 'root'
})
export class RegisterationFormParentService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegisterationFormParent[]>(`${this.baseUrl}/RegisterationFormParent`, { headers })
  }

  GetByParentId(parent: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegisterationFormParent[]>(`${this.baseUrl}/RegisterationFormParent/GetByParentID/${parent}`, { headers })
  }

  GetBySchoolId(school: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegisterationFormParent[]>(`${this.baseUrl}/RegisterationFormParent/GetBySchoolID/${school}`, { headers })
  }

  GetByYearId(year: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegisterationFormParent[]>(`${this.baseUrl}/RegisterationFormParent/GetByAcademicYearID/${year}`, { headers })
  }

  GetByStateId(state: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegisterationFormParent[]>(`${this.baseUrl}/RegisterationFormParent/GetByStateID/${state}`, { headers })
  }

  GetById(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegisterationFormParent>(`${this.baseUrl}/RegisterationFormParent/GetByID/${id}`, { headers })
  }

  Edit(id: number, registerationFormStateID: number, DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
    
    let body = {
      id: id,
      registerationFormStateID: registerationFormStateID
    }

    return this.http.put(`${this.baseUrl}/RegisterationFormParent`, body, { headers });
  }
}
