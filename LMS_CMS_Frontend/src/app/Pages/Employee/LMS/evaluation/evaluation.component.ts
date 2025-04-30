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
import { EvaluationTemplateGroupService } from '../../../../Services/Employee/LMS/evaluation-template-group.service';
import { EvaluationTemplateGroups } from '../../../../Models/LMS/evaluation-template-groups';
import { EvaluationEmployeeStudentBookCorrectionAdd } from '../../../../Models/LMS/evaluation-employee-student-book-correction-add';
import { EvaluationEmployeeQuestionAdd } from '../../../../Models/LMS/evaluation-employee-question-add';
import { EvaluationBookCorrection } from '../../../../Models/LMS/evaluation-book-correction';
import { EvaluationBookCorrectionService } from '../../../../Services/Employee/LMS/evaluation-book-correction.service';
import { Student } from '../../../../Models/student';
import { StudentService } from '../../../../Services/student.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

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
  EvaluationTemplateGroups: EvaluationTemplateGroups[] = []
  CorrectionBooks: EvaluationBookCorrection[] = []
  Students: Student[] = []
  SchoolID: number = 0;
  
  DomainName: string = '';
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData( '', 0, 0, 0, 0, '', '', '', '', '' );

  isNarrationOpen = true
  isCorrectionOpen = true

  selectedStudentIds: number[] = [];
  allSelected: boolean = false;  
  studentEvaluationCorectionBooks: EvaluationEmployeeStudentBookCorrectionAdd[] = [];

  currentStep = 1

  constructor(
    public account: AccountService, 
    public ApiServ: ApiService, 
    public classroomService: ClassroomService, 
    public employeeService: EmployeeService,
    public SchoolServ: SchoolService,
    public templateServ: EvaluationTemplateService,
    public evaluationTemplateGroupService: EvaluationTemplateGroupService,
    public evaluationBookCorrectionService: EvaluationBookCorrectionService,
    public studentService: StudentService,
    public evaluationEmployeeService: EvaluationEmployeeService 
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.getSchoolData()
    this.getEmployeeData()
    this.getTemplateData()
    this.getCorrectionBookData()
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
    this.Templates = [];
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

  getStudentData() {
    this.Students = []
    this.studentService.GetByClassID(this.EvaluationEmployee.classroomID, this.DomainName).subscribe(
      data => {
        this.Students = data 
      }
    )
  }
  
  getTemplateGroupQuestionData() {
    this.EvaluationTemplateGroups = [];
    this.evaluationTemplateGroupService.GetByTemplateID(this.EvaluationEmployee.evaluationTemplateID, this.DomainName).subscribe((data) => {
      this.EvaluationTemplateGroups = data;  
      for (let group = 0; group < this.EvaluationTemplateGroups.length; group++) {
        this.EvaluationTemplateGroups[group].isOpen = true
        for (let question = 0; question < this.EvaluationTemplateGroups[group].evaluationTemplateGroupQuestions.length; question++){
          let evaluationEmployeeQuestionAdd = new EvaluationEmployeeQuestionAdd()
          evaluationEmployeeQuestionAdd.evaluationTemplateGroupQuestionID = this.EvaluationTemplateGroups[group].evaluationTemplateGroupQuestions[question].id
          this.EvaluationEmployee.evaluationEmployeeQuestionsList.push(evaluationEmployeeQuestionAdd)
        } 
      }  
    });
  }
 
  getCorrectionBookData() {
    this.CorrectionBooks = [];
    this.evaluationBookCorrectionService.Get(this.DomainName).subscribe((d) => {
      this.CorrectionBooks = d
    });
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

  onClassChange() {
    this.Students = []
    this.EvaluationEmployee.evaluationEmployeeStudentBookCorrectionsList = []
    this.getStudentData()
  }

  onTemplateChange(){
    this.EvaluationTemplateGroups = []
    this.EvaluationEmployee.evaluationEmployeeQuestionsList = []
    this.EvaluationEmployee.evaluationEmployeeStudentBookCorrectionsList = []
    this.EvaluationEmployee.narration = ''
    this.EvaluationEmployee.evaluationBookCorrectionID = 0
  } 

  ViewTemplate(){
    this.EvaluationEmployee.evaluatorID = this.UserID
    this.getTemplateGroupQuestionData()
  }

  toggleQuestionVisibility(index: number) {
    this.EvaluationTemplateGroups[index].isOpen = !this.EvaluationTemplateGroups[index].isOpen;
  }

  toggleNarrationVisibility() {
    this.isNarrationOpen = !this.isNarrationOpen;
  }

  toggleCorrectionVisibility() {
    this.isCorrectionOpen = !this.isCorrectionOpen;
  }

  ////////////////////////////////////////// Questions Mark ///////////////////////////////////////////////

  onQuestionInputValueChange(event: Event, question:number) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.EvaluationEmployee.evaluationEmployeeQuestionsList.forEach(element => {
      if(element.evaluationTemplateGroupQuestionID == question){
        element.mark = +selectedValue
      }
    }); 
  }
  
  onNotesInputValueChange(event: Event, question:number) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.EvaluationEmployee.evaluationEmployeeQuestionsList.forEach(element => {
      if(element.evaluationTemplateGroupQuestionID == question){
        element.note = selectedValue
      }
    });  
  } 

  getQuestionMark(questionID: number): number {
    return this.EvaluationEmployee.evaluationEmployeeQuestionsList.find(q => q.evaluationTemplateGroupQuestionID === questionID)?.mark || 0;
  }
  
  getQuestionNote(questionID: number): string {
    return this.EvaluationEmployee.evaluationEmployeeQuestionsList.find(q => q.evaluationTemplateGroupQuestionID === questionID)?.note || '';
  }
  
  ////////////////////////////////////////// Correction Book ///////////////////////////////////////////////

  getOrCreateEvaluation(studentID: number): EvaluationEmployeeStudentBookCorrectionAdd {
    let evalEntry = this.studentEvaluationCorectionBooks.find(e => e.studentID === studentID);
    if (!evalEntry) {
      evalEntry = new EvaluationEmployeeStudentBookCorrectionAdd(0, studentID, '');
      this.studentEvaluationCorectionBooks.push(evalEntry);
    }
    return evalEntry;
  }
 
  isStudentSelected(id: number): boolean {
    return this.selectedStudentIds.includes(id);
  }

  toggleSelectAll(event: Event): void {
    const isChecked = (event.target as HTMLInputElement).checked;

    this.allSelected = isChecked;
    this.selectedStudentIds = isChecked ? this.Students.map(s => s.id) : []; 
  }

  toggleStudentSelection(event: Event, id: number): void {
    const isChecked = (event.target as HTMLInputElement).checked;

    if (isChecked) {
      this.selectedStudentIds.push(id);
    } else {
      this.selectedStudentIds = this.selectedStudentIds.filter(x => x !== id);
    }

    this.allSelected = this.selectedStudentIds.length === this.Students.length; 
  }
 
  updateRatingForStudent(star: number, id: number): void {
    const targetIDs = this.selectedStudentIds.includes(id) ? this.selectedStudentIds : [id];

    targetIDs.forEach(studentID => {
      const evalEntry = this.getOrCreateEvaluation(studentID);
      evalEntry.state = star;
    }); 
  }
 
  updateNotesForStudent(event: Event, id: number): void {
    const value = (event.target as HTMLTextAreaElement).value;
    const targetIDs = this.selectedStudentIds.includes(id) ? this.selectedStudentIds : [id];

    targetIDs.forEach(studentID => {
      const evalEntry = this.getOrCreateEvaluation(studentID);
      evalEntry.note = value;
    }); 
  }
 
  getNote(studentID: number): string {
    return this.studentEvaluationCorectionBooks.find(e => e.studentID === studentID)?.note || '';
  }

  getRating(studentID: number): number {
    return this.studentEvaluationCorectionBooks.find(e => e.studentID === studentID)?.state || 0;
  }

  ////////////////////////////////////////// Submit ///////////////////////////////////////////////

  Submit(){
    if(this.EvaluationEmployee.evaluationBookCorrectionID == 0){
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please Select Correction Book',
        confirmButtonText: 'Okay',
        customClass: { confirmButton: 'secondaryBg' },
      });
    } else{
      this.EvaluationEmployee.evaluationEmployeeStudentBookCorrectionsList = this.studentEvaluationCorectionBooks
      
      this.evaluationEmployeeService.Add(this.EvaluationEmployee, this.DomainName).subscribe(
        data => { 
          Swal.fire({
            icon: 'success', 
            text: 'Evaluated Successfully',
            confirmButtonText: 'Okay',
            customClass: { confirmButton: 'secondaryBg' },
          }).then(() => {
            window.location.reload();
          });
        }
      )
    }
  }
}
