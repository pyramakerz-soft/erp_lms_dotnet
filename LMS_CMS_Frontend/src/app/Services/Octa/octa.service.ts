import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Account } from '../../Models/Octa/account';

@Injectable({
  providedIn: 'root'
})
export class OctaService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrlOcta
  }

  Get(){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json')
    
    return this.http.get<Account[]>(`${this.baseUrl}/Octa`, { headers })
  }

  GetByID(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json')
    
    return this.http.get<Account>(`${this.baseUrl}/Octa/${id}`, { headers })
  }

  Add(account:Account){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json')

    const body = {
      "user_Name": account.user_Name,
      "arabic_Name": account.arabic_Name,
      "password": account.password
    }
    
    return this.http.post(`${this.baseUrl}/Octa`, body, { headers })
  }

  Edit(account:Account){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json')
    
    return this.http.put(`${this.baseUrl}/Octa`, account, { headers })
  }
  
  Delete(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json')
    
    return this.http.delete(`${this.baseUrl}/Octa/${id}`, { headers })
  }

}