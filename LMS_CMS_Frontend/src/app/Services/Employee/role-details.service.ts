import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class RoleDetailsService {
  baseUrl=""
  header=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
    this.header=ApiServ.GetHeader();
  }

  Get_Pages_With_RoleID(roleId: number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header) 
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');

    return this.http.get(`${this.baseUrl}/Role_Details/Get_With_RoleID_Group_By/${roleId}`, { headers });
  }
  Get_All_With_Group_By(){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header) 
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');

    return this.http.get(`${this.baseUrl}/Role_Details/Get_All_With_Group_By`, { headers });
  }
}
