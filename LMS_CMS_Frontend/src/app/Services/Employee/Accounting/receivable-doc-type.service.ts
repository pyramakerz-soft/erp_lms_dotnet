import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReceivableDocType } from '../../../Models/Accounting/receivable-doc-type';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class ReceivableDocTypeService {
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
    return this.http.get<ReceivableDocType[]>(`${this.baseUrl}/ReceivableDocType`, { headers });
  }
}
