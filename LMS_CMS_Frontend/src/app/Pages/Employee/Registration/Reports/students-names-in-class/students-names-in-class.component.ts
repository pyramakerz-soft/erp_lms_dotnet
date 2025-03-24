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
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

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
    return date.toLocaleDateString(locale, { weekday: 'long', day: 'numeric', month: 'long', year: 'numeric' });
  }

  Print() {
    this.reportsService.PrintPDF()
  } 

  DownloadAsPDF() {
    this.reportsService.DownloadAsPDF("List of students' names in class")
  }

  DownloadAsExcel() { 
    const tableElement = document.getElementById("TableData");
    if (!tableElement) {
      console.error("Table not found!");
      return;
    }

    const html = `
      <html>
        <head>
          <meta charset="UTF-8">
          <style>
            body { font-family: Arial, sans-serif; padding: 20px; } 
            .header-table { 
              width: 100%; 
            } 
            .header-table td {
              padding: 10px;
              vertical-align: middle;
            }
            .header-right p{
              text-align: right; 
            }
            .header-right, .heade-left{
              text-align: right; 
              vertical-align: middle;
            }
            table { 
              border: 1px solid #BDBDBD; 
              width: 100%; 
              border-collapse: collapse; 
              background: #EBEBEB; 
            } 
            th, td {  
              text-align: left;  
            } 
            .text-center { text-align: center; }
            .text-base{
              font-weight: bold;
              font-size: 18px;
            }
            .text-sm{
              font-weight: lighter;
              font-size: 14px;
            }  
          </style>
        </head>
        <body>
          <table class="header-table">
            <tr>
              <td class="header-left">
                <p class="text-base">${this.school.reportHeaderOneEn}</p>
                <p class="text-sm">${this.school.reportHeaderTwoEn}</p>
              </td>
              <td class="header-center">
                <img src="${this.school.reportImage}" width="120" height="120">
              </td>
              <td class="header-right">
                <p class="text-base">${this.school.reportHeaderOneAr}</p>
                <p class="text-sm">${this.school.reportHeaderTwoAr}</p>
              </td>
            </tr>
          </table>

          <div class="my-10">
            <p class="text-base">Class: <span class="text-sm">${this.class.name}</span></p>
            <p class="text-base">Number Of Students: <span class="text-sm">${this.studentsCount}</span></p> 
            <p class="text-base">Date: <span class="text-sm">${this.date}</span></p>  
          </div>

          <div class="table-container">
            ${tableElement.outerHTML}
          </div>
        </body>
      </html>
    `;

    const blob = new Blob([html], { type: "application/vnd.ms-excel" });
    saveAs(blob, "List of students' names in class.xls");
  }
}
