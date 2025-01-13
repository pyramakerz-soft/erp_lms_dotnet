import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { RegisterationFormTestAnswer } from '../../../Models/Registration/registeration-form-test-answer';

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
   
}
