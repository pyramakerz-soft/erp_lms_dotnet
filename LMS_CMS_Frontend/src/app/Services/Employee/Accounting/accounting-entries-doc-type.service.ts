import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountingEntriesDocType } from '../../../Models/Accounting/accounting-entries-doc-type';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class AccountingEntriesDocTypeService {

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
    return this.http.get<AccountingEntriesDocType[]>(`${this.baseUrl}/AccountingEntriesDocTypes`, { headers })
  }

  GetById(id:number ,DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<AccountingEntriesDocType>(`${this.baseUrl}/AccountingEntriesDocTypes/${id}`, { headers })
  }

  Add(Type: AccountingEntriesDocType, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post<any>(`${this.baseUrl}/AccountingEntriesDocTypes`, Type, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  Edit(Type: AccountingEntriesDocType, DomainName: string): Observable<AccountingEntriesDocType> {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put<AccountingEntriesDocType>(`${this.baseUrl}/AccountingEntriesDocTypes`, Type, { headers });
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
    return this.http.delete(`${this.baseUrl}/AccountingEntriesDocTypes/${id}`, { headers })
  }

}
