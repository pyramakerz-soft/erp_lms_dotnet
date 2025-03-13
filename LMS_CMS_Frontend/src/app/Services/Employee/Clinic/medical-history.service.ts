// medical-history.service.ts
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicalHistory } from '../../../Models/Clinic/MedicalHistory';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root',
})
export class MedicalHistoryService {
  baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl;
  }

  // Fetch all medical histories
  Get(DomainName: string): Observable<MedicalHistory[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<MedicalHistory[]>(`${this.baseUrl}/MedicalHistory/GetByDoctor`, { headers });
  }


Add(medicalHistory: FormData, DomainName: string): Observable<any> {
  if (DomainName != null) {
    this.header = DomainName;
  }
  const token = localStorage.getItem('current_token');
  const headers = new HttpHeaders()
    .set('Domain-Name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('accept', '*/*');
  return this.http.post<any>(`${this.baseUrl}/MedicalHistory/AddByDoctor`, medicalHistory, { headers });
}

Edit(medicalHistory: FormData, DomainName: string): Observable<any> {
  if (DomainName != null) {
    this.header = DomainName;
  }
  const token = localStorage.getItem('current_token');
  const headers = new HttpHeaders()
    .set('Domain-Name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('accept', '*/*');
  return this.http.put<any>(`${this.baseUrl}/MedicalHistory/UpdateByDoctorAsync`, medicalHistory, { headers });
}


 // Delete a medical history
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

    return this.http.delete(`${this.baseUrl}/MedicalHistory/id?id=${id}`, { headers, responseType: 'text' });
  }
  GetById(id: number, DomainName: string): Observable<MedicalHistory> {
    if (DomainName != null) {
        this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
        .set('Domain-Name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('accept', '*/*');

    return this.http.get<MedicalHistory>(`${this.baseUrl}/MedicalHistory/GetByIdByDoctor/id?id=${id}`, { headers });
}


  // Search medical histories
  Search(key: string, value: string, DomainName: string): Observable<MedicalHistory[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<MedicalHistory[]>(`${this.baseUrl}/MedicalHistory/search?key=${key}&value=${value}`, { headers });
  }
}