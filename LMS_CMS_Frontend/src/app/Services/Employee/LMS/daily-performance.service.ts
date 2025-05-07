import { Injectable } from '@angular/core';
import { DailyPerformance } from '../../../Models/LMS/daily-performance';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class DailyPerformanceService {

 baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Add(StudentPerformance: DailyPerformance[],DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');

    return this.http.post(`${this.baseUrl}/DailyPerformance`, StudentPerformance, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

}
