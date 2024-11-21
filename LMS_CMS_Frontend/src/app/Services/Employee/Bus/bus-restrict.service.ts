import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { BusType } from '../../../Models/Bus/bus-type';
import { Observable } from 'rxjs';
import { BusTypeAdd } from '../../../Models/Bus/bus-type-add';

@Injectable({
  providedIn: 'root'
})
export class BusRestrictService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get(){
    return this.http.get<BusType[]>(`${this.baseUrl}/BusRestrict`)
  }

  Add(domainId: number, name: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/BusRestrict`, { DomainId:domainId,Name: name},{ responseType: 'text' as 'json' });
  }


  Edit(NewType:BusTypeAdd): Observable<BusTypeAdd> {
    return this.http.put<BusTypeAdd>(`${this.baseUrl}/BusRestrict`, NewType);
  }

  Delete(id:number){
    return this.http.delete(`${this.baseUrl}/BusRestrict/${id}`)
  }

  GetByID(id:number){
    return this.http.get(`${this.baseUrl}/BusRestrict/${id}`)
  }
}
