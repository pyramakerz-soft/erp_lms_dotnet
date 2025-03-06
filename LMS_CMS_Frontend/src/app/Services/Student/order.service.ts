import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../../Models/Student/ECommerce/order';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  getByID(id:number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    
    return this.http.get<Order>(`${this.baseUrl}/Order/${id}`, { headers });
  }

  getByStudentID(id:number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    
    return this.http.get<Order[]>(`${this.baseUrl}/Order/ByStudentId/${id}`, { headers });
  }

  cancelOrder(id:number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    
    return this.http.delete(`${this.baseUrl}/Order/CancelOrder/${id}`, { headers });
  }

  ConfirmOrder(id:number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    
    return this.http.get(`${this.baseUrl}/Order/ConfirmCart/${id}`, { headers });
  }
}
