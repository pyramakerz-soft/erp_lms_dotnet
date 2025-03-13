import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HygieneFormTableComponent } from '../hygiene-form-table/hygiene-form-table.component';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { ApiService } from '../../../../../Services/api.service';
import Swal from 'sweetalert2';
import { HygieneTypesService } from '../../../../../Services/Employee/Clinic/hygiene-type.service';
import { HygieneTypes } from '../../../../../Models/Clinic/hygiene-types';
import { GradeService } from '../../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../../Services/Employee/LMS/classroom.service';
import { Grade } from '../../../../../Models/LMS/grade';
import { Classroom } from '../../../../../Models/LMS/classroom';
import { School } from '../../../../../Models/school';
import { Student } from '../../../../../Models/student';
import { SchoolService } from '../../../../../Services/Employee/school.service';
import { StudentService } from '../../../../../Services/student.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-create-hygiene-form',
  standalone: true,
  imports: [CommonModule, FormsModule, HygieneFormTableComponent],
  templateUrl: './create-hygiene-form.component.html',
  styleUrls: ['./create-hygiene-form.component.css'],
})
export class CreateHygieneFormComponent implements OnInit {
  schools: School[] = [];
  grades: Grade[] = [];
  classes: Classroom[] = [];
  students: Student[] = [];
  hygieneTypes: HygieneTypes[] = [];

  selectedSchool: number | null = null;
  selectedGrade: number | null = null;
  selectedClass: number | null = null;
  selectedDate: string = '';

  constructor(
    private http: HttpClient,
    private router: Router,
    private apiService: ApiService,
    private hygieneTypesService: HygieneTypesService,
    private schoolService: SchoolService,
    private gradeService: GradeService,
    private classroomService: ClassroomService,
    private studentService: StudentService
  ) {}

  ngOnInit(): void {
    this.loadHygieneTypes();
    this.loadSchools();
  }

  async loadHygieneTypes() {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.hygieneTypesService.Get(domainName));
      this.hygieneTypes = data;
    } catch (error) {
      console.error('Error loading hygiene types:', error);
      Swal.fire('Error', 'Failed to load hygiene types.', 'error');
    }
  }

  async loadSchools() {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.schoolService.Get(domainName));
      this.schools = data;
    } catch (error) {
      console.error('Error loading schools:', error);
      Swal.fire('Error', 'Failed to load schools.', 'error');
    }
  }

  async loadGrades() {
    if (this.selectedSchool) {
      try {
        const domainName = this.apiService.GetHeader();
        const data = await firstValueFrom(this.gradeService.GetBySchoolId(this.selectedSchool, domainName));
        this.grades = data;
      } catch (error) {
        console.error('Error loading grades:', error);
        Swal.fire('Error', 'Failed to load grades.', 'error');
      }
    }
  }

  async loadClasses() {
    if (this.selectedGrade) {
      try {
        const domainName = this.apiService.GetHeader();
        const data = await firstValueFrom(this.classroomService.GetByGradeId(this.selectedGrade, domainName));
        this.classes = data;
      } catch (error) {
        console.error('Error loading classes:', error);
        Swal.fire('Error', 'Failed to load classes.', 'error');
      }
    }
  }

async loadStudents() {
  if (this.selectedClass) {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.studentService.GetByClassID(this.selectedClass, domainName));

      if (data.length === 0) {
        // Handle the case when no students are found
        Swal.fire('Info', 'No students found for the selected class.', 'info');
        this.students = []; // Clear the students array
      } else {
        // Map the students and add dynamic properties
        this.students = data.map(student => ({
          ...student,
          attendance: null,
          comment: '',
          actionTaken: ''
        }));
      }
    } catch (error) {
      console.error('Error loading students:', error);
      Swal.fire('Error', 'Failed to load students.', 'error');
    }
  }
}

  onSchoolChange() {
    this.selectedGrade = null;
    this.selectedClass = null;
    this.grades = [];
    this.classes = [];
    this.students = [];
    this.loadGrades();
  }

  onGradeChange() {
    this.selectedClass = null;
    this.classes = [];
    this.students = [];
    this.loadClasses();
  }

  onClassChange() {
    this.students = [];
    this.loadStudents();
  }

saveHygieneForm() {
  const domainName = this.apiService.GetHeader();
  const token = localStorage.getItem('current_token');

  const headers = new HttpHeaders()
    .set('Domain-Name', domainName)
    .set('Authorization', `Bearer ${token}`)
    .set('Content-Type', 'application/json');

  const studentHygieneTypes = this.students.map(student => {
    const hygieneTypesIds = this.hygieneTypes
      .filter(ht => student[`hygieneType_${ht.id}`] === true)
      .map(ht => ht.id);

    return {
      studentId: student.id,
      hygieneTypesIds: hygieneTypesIds,
      attendance: student['attendance'], // Use bracket notation
      comment: student['comment'],       // Use bracket notation
      actionTaken: student['actionTaken'] // Use bracket notation
    };
  });

  const requestBody = {
    schoolId: this.selectedSchool,
    gradeId: this.selectedGrade,
    classRoomID: this.selectedClass,
    date: new Date(this.selectedDate).toISOString(),
    studentHygieneTypes: studentHygieneTypes
  };

  this.http.post(`${this.apiService.BaseUrl}/HygieneForm`, requestBody, { headers })
    .subscribe({
      next: (response) => {
        console.log('Hygiene form saved successfully:', response);
        Swal.fire('Success', 'Hygiene form saved successfully!', 'success');
        this.router.navigate(['/Employee/HygieneForm']);
      },
      error: (error) => {
        console.error('Error saving hygiene form:', error);
        Swal.fire('Error', 'Failed to save hygiene form.', 'error');
      }
    });
}
}