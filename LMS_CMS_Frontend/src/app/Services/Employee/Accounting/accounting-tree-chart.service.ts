import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { AccountingTreeChart } from '../../../Models/Accounting/accounting-tree-chart';

@Injectable({
  providedIn: 'root'
})
export class AccountingTreeChartService {
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
    return this.http.get<AccountingTreeChart[]>(`${this.baseUrl}/AccountingTreeChart`, { headers })
  }
  
  GetByID(id: number,DomainName:string) {
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<AccountingTreeChart>(`${this.baseUrl}/AccountingTreeChart/${id}`, { headers })
  }
  
  GetByMainID(DomainName:string) {
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<AccountingTreeChart[]>(`${this.baseUrl}/AccountingTreeChart/GetByMainId`, { headers })
  }
  
  GetBySubAndFileLinkID(id: number,DomainName:string) {
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<AccountingTreeChart[]>(`${this.baseUrl}/AccountingTreeChart/GetBySubAndLinkFileId/${id}`, { headers })
  }
  
  Add( NewAccountingTreeChart: AccountingTreeChart,DomainName:string) {
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.post<any>(`${this.baseUrl}/AccountingTreeChart`, NewAccountingTreeChart, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  Edit(NewAccountingTreeChart: AccountingTreeChart,DomainName:string) {
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/AccountingTreeChart`, NewAccountingTreeChart, { headers });
  }
}
