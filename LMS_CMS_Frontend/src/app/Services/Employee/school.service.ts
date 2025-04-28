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
    formData.append('vatNumber', school.vatNumber?.toString() ?? '');
    formData.append('maximumPeriodCountRemedials', school.maximumPeriodCountRemedials?.toString() ?? '');
    formData.append('maximumPeriodCountTimeTable', school.maximumPeriodCountTimeTable?.toString() ?? '');
    formData.append('postalZone', school.postalZone?.toString() ?? '');
    formData.append('city', school.city?.toString() ?? '');
    formData.append('citySubdivision', school.citySubdivision?.toString() ?? '');
    formData.append('buildingNumber', school.buildingNumber?.toString() ?? '');
    formData.append('streetName', school.streetName?.toString() ?? '');
    if (school.reportImageFile) {
      formData.append('reportImageFile', school.reportImageFile, school.reportImageFile.name);
    } else if (school.reportImage) {
      formData.append('reportImage', school.reportImage?.toString() ?? '');
    } 

    formData.forEach((value, key) => {
    });

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
