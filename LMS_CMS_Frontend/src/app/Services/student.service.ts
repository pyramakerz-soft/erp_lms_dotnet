import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Student } from '../Models/student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  baseUrl=""
  header = ""
  
  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
    this.header = ApiServ.GetHeader()
  }

  GetAll(DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Student[]>(`${this.baseUrl}/Student`, { headers })
  }

  GetByID(id:number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get(`${this.baseUrl}/Student/${id}`, { headers })
  }

  GetByClassID(id:number,DomainName:string){
    this.header=DomainName 
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Student[]>(`${this.baseUrl}/Student/Get_By_ClassID/${id}`, { headers })
  }

  EditAccountingEmployee(student:Student,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header) 
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/Student/StudentAccounting`, student, { headers });
  }
    
  GetByNationalID(NationalID:string,DomainName:string){
    this.header=DomainName 
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Student>(`${this.baseUrl}/Student/SearchByNationality/${NationalID}`, { headers })
  }
  
  GetBySchoolGradeClassID(schoolId: number, gradeId: number, classId: number, DomainName: string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    let params = new HttpParams();
    params = params.set('schoolId', schoolId.toString()); 
    params = params.set('gradeId', gradeId.toString());
    params = params.set('classId', classId.toString());

    return this.http.get<any>(`${this.baseUrl}/Student/GetBySchoolGradeClassID`, { headers, params });
  }
}