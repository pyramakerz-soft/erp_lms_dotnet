import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class RoleDetailsService {
  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get_Pages_With_RoleID(roleId: number){
    console.log(`${this.baseUrl}/Role_Details/Get_With_RoleID_Group_By/${roleId}`)
    return this.http.get(`${this.baseUrl}/Role_Details/Get_With_RoleID_Group_By/${roleId}`)
  }
  Get_All_With_Group_By(){
    return this.http.get(`${this.baseUrl}/Role_Details/Get_All_With_Group_By`)
  }
}
