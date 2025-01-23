import { Injectable } from '@angular/core';
import { Income } from '../../../Models/Accounting/income';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class IncomeService {

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
      return this.http.get<Income[]>(`${this.baseUrl}/Income`, { headers })
    }
  
    Add(income: Income,DomainName:string): Observable<any> {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
  
      return this.http.post<any>(`${this.baseUrl}/Income`,income, {
        headers: headers,
        responseType: 'text' as 'json'
      });
    }
  
    Edit(income: Income,DomainName:string): Observable<Income> {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.put<Income>(`${this.baseUrl}/Income`, income, { headers });
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
      return this.http.delete(`${this.baseUrl}/Income/${id}`, { headers })
    }
  
 }
 