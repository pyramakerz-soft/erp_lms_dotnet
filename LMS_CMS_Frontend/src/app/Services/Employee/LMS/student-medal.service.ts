import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StudentMedal } from '../../../Models/LMS/student-medal';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class StudentMedalService {
 baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Add(StudentMedal: StudentMedal,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post(`${this.baseUrl}/StudentMedal`, StudentMedal, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  GetByStudentID(id: number,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<StudentMedal[]>(`${this.baseUrl}/StudentMedal/${id}`, { headers })
  }
}
