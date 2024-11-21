import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Domain } from '../../Models/domain';

@Injectable({
  providedIn: 'root'
})
export class DomainService {
  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get(){
    return this.http.get<Domain[]>(`${this.baseUrl}/Domain`)
  }

  
}