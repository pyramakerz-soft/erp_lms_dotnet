import { Injectable } from '@angular/core';
import { Credit } from '../../../Models/Accounting/credit';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class CreditService {

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
     return this.http.get<Credit[]>(`${this.baseUrl}/Credit`, { headers })
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
    return this.http.get<Credit>(`${this.baseUrl}/Credit/${id}`, { headers })
  }

   Add(credit: Credit,DomainName:string): Observable<any> {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
 
     return this.http.post<any>(`${this.baseUrl}/Credit`,credit, {
       headers: headers,
       responseType: 'text' as 'json'
     });
   }
 
   Edit(credit: Credit,DomainName:string): Observable<Credit> {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.put<Credit>(`${this.baseUrl}/Credit`, credit, { headers });
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
     return this.http.delete(`${this.baseUrl}/Credit/${id}`, { headers })
   }
 
}
