import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { School } from '../../../../../Models/school';
import { AcademicYear } from '../../../../../Models/LMS/academic-year';
import { Grade } from '../../../../../Models/LMS/grade';
import { Classroom } from '../../../../../Models/LMS/classroom';
import { TokenData } from '../../../../../Models/token-data';
import { AccountService } from '../../../../../Services/account.service';
import { ApiService } from '../../../../../Services/api.service';
import { DomainService } from '../../../../../Services/Employee/domain.service';
import { GradeService } from '../../../../../Services/Employee/LMS/grade.service';
import { SchoolService } from '../../../../../Services/Employee/school.service';
import { AcadimicYearService } from '../../../../../Services/Employee/LMS/academic-year.service';
import { ClassroomService } from '../../../../../Services/Employee/LMS/classroom.service';
import { Student } from '../../../../../Models/student';
import { StudentService } from '../../../../../Services/student.service';
import { ReportsService } from '../../../../../Services/shared/reports.service';

@Component({
  selector: 'app-students-names-in-class',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './students-names-in-class.component.html',
  styleUrl: './students-names-in-class.component.css'
})
export class StudentsNamesInClassComponent {
[x: string]: any;
  SchoolId:number = 0
  AcademicYearId:number = 0
  GradeId:number = 0
  ClassId:number = 0
  Schools:School[] = []
  AcademicYears:AcademicYear[] = []
  Grades:Grade[] = []
  Classrooms:Classroom[] = []
  
  StudentData:Student[] = []
  class:Classroom = new Classroom()
  school:School = new School()
  date:string = ""
  studentsCount = 0

  showTable = false

  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');
  DomainName: string = '';
  UserID: number = 0;

  direction: string = "";

  constructor(  
    public account: AccountService,
    public DomainServ: DomainService, 
    public ApiServ: ApiService,
    public SchoolServ: SchoolService,
    public GradeServ: GradeService,
    public classroomService: ClassroomService,
    public acadimicYearService: AcadimicYearService,
    public studentService: StudentService,
    public reportsService:ReportsService
  ) { }

  ngOnInit() {
    this.direction = document.dir || 'ltr';

    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();

    this.getSchool()
  }

  getSchool(){
    this.Schools = []
    this.SchoolServ.Get(this.DomainName).subscribe(
      data => {
        this.Schools = data
      }
    )
  }

  onSchoolChange(event: Event) { 
    const selectedValue = (event.target as HTMLSelectElement).value; 
    this.SchoolId = Number(selectedValue)
    if (this.SchoolId) {
      this.GetYearData(); 
      this.GetGradeData(); 
    }
  } 

  onGradeChange(event: Event) { 
    const selectedValue = (event.target as HTMLSelectElement).value; 
    this.GradeId = Number(selectedValue)
    if (this.GradeId) {
      this.GetClassData(); 
    }
  } 

  onYearChange(event: Event) { 
    const selectedValue = (event.target as HTMLSelectElement).value; 
    this.AcademicYearId = Number(selectedValue)
    if (this.GradeId) {
      this.GetClassData(); 
    }
  } 

  GetYearData() {
    this.AcademicYears = []
    this.acadimicYearService.GetBySchoolId(this.SchoolId, this.DomainName).subscribe((d) => {
      this.AcademicYears = d
    })
  }

  GetGradeData() {
    this.Grades = []
    this.GradeServ.GetBySchoolId(this.SchoolId, this.DomainName).subscribe((d) => {
      this.Grades = d
    })
  }

  GetClassData() {
    this.Classrooms = []
    this.classroomService.GetByGradeAndAcYearId(this.GradeId, this.AcademicYearId, this.DomainName).subscribe((d) => {
      this.Classrooms = d
    })
  }

  ViewReport() {
    this.StudentData = []
    this.showTable = true
    this.studentService.GetBySchoolGradeClassID(this.SchoolId, this.GradeId, this.ClassId, this.DomainName).subscribe(
      (d) => {
        this.StudentData = d.students
        this.class = d.class
        this.school = d.school
        this.studentsCount = d.studentsCount 
        this.date = d.date 
        this.date = this.formatDate(this.date, this.direction);
      }
    )
  }

  formatDate(dateString: string, dir: string): string {
    const date = new Date(dateString);
    const locale = dir === 'rtl' ? 'ar-EG' : 'en-US';  
    return date.toLocaleDateString(locale, { weekday: 'long', month: 'long', year: 'numeric' });
  }

  Print() {
  } 

  DownloadAsPDF() {
    this.reportsService.DownloadAsPDF("List of students' names in class")
  }

  DownloadAsExcel() {
  }
}
