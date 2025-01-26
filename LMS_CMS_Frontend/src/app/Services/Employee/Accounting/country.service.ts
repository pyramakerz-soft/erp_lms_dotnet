import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Debit } from '../../../Models/Accounting/debit';
import { ApiService } from '../../api.service';
import { Country } from '../../../Models/Accounting/country';

@Injectable({
  providedIn: 'root',
})
export class CountryService {
  baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrlOcta;
  }

  Get() {
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<Country[]>(`${this.baseUrl}/Country`, { headers });
  }
}
