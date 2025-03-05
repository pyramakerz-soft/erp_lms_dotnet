import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FollowUp } from '../../../Models/Clinic/FollowUp';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root',
})
export class FollowUpService {
  baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl; 
  }

  // Get all follow-ups
  Get(DomainName: string): Observable<FollowUp[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header) 
      .set('Authorization', `Bearer ${token}`) 
      .set('accept', '*/*'); 
    return this.http.get<FollowUp[]>(`${this.baseUrl}/FollowUp`, { headers });
  }

  // Add a new follow-up
  Add(followUp: FollowUp, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*')
      .set('Content-Type', 'application/json');
    return this.http.post<any>(`${this.baseUrl}/FollowUp`, followUp, {
      headers: headers,
      responseType: 'text' as 'json',
    });
  }

  // Update an existing follow-up
  Edit(followUp: FollowUp, DomainName: string): Observable<FollowUp> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*')
      .set('Content-Type', 'application/json');
    return this.http.put<FollowUp>(`${this.baseUrl}/FollowUp`, followUp, { headers });
  }

  // Delete a follow-up
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
    return this.http.delete(`${this.baseUrl}/FollowUp?id=${id}`, { headers, responseType: 'text' });
  }
}