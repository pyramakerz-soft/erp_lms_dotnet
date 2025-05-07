import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { LessonResource } from '../../../Models/LMS/lesson-resource';

@Injectable({
  providedIn: 'root'
})
export class LessonResourceService {
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
    return this.http.get<LessonResource>(`${this.baseUrl}/LessonResource/GetByID/${id}`, { headers })
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
    return this.http.get<LessonResource[]>(`${this.baseUrl}/LessonResource/GetByLessonID/${id}`, { headers })
  } 

  Add(lessonResource: LessonResource,DomainName:string) { 
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`) 

    const formData = new FormData();

    formData.append('englishTitle', lessonResource.englishTitle);
    formData.append('arabicTitle', lessonResource.arabicTitle);
    formData.append('attachmentLink', lessonResource.attachmentLink || '');  
    formData.append('lessonID', lessonResource.lessonID?.toString() ?? ''); 
    formData.append('lessonResourceTypeID', lessonResource.lessonResourceTypeID?.toString() ?? '');   
    if (lessonResource.classes && lessonResource.classes.length != 0) {
      lessonResource.classes.forEach(item => {
        formData.append('classes', item.toString());
      });
    }

    if (lessonResource.attachmentFile) {
      formData.append('attachmentFile', lessonResource.attachmentFile, lessonResource.attachmentFile.name);
    }  

    return this.http.post(`${this.baseUrl}/LessonResource`, formData, {
      headers: headers,
      responseType: 'text' as 'json'
    });
  }

  Edit(lessonResource: LessonResource,DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)

    const formData = new FormData();

    formData.append('id', lessonResource.id?.toString() ?? '');
    formData.append('englishTitle', lessonResource.englishTitle);
    formData.append('arabicTitle', lessonResource.arabicTitle);
    formData.append('attachmentLink', lessonResource.attachmentLink || ''); 
    formData.append('lessonID', lessonResource.lessonID?.toString() ?? ''); 
    formData.append('lessonResourceTypeID', lessonResource.lessonResourceTypeID?.toString() ?? ''); 
 
    if (lessonResource.classes && lessonResource.classes.length != 0) {
      lessonResource.classes.forEach(item => {
        formData.append('classes', item.toString());
      });
    }
    if (lessonResource.newClassRooms && lessonResource.newClassRooms.length != 0) {
      lessonResource.newClassRooms.forEach(item => {
        formData.append('newClassRooms', item.toString());
      });
    } 

    if (lessonResource.attachmentFile) {
      formData.append('attachmentFile', lessonResource.attachmentFile, lessonResource.attachmentFile.name);
    }  
    return this.http.put(`${this.baseUrl}/LessonResource`, formData, { headers });
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
    return this.http.delete(`${this.baseUrl}/LessonResource/${id}`, { headers })
  }
}
