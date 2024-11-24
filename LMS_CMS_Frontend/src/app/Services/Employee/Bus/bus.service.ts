import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { Bus } from '../../../Models/Bus/bus';

@Injectable({
  providedIn: 'root'
})
export class BusService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get(){
    return this.http.get(`${this.baseUrl}/Bus`)
  }
  
  GetbyDomainId(domainId: number){
    return this.http.get(`${this.baseUrl}/Bus/GetByDomainID/${domainId}`)
  }
  
  GetbyBusId(busId: number){
    return this.http.get<Bus>(`${this.baseUrl}/Bus/${busId}`)
  }

  DeleteBus(busId:number){
    return this.http.delete(`${this.baseUrl}/Bus/${busId}`, { responseType: 'text' })
  }

  Add(bus:Bus){
    return this.http.post<any>(`${this.baseUrl}/Bus`, bus,{ responseType: 'text' as 'json' });
  }

  Edit(bus:Bus){
    return this.http.put(`${this.baseUrl}/Bus`, bus,{ responseType: 'text' as 'json' });
  }
}
