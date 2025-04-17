import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InventoryDetails } from '../../../Models/Inventory/InventoryDetails';
import { Store } from '../../../Models/Inventory/store';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class InventoryDetailsService {

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
      return this.http.get<InventoryDetails[]>(`${this.baseUrl}/InventoryDetails/BySaleId/${id}`, { headers });
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
    return this.http.get<InventoryDetails>(`${this.baseUrl}/InventoryDetails/${id}`, { headers })
  }

  Add(Detail: InventoryDetails[], DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

      const cleanedDetails = Detail.map(item => {
        const { id, ...rest } = item;
        return rest;
      });
      return this.http.post<any>(`${this.baseUrl}/InventoryDetails`, cleanedDetails, {
        headers: headers,
        responseType: 'text' as 'json'
      }); 
  }

  Edit(Detail: InventoryDetails[], DomainName: string): Observable<InventoryDetails[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
  
    return this.http.put<InventoryDetails[]>(`${this.baseUrl}/InventoryDetails`, Detail, { headers });
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
    return this.http.delete(`${this.baseUrl}/InventoryDetails/${id}`, { headers })
  }

}
