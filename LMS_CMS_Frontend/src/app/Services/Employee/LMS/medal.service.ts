import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Medal } from '../../../Models/LMS/medal';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class MedalService {

baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl;
  }

  Get(DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<Medal[]>(`${this.baseUrl}/Medal`, { headers });
  }

  GetByID(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<Medal>(`${this.baseUrl}/Medal/${id}`, { headers });
  }

  Add(Medal: Medal, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`);
      
    const formData = new FormData();
    formData.append('englishName', Medal.englishName ?? '');
    formData.append('arabicName', Medal.arabicName ?? '');
    formData.append('imageLink', Medal.imageLink ?? '');
  
    if (Medal.imageForm) {
      formData.append('imageForm', Medal.imageForm, Medal.imageForm.name);
    } 
    return this.http.post(`${this.baseUrl}/Medal`, formData, {
      headers: headers,
      responseType: 'text' as 'json',
    });
  }

  Edit(Medal: Medal, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`);
      const formData = new FormData();
      formData.append('id', Medal.id.toString() ?? '');
      formData.append('englishName', Medal.englishName ?? '');
      formData.append('arabicName', Medal.arabicName ?? '');
      formData.append('imageLink', Medal.imageLink ?? '');
    
      if (Medal.imageForm) {
        formData.append('imageForm', Medal.imageForm, Medal.imageForm.name);
      } 
      else if (Medal.imageLink) {
        formData.append('imageLink', Medal.imageLink?.toString() ?? '');
      } 
  

    return this.http.put(`${this.baseUrl}/Medal`, formData, { headers });
  }

  Delete(id: number, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/Medal/${id}`, { headers });
  }
}
