import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { SchoolType } from '../../Models/Octa/school-type';

@Injectable({
  providedIn: 'root'
})
export class SchoolTypeService {
  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrlOcta
  }

  Get(){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<SchoolType[]>(`${this.baseUrl}/SchoolType`, { headers })
  }

  GetSchoolTypeBuID(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<SchoolType>(`${this.baseUrl}/schoolType/${id}`, { headers })
  }

  Add(schoolType:SchoolType){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');

    return this.http.post(`${this.baseUrl}/schoolType`, schoolType, { headers })
  }

  Edit(schoolType:SchoolType){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/schoolType`, schoolType, { headers })
  }

  DeleteSchoolType(id:number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/schoolType/${id}`, { headers })
  }
}
