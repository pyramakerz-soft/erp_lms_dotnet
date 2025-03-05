import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Cart } from '../../Models/Student/ECommerce/cart';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
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
      
    return this.http.get<Cart>(`${this.baseUrl}/Cart/ByStudentId/${id}`, { headers });
  }

  getByOrderID(id:number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
      
    return this.http.get<Cart>(`${this.baseUrl}/Cart/ByOrderId/${id}`, { headers });
  }
}
