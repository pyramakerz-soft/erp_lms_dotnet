import { Injectable } from '@angular/core';
import { RegistrationFormInterview } from '../../../Models/Registration/registration-form-interview';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class RegistrationFormInterviewService {
  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  GetRegistrationFormInterviewByInterviewID(id:number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegistrationFormInterview[]>(`${this.baseUrl}/RegistrationFormInterview/GetRegistrationFormInterviewByInterviewID/${id}`, { headers })
  }

  GetRegistrationFormInterviewByID(id:number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegistrationFormInterview>(`${this.baseUrl}/RegistrationFormInterview/GetRegistrationFormInterviewByID/${id}`, { headers })
  }

  Edit(id:number, InterviewStateID:number ,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    let body = {
      id: id, 
      interviewStateID: InterviewStateID
    }
    
    return this.http.put(`${this.baseUrl}/RegistrationFormInterview`, body, { headers });
  }

  Add(RegisterationFormParentID:number, InterviewTimeID:number ,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    let body = {
      registerationFormParentID: RegisterationFormParentID, 
      interviewTimeID: InterviewTimeID
    }
    
    return this.http.post(`${this.baseUrl}/RegistrationFormInterview`, body, { headers });
  }
}
