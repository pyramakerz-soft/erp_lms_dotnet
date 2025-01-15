import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { Question } from '../../../Models/Registration/question';
import { QuestionAddEdit } from '../../../Models/Registration/question-add-edit';
import { TestWithQuestion } from '../../../Models/Registration/test-with-question';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

 baseUrl = ""
   header = ""
 
   constructor(public http: HttpClient, public ApiServ: ApiService) {
     this.baseUrl = ApiServ.BaseUrl
   }
 
  Add(question: QuestionAddEdit,DomainName:string) {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.post(`${this.baseUrl}/Question`, question, {
       headers: headers,
       responseType: 'text' as 'json'
     });
   }
 
   Edit(question: QuestionAddEdit,DomainName:string) {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.put(`${this.baseUrl}/Question`, question, { headers });
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
     return this.http.delete(`${this.baseUrl}/Question/${id}`, { headers })
   }
 
   Get(DomainName:string) {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.get<Question[]>(`${this.baseUrl}/Question`, { headers })
   }
   GetByTestID(id:number,DomainName:string) {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.get<Question[]>(`${this.baseUrl}/Question/ByTest/${id}`, { headers })
   }

   GetByTestIDGroupBy(id:number,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<TestWithQuestion[]>(`${this.baseUrl}/Question/ByTestGroupBy/${id}`, { headers })
  }

  }