import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { BusStudent } from '../../../Models/Bus/bus-student';

@Injectable({
  providedIn: 'root'
})
export class BusStudentService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  GetbyId(busStuId: number,DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<BusStudent>(`${this.baseUrl}/BusStudent/${busStuId}`, { headers })
  }

  GetbyBusId(busId: number,DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<BusStudent[]>(`${this.baseUrl}/BusStudent/GetByBusId/${busId}`, { headers })
  }

  DeleteBusStudent(busStudentId: number,DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/BusStudent/${busStudentId}`, { headers })
  }

  Add(busStu:BusStudent, DomainName:string){
    this.header=DomainName 
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.post(`${this.baseUrl}/BusStudent`, busStu, { headers })
  }

  Edit(busStu:BusStudent, DomainName:string){
    this.header=DomainName 
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/BusStudent`, busStu, { headers })
  }
}
