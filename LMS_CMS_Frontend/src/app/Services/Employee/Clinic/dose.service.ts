import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Dose } from '../../../Models/Clinic/dose';
import { ApiService } from '../../api.service';


@Injectable({
  providedIn: 'root',
})
export class DoseService {
  baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl; // Use the base URL from ApiService
  }

  // Fetch all doses
  Get(DomainName: string): Observable<Dose[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<Dose[]>(`${this.baseUrl}/Dose`, { headers });
  }

  // Add a new dose
  Add(dose: Dose, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.post(`${this.baseUrl}/Dose`, dose, { headers, responseType: 'text' });
  }

  // Edit an existing dose
  Edit(dose: Dose, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/Dose`, dose, { headers });
  }

  // Delete a dose
  Delete(id: number, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.baseUrl}/Dose?id=${id}`, { headers, responseType: 'text' });
  }

  // Search doses
  Search(key: string, value: string, DomainName: string): Observable<Dose[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<Dose[]>(`${this.baseUrl}/Dose/search?key=${key}&value=${value}`, { headers });
  }
}