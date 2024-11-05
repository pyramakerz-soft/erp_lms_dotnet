import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { SchoolAdd } from '../../Models/school-add';
import { RoleAdd } from '../../Models/role-add';

@Injectable({
  providedIn: 'root'
})
export class SchoolRoleService {
  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get_All_Domain(){
    return this.http.get(`${this.baseUrl}/SchoolRole`)
  }

  Get_SchoolRole_By_Id(id:number){
    return this.http.get(`${this.baseUrl}/SchoolRole/${id}`)
  } 

  Delete_SchoolRole_By_Id(id:number){
    return this.http.delete(`${this.baseUrl}/SchoolRole?id=${id}`)
  }  

  AddSchoolRole(SchoolRole:RoleAdd){
    return this.http.post<RoleAdd>(`${this.baseUrl}/SchoolRole`,SchoolRole)
  }
}