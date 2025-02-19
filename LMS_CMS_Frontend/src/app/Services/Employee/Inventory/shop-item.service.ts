import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { ShopItem } from '../../../Models/Inventory/shop-item';

@Injectable({
  providedIn: 'root'
})
export class ShopItemService {
  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName: string) {
      if (DomainName != null) {
        this.header = DomainName
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
        return this.http.get<ShopItem[]>(`${this.baseUrl}/ShopItem`, { headers });
    }

  GetById(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<ShopItem>(`${this.baseUrl}/ShopItem/${id}`, { headers })
  }

  // Add(sales: Sales, DomainName: string) {
  //   if (DomainName != null) {
  //     this.header = DomainName
  //   }
  //   const token = localStorage.getItem("current_token");
  //   const headers = new HttpHeaders()
  //     .set('domain-name', this.header)
  //     .set('Authorization', `Bearer ${token}`)
  //     .set('Content-Type', 'application/json');

  //   return this.http.post<any>(`${this.baseUrl}/Sales`, sales, {
  //     headers: headers,
  //     responseType: 'text' as 'json'
  //   });
  // }

  // Edit(sales: Sales, DomainName: string): Observable<Store> {
  //   if (DomainName != null) {
  //     this.header = DomainName
  //   }
  //   const token = localStorage.getItem("current_token");
  //   const headers = new HttpHeaders()
  //     .set('domain-name', this.header)
  //     .set('Authorization', `Bearer ${token}`)
  //     .set('Content-Type', 'application/json');
  //   return this.http.put<Store>(`${this.baseUrl}/Sales`, sales, { headers });
  // }

  Delete(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/ShopItem/${id}`, { headers })
  }
}
