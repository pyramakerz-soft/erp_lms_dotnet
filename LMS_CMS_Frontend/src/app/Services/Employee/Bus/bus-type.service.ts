import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { BusType } from '../../../Models/Bus/bus-type';
import { Observable } from 'rxjs';
import { BusTypeAdd } from '../../../Models/Bus/bus-type-add';

@Injectable({
  providedIn: 'root'
})
export class BusTypeService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get(){
    return this.http.get<BusType[]>(`${this.baseUrl}/BusType`)
  }

  Add(domainId: number, name: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/BusType`, { DomainId:domainId,Name: name},{ responseType: 'text' as 'json' });
  }


  Edit(NewType:BusTypeAdd): Observable<BusTypeAdd> {
    return this.http.put<BusTypeAdd>(`${this.baseUrl}/BusType`, NewType);
  }

  Delete(id:number){
    return this.http.delete(`${this.baseUrl}/BusType/${id}`)
  }

  GetByID(id:number){
    return this.http.get(`${this.baseUrl}/BusType/${id}`)
  }
  GetByDomainId(id:number){
    return this.http.get<BusType[]>(`${this.baseUrl}/BusType/DomainId?id=${id}`)

  }
}
