import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { BusStudent } from '../../../Models/Bus/bus-student';

@Injectable({
  providedIn: 'root'
})
export class BusStudentService {

  baseUrl=""

  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
  }

  GetbyBusId(busId: number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<BusStudent[]>(`${this.baseUrl}/BusStudent/GetByBusId/${busId}`, { headers })
  }

  DeleteBusStudent(busStudentId: number){
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.baseUrl}/BusStudent/${busStudentId}`, { headers })
  }
}
