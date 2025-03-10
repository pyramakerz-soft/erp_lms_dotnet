import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Domain } from '../../Models/domain';

@Injectable({
  providedIn: 'root'
})
export class DomainService {
  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrlOcta
  }

  Get(){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<Domain[]>(`${this.baseUrl}/Domain`, { headers })
  }

  GetDomainBuID(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<Domain>(`${this.baseUrl}/Domain/${id}`, { headers })
  }

  Add(domain:Domain){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
 
    return this.http.post(`${this.baseUrl}/Domain`, domain, { headers })
  }

  Edit(domain:Domain){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/Domain`, domain, { headers })
  }

  DeleteDomain(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/Domain/${id}`, { headers })
  }
}
