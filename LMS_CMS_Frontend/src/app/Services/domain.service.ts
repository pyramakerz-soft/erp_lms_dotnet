import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Domain } from '../Models/domain';
import { DomainAdd } from '../Models/domain-add';

@Injectable({
  providedIn: 'root'
})
export class DomainService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get_All_Domain(){
    return this.http.get(`${this.baseUrl}/Domain`)
  }

  Get_Domain_By_Id(id:number){
    return this.http.get(`${this.baseUrl}/Domain/${id}`)
  } 

  Delete_Domain_By_Id(id:number){
    return this.http.delete(`${this.baseUrl}/Domain?id=${id}`)
  }  

 Update_Domain(domain:Domain){
    return this.http.put<Domain>(`${this.baseUrl}/Domain`,domain)
  }   

  AddDomain(domain:DomainAdd){
    console.log(domain)
    return this.http.post<Domain>(`${this.baseUrl}/Domain`,domain)
  }
}
