import { Injectable } from '@angular/core';
import { InstallmentDeductionDetail } from '../../../Models/Accounting/installment-deduction-detail';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class InstallmentDeductionDetailService {

   baseUrl = ""
   header = ""
 
   constructor(public http: HttpClient, public ApiServ: ApiService) {
     this.baseUrl = ApiServ.BaseUrl
   }
 
 
   Get(DomainName: string) {
     if (DomainName != null) {
       this.header = DomainName
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.get<InstallmentDeductionDetail[]>(`${this.baseUrl}/InstallmentDeductionDetails`, { headers })
   }

   GetByMasterId(id:number ,DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<InstallmentDeductionDetail[]>(`${this.baseUrl}/InstallmentDeductionDetails/GetByMaster/${id}`, { headers })
  }
 
   Add(detail: InstallmentDeductionDetail, DomainName: string): Observable<any> {
     if (DomainName != null) {
       this.header = DomainName
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
 
     return this.http.post<any>(`${this.baseUrl}/InstallmentDeductionDetails`, detail, {
       headers: headers,
       responseType: 'text' as 'json'
     });
   }
 
   Edit(detail: InstallmentDeductionDetail, DomainName: string): Observable<InstallmentDeductionDetail> {
     if (DomainName != null) {
       this.header = DomainName
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.put<InstallmentDeductionDetail>(`${this.baseUrl}/InstallmentDeductionDetails`, detail, { headers });
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
     return this.http.delete(`${this.baseUrl}/InstallmentDeductionDetails/${id}`, { headers })
   }
 
 }