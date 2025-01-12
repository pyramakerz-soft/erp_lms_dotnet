import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { RegistrationCategory } from '../../../Models/Registration/registration-category';

@Injectable({
  providedIn: 'root'
})
export class RegistrationCategoryService {

 baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<RegistrationCategory[]>(`${this.baseUrl}/Category`, { headers })
  }

   Add(category: RegistrationCategory,DomainName:string) {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
  
      return this.http.post(`${this.baseUrl}/Category`, category, {
        headers: headers,
        responseType: 'text' as 'json'
      });
    }
  
    Edit(category: RegistrationCategory,DomainName:string) {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.put(`${this.baseUrl}/Category`, category, { headers });
    }
  
    Delete(id: number,DomainName:string) {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.delete(`${this.baseUrl}/Category/${id}`, { headers })
    }
  
    GetByID(id: number,DomainName:string) {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.get<RegistrationCategory>(`${this.baseUrl}/Category/${id}`, { headers })
    }
}
