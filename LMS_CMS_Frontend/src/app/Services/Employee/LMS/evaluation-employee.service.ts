import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { EvaluationEmployeeAdd } from '../../../Models/LMS/evaluation-employee-add';
import { EvaluationEmployee } from '../../../Models/LMS/evaluation-employee';

@Injectable({
  providedIn: 'root'
})
export class EvaluationEmployeeService {
  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Add(evaluationEmployee:EvaluationEmployeeAdd ,DomainName:string) { 
    if(DomainName!=null) {
      this.header=DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post(`${this.baseUrl}/EvaluationEmployee`, evaluationEmployee, { headers })
  }

  GetEvaluatorEvaluations(id:number ,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.get<EvaluationEmployeeAdd[]>(`${this.baseUrl}/EvaluationEmployee/GetEvaluatorEvaluations/${id}`, { headers })
  }

  GetEvaluatedEvaluations(id:number ,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.get<EvaluationEmployeeAdd[]>(`${this.baseUrl}/EvaluationEmployee/GetEvaluatedEvaluations/${id}`, { headers })
  }

  GetEvaluations(id:number ,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.get<EvaluationEmployee>(`${this.baseUrl}/EvaluationEmployee/GetEvaluation/${id}`, { headers })
  }

  EditFeedback(obj: any, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put<EvaluationEmployee>(
      `${this.baseUrl}/EvaluationEmployee/AddFeedback`,obj,  { headers }
    );
  }
  
}
