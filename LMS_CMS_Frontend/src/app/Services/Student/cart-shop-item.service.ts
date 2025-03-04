import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { CartShopItem } from '../../Models/Student/ECommerce/cart-shop-item';

@Injectable({
  providedIn: 'root'
})
export class CartShopItemService {
  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Add(CartShopItem:CartShopItem, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
      return this.http.post(`${this.baseUrl}/Cart_ShopItem`, CartShopItem, { headers });
  }
}
