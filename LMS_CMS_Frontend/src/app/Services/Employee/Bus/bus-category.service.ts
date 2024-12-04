import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { Observable } from 'rxjs';
import { BusType } from '../../../Models/Bus/bus-type';
import { BusTypeAdd } from '../../../Models/Bus/bus-type-add';

@Injectable({
  providedIn: 'root'
})
export class BusCategoryService {
  baseUrl=""
  header=""
  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
    this.header=ApiServ.GetHeader();
  }

  Get() {
    const token = localStorage.getItem("current_token");
    const domainName = "Domain 2"; 
      const headers = new HttpHeaders()
      .set('domain-name', 'Domain 2') 
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
  
    return this.http.get<BusType[]>(`${this.baseUrl}/BusCategory`, { headers });
  }

  Add(domainId: number, name: string): Observable<any> {
    const token = localStorage.getItem("current_token");

    const headers = new HttpHeaders()
    .set('domain-name', 'Domain 2') 
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');

    const body = { DomainId: domainId, Name: name };

    return this.http.post<any>(`${this.baseUrl}/BusCategory`, body, {
        headers,
        responseType: 'text' as 'json' 
    });
}

  Edit(NewType:BusType): Observable<BusType> {
    const token = localStorage.getItem("current_token");

    const headers = new HttpHeaders()
    .set('domain-name', 'Domain 2') 
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');

    return this.http.put<BusType>(`${this.baseUrl}/BusCategory`, NewType , { headers });
  }

  Delete(id:number){
    const token = localStorage.getItem("current_token");

   const domainName = "Domain 2"; 
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('DomainName', domainName); 

    return this.http.delete(`${this.baseUrl}/BusCategory?id=${id}` , { headers })
  }

  GetByID(id:number){
    const token = localStorage.getItem("current_token");

    const domainName = "Domain 2"; 
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('DomainName', domainName); 

    return this.http.get(`${this.baseUrl}/BusCategory/${id}`, { headers })
  }
  GetByDomainName(id:number){
    
    const token = localStorage.getItem("current_token");

    const domainName = "Domain 2"; 
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('DomainName', domainName); 

    return this.http.get<BusType[]>(`${this.baseUrl}/BusCategory/DomainId?id=${id}`, { headers })

  }
}
