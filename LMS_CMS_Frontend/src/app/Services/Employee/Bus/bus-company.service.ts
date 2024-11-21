import { HttpClient } from '@angular/common/http';
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
    return this.http.get<BusType[]>(`${this.baseUrl}/BusCompany`)
  }

  Add(domainId: number, name: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/BusCompany`, { DomainId:domainId,Name: name},{ responseType: 'text' as 'json' });
  }


  Edit(NewType:BusType): Observable<BusType> {
    return this.http.put<BusType>(`${this.baseUrl}/BusCompany`, NewType);
  }

  Delete(id:number){
    return this.http.delete(`${this.baseUrl}/BusCompany?id=${id}`)
  }

  GetByID(id:number){
    return this.http.get(`${this.baseUrl}/BusCompany/${id}`)
  }
  GetByDomainId(id:number){
    return this.http.get<BusType[]>(`${this.baseUrl}/BusCompany/DomainId?id=${id}`)

  }
}
