import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { Classroom } from '../../../../Models/LMS/classroom';
import { Employee } from '../../../../Models/Employee/employee';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { EmployeeService } from '../../../../Services/Employee/employee.service';
import { EvaluationEmployeeAdd } from '../../../../Models/LMS/evaluation-employee-add';
import { Template } from '../../../../Models/LMS/template';
import { EvaluationTemplateService } from '../../../../Services/Employee/LMS/evaluation-template.service';
import { School } from '../../../../Models/school';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { EvaluationEmployeeService } from '../../../../Services/Employee/LMS/evaluation-employee.service';

@Component({
  selector: 'app-evaluation',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './evaluation.component.html',
  styleUrl: './evaluation.component.css'
})
export class EvaluationComponent {
  EvaluationEmployee:EvaluationEmployeeAdd = new EvaluationEmployeeAdd()
  Templates: Template[] = []
  Employees: Employee[] = []
  Classs: Classroom[] = []
  Periods: number[] = []
  Schools: School[] = []
  SchoolID: number = 0;
  
  DomainName: string = '';
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData( '', 0, 0, 0, 0, '', '', '', '', '' );

  constructor(
    public account: AccountService, 
    public ApiServ: ApiService, 
    public classroomService: ClassroomService, 
    public employeeService: EmployeeService,
    public SchoolServ: SchoolService,
    public templateServ: EvaluationTemplateService,
    public evaluationEmployeeService: EvaluationEmployeeService
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.getSchoolData()
    this.getEmployeeData()
    this.getTemplateData()
  }
 
  getClassData() {
    this.Classs = [];
    this.classroomService.GetBySchoolId(this.SchoolID, this.DomainName).subscribe((data) => {
      this.Classs = data;
    });
  }
 
  getEmployeeData() {
    this.Employees = [];
    this.employeeService.Get_Employees(this.DomainName).subscribe((data) => {
      this.Employees = data;
    });
  }
 
  getTemplateData() {
    this.Classs = [];
    this.templateServ.Get(this.DomainName).subscribe((data) => {
      this.Templates = data;
    });
  }

  getSchoolData() {
    this.Schools = []
    this.SchoolServ.Get(this.DomainName).subscribe(
      data => {
        this.Schools = data 
      }
    )
  }

  onSchoolChange(event: Event) {
    this.Classs = [];
    this.Periods = []
    this.EvaluationEmployee.period = 0
    this.EvaluationEmployee.classroomID = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.SchoolID = Number(selectedValue)
    if (this.SchoolID) {
      this.getClassData();

      let SelectedSchool = new School()
      this.Schools.forEach(element => {
        if(element.id == this.SchoolID){
          SelectedSchool = element
        }
      });

      for (let index = 0; index < SelectedSchool.maximumPeriodCountTimeTable; index++) {
        this.Periods.push(index + 1)
      } 
    }
  }

  ViewTemplate(){
    this.EvaluationEmployee.evaluatorID = this.UserID
    console.log(this.EvaluationEmployee)
  }
}
