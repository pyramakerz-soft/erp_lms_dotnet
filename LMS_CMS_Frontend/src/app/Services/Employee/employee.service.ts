import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Employee } from '../../Models/Employee/employee';
import { EmployeeGet } from '../../Models/Employee/employee-get';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  baseUrl=""
  header=""
  
  constructor(public http: HttpClient, public ApiServ:ApiService){  
    this.baseUrl=ApiServ.BaseUrl
    this.header = ApiServ.GetHeader();
  }

  GetWithTypeId(typeId: number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Employee[]>(`${this.baseUrl}/Employee/GetByTypeId/${typeId}`, { headers })
  }
  
  GetByID(empID: number,DomainName?:string){
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('Authorization', `Bearer ${token}`)
    .set('domain-name', this.header)
    .set('Content-Type', 'application/json');

    return this.http.get<Employee[]>(`${this.baseUrl}/Employee/${empID}`, { headers })
  }


  Add(employee: EmployeeGet, DomainName?: string) {
    if (DomainName != null) {
      this.header = DomainName;
    }

    const token = localStorage.getItem('current_token');
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${token}`)
      .set('domain-name', this.header);

    const formData = new FormData();

    formData.append('user_Name', employee.user_Name);
    formData.append('en_name', employee.en_name);
    formData.append('ar_name', employee.ar_name || ''); 
    formData.append('password', employee.password);
    formData.append('mobile', employee.mobile || '');
    formData.append('phone', employee.phone || '');
    formData.append('email', employee.email || '');
    formData.append('licenseNumber', employee.licenseNumber || '');
    formData.append('expireDate', employee.expireDate || '');
    formData.append('address', employee.address || '');
    formData.append('role_ID', employee.role_ID.toString());
    formData.append('busCompanyID', employee.busCompanyID.toString());
    formData.append('employeeTypeID', employee.employeeTypeID.toString());

    if (employee.files && employee.files.length > 0) {
      employee.files.forEach((file, index) => {
        formData.append('files', file, file.name);
      });
    }

    return this.http.post<EmployeeGet>(`${this.baseUrl}/Employee`, formData, { headers });
  }

    Get_Employees(DomainName?:string){
      if(DomainName!=null) {
        this.header=DomainName 
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
      .set('domain-name', this.header) 
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
      return this.http.get<EmployeeGet[]>(`${this.baseUrl}/Employee`, { headers });
    }

}
