import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { Bus } from '../../../Models/Bus/bus';

@Injectable({
  providedIn: 'root'
})
export class BusService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
    this.header = ApiServ.GetHeader();
  }

  Get(DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<Bus[]>(`${this.baseUrl}/Bus`,{ headers })
  }
  
  GetbyDomainName(domainName: string){
     if(domainName!=null) {
      this.header=domainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get(`${this.baseUrl}/Bus`,{ headers })
  }
  
  GetbyBusId(busId: number,DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<Bus>(`${this.baseUrl}/Bus/${busId}`,{ headers })
  }

  DeleteBus(busId:number,DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/Bus/${busId}`, {
      headers: headers,
      responseType: 'text' as 'json'
  });
  }

  Add(bus:Bus,DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.post<any>(`${this.baseUrl}/Bus`, bus,{
      headers: headers,
      responseType: 'text' as 'json'
  });
  }

  Edit(bus:Bus,DomainName:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/Bus`, bus,{ headers: headers, responseType: 'text' as 'json' });
  }
}
