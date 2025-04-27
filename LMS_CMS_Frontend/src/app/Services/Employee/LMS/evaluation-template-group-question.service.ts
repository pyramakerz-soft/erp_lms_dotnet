import { Injectable } from '@angular/core';
import { EvaluationTemplateGroupQuestion } from '../../../Models/LMS/evaluation-template-group-question';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root',
})
export class EvaluationTemplateGroupQuestionService {
  
  baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl;
  }

  Get(DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<EvaluationTemplateGroupQuestion[]>(
      `${this.baseUrl}/EvaluationTemplateGroupQuestion`,
      { headers }
    );
  }

  Add(
    EvaluationTemplateGroupQuestion: EvaluationTemplateGroupQuestion,
    DomainName: string
  ) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post(
      `${this.baseUrl}/EvaluationTemplateGroupQuestion`,
      EvaluationTemplateGroupQuestion,
      {
        headers: headers,
        responseType: 'text' as 'json',
      }
    );
  }

  Edit(
    EvaluationTemplateGroupQuestion: EvaluationTemplateGroupQuestion,
    DomainName: string
  ) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put(
      `${this.baseUrl}/EvaluationTemplateGroupQuestion`,
      EvaluationTemplateGroupQuestion,
      { headers }
    );
  }

  Delete(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.delete(
      `${this.baseUrl}/EvaluationTemplateGroupQuestion/${id}`,
      { headers }
    );
  }

  GetByID(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<EvaluationTemplateGroupQuestion>(
      `${this.baseUrl}/EvaluationTemplateGroupQuestion/${id}`,
      { headers }
    );
  }
}
