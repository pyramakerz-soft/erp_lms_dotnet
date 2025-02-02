import { Injectable } from '@angular/core';
import { empty, Observable } from 'rxjs';
import { EmplyeeStudent } from '../../../Models/Accounting/emplyee-student';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeStudentService {
 baseUrl = ""
   header = ""
 
   constructor(public http: HttpClient, public ApiServ: ApiService) {
     this.baseUrl = ApiServ.BaseUrl
   }
 
 
   Get(EmpId:number ,DomainName:string) {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.get<EmplyeeStudent[]>(`${this.baseUrl}/EmployeeStudent/ByEmployeeId/${EmpId}`, { headers })
   }
 
   Add(newChild: EmplyeeStudent,DomainName:string): Observable<any> {
     if(DomainName!=null) {
       this.header=DomainName 
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
 
     return this.http.post<any>(`${this.baseUrl}/EmployeeStudent`,newChild, {
       headers: headers,
       responseType: 'text' as 'json'
     });
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
    return this.http.delete(`${this.baseUrl}/EmployeeStudent/${id}`, { headers })
  }
  }