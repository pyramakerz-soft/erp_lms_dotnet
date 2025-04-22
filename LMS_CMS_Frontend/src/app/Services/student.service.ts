import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Student } from '../Models/student';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  baseUrl=""
  header = ""
  
  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
    this.header = ApiServ.GetHeader()
  }

  GetAll(DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Student[]>(`${this.baseUrl}/Student`, { headers })
  }

  GetAllWithSearch(KeyWord:string,PageNumber:number =1 ,pageSize:number =10 ,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<any>(`${this.baseUrl}/Student/Search?keyword=${KeyWord}&pageNumber=${PageNumber}&pageSize=${pageSize}`, { headers })
  }

  GetByID(id:number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Student>(`${this.baseUrl}/Student/${id}`, { headers })
  }

  GetByAcademicYearID(id:number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Student[]>(`${this.baseUrl}/Student/GetByAcademicYearID/${id}`, { headers })
  }

  GetByYear(Yearid:number,StudentId:number,SchoolId:number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<any>(`${this.baseUrl}/Student/GetStudentByYearID?yearId=${Yearid}&stuId=${StudentId}&schoolId=${SchoolId}`, { headers })
  }

GetByClassID(id: number, DomainName: string): Observable<Student[]> {
  const headers = new HttpHeaders()
    .set('Domain-Name', DomainName)
    .set('Authorization', `Bearer ${localStorage.getItem('current_token')}`)
    .set('Content-Type', 'application/json');

  return this.http.get<Student[]>(`${this.baseUrl}/Student/Get_By_ClassID/${id}`, { headers });
}

  GetByStudentID(id:number,DomainName:string){
    this.header=DomainName 
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Student[]>(`${this.baseUrl}/Student/Get_By_SchoolID/${id}`, { headers })
  }

  EditAccountingEmployee(student:Student,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header) 
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.put(`${this.baseUrl}/Student/StudentAccounting`, student, { headers });
  }
    
  GetByNationalID(NationalID:string,DomainName:string){
    this.header=DomainName 
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Student>(`${this.baseUrl}/Student/SearchByNationality/${NationalID}`, { headers })
  }
  
  GetBySchoolGradeClassID(schoolId: number, gradeId: number, classId: number, DomainName: string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    let params = new HttpParams();
    params = params.set('schoolId', schoolId.toString()); 
    params = params.set('gradeId', gradeId.toString());
    params = params.set('classId', classId.toString());

    return this.http.get<any>(`${this.baseUrl}/Student/GetBySchoolGradeClassID`, { headers, params });
  }

  GetProofRegistrationAndSuccessForm(Yearid:number,StudentId:number,SchoolId:number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<any>(`${this.baseUrl}/Student/GetStudentProofRegistrationAndSuccessForm?yearId=${Yearid}&stuId=${StudentId}&schoolId=${SchoolId}`, { headers })
  }

  GetStudentProofRegistration(Yearid:number,StudentId:number,SchoolId:number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<any>(`${this.baseUrl}/Student/GetStudentProofRegistration?yearId=${Yearid}&stuId=${StudentId}&schoolId=${SchoolId}`, { headers })
  }

  GetByClassIDReport(SchoolId:number,ClassId:number , DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<any>(`${this.baseUrl}/Student/GetByClassIDReport?schoolId=${SchoolId}&classId=${ClassId}`, { headers })
  }

  GetAcademicSequential(StudentId:number,SchoolId:number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<any>(`${this.baseUrl}/Student/AcademicSequentialReport?stuId=${StudentId}&schoolId=${SchoolId}`, { headers })
  }

  TransferedFromKindergarten(StudentId:number,SchoolId:number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<any>(`${this.baseUrl}/Student/TransferedFromKindergartenReport?stuId=${StudentId}&schoolId=${SchoolId}`, { headers })
  }

}