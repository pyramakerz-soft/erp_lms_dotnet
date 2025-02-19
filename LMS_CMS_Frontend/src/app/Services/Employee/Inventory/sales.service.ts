import { Injectable } from '@angular/core';
import { Sales } from '../../../Models/Inventory/sales';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Store } from '../../../Models/Inventory/store';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName: string, pageNumber:number, pageSize:number) {
      if (DomainName != null) {
        this.header = DomainName
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
        return this.http.get<{ data: Sales[], pagination: any }>(`${this.baseUrl}/Sales?pageNumber=${pageNumber}&pageSize=${pageSize}`, { headers });
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
    return this.http.get<Sales>(`${this.baseUrl}/Sales/${id}`, { headers })
  }

  Add(sales: Sales, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post<any>(`${this.baseUrl}/Sales`, sales, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  Edit(sales: Sales, DomainName: string): Observable<Store> {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put<Store>(`${this.baseUrl}/Sales`, sales, { headers });
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
    return this.http.delete(`${this.baseUrl}/Sales/${id}`, { headers })
  }

}
