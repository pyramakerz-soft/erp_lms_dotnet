import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Subject } from '../../../../Models/LMS/subject';
import { SubjectService } from '../../../../Services/Employee/LMS/subject.service';
import { SubjectCategoryService } from '../../../../Services/Employee/LMS/subject-category.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { SectionService } from '../../../../Services/Employee/LMS/section.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Grade } from '../../../../Models/LMS/grade';
import { Section } from '../../../../Models/LMS/section';
import { School } from '../../../../Models/school';
import { ApiService } from '../../../../Services/api.service';
import { SubjectCategory } from '../../../../Models/LMS/subject-category';

@Component({
  selector: 'app-add-edit-subject',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-edit-subject.component.html',
  styleUrl: './add-edit-subject.component.css'
})
export class AddEditSubjectComponent {
  editSubject:boolean = false
  subjectId:number = 0
  subject:Subject = new Subject()
  validationErrors: { [key in keyof Subject]?: string } = {};
  DomainName: string = "";
  selectedSchool: number | null = null;
  Schools: School[] = []
  selectedSection: number | null = null;
  Sections: Section[] = []
  Grades: Grade[] = []
  subjectCategories:SubjectCategory[] = []

  constructor(public subjectService: SubjectService, public subjectCategoryService: SubjectCategoryService, public dialogRef: MatDialogRef<AddEditSubjectComponent>, 
    public schoolService: SchoolService, public sectionService:SectionService, public gradeService:GradeService, public ApiServ:ApiService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
      this.editSubject = data.editSubject
      if(this.editSubject){
        this.subjectId = data.subjectId
      }
  }
      
  ngOnInit(){
    this.DomainName = this.ApiServ.GetHeader();

    this.getSubjectCategoryData() 
    this.getSchools()

    if(this.editSubject){
      this.GetSubjectById(this.subjectId)
    }
  }

  GetSubjectById(subjectId: number) {
    this.subjectService.GetByID(subjectId, this.DomainName).subscribe((data) => {
      this.subject = data;
    });
  }

  closeDialog(): void {
    this.subject= new Subject()
    this.subjectCategories = []
    this.Schools = []
    this.Sections = []
    this.Grades = []
    this.selectedSchool = null
    this.selectedSection = null

    if(this.editSubject){
      this.editSubject = false
    }
    this.validationErrors = {}; 

    this.dialogRef.close();
  }

  getSubjectCategoryData(){
    this.subjectCategoryService.Get(this.DomainName).subscribe(
      (data) => {
        this.subjectCategories = data;
      }
    )
  }

  getSchools(){
    this.schoolService.Get(this.DomainName).subscribe(
      (data) => {
        this.Schools = data;
      }
    )
  }

  getSections(){
    this.sectionService.Get(this.DomainName).subscribe(
      (data) => {
        this.Sections = data.filter((section) => this.checkSchool(section))
      }
    )
  }

  getGrades(){
    this.gradeService.Get(this.DomainName).subscribe(
      (data) => {
        this.Grades = data.filter((grade) => this.checkSection(grade))
      }
    )
  }

  checkSchool(section:Section) {
    return section.schoolID == this.selectedSchool
  }
 
  checkSection(grade:Grade) {
    return grade.sectionID == this.selectedSection
  }

  onSchoolChange(event: Event) {
    this.Sections = []
    this.Grades = []
    this.selectedSection = null
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.selectedSchool = Number(selectedValue)
    if (this.selectedSchool) {
      this.getSections(); 
    }
  }
 
  onSectionChange(event: Event) {
    this.Grades = []
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.selectedSection = Number(selectedValue)
    if (this.selectedSection) {
      this.getGrades(); 
    }
  }

  capitalizeField(field: keyof Subject): string {
      return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.subject) {
      if (this.subject.hasOwnProperty(key)) {
        const field = key as keyof Subject;
        if (!this.subject[field]) {
          if(field == "ar_name" || field == "en_name" || field == "creditHours" || field == "gradeID" || field == "numberOfSessionPerWeek" || field == "orderInCertificate"
             || field == "passByDegree"  || field == "totalMark"  || field == "subjectCategoryID"  || field == "subjectCode"
          ){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          } else if(field == "iconFile"){
            if(!this.editSubject){
              this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
              isValid = false;
            }
          }
        } else {
          if(field == "en_name" || field == "ar_name"){
            if(this.subject.en_name.length > 100 || this.subject.ar_name.length > 100){
              this.validationErrors[field] = `*${this.capitalizeField(field)} cannot be longer than 100 characters`
              isValid = false;
            }
          } else{
            this.validationErrors[field] = '';
          }
        }
      }
    }
    return isValid;
  }

  validateNumber(event: any): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
        event.target.value = '';
    }
  }

  onIsHideChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.subject.hideFromGradeReport = isChecked
  }

  onInputValueChange(event: { field: keyof Subject, value: any }) {
    const { field, value } = event;
    (this.subject as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  onImageFileSelected(event: any) {
    const file: File = event.target.files[0];
    
    if (file) {
      if (file.size > 25 * 1024 * 1024) {
        this.validationErrors['iconFile'] = 'The file size exceeds the maximum limit of 25 MB.';
        this.subject.iconFile = null;
        return; 
      }
      if (file.type === 'image/jpeg' || file.type === 'image/png') {
        this.subject.iconFile = file; 
        this.validationErrors['iconFile'] = ''; 

        const reader = new FileReader();
        reader.readAsDataURL(file);
      } else {
        this.validationErrors['iconFile'] = 'Invalid file type. Only JPEG, JPG and PNG are allowed.';
        this.subject.iconFile = null;
        return; 
      }
    }
  }

  SaveSubject(){
    console.log(this.subject)
    if(this.isFormValid()){
      if(this.editSubject == false){
        (this.subjectService.Add(this.subject, this.DomainName)).subscribe(
          (result: any) => {
            this.closeDialog()
          },
          error => {
            console.log(error)
          }
        );
      } else{
        this.subjectService.Edit(this.subject, this.DomainName).subscribe(
          (result: any) => {
            this.closeDialog()
          },
          error => {
            console.log(error)
          }
        );
      }  
    }
  } 
}
