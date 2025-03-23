// medical-history.service.ts
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicalHistory } from '../../../Models/Clinic/MedicalHistory';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root',
})
export class MedicalHistoryService {
  baseUrl = '';
  header = '';

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl;
  }

  // Fetch all medical histories
  GetByDoctor(DomainName: string): Observable<MedicalHistory[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<MedicalHistory[]>(`${this.baseUrl}/MedicalHistory/GetByDoctor`, { headers });
  }

  // Add a new medical history
  AddByDoctor(medicalHistory: MedicalHistory, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }

    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`);

    const formData = new FormData();
    formData.append('Id', medicalHistory.id?.toString() ?? '');
    formData.append('SchoolId', medicalHistory.schoolId?.toString() ?? '');
    formData.append('GradeId', medicalHistory.gradeId?.toString() ?? '');
    formData.append('ClassRoomID', medicalHistory.classRoomID?.toString() ?? '');
    formData.append('StudentId', medicalHistory.studentId?.toString() ?? '');
    formData.append('Details', medicalHistory.details ?? '');
    formData.append('PermanentDrug', medicalHistory.permanentDrug ?? '');
    formData.append('Date', medicalHistory.insertedAt ?? '');

    // Handle FirstReport file
    if (medicalHistory.firstReport instanceof File) {
      formData.append('FirstReport', medicalHistory.firstReport, medicalHistory.firstReport.name);
    } else if (medicalHistory.firstReport === null) {
      formData.append('FirstReport', ''); // Set to empty if file is deleted
    } else {
      formData.append('FirstReport', medicalHistory.firstReport); // Retain existing link
    }

    // Handle SecReport file
    if (medicalHistory.secReport instanceof File) {
      formData.append('secReport', medicalHistory.secReport, medicalHistory.secReport.name);
    } else if (medicalHistory.secReport === null) {
      formData.append('SecReport', ''); // Set to empty if file is deleted
    } else {
      formData.append('SecReport', medicalHistory.secReport); // Retain existing link
    }
    console.log("doctor")
    return this.http.post(`${this.baseUrl}/MedicalHistory/AddByDoctor`, formData, { headers });
  }

  // Edit an existing medical history
  UpdateByDoctorAsync(medicalHistory: MedicalHistory, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }

    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`);

    const formData = new FormData();
    formData.append('Id', medicalHistory.id?.toString() ?? '');
    formData.append('SchoolId', medicalHistory.schoolId?.toString() ?? '');
    formData.append('GradeId', medicalHistory.gradeId?.toString() ?? '');
    formData.append('ClassRoomID', medicalHistory.classRoomID?.toString() ?? '');
    formData.append('StudentId', medicalHistory.studentId?.toString() ?? '');
    formData.append('Details', medicalHistory.details ?? '');
    formData.append('PermanentDrug', medicalHistory.permanentDrug ?? '');
    formData.append('Date', medicalHistory.insertedAt ?? '');

    // Handle FirstReport file
    if (medicalHistory.firstReport instanceof File) {
      formData.append('FirstReportFile', medicalHistory.firstReport, medicalHistory.firstReport.name);
    } else if (medicalHistory.firstReport === null) {
      formData.append('FirstReport', ''); // Set to empty if file is deleted
    } else {
      formData.append('FirstReport', medicalHistory.firstReport); // Retain existing link
    }

    // Handle SecReport file
    if (medicalHistory.secReport instanceof File) {
      formData.append('SecReportFile', medicalHistory.secReport, medicalHistory.secReport.name);
    } else if (medicalHistory.secReport === null) {
      formData.append('SecReport', ''); // Set to empty if file is deleted
    } else {
      formData.append('SecReport', medicalHistory.secReport); // Retain existing link
    }

    return this.http.put(`${this.baseUrl}/MedicalHistory/UpdateByDoctorAsync`, formData, { headers });
  }

  // Delete a medical history
  Delete(id: number, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*')
      .set('Content-Type', 'application/json');
    return this.http.delete(`${this.baseUrl}/MedicalHistory/id?id=${id}`, { headers, responseType: 'text' });
  }

  // Get medical history by ID
  GetByIdByDoctor(id: number, DomainName: string): Observable<MedicalHistory> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<MedicalHistory>(`${this.baseUrl}/MedicalHistory/GetByIdByDoctor/id?id=${id}`, { headers });
  }

  // Search medical histories
  Search(key: string, value: string, DomainName: string): Observable<MedicalHistory[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<MedicalHistory[]>(`${this.baseUrl}/MedicalHistory/search?key=${key}&value=${value}`, { headers });
  }

  
  // Fetch all medical histories by parent
  GetByParent(DomainName: string): Observable<MedicalHistory[]> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<MedicalHistory[]>(`${this.baseUrl}/MedicalHistory/GetByParent`, { headers });
  }

  // Fetch medical history by ID for parent
  GetByIdByParent(id: number, DomainName: string): Observable<MedicalHistory> {
    if (DomainName != null) {
      this.header = DomainName;
    }
    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('accept', '*/*');
    return this.http.get<MedicalHistory>(`${this.baseUrl}/MedicalHistory/GetByIdByParent/id?id=${id}`, { headers });
  }

  // Add a new medical history by parent
  AddByParent(medicalHistory: MedicalHistory, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName;
    }

    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Domain-Name', this.header)
      .set('Authorization', `Bearer ${token}`);

    const formData = new FormData();
    formData.append('Details', medicalHistory.details ?? '');
    formData.append('PermanentDrug', medicalHistory.permanentDrug ?? '');

    // Handle FirstReport file
    if (medicalHistory.firstReport instanceof File) {
      formData.append('FirstReport', medicalHistory.firstReport, medicalHistory.firstReport.name);
    } else if (medicalHistory.firstReport === null) {
      formData.append('FirstReport', ''); // Set to empty if file is deleted
    } else {
      formData.append('FirstReport', medicalHistory.firstReport); // Retain existing link
    }

    // Handle SecReport file
    if (medicalHistory.secReport instanceof File) {
      formData.append('SecReport', medicalHistory.secReport, medicalHistory.secReport.name);
    } else if (medicalHistory.secReport === null) {
      formData.append('SecReport', ''); // Set to empty if file is deleted
    } else {
      formData.append('SecReport', medicalHistory.secReport); // Retain existing link
    }

    return this.http.post(`${this.baseUrl}/MedicalHistory/AddByParent`, formData, { headers });
  }

}