import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { BusType } from '../../../Models/Bus/bus-type';
import { Observable } from 'rxjs';
import { BusTypeAdd } from '../../../Models/Bus/bus-type-add';

@Injectable({
  providedIn: 'root'
})
export class BusStatusService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
    this.header = ApiServ.GetHeader();
  }

  Get() {
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<BusType[]>(`${this.baseUrl}/BusStatus`, { headers })
  }

  Add(domainId: number, name: string): Observable<any> {
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    const body = { DomainId: domainId, Name: name };
    return this.http.post<any>(`${this.baseUrl}/BusStatus`, body, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  Edit(NewType: BusType): Observable<BusType> {
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put<BusType>(`${this.baseUrl}/BusStatus`, NewType, { headers });
  }

  Delete(id: number) {
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/BusStatus?id=${id}`, { headers })
  }

  GetByID(id: number) {
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get(`${this.baseUrl}/BusStatus/${id}`, { headers })
  }

  GetByDomainId(id: number) {
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<BusType[]>(`${this.baseUrl}/BusStatus/DomainId?id=${id}`, { headers })

  }
}
