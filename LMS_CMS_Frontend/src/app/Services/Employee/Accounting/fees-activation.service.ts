import { Injectable } from '@angular/core';
import { FeesActivation } from '../../../Models/Accounting/fees-activation';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiService } from '../../api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeesActivationService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<FeesActivation[]>(`${this.baseUrl}/FeesActivation`, { headers })
  }

   Add(fees: FeesActivation, DomainName: string): Observable<any> {
      if (DomainName != null) {
        this.header = DomainName
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
  
      return this.http.post<any>(`${this.baseUrl}/FeesActivation`, fees, {
        headers: headers,
        responseType: 'text' as 'json'
      });
    }
  
    Edit(fees: FeesActivation, DomainName: string): Observable<FeesActivation> {
      if (DomainName != null) {
        this.header = DomainName
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.put<FeesActivation>(`${this.baseUrl}/FeesActivation`, fees, { headers });
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
      return this.http.delete(`${this.baseUrl}/FeesActivation/${id}`, { headers })
    }
  
}
