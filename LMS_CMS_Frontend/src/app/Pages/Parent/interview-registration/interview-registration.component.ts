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
import Swal from 'sweetalert2';

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
  registrationFormInterviewID = 0
  academicYearIdID = 0
  selectedDate = ""
  selectedTime = ""

  interviewTimeTable:InterviewTimeTable[] = []
  ToChooseFromInterviewTimeTable:InterviewTimeTable[] = []

  calendarMonths: any[] = [];
  today: Date = new Date();

  currentMonth = 0;

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
      }
    )
  }

  getInterviewByYearID(){
    this.interviewTimeTableService.GetByYearId(this.academicYearIdID, this.DomainName).subscribe(
      (data) => {
        this.interviewTimeTable = data
        this.filterFutureDates()
        this.generateCalendar()
      }
    )
  }

  openModal(id: number, academicYearId: number, registrationFormInterviewID?:number, interviewID?:number, date?:string) {
    if(registrationFormInterviewID && interviewID && date){
      this.registrationFormInterviewID = registrationFormInterviewID
      this.InterviewTimeID = interviewID
      this.selectedDate = date
    }
    this.registrationFormParentID = id
    this.academicYearIdID = academicYearId
 
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
    this.registrationFormInterviewID = 0
    this.selectedDate = ""
    this.selectedTime = ""

    this.interviewTimeTable = []

    this.calendarMonths = [];
    this.today = new Date();

    this.ToChooseFromInterviewTimeTable = []
    this.currentMonth = 0;

    this.closeModalcalender()
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
    Swal.fire({
          title: 'Are you sure you want to Cancel this Interview Registration?',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#FF7519',
          cancelButtonColor: '#17253E',
          confirmButtonText: 'Yes',
          cancelButtonText: 'No',
        }).then((result) => {
          if (result.isConfirmed) {
            this.registrationFormInterview.Cancel(id, this.DomainName).subscribe(
              (data) => {
                this.getRegistrationFormParentIncludeRegistrationFormInterviewData()
              },
              (error) => {
                Swal.fire({
                  title: 'Error',
                  text: 'An error occurred while canceling the interview. Please try again later.',
                  icon: 'error',
                  confirmButtonText: 'OK',
                });
              }
            )
          }
    });
  }

  ViewDate(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const selectedId = +target.value;
    
    const selectedState = this.interviewTimeTable.find(state => state.id === selectedId);

    if (selectedState) {
      this.selectedDate = selectedState.date
    }
  }

  Register() {
    if(!this.registrationFormInterviewID){
      this.registrationFormInterview.Add(this.registrationFormParentID, this.InterviewTimeID, this.DomainName).subscribe(
        (data) => {
          this.closeModal()
          this.getRegistrationFormParentIncludeRegistrationFormInterviewData()
        },
        (error) => {
          Swal.fire({
            title: 'Error',
            text: error.error,
            icon: 'error',
            confirmButtonColor: '#FF7519',
            confirmButtonText: 'OK',
          });
        }
      )
    } else{
      this.registrationFormInterview.EditByParent(this.registrationFormInterviewID, this.InterviewTimeID, this.DomainName).subscribe(
        (data) => {
          this.closeModal()
          this.getRegistrationFormParentIncludeRegistrationFormInterviewData()
        },
        (error) => {
          Swal.fire({
            title: 'Error',
            text: error.error,
            icon: 'error',
            confirmButtonColor: '#FF7519',
            confirmButtonText: 'OK',
          });
        }
      )
    }
  }
    
  filterFutureDates() {
    const today = new Date(); 
    this.interviewTimeTable = this.interviewTimeTable.filter((interviewTime) => {
      const interviewDate = new Date(interviewTime.date);
      return interviewDate >= today; // Only include dates today or later
    });
  }

  generateCalendar() {
    const dates = this.interviewTimeTable.map((item) => new Date(item.date)); 
    const lastDate = new Date(Math.max(...dates.map((date) => date.getTime())));  

    let currentMonth = new Date(this.today.getFullYear(), this.today.getMonth(), 1); 
    
    const endMonth = new Date(lastDate.getFullYear(), lastDate.getMonth(), 1); 
 
    while (currentMonth <= endMonth) {
      const month = currentMonth.getMonth();
      const year = currentMonth.getFullYear();
      this.calendarMonths.push({ month, year });
      currentMonth = new Date(year, month + 1, 1);
    }
    
    this.currentMonth = this.calendarMonths[0].month

  }

  getDaysInMonth(month: number, year: number): DayWithInterviews[] { 
    const totalDays = new Date(year, month + 1, 0).getDate();

    let plainDays: DayWithInterviews[] = [];
  
    for (let day = 1; day <= totalDays; day++) {
      const date = new Date(year, month, day);
       
      const dayObj: DayWithInterviews = {
        day: date.getDate(),
        interviews: []
      };

      this.interviewTimeTable.forEach(element => {
        if(new Date(element.date).toDateString() === date.toDateString()){
          dayObj.interviews.push(element);
        }
      });

      plainDays.push(dayObj); 
    }

    return plainDays;
  }

  getMonthDate(month: number, year: number): Date {
    return new Date(year, month, 1); 
  }

  isDayInInterviewTimeTable(dayId: number): boolean {
    return this.interviewTimeTable.some(item => item.id === dayId);
  }

  lastMonth(){
    this.currentMonth--
  }
  nextMonth(){
    this.currentMonth++
  }

  isGreen(day: DayWithInterviews): boolean {
    let isgreen=false

    day.interviews.forEach(element => {
      if(element.reserved != element.capacity){
        isgreen = true
      }
    });
    return isgreen
  }
   
  showTime(day?: DayWithInterviews) {
    this.ToChooseFromInterviewTimeTable = []
    document.getElementById("timePart")?.classList.remove("flex");
    document.getElementById("timePart")?.classList.add("hidden");
    if(day){
      this.ToChooseFromInterviewTimeTable = day.interviews
      document.getElementById("timePart")?.classList.remove("hidden");
      document.getElementById("timePart")?.classList.add("flex");
    } 
  }
    
  ChooseInterviewTime(interview:InterviewTimeTable) {
    if(interview.capacity != interview.reserved){
      this.closeModalcalender()
      console.log(interview)
      this.selectedDate = interview.date
      this.selectedTime = `From ${interview.fromTime} To ${interview.toTime}`
      this.InterviewTimeID = interview.id
    }
  }
}

  
interface DayWithInterviews {
  day: number;  
  interviews: InterviewTimeTable[];  
}