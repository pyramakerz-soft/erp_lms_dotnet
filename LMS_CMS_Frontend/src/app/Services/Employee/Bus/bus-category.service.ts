import { HttpClient } from '@angular/common/http';
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

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get(){
    return this.http.get<BusType[]>(`${this.baseUrl}/BusCategory`)
  }

  Add(domainId: number, name: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/BusCategory`, { DomainId:domainId,Name: name},{ responseType: 'text' as 'json' });
  }


  Edit(NewType:BusType): Observable<BusType> {
    return this.http.put<BusType>(`${this.baseUrl}/BusCategory`, NewType);
  }

  Delete(id:number){
    return this.http.delete(`${this.baseUrl}/BusCategory?id=${id}`)
  }

  GetByID(id:number){
    return this.http.get(`${this.baseUrl}/BusCategory/${id}`)
  }
  GetByDomainId(id:number){
    return this.http.get<BusType[]>(`${this.baseUrl}/BusCategory/DomainId?id=${id}`)

  }
}
