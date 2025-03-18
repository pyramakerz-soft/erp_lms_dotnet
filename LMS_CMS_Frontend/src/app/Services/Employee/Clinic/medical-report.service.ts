import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root',
})
export class MedicalReportService {
  baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl;
  }

  // Method to get all MH By Parent data
  getAllMHByParent(DomainName: string): Observable<any[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');

    return this.http.get<any[]>(`${this.baseUrl}/MedicalReport/GetAllMHByParent`, { headers });
  }


    getAllMHByDoctor(DomainName: string): Observable<any[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');

    return this.http.get<any[]>(`${this.baseUrl}/MedicalReport/GetAllMHByDoctor`, { headers });
  }


    // Method to get all hygiene forms
getAllHygieneForms(DomainName: string): Observable<any[]> {
  if (DomainName != null) {
    this.header = DomainName;
  }
  const token = localStorage.getItem('current_token');
  const headers = new HttpHeaders()
    .set('Domain-Name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('accept', '*/*');

  return this.http.get<any[]>(`${this.baseUrl}/MedicalReport/GetAllHygienesForms`, { headers });
}
}