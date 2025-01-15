import { Component } from '@angular/core';
import { RegistrationFormParentIncludeRegistrationFormInterview } from '../../../Models/Registration/registration-form-parent-include-registration-form-interview';
import { RegisterationFormParentService } from '../../../Services/Employee/Registration/registeration-form-parent.service';
import { TokenData } from '../../../Models/token-data';
import { ApiService } from '../../../Services/api.service';
import { AccountService } from '../../../Services/account.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InterviewTimeTable } from '../../../Models/Registration/interview-time-table';
import { InterviewTimeTableService } from '../../../Services/Employee/Registration/interview-time-table.service';
import { RegistrationFormInterviewService } from '../../../Services/Employee/Registration/registration-form-interview.service';

@Component({
  selector: 'app-interview-registration',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './interview-registration.component.html',
  styleUrl: './interview-registration.component.css'
})
export class InterviewRegistrationComponent {

  User_Data_After_Login: TokenData = new TokenData( '', 0, 0, 0, 0, '', '', '', '', '' );
  
  DomainName: string = '';
  UserID: number = 0;

  registrationFormParentIncludeRegistrationFormInterview: RegistrationFormParentIncludeRegistrationFormInterview[] = []

  InterviewTimeID = 0
  registrationFormParentID = 0
  academicYearIdID = 0
  date = ""

  interviewTimeTable:InterviewTimeTable[] = []

  constructor(public account: AccountService, public ApiServ: ApiService, public registerationFormParentService:RegisterationFormParentService, 
    public interviewTimeTableService:InterviewTimeTableService, public registrationFormInterview: RegistrationFormInterviewService){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.getRegistrationFormParentIncludeRegistrationFormInterviewData()
  }

  getRegistrationFormParentIncludeRegistrationFormInterviewData(){
    this.registerationFormParentService.GetByParentIDIncludeRegistrationFormInterview(this.UserID, this.DomainName).subscribe(
      (data) => {
        this.registrationFormParentIncludeRegistrationFormInterview = data
        console.log(data)
      }
    )
  }

  getInterviewByYearID(){
    this.interviewTimeTableService.GetByYearId(this.academicYearIdID, this.DomainName).subscribe(
      (data) => {
        this.interviewTimeTable = data
        console.log(this.interviewTimeTable)
      }
    )
  }

  openModal(id: number, academicYearId: number, date:string) {
    this.registrationFormParentID = id
    this.academicYearIdID = academicYearId
    this.date= date
 
    this.getInterviewByYearID()

    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }
  
  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");
     
    this.registrationFormParentID = 0
    this.InterviewTimeID = 0
    this.academicYearIdID = 0
    this.date = ""

    this.interviewTimeTable = []
  }

  openModalcalender() {
    document.getElementById("Calender_Modal")?.classList.remove("hidden");
    document.getElementById("Calender_Modal")?.classList.add("flex");
  }
  
  closeModalcalender() {
    document.getElementById("Calender_Modal")?.classList.remove("flex");
    document.getElementById("Calender_Modal")?.classList.add("hidden");
  }

  deleteInterview(id: number) {
  }

  Register() {
    console.log(this.InterviewTimeID)
    console.log(this.registrationFormParentID)
    this.registrationFormInterview.Add(this.registrationFormParentID, this.InterviewTimeID, this.DomainName).subscribe(
      (data) => {
        console.log(data)
      }
    )
  }
    
}
