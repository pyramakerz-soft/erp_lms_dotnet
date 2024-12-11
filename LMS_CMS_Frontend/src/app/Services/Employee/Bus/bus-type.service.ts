import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { BusType } from '../../../Models/Bus/bus-type';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BusTypeService {

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
    return this.http.get<BusType[]>(`${this.baseUrl}/BusType`, { headers })
  }

  Add(busType: BusType,DomainName:string): Observable<any> {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post<any>(`${this.baseUrl}/BusType`, busType, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  Edit(NewType: BusType,DomainName:string): Observable<BusType> {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put<BusType>(`${this.baseUrl}/BusType`, NewType, { headers });
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
    return this.http.delete(`${this.baseUrl}/BusType?id=${id}`, { headers })
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
    return this.http.get(`${this.baseUrl}/BusType/${id}`, { headers })
  }
  // GetByDomainName(DomainName:string){
  //   const token = localStorage.getItem("current_token");
  //   const headers = new HttpHeaders()
  //   .set('domain-name', DomainName)
  //   .set('Authorization', `Bearer ${token}`)
  //   .set('Content-Type', 'application/json');
  //   return this.http.get<BusType[]>(`${this.baseUrl}/BusType`, { headers })
  // }
}
