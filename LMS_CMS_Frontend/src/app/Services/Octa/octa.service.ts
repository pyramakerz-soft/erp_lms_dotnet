import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class OctaService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrlOcta
  }

  GetByID(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json')
    
    return this.http.get(`${this.baseUrl}/Octa/${id}`, { headers })
  }

}