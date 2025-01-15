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
import Swal from 'sweetalert2';
import { RegistrationFormForFormSubmissionForFiles } from '../../../../Models/Registration/registration-form-for-form-submission-for-files';

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
  registrationFormForFiles:RegistrationFormForFormSubmissionForFiles [] = []
  isFormSubmitted: boolean = false
  isGuardianEmailValid: boolean = true
  isMotherEmailValid: boolean = true

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

  selectedOptions: any[] = [];

  currentCategory = 1;

  emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;

  isSuccess:boolean = false

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
        this.RegistrationFormData.categories.sort((a, b) => a.orderInForm - b.orderInForm)
        this.RegistrationFormData.categories.forEach(element => {
          element.fields.sort((a, b) => a.orderInForm - b.orderInForm)
        });
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

  handleFileUpload(event:any, fieldId:number){
    const file: File = event.target.files[0];

    this.registrationFormForFiles.push({
      categoryFieldID: fieldId,
      selectedFile: file
    })
  }

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

  MultiOptionDataPush(fieldId:number , fieldTypeId:number, optionAnswer:number){
    if(fieldTypeId == 5){
      this.registrationForm.registerationFormSubmittions.push({
        categoryFieldID: fieldId,
        selectedFieldOptionID: optionAnswer,
        textAnswer: null
      })
    }
  }

  FillOptionData(){
    this.selectedOptions.forEach(element => {
      this.MultiOptionDataPush(element.fieldId, element.fieldTypeId, element.optionId)
    });
    this.selectedOptions = []
  }

  multiOptionHandling(event:Event, fieldId:number , fieldTypeId:number, optionId: number){
    const checkbox = event.target as HTMLInputElement;
    
    if (checkbox.checked) {
      this.addOptionToArray(fieldId, fieldTypeId, optionId);
    } else {
      this.removeOptionFromArray(fieldId, fieldTypeId, optionId);
    }
  }

  private addOptionToArray(fieldId: number, fieldTypeId: number, optionId: number): void {
    const option = { fieldId, fieldTypeId, optionId };
    this.selectedOptions.push(option);
  }

  private removeOptionFromArray(fieldId: number, fieldTypeId: number, optionId: number): void {
    this.selectedOptions = this.selectedOptions.filter(option => 
      !(option.fieldId === fieldId && option.fieldTypeId === fieldTypeId && option.optionId === optionId)
    );
  }

  validateNumber(event: any): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
        event.target.value = '';
    }
  }

  isFieldInvalid(field: any) {
    if(this.isFormSubmitted){
      const fieldSubmission = this.registrationForm.registerationFormSubmittions.find(
        (submission) => submission.categoryFieldID === field.id
      );

      const fieldSubmissionFile = this.registrationFormForFiles.find(
        (submission) => submission.categoryFieldID === field.id
      );

      let fieldData
      
      if (field.isMandatory) {
        if (field.fieldTypeID !== 6) {
          fieldData = fieldSubmission;
        }

        if (field.fieldTypeID === 6) {
          fieldData = fieldSubmissionFile;
        }

        if (fieldData) {
          return false;
        }

        if (field.fieldTypeID === 5) {
          return !this.selectedOptions.some(option => option.fieldId === field.id);
        }

        if (field.fieldTypeID === 6) {
            return !fieldSubmissionFile || !fieldSubmissionFile.selectedFile || 
                  !this.selectedOptions.some(option => option.fieldId === field.id);
        }

        return !fieldSubmission || !fieldSubmission.textAnswer || !fieldSubmission.selectedFieldOptionID;
      }
      return false;
    }
    return false
  }
  
  IsEmailValid(){
    this.registrationForm.registerationFormSubmittions.forEach(element => {
      if(element.categoryFieldID == 21){
        if(element.textAnswer){
          if( !this.emailPattern.test(element.textAnswer)){
            this.isGuardianEmailValid = false
            return false
          } else{
            this.isGuardianEmailValid = true
            return true
          }
        }
        return true
      }
      else if(element.categoryFieldID == 28){
        if(element.textAnswer){
          if( !this.emailPattern.test(element.textAnswer)){
            this.isMotherEmailValid = false
            return false
          } else{
            this.isMotherEmailValid = true
            return true
          }
        }
      }
      return true
    });

    return true
  }

  Save(){
    this.isFormSubmitted = true;

    this.FillOptionData()
    
    let valid = true;
    let EmptyFieldCat = []

    // Validate all fields
    for (const cat of this.RegistrationFormData.categories) {
      for (const field of cat.fields) {
        if (field.isMandatory && this.isFieldInvalid(field)) {
          valid = false;
          EmptyFieldCat.push(cat.orderInForm)
        }
      }
    }

    if (valid) {
      this.IsEmailValid()
      if(this.isMotherEmailValid && this.isGuardianEmailValid){
        this.registrationFormService.Add(this.registrationForm, this.registrationFormForFiles, this.DomainName).subscribe(
          (data) => {
            this.DoneSuccessfully()
          },
          (error) => {
            console.log(error.error)
            if(error.error == "Email Already Exists"){
              Swal.fire({
                icon: 'warning',
                title: 'Warning!',
                text: 'Guardian’s Email Already Exists',
                confirmButtonColor: '#FF7519',
              });
              this.goToCategory(2)
            }
          }
        )
      } else if(!this.isGuardianEmailValid){
        this.goToCategory(2)
      } else if(!this.isMotherEmailValid){
        this.goToCategory(3)
      }
    } else{
      this.goToCategory(EmptyFieldCat[0])
    }
  }

 
  navigateToNext() {
    if (this.currentCategory < this.RegistrationFormData.categories.length) {
      this.currentCategory++;
    }
  }

  goToCategory(categoryIndex: number) {
    this.currentCategory = categoryIndex;
  }

  DoneSuccessfully(){
    this.RegistrationFormData = new RegistrationForm()
    this.registrationForm = new RegistrationFormForFormSubmission()
    this.registrationFormForFiles = []

    this.isFormSubmitted = false
    this.isGuardianEmailValid = true
    this.isMotherEmailValid = true

    this.schools = []
    this.selectedSchool = null;
    this.Grades = []
    this.selectedGrade = null;
    this.AcademicYears = []
    this.selectedAcademicYear = null;
    this.Sections = []
  
    this.selectedOptions = [];

    //////

    this.isSuccess = true
  }
}
