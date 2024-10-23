import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  baseurl:string = "";

  constructor(public Api:ApiService, private http: HttpClient) {
    this.baseurl=Api.BaseUrl
  }

  getData() {
    console.log(`${this.baseurl}WeatherForecast`)
    return this.http.get<any>(`${this.baseurl}WeatherForecast`);
  }
}
