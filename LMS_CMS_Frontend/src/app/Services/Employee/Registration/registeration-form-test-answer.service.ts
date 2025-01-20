import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { RegisterationFormTestAnswer } from '../../../Models/Registration/registeration-form-test-answer';
import { Answer } from '../../../Models/Registration/answer';

@Injectable({
  providedIn: 'root'
})
export class RegisterationFormTestAnswerService {

 
   baseUrl = ""
   header = ""
 
   constructor(public http: HttpClient, public ApiServ: ApiService) {
     this.baseUrl = ApiServ.BaseUrl
   }
 
   GetByRegistrationParentId(RegisterParentID: number,testId: number, DomainName: string) {
     if (DomainName != null) {
       this.header = DomainName
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.get<RegisterationFormTestAnswer[]>(`${this.baseUrl}/RegistrationFormTestAnswer/${RegisterParentID}?testId=${testId}`, { headers })
   }

    Add(answers: Answer[],RegisterationFormParentId:number,TestId:number,DomainName:string) {
       if(DomainName!=null) {
         this.header=DomainName 
       }
       const token = localStorage.getItem("current_token");
       const headers = new HttpHeaders()
         .set('domain-name', this.header)
         .set('Authorization', `Bearer ${token}`)
         .set('Content-Type', 'application/json');
       return this.http.post(`${this.baseUrl}/RegistrationFormTestAnswer/${RegisterationFormParentId}/${TestId}`, answers, {
         headers: headers,
         responseType: 'text' as 'json'
       });
     }
   
}
