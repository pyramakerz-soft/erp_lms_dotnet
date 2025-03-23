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

  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');
  DomainName: string = '';
  UserID: number = 0;

  constructor(  
    public account: AccountService,
    public DomainServ: DomainService, 
    public ApiServ: ApiService,
    public SchoolServ: SchoolService,
    public GradeServ: GradeService,
    public classroomService: ClassroomService,
    public acadimicYearService: AcadimicYearService,
    public studentService: StudentService
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
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
    this.classroomService.GetByGradeId(this.GradeId, this.DomainName).subscribe((d) => {
      this.Classrooms = d
    })
  }

  ViewReport() {
    this.StudentData = []
    this.studentService.GetBySchoolYearGradeClassID(this.SchoolId, this.AcademicYearId, this.GradeId, this.ClassId, this.DomainName).subscribe((d) => {
      this.StudentData = d
    })
  }
}
