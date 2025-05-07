import { Injectable } from '@angular/core';
import { LessonActivity } from '../../../Models/LMS/lesson-activity';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class LessonActivityService {
  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
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
    return this.http.get<LessonActivity>(`${this.baseUrl}/LessonActivity/GetByID/${id}`, { headers })
  }

  GetByLessonId(id:number, DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<LessonActivity[]>(`${this.baseUrl}/LessonActivity/GetByLessonID/${id}`, { headers })
  } 

  Add(lessonActivity: LessonActivity,DomainName:string) { 
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`) 

    const formData = new FormData();

    formData.append('englishTitle', lessonActivity.englishTitle);
    formData.append('arabicTitle', lessonActivity.arabicTitle);
    formData.append('attachmentLink', lessonActivity.attachmentLink || ''); 
    formData.append('order', lessonActivity.order?.toString() ?? '');
    formData.append('details', lessonActivity.details || '');
    formData.append('lessonID', lessonActivity.lessonID?.toString() ?? ''); 
    formData.append('lessonActivityTypeID', lessonActivity.lessonActivityTypeID?.toString() ?? ''); 

    if (lessonActivity.attachmentFile) {
      formData.append('attachmentFile', lessonActivity.attachmentFile, lessonActivity.attachmentFile.name);
    }  

    return this.http.post(`${this.baseUrl}/LessonActivity`, formData, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  Edit(lessonActivity: LessonActivity,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)

    const formData = new FormData();

    formData.append('id', lessonActivity.id?.toString() ?? '');
    formData.append('englishTitle', lessonActivity.englishTitle);
    formData.append('arabicTitle', lessonActivity.arabicTitle);
    formData.append('attachmentLink', lessonActivity.attachmentLink || ''); 
    formData.append('order', lessonActivity.order?.toString() ?? '');
    formData.append('details', lessonActivity.details || '');
    formData.append('lessonID', lessonActivity.lessonID?.toString() ?? ''); 
    formData.append('lessonActivityTypeID', lessonActivity.lessonActivityTypeID?.toString() ?? ''); 

    if (lessonActivity.attachmentFile) {
      formData.append('attachmentFile', lessonActivity.attachmentFile, lessonActivity.attachmentFile.name);
    }  
    return this.http.put(`${this.baseUrl}/LessonActivity`, formData, { headers });
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
    return this.http.delete(`${this.baseUrl}/LessonActivity/${id}`, { headers })
  }
}
