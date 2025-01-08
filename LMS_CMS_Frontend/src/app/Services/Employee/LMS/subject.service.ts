import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { Subject } from '../../../Models/LMS/subject';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {
  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<Subject[]>(`${this.baseUrl}/Subject`, { headers })
  }
 
  Add(Subject: Subject, DomainName: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
  
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`);
  
    const formData = new FormData();
    formData.append('en_name', Subject.en_name ?? '');
    formData.append('ar_name', Subject.ar_name ?? '');
    formData.append('orderInCertificate', Subject.orderInCertificate?.toString() ?? '');
    formData.append('creditHours', Subject.creditHours?.toString() ?? '');
    formData.append('subjectCode', Subject.subjectCode?.toString() ?? '');
    formData.append('passByDegree', Subject.passByDegree?.toString() ?? '');
    formData.append('totalMark', Subject.totalMark?.toString() ?? '');
    formData.append('hideFromGradeReport', String(Subject.hideFromGradeReport));
    formData.append('numberOfSessionPerWeek', Subject.numberOfSessionPerWeek?.toString() ?? '');
    formData.append('gradeID', String(Subject.gradeID));
    formData.append('subjectCategoryID', String(Subject.subjectCategoryID));
  
    if (Subject.iconFile) {
      formData.append('iconFile', Subject.iconFile, Subject.iconFile.name);
    } 
   
    return this.http.post(`${this.baseUrl}/Subject`, formData, { headers });
  }
  

  Edit(Subject: Subject,DomainName:string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
  
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`);
   
    const formData = new FormData();
    formData.append('id', Subject.id.toString() ?? '');
    formData.append('en_name', Subject.en_name.toString() ?? '');
    formData.append('ar_name', Subject.ar_name.toString() ?? '');
    formData.append('orderInCertificate', Subject.orderInCertificate?.toString() ?? '');
    formData.append('creditHours', Subject.creditHours?.toString() ?? '');
    formData.append('subjectCode', Subject.subjectCode?.toString() ?? '');
    formData.append('passByDegree', Subject.passByDegree?.toString() ?? '');
    formData.append('totalMark', Subject.totalMark?.toString() ?? '');
    formData.append('hideFromGradeReport', String(Subject.hideFromGradeReport));
    formData.append('numberOfSessionPerWeek', Subject.numberOfSessionPerWeek?.toString() ?? '');
    formData.append('gradeID', String(Subject.gradeID));
    formData.append('subjectCategoryID', String(Subject.subjectCategoryID));
  
    if (Subject.iconFile) {
      formData.append('iconFile', Subject.iconFile, Subject.iconFile.name);
    } else if (Subject.iconLink) {
      formData.append('iconLink', Subject.iconLink?.toString() ?? '');
    } 

    return this.http.put(`${this.baseUrl}/Subject`, formData, { headers });
  }

  Delete(id: number,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/Subject/${id}`, { headers })
  }

  GetByID(id: number,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<Subject>(`${this.baseUrl}/Subject/${id}`, { headers })
  }
}
