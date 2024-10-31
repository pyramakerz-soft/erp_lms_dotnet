import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class ParentService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get_Parent_By_Id(Id:number){
    return this.http.get(`${this.baseUrl}/Parent/${Id}`)
  }

}
