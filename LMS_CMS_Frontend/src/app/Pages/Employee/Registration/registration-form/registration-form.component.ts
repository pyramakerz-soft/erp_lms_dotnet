import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegistrationFormService } from '../../../../Services/Employee/Registration/registration-form.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { RegistrationForm } from '../../../../Models/Registration/registration-form';
import { RegistrationFormSubmission } from '../../../../Models/Registration/registration-form-submission';
import { RegistrationFormForFormSubmission } from '../../../../Models/Registration/registration-form-for-form-submission';
import { HttpClient } from '@angular/common/http';
import * as countries from 'countries-list';
import { School } from '../../../../Models/school';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { Grade } from '../../../../Models/LMS/grade';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { SectionService } from '../../../../Services/Employee/LMS/section.service';
import { Section } from '../../../../Models/LMS/section';

@Component({
  selector: 'app-registration-form',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './registration-form.component.html',
  styleUrl: './registration-form.component.css'
})
export class RegistrationFormComponent {
  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  RegistrationFormData:RegistrationForm = new RegistrationForm()
  registrationForm:RegistrationFormForFormSubmission = new RegistrationFormForFormSubmission()

  nationalities = Object.values(countries.countries).map(country => ({
    name: country.name
  }));

  schools:School[] = []
  selectedSchool: number | null = null;
  Grades:Grade[] = []
  selectedGrade: number | null = null;
  AcademicYears:AcademicYear[] = []
  selectedAcademicYear: number | null = null;
  Sections:Section[] = []

  currentCategory = 2;

  constructor(public account: AccountService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, public schoolService:SchoolService,
    public activeRoute: ActivatedRoute, public registrationFormService: RegistrationFormService, public router:Router, 
    public http: HttpClient, public academicYearServce:AcadimicYearService, public gradeServce:GradeService, public sectionServce:SectionService){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.getRegistrationFormData()
    this.getSchools()

    this.registrationForm.registrationFormID = 1
  }

  getRegistrationFormData(){
    this.registrationFormService.GetById(1, this.DomainName).subscribe(
      (data) => {
        this.RegistrationFormData = data
      }
    )
  }

  getSchools(){
    this.schoolService.Get(this.DomainName).subscribe(
      (data) => {
        this.schools = data
      }
    )
  }

  onSchoolChange(event: Event) {
    this.Grades = []
    this.AcademicYears = []
    this.Sections = []
    this.selectedAcademicYear = null
    this.selectedGrade = null
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.selectedSchool = Number(selectedValue)
    if (this.selectedSchool) {
      this.getAcademicYear(); 
      this.getSections(); 
    }
  }
 
  getAcademicYear(){
    this.academicYearServce.Get(this.DomainName).subscribe(
      (data) => {
        this.AcademicYears = data.filter((ac) => this.checkSchool(ac))
      }
    )
  }
 
  getSections(){
    this.sectionServce.Get(this.DomainName).subscribe(
      (data) => {
        this.Sections = data.filter((ac) => this.checkSchool(ac))
        this.Sections.forEach(element => {
          this.getGrades(element.id)
        });
      }
    )
  }
 
  getGrades(sectionId: number){
    this.gradeServce.GetBySectionId(sectionId, this.DomainName).subscribe(
      (data) => {
        data.forEach(element => {
          this.Grades.push(element)
        });
      }
    )
  }

  checkSchool(el:any) {
    return el.schoolID == this.selectedSchool
  }

  handleFileUpload(event:Event, id:number){}


  FillData(event:Event, fieldId:number , fieldTypeId:number){
    const selectedValue = (event.target as HTMLSelectElement).value;

    let answer = ""
    let option = null

    if(fieldTypeId == 1 || fieldTypeId == 2 || fieldTypeId == 3 || 
      (fieldTypeId == 4 && (fieldId == 3 || fieldId == 5 || fieldId == 6 || fieldId == 7 || fieldId == 8 || fieldId == 9 || fieldId == 14))){
      answer = selectedValue
    } else if(fieldTypeId == 4){
      option = parseInt(selectedValue) 
    }
    
    const existingElement = this.registrationForm.registerationFormSubmittions.find(
      (element) => element.categoryFieldID === fieldId
    );
  
    if (existingElement) {
      existingElement.textAnswer = selectedValue;
    } else {
      this.registrationForm.registerationFormSubmittions.push({
        categoryFieldID: fieldId,
        selectedFieldOptionID: option,
        textAnswer: answer,
      });
    }
  }

  validateNumber(event: any): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
        event.target.value = '';
    }
  }

  Save(){
    this.registrationFormService.Add(this.registrationForm, this.DomainName).subscribe(
      (data) => {
        console.log(data)
      },
      (error) => {
        console.log(error)
      }
    )
    console.log(this.registrationForm)
  }
}
