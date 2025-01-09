import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { School } from '../../Models/school';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<School[]>(`${this.baseUrl}/Schools`, { headers })
  }

  GetBySchoolId(id:number, DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<School>(`${this.baseUrl}/Schools/${id}`, { headers })
  }

  Add(school:School, DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.post(`${this.baseUrl}/Schools`, school, { headers })
  }

  Edit(school:School, DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)

    const formData = new FormData();
    formData.append('id', school.id.toString() ?? '');
    formData.append('name', school.name.toString() ?? ''); 
    formData.append('schoolTypeID', String(school.schoolTypeID) ?? '');
    formData.append('address', school.address?.toString() ?? '');
    formData.append('reportHeaderOneEn', school.reportHeaderOneEn?.toString() ?? '');
    formData.append('reportHeaderOneAr', school.reportHeaderOneAr?.toString() ?? ''); 
    formData.append('reportHeaderTwoEn', school.reportHeaderTwoEn?.toString() ?? '');
    formData.append('reportHeaderTwoAr', school.reportHeaderTwoAr?.toString() ?? '');
  
    if (school.reportImageFile) {
      formData.append('reportImageFile', school.reportImageFile, school.reportImageFile.name);
    } else if (school.reportImage) {
      formData.append('reportImage', school.reportImage?.toString() ?? '');
    } 

    formData.forEach((value, key) => {
      console.log(key, value);
    });

    console.log(`${this.baseUrl}/Schools`)
    return this.http.put(`${this.baseUrl}/Schools`, formData, { headers })
  }

  Delete(id:number, DomainName:string){
     if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/Schools/${id}`, { headers })
  }
}
