import { Injectable } from '@angular/core';
import { Asset } from '../../../Models/Accounting/asset';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class AssetService {

   baseUrl = ""
    header = ""
  
    constructor(public http: HttpClient, public ApiServ: ApiService) {
      this.baseUrl = ApiServ.BaseUrl
    }
  
  
    Get(DomainName:string) {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.get<Asset[]>(`${this.baseUrl}/Asset`, { headers })
    }

    GetById(id:number ,DomainName:string) {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.get<Asset>(`${this.baseUrl}/Asset/${id}`, { headers })
    }
  
    Add(asset: Asset,DomainName:string): Observable<any> {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
  
      return this.http.post<any>(`${this.baseUrl}/Asset`,asset, {
        headers: headers,
        responseType: 'text' as 'json'
      });
    }
  
    Edit(asset: Asset,DomainName:string): Observable<Asset> {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.put<Asset>(`${this.baseUrl}/Asset`, asset, { headers });
    }
  
    Delete(id: number,DomainName:string) {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.delete(`${this.baseUrl}/Asset/${id}`, { headers })
    }
  
 }
 