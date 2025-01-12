import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { RegistrationForm } from '../../../Models/Registration/registration-form';

@Injectable({
  providedIn: 'root'
})
export class RegistrationFormService {
  baseUrl = ""
  header = ""
  
  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
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
    return this.http.get<RegistrationForm>(`${this.baseUrl}/RegistrationForm/${id}`, { headers })
  }
}
