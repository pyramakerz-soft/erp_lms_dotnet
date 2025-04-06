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
import FileSaver, { saveAs } from 'file-saver';
import * as ExcelJS from 'exceljs'

@Component({
  selector: 'app-students-names-in-class',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './students-names-in-class.component.html',
  styleUrl: './students-names-in-class.component.css'
})
export class StudentsNamesInClassComponent {
 
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
    this.AcademicYearId = 0
    this.GradeId = 0
    this.ClassId = 0 
    this.AcademicYears  = []
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
    let element = document.getElementById("Data");
    if (!element) {
      console.error("Element not found!");
      return;
    }
    
    element.classList.remove("hidden"); 
    
    setTimeout(() => {
      this.reportsService.PrintPDF("List of students' names in class") 
  
      setTimeout(() => {
        element.classList.add("hidden"); 
      }, 1000); 
    }, 200); 
  } 

  DownloadAsPDF() {  
    let element = document.getElementById("Data");
    if (!element) {
      console.error("Element not found!");
      return;
    }
    
    element.style.display = 'block'; 
    element.style.top = '0px';
    element.style.left = '0px';
    element.style.zIndex = '-5';
    
    setTimeout(() => {
      this.reportsService.DownloadAsPDF("List of students' names in class")  
  
      setTimeout(() => {
        element.style.display = 'none';
      }, 1000); 
    }, 200); 
  }

  async DownloadAsExcel() {  
    const workbook = new ExcelJS.Workbook();

    let base64Image = '';
    if (this.school.reportImage.startsWith('http')) {
      base64Image = await this.reportsService.getBase64ImageFromUrl(this.school.reportImage);
    } else {
      base64Image = this.school.reportImage; 
    }

    const worksheet = workbook.addWorksheet("List of students' names in class"); 
    worksheet.mergeCells('A1:E1');
    worksheet.getCell('A1').value = this.school.reportHeaderOneEn;
    worksheet.getCell('A1').font = { bold: true, size: 14 };
    worksheet.getCell('A1').alignment = { horizontal: 'left' };

    worksheet.mergeCells('F1:J1');
    worksheet.getCell('F1').value = this.school.reportHeaderOneAr;
    worksheet.getCell('F1').font = { bold: true, size: 14 };
    worksheet.getCell('F1').alignment = { horizontal: 'right' };

    worksheet.mergeCells('A2:E2');
    worksheet.getCell('A2').value = this.school.reportHeaderTwoEn;
    worksheet.getCell('A2').font = { size: 12 };
    worksheet.getCell('A2').alignment = { horizontal: 'left' };

    worksheet.mergeCells('F2:J2');
    worksheet.getCell('F2').value = this.school.reportHeaderTwoAr;
    worksheet.getCell('F2').font = { size: 12 };
    worksheet.getCell('F2').alignment = { horizontal: 'right' };

    if (base64Image) {
      const imageId = workbook.addImage({
        base64: base64Image.split(',')[1], 
        extension: 'png',
      });
  
      worksheet.addImage(imageId, {
        tl: { col: 4, row: 0 },
        ext: { width: 100, height: 50 },
      });
    }

    worksheet.addRow([]);
    worksheet.addRow([`Class: ${this.class.name}`]).font = { bold: true, size: 12 };
    worksheet.addRow([`Number of Students: ${this.studentsCount}`]).font = { bold: true, size: 12 };
    worksheet.addRow([`Date: ${this.date}`]).font = { bold: true, size: 12 };
    worksheet.addRow([]);
   
    const headerRow = worksheet.addRow(['ID', 'Name', 'Mobile', 'Nationality', 'Gender']);
    headerRow.font = { bold: true, color: { argb: 'FFFFFF' } };
    headerRow.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: '4F81BD' } };
    headerRow.eachCell((cell) => {
      cell.border = { bottom: { style: 'thin' } };
    });
   
    this.StudentData.forEach((row) => {
      worksheet.addRow([row.id, row.en_name, row.mobile, row.nationalityName, row.genderName]);
    });
   
    worksheet.columns.forEach((column) => {
      column.width = 20;
    });
   
    const buffer = await workbook.xlsx.writeBuffer();
    FileSaver.saveAs(new Blob([buffer]), "List of students' names in class.xlsx"); 
  }
}
