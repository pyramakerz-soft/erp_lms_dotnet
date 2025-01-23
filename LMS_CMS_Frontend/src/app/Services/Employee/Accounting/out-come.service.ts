import { Injectable } from '@angular/core';
import { Outcome } from '../../../Models/Accounting/outcome';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class OutComeService {

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
      return this.http.get<Outcome[]>(`${this.baseUrl}/Outcome`, { headers })
    }
  
    Add(outCome: Outcome,DomainName:string): Observable<any> {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
  
      return this.http.post<any>(`${this.baseUrl}/Outcome`,outCome, {
        headers: headers,
        responseType: 'text' as 'json'
      });
    }
  
    Edit(outCome: Outcome,DomainName:string): Observable<Outcome> {
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.put<Outcome>(`${this.baseUrl}/Outcome`, outCome, { headers });
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
      return this.http.delete(`${this.baseUrl}/Outcome/${id}`, { headers })
    }
  
 }
 