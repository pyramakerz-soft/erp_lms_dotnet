import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ParentAdd } from '../Models/parent-add';

@Injectable({
  providedIn: 'root'
})
export class ParentService {

  baseUrl=""
  header = ""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
    this.header = ApiServ.GetHeader()
  }

  GetByID(id:number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json')
    
    return this.http.get<any>(`${this.baseUrl}/Parent/${id}`, { headers })
  }

  AddParent(parent:ParentAdd,DomainName:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json')
    
    return this.http.post(`${this.baseUrl}/Parent`,parent, { headers })
  }

}
