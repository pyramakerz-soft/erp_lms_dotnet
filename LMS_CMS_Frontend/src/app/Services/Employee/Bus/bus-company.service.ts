import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { BusType } from '../../../Models/Bus/bus-type';
import { Observable } from 'rxjs';
import { BusTypeAdd } from '../../../Models/Bus/bus-type-add';

@Injectable({
  providedIn: 'root'
})
export class BusCompanyService {
  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get(){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<BusType[]>(`${this.baseUrl}/BusCompany`, { headers })
  }

  Add(domainId: number, name: string): Observable<any> {
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const body = { DomainId: domainId, Name: name };

    return this.http.post<any>(`${this.baseUrl}/BusCompany`, body, {
        headers,
        responseType: 'text' as 'json'
    });
}


  Edit(NewType:BusType): Observable<BusType> {
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.put<BusType>(`${this.baseUrl}/BusCompany`, NewType , { headers });
  }

  Delete(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.baseUrl}/BusCompany?id=${id}`, { headers })
  }

  GetByID(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/BusCompany/${id}`, { headers })
  }
  GetByDomainId(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<BusType[]>(`${this.baseUrl}/BusCompany/DomainId?id=${id}`, { headers })

  }
}
