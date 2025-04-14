import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
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
import FileSaver, { saveAs } from 'file-saver';
import * as ExcelJS from 'exceljs'
import { PdfPrintComponent } from '../../../../../Component/pdf-print/pdf-print.component';
import html2pdf from 'html2pdf.js';

@Component({
  selector: 'app-students-names-in-class',
  standalone: true,
  imports: [FormsModule, CommonModule, PdfPrintComponent],
  templateUrl: './students-names-in-class.component.html',
  styleUrl: './students-names-in-class.component.css'
})
export class StudentsNamesInClassComponent {

  SchoolId: number = 0
  AcademicYearId: number = 0
  GradeId: number = 0
  ClassId: number = 0
  Schools: School[] = []
  AcademicYears: AcademicYear[] = []
  Grades: Grade[] = []
  Classrooms: Classroom[] = []

  StudentData: Student[] = []
  class: Classroom = new Classroom()
  school: School = new School()
  date: string = ""
  studentsCount = 0

  showTable = false

  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');
  DomainName: string = '';
  UserID: number = 0;

  direction: string = "";

  showPDF = false;
  AcademicYearName: string = ""
  GradeName: string = ""
  @ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;

  constructor(
    public account: AccountService,
    public DomainServ: DomainService,
    public ApiServ: ApiService,
    public SchoolServ: SchoolService,
    public GradeServ: GradeService,
    public classroomService: ClassroomService,
    public acadimicYearService: AcadimicYearService,
    public studentService: StudentService,
    public reportsService: ReportsService
  ) { }

  ngOnInit() {
    this.direction = document.dir || 'ltr';

    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();

    this.getSchool()
  }

  getSchool() {
    this.Schools = []
    this.SchoolServ.Get(this.DomainName).subscribe(
      data => {
        this.Schools = data
      }
    )
  }

  onSchoolChange(event: Event) {
    this.AcademicYearId = 0
    this.GradeId = 0
    this.ClassId = 0
    this.AcademicYears = []
    this.Grades = []
    this.Classrooms = []
    this.showTable = false

    this.StudentData = []
    this.class = new Classroom()
    this.school = new School()
    this.date = ""
    this.studentsCount = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.SchoolId = Number(selectedValue)
    if (this.SchoolId) {
      this.GetYearData();
      this.GetGradeData();
    }
  }

  onGradeChange(event: Event) {
    this.ClassId = 0
    this.Classrooms = []
    this.showTable = false

    this.StudentData = []
    this.class = new Classroom()
    this.school = new School()
    this.date = ""
    this.studentsCount = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.GradeId = Number(selectedValue)
    if (this.GradeId) {
      this.GradeName = this.Grades.find(s => s.id == this.GradeId)?.name ?? ""
      this.GetClassData();
    }
  }

  onYearChange(event: Event) {
    this.ClassId = 0
    this.Classrooms = []
    this.showTable = false

    this.StudentData = []
    this.class = new Classroom()
    this.school = new School()
    this.date = ""
    this.studentsCount = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.AcademicYearId = Number(selectedValue)
    if (this.AcademicYearId) {
      this.AcademicYearName = this.AcademicYears.find(s => s.id == this.AcademicYearId)?.name ?? ""
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
    const element = document.getElementById("Data");
    if (!element) {
      console.error("Element not found!");
      return;
    }
    this.showPDF = true;
    setTimeout(() => {
      element.classList.remove("hidden");
      setTimeout(() => {
        const printContents = element.innerHTML;
        const printWindow = window.open('', '_blank', 'width=800,height=600');
        if (!printWindow) return;
        printWindow.document.open();
        printWindow.document.write(`
          <html>
            <head>
              <title>Print</title>
              <style>
                * { font-family: sans-serif; }
                table { border-collapse: collapse; width: 100%; }
                th, td { border: 1px solid #ccc; padding: 8px; text-align: left; }
                @media print {
                  body { margin: 1cm; }
                }
              </style>
            </head>
            <body onload="window.print(); window.close();">
              ${printContents}
            </body>
          </html>
        `);
        printWindow.document.close();
        setTimeout(() => {
          element.classList.add("hidden");
          this.showPDF = false;
        }, 1000);
  
      }, 200); // allow component to render
    }, 100);
  }
  
  // DownloadAsPDF() {
  //   let element = document.getElementById("Data");
  //   console.log("Element", element)
  //   if (!element) {
  //     console.error("Element not found!");
  //     return;
  //   }

  //   element.style.display = 'block';
  //   element.style.top = '0px';
  //   element.style.left = '0px';
  //   element.style.zIndex = '-5';

  //   setTimeout(() => {
  //     this.reportsService.DownloadAsPDF("List of students' names in class")

  //     setTimeout(() => {
  //       element.style.display = 'none';
  //     }, 1000);
  //   }, 200);
  // }

  DownloadAsPDF() {
    this.showPDF = true;
    setTimeout(() => {
      setTimeout(() => this.showPDF = false, 2000);
    }, 500); // give DOM time to render <app-pdf-print>
  }

  async DownloadAsExcel() {
    await this.reportsService.generateExcelReport({
      mainHeader: {
        en: this.school.reportHeaderOneEn,
        ar: this.school.reportHeaderOneAr
      },
      subHeaders: [
        { en: this.school.reportHeaderTwoEn, ar: this.school.reportHeaderTwoAr },
      ],
      infoRows: [
        { key: 'Class', value: this.class.name },
        { key: 'Number of Students', value: this.studentsCount },
        { key: 'Date', value: this.date },
        { key: 'Session', value: '2024/2025' },
        { key: 'School', value: this.school.name },
        { key: 'Year', value: this.AcademicYearName },
        { key: 'Grade', value: this.GradeName }

      ],
      reportImage: this.school.reportImage,
      filename: "List of students' names in class.xlsx",
      tables: [
        {
          title: "Students List",
          headers: ['id', 'en_name', 'mobile', 'nationalityName', 'genderName'],
          data: this.StudentData.map((row) => [row.id, row.en_name, row.mobile, row.nationalityName, row.genderName])
        }
      ]
    });
  }

}
