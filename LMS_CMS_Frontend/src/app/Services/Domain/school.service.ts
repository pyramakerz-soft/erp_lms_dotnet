import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { SchoolAdd } from '../../Models/school-add';
import { School } from '../../Models/school';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  Get_All_Domain(){
    return this.http.get(`${this.baseUrl}/School`)
  }

  Get_School_By_Id(id:number){
    return this.http.get(`${this.baseUrl}/School/${id}`)
  } 

  Delete_School_By_Id(id:number){
    return this.http.delete(`${this.baseUrl}/School?id=${id}`)
  }  

  AddSchool(School:SchoolAdd){
    console.log(School)
    return this.http.post<School>(`${this.baseUrl}/School`,School)
  }
}