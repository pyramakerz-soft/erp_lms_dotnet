import { Injectable } from '@angular/core';
import { OrderState } from '../../../Models/E-Commerce/order-state';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class OrderStateService {
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
    return this.http.get<OrderState[]>(`${this.baseUrl}/OrderState`, { headers });
  }
}
