import { Injectable } from '@angular/core';
import { Receivable } from '../../../Models/Accounting/receivable';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class ReceivableService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }


  Get(DomainName: string, pageNumber:number, pageSize:number) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<{ data: Receivable[], pagination: any }>(`${this.baseUrl}/ReceivableMaster?pageNumber=${pageNumber}&pageSize=${pageSize}`, { headers });
  }

  Add(Receivable: Receivable, DomainName: string){
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post<any>(`${this.baseUrl}/ReceivableMaster`, Receivable, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  // Edit(save: Saves, DomainName: string): Observable<Saves> {
  //   if (DomainName != null) {
  //     this.header = DomainName
  //   }
  //   const token = localStorage.getItem("current_token");
  //   const headers = new HttpHeaders()
  //     .set('domain-name', this.header)
  //     .set('Authorization', `Bearer ${token}`)
  //     .set('Content-Type', 'application/json');
  //   return this.http.put<Saves>(`${this.baseUrl}/Save`, save, { headers });
  // }

  Delete(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/ReceivableMaster/${id}`, { headers })
  }

}
