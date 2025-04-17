import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { InterviewTimeTableService } from '../../../../Services/Employee/Registration/interview-time-table.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { InterviewTimeTable } from '../../../../Models/Registration/interview-time-table';
import { CommonModule } from '@angular/common';
import { RegistrationFormInterview } from '../../../../Models/Registration/registration-form-interview';
import { RegistrationFormInterviewService } from '../../../../Services/Employee/Registration/registration-form-interview.service';
import { FormsModule } from '@angular/forms';
import { InterviewState } from '../../../../Models/Registration/interview-state';
import { InterviewStateService } from '../../../../Services/Employee/Registration/interview-state.service';
import { TranslateModule } from '@ngx-translate/core';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-interview-registration',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule],
  templateUrl: './interview-registration.component.html',
  styleUrl: './interview-registration.component.css'
})
export class InterviewRegistrationComponent {

  DomainName: string = "";
  interviewTimeID  = 0
  interviewTimeTable:InterviewTimeTable = new InterviewTimeTable()

  registrationFormInterviewData:RegistrationFormInterview[] = []
  registrationFormInterview:RegistrationFormInterview = new RegistrationFormInterview()
  StateData:InterviewState[] = []

  isLoading=false

  constructor(public ApiServ: ApiService, public activeRoute: ActivatedRoute, public router:Router, public interviewStateService: InterviewStateService,
    public interviewTimeTableService: InterviewTimeTableService, public registrationFormInterviewService: RegistrationFormInterviewService ){}
  
  ngOnInit(){
    this.DomainName = this.ApiServ.GetHeader();

    this.interviewTimeID = Number(this.activeRoute.snapshot.paramMap.get('Id'))
    this.getRegistrationFormInterviewData()
    this.getInterviewTimeByID()
  }

  getRegistrationFormInterviewData(){
    this.registrationFormInterviewData = []
    this.registrationFormInterviewService.GetRegistrationFormInterviewByInterviewID(this.interviewTimeID, this.DomainName).subscribe(
      (data) => { 
        this.registrationFormInterviewData = data
      }
    )
  }

  getInterviewTimeByID(){
    this.interviewTimeTableService.GetById(this.interviewTimeID, this.DomainName).subscribe(
      (data) => {
        this.interviewTimeTable = data
      }
    )
  }
  
  getInterviewState(){
    this.interviewStateService.Get(this.DomainName).subscribe(
      (data) => {
        this.StateData = data
      }
    )
  }

  moveToInterviewTimeTable(){
    this.router.navigateByUrl('Employee/Interview Time Table');
  }

  GetRegistrationFormInterviewById(id:number){
    this.registrationFormInterviewService.GetRegistrationFormInterviewByID(id, this.DomainName).subscribe(
      (data) => {
        this.registrationFormInterview = data
      }
    )
  }

  openModal(Id: number) {
    this.GetRegistrationFormInterviewById(Id); 
    this.getInterviewState()

    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.registrationFormInterview = new RegistrationFormInterview()
    this.StateData = []
    this.getRegistrationFormInterviewData()
  }

  Submit(){
    this.isLoading=true
    this.registrationFormInterviewService.Edit(this.registrationFormInterview.id, this.registrationFormInterview.interviewStateID, this.DomainName).subscribe(
      (result: any) => {
        this.closeModal()
        this.getRegistrationFormInterviewData()
        this.isLoading=false
      },
      error => {
        this.isLoading=false
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Try Again Later!',
          confirmButtonText: 'Okay',
          customClass: { confirmButton: 'secondaryBg' },
        });
      }
    );
  }
}
