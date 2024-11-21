import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { BusType } from '../../../Models/Bus/bus-type';
import { Observable } from 'rxjs';
import { BusTypeAdd } from '../../../Models/Bus/bus-type-add';

@Injectable({
  providedIn: 'root'
})
export class BusStatusService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get(){
    return this.http.get<BusType[]>(`${this.baseUrl}/BusStatus`)
  }

  Add(domainId: number, name: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/BusStatus`, { DomainId:domainId,Name: name},{ responseType: 'text' as 'json' });
  }


  Edit(NewType:BusTypeAdd): Observable<BusTypeAdd> {
    return this.http.put<BusTypeAdd>(`${this.baseUrl}/BusStatus`, NewType);
  }

  Delete(id:number){
    return this.http.delete(`${this.baseUrl}/BusStatus/${id}`)
  }

  GetByID(id:number){
    return this.http.get(`${this.baseUrl}/BusStatus/${id}`)
  }
}
