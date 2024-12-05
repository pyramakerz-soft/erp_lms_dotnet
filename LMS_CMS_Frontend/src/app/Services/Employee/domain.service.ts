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

}