import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';

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
}
