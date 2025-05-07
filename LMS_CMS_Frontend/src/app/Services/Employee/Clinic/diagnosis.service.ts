import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../api.service';
import { Diagnosis } from '../../../Models/Clinic/diagnosis';

@Injectable({
  providedIn: 'root',
})
export class DiagnosisService {
  baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl; 
  }

  
  Get(DomainName: string): Observable<Diagnosis[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header) 
      .set('Authorization', `Bearer ${token}`) 
      .set('accept', '*/*'); 
    return this.http.get<Diagnosis[]>(`${this.baseUrl}/Diagnosis`, { headers });
  }

  
  Add(diagnosis: Diagnosis, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*')
      .set('Content-Type', 'application/json');
    return this.http.post<any>(`${this.baseUrl}/Diagnosis`, diagnosis, {
      headers: headers,
      responseType: 'text' as 'json',
    });
  }

  
  Edit(diagnosis: Diagnosis, DomainName: string): Observable<Diagnosis> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*')
      .set('Content-Type', 'application/json');
    return this.http.put<Diagnosis>(`${this.baseUrl}/Diagnosis`, diagnosis, { headers });
  }

  Delete(id: number, DomainName: string): Observable<any> {
  if (DomainName != null) {
    this.header = DomainName;
  }
  const token = localStorage.getItem('current_token');
  const headers = new HttpHeaders()
    .set('Domain-Name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('accept', '*/*')
    .set('Content-Type', 'application/json');

  
  return this.http.delete(`${this.baseUrl}/Diagnosis?id=${id}`, { headers, responseType: 'text' });
}
}