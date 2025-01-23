import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../api.service';
import { Debit } from '../../../Models/Accounting/debit';

@Injectable({
  providedIn: 'root'
})
export class DebitService {

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
     return this.http.get<Debit[]>(`${this.baseUrl}/Debit`, { headers })
   }
 
   Add(debit: Debit,DomainName:string): Observable<any> {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
 
     return this.http.post<any>(`${this.baseUrl}/Debit`,debit, {
       headers: headers,
       responseType: 'text' as 'json'
     });
   }
 
   Edit(debit: Debit,DomainName:string): Observable<Debit> {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.put<Debit>(`${this.baseUrl}/Debit`, debit, { headers });
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
     return this.http.delete(`${this.baseUrl}/Debit/${id}`, { headers })
   }
 
}
