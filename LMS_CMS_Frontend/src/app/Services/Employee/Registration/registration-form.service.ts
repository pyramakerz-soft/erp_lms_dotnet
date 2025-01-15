import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { RegistrationForm } from '../../../Models/Registration/registration-form';
import { RegistrationFormForFormSubmission } from '../../../Models/Registration/registration-form-for-form-submission';
import { RegistrationFormForFormSubmissionForFiles } from '../../../Models/Registration/registration-form-for-form-submission-for-files';

@Injectable({
  providedIn: 'root'
})
export class RegistrationFormService {
  baseUrl = ""
  header = ""
  
  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  GetById(id:number, DomainName:string){
      if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
    .set('domain-name', this.header)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');
    return this.http.get<RegistrationForm>(`${this.baseUrl}/RegistrationForm/${id}`, { headers })
  }

  Add(registrationForm: RegistrationFormForFormSubmission, registrationFormForFiles:RegistrationFormForFormSubmissionForFiles[],DomainName:string) {
    if(DomainName!=null) {
      this.header=DomainName 
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)

    const formData = new FormData();
    formData.append("registerationFormParentAddDTO.RegistrationFormID", registrationForm.registrationFormID.toString());

    registrationForm.registerationFormSubmittions.forEach((field: any, index) => {
      if(field.textAnswer != null){
        formData.append(`registerationFormParentAddDTO.RegisterationFormSubmittions[${index}].TextAnswer`, field.textAnswer.toString());
      } else{
        formData.append(`registerationFormParentAddDTO.RegisterationFormSubmittions[${index}].SelectedFieldOptionID`, field.selectedFieldOptionID.toString());
      }
      formData.append(`registerationFormParentAddDTO.RegisterationFormSubmittions[${index}].categoryFieldID`, field.categoryFieldID.toString());
    });

    if (registrationFormForFiles && registrationFormForFiles.length > 0) {
      registrationFormForFiles.forEach((file: any, index) => {
        formData.append(`filesFieldCat[${index}].CategoryFieldID`, file.categoryFieldID.toString());
        formData.append(`filesFieldCat[${index}].SelectedFile`, file.selectedFile, file.selectedFile.name);
      });
    }
    
    return this.http.post(`${this.baseUrl}/RegistrationForm`, formData, {
      headers: headers
    });
  }
}
