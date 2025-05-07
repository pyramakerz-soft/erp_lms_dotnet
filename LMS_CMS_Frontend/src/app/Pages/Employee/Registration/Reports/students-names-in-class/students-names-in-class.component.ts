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
import { PdfPrintComponent } from '../../../../../Component/pdf-print/pdf-print.component';

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
    public reportsService: ReportsService,
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
        console.log("fdfd",this.school)
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
    this.showPDF = true;
    setTimeout(() => {
      const printContents = document.getElementById("Data")?.innerHTML;
      if (!printContents) {
        console.error("Element not found!");
        return;
      }
  
      // Create a print-specific stylesheet
      const printStyle = `
        <style>
          @page { size: auto; margin: 0mm; }
          body { 
            margin: 0; 
          }
  
          @media print {
            body > *:not(#print-container) {
              display: none !important;
            }
            #print-container {
              display: block !important;
              position: static !important;
              top: auto !important;
              left: auto !important;
              width: 100% !important;
              height: auto !important;
              background: white !important;
              box-shadow: none !important;
              margin: 0 !important;
            }
          }
        </style>
      `;
  
      // Create a container for printing
      const printContainer = document.createElement('div');
      printContainer.id = 'print-container';
      printContainer.innerHTML = printStyle + printContents;
  
      // Add to body and print
      document.body.appendChild(printContainer);
      window.print();
      
      // Clean up
      setTimeout(() => {
        document.body.removeChild(printContainer);
        this.showPDF = false;
      }, 100);
    }, 500);
  }

  DownloadAsPDF() {
    this.showPDF = true;
    console.log(this.school)
    setTimeout(() => {
      this.pdfComponentRef.downloadPDF(); // Call manual download
      setTimeout(() => this.showPDF = false, 2000);
    }, 500);
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
