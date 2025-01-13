import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { RegisterationFormParent } from '../../../Models/Registration/registeration-form-parent';

@Injectable({
  providedIn: 'root'
})
export class RegisterationFormParentService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  GetByParentId(parent: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<RegisterationFormParent[]>(`${this.baseUrl}/RegisterationFormParent/GetByParentID/${parent}`, { headers })
  }
}
