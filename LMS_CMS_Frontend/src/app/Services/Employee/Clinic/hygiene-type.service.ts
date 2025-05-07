import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HygieneTypes } from '../../../Models/Clinic/hygiene-types';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root',
})
export class HygieneTypesService {
  baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl; 
  }

  
  Get(DomainName: string): Observable<HygieneTypes[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header) 
      .set('Authorization', `Bearer ${token}`) 
      .set('accept', '*/*'); 
    return this.http.get<HygieneTypes[]>(`${this.baseUrl}/HygieneType`, { headers });
  }

  
  Add(hygieneType: HygieneTypes, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*')
      .set('Content-Type', 'application/json');
    return this.http.post<any>(`${this.baseUrl}/HygieneType`, hygieneType, {
      headers: headers,
      responseType: 'text' as 'json',
    });
  }

  
  Edit(hygieneType: HygieneTypes, DomainName: string): Observable<HygieneTypes> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*')
      .set('Content-Type', 'application/json');
    return this.http.put<HygieneTypes>(`${this.baseUrl}/HygieneType`, hygieneType, { headers });
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
    return this.http.delete(`${this.baseUrl}/HygieneType?id=${id}`, { headers, responseType: 'text' });
  }


  
  Search(key: string, value: string, DomainName: string): Observable<HygieneTypes[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<HygieneTypes[]>(`${this.baseUrl}/HygieneTypes/search?key=${key}&value=${value}`, { headers });
  }
}