import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class DataAccordingToLinkFileService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }


  GetTableDataAccordingToLinkFile(DomainName: string, linkFileId:number) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<any[]>(`${this.baseUrl}/GetTableDataAccordingToLinkFile/GetTableDataAccordingToLinkFile/${linkFileId}`, { headers });
  }

  GetTableDataAccordingToLinkFileAndSubAccount(DomainName: string, linkFileId:number, subAccountId:number) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<any[]>(`${this.baseUrl}/GetTableDataAccordingToLinkFile/GetTableDataAccordingToLinkFileAndSubAccount/${linkFileId}/${subAccountId}`, { headers });
  }
}
