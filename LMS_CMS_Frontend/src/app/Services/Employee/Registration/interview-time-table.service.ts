import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { InterviewTimeTable } from '../../../Models/Registration/interview-time-table';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class InterviewTimeTableService {
  baseUrl = ""
  header = ""
  
  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName:string){
      if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<InterviewTimeTable[]>(`${this.baseUrl}/InterviewTimeTable`, { headers })
  }

  GetById(id:number, DomainName:string){
      if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<InterviewTimeTable>(`${this.baseUrl}/InterviewTimeTable/GetInterviewTableByID/${id}`, { headers })
  }

  Delete(id: number,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/InterviewTimeTable/${id}`, { headers })
  }

  Add(InterviewTimeTable: InterviewTimeTable,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post(`${this.baseUrl}/InterviewTimeTable`, InterviewTimeTable, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  Edit(InterviewTimeTable: InterviewTimeTable,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/InterviewTimeTable`, InterviewTimeTable, { headers });
  }
}
