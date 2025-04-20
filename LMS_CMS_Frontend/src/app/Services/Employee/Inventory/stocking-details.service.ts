import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StockingDetails } from '../../../Models/Inventory/stocking-details';
import { Store } from '../../../Models/Inventory/store';
import { ApiService } from '../../api.service';
import { ShopItem } from '../../../Models/Inventory/shop-item';

@Injectable({
  providedIn: 'root'
})
export class StockingDetailsService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  GetBySalesId(id:number ,DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
      return this.http.get<StockingDetails[]>(`${this.baseUrl}/StockingDetails/ByStockingId/${id}`, { headers });
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
    return this.http.get<StockingDetails>(`${this.baseUrl}/StockingDetails/${id}`, { headers })
  }

  Add(Detail: StockingDetails[], DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post<any>(`${this.baseUrl}/StockingDetails`, Detail, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  Edit(Detail: StockingDetails[], DomainName: string): Observable<Store> {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put<Store>(`${this.baseUrl}/StockingDetails`, Detail, { headers });
  }

  Delete(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/StockingDetails/${id}`, { headers })
  }

  GetCurrentStock(StoreId: number,ShopItemId:number ,date :string , DomainName: string) {
    if (DomainName != null) {  
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<number>(`${this.baseUrl}/StockingDetails/${StoreId}/${ShopItemId}/${date}`, { headers })
  }

  GetCurrentStockForAllItems(StoreId: number,CategoryId:number ,date :string , DomainName: string) {
    if (DomainName != null) {  
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<ShopItem[]>(`${this.baseUrl}/StockingDetails/CurrentStockByCategoryId/${StoreId}/${CategoryId}/${date}`, { headers })
  }

  GetCurrentStockForAllItemsBySub(StoreId: number,SubCategoryId:number ,date :string , DomainName: string) {
    if (DomainName != null) {  
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<ShopItem[]>(`${this.baseUrl}/StockingDetails/CurrentStockBySubCategoryId/${StoreId}/${SubCategoryId}/${date}`, { headers })
  }

}
