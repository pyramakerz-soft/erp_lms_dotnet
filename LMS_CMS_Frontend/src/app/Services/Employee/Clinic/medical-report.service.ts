import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicalHistoryByParent } from '../../../Models/Clinic/mh-by-parent';
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
     console.log("parent")
    return this.http.get<any[]>(`${this.baseUrl}/MedicalReport/GetAllMHByParent`, { headers });
  }

  // Method to get medical history by parent ID
  getMHByParentById(id: number, DomainName: string): Observable<MedicalHistoryByParent> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');

    return this.http.get<MedicalHistoryByParent>(`${this.baseUrl}/MedicalHistory/GetByIdByParent/id?id=${id}`, { headers });
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