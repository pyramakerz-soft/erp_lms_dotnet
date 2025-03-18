import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TableComponent } from '../../../../Component/reuse-table/reuse-table.component';
import Swal from 'sweetalert2';
import { MedicalHistoryService } from '../../../../Services/Employee/Clinic/medical-history.service';
import { ApiService } from '../../../../Services/api.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { StudentService } from '../../../../Services/student.service';
import { MedicalHistory } from '../../../../Models/Clinic/MedicalHistory';

@Component({
  selector: 'app-medical-history',
  standalone: true,
  imports: [FormsModule, CommonModule, TableComponent],
  templateUrl: './medical-history.component.html',
  styleUrls: ['./medical-history.component.css'],
})
export class MedicalHistoryComponent implements OnInit {
  @ViewChild('firstReportInput') firstReportInput!: ElementRef;
  @ViewChild('secReportInput') secReportInput!: ElementRef;

  firstReportPreview: string | null = null;
  secReportPreview: string | null = null;

  headers: string[] = ['ID', 'School', 'Grade', 'Class', 'Student', 'Details', 'Permanent Drug', 'Date', 'Actions'];
  keys: string[] = ['id', 'school', 'grade', 'classRoom', 'student', 'details', 'permanentDrug', 'insertedAt'];
  medicalHistories: any[] = [];
  isModalVisible = false;
  editMode = false;
  medicalHistory: MedicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);

  schools: any[] = [];
  grades: any[] = [];
  classes: any[] = [];
  students: any[] = [];

  validationErrors: { [key: string]: string } = {};

  constructor(
    private medicalHistoryService: MedicalHistoryService,
    private apiService: ApiService,
    private schoolService: SchoolService,
    private gradeService: GradeService,
    private classroomService: ClassroomService,
    private studentService: StudentService
  ) {}

   ngOnInit(): void {
    this.loadMedicalHistories();
    this.loadDropdownOptions();
  }

  async loadMedicalHistories() {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.medicalHistoryService.Get(domainName));
      console.log(data);

      this.medicalHistories = data.map(item => ({
        ...item,
        insertedAt: new Date(item.insertedAt).toLocaleDateString(),
        actions: { edit: true, delete: true },
      }));
    } catch (error) {
      console.error('Error loading medical histories:', error);
    }
  }

  async loadDropdownOptions() {
    try {
      const domainName = this.apiService.GetHeader();
      this.schools = await firstValueFrom(this.schoolService.Get(domainName));
      this.grades = await firstValueFrom(this.gradeService.Get(domainName));
      this.classes = await firstValueFrom(this.classroomService.Get(domainName));
      const studentsData = await firstValueFrom(this.studentService.GetAll(domainName));
      this.students = studentsData.map(student => ({ id: student.id, name: student.en_name }));
    } catch (error) {
      console.error('Error loading dropdown options:', error);
      Swal.fire('Error', 'Failed to load dropdown options. Please try again later.', 'error');
    }
  }
    onSchoolChange(event: Event) {
    this.medicalHistory.gradeId = 0;
    this.medicalHistory.classRoomID = 0;
    this.medicalHistory.studentId = 0;
    this.grades = [];
    this.classes = [];
    this.students = [];

    const selectedSchoolId = (event.target as HTMLSelectElement).value;
    if (selectedSchoolId) {
      this.loadGrades(Number(selectedSchoolId));
    }
  }
    onGradeChange(event: Event) {
    this.medicalHistory.classRoomID = 0;
    this.medicalHistory.studentId = 0;
    this.classes = [];
    this.students = [];

    const selectedGradeId = (event.target as HTMLSelectElement).value;
    if (selectedGradeId) {
      this.loadClasses(Number(selectedGradeId));
    }
  }
    onClassChange(event: Event) {
    this.medicalHistory.studentId = 0;
    this.students = [];

    const selectedClassId = (event.target as HTMLSelectElement).value;
    if (selectedClassId) {
      this.loadStudents(Number(selectedClassId));
    }
  }
    async loadGrades(schoolId: number) {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.gradeService.GetBySchoolId(schoolId, domainName));
      this.grades = data;
    } catch (error) {
      console.error('Error loading grades:', error);
      Swal.fire('Error', 'Failed to load grades.', 'error');
    }
  }

  async loadClasses(gradeId: number) {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.classroomService.GetByGradeId(gradeId, domainName));
      this.classes = data;
    } catch (error) {
      console.error('Error loading classes:', error);
      Swal.fire('Error', 'Failed to load classes.', 'error');
    }
  }

  async loadStudents(classId: number) {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.studentService.GetByClassID(classId, domainName));
      this.students = data.map(student => ({ id: student.id, name: student.en_name }));
    } catch (error) {
      console.error('Error loading students:', error);
      Swal.fire('Error', 'Failed to load students.', 'error');
    }
  }

  openModal(id?: number) {
    this.isModalVisible = true;
    if (id) {
      this.editMode = true;
      const existingHistory = this.medicalHistories.find(mh => mh.id === id);
      if (existingHistory) {
        this.medicalHistory = { ...existingHistory };
        this.firstReportPreview = existingHistory.firstReport; // Set the preview to the existing file link
        this.secReportPreview = existingHistory.secReport; // Set the preview to the existing file link
      }
    } else {
      this.editMode = false;
      this.medicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);
    }
  }

  closeModal() {
    this.isModalVisible = false;
    this.medicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);
    this.validationErrors = {};
    this.firstReportPreview = null;
    this.secReportPreview = null;
  }

  onInputValueChange(event: { field: string; value: any }) {
    const { field, value } = event;
    (this.medicalHistory as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    } else {
      this.validationErrors[field] = `*${this.capitalizeField(field)} is required`;
    }
  }

  capitalizeField(field: string): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

onFileUpload(event: Event, field: 'firstReport' | 'secReport') {
  const input = event.target as HTMLInputElement;
  if (input.files && input.files[0]) {
    const file = input.files[0];
    const fileType = file.type;

    // Validate file type (image or video)
    if (fileType.startsWith('image/') || fileType.startsWith('video/')) {
      this.medicalHistory[field] = file; // Store the file in the medicalHistory object

      // Create a preview URL for the file
      const reader = new FileReader();
      reader.onload = (e: any) => {
        if (field === 'firstReport') {
          this.firstReportPreview = e.target.result; // Update the preview for firstReport
        } else if (field === 'secReport') {
          this.secReportPreview = e.target.result; // Update the preview for secReport
        }
      };
      reader.readAsDataURL(file);
    } else {
      alert('Invalid file type. Please upload an image or video.');
    }
  }
}

async saveMedicalHistory() {
  if (this.isFormValid()) {
    try {
      const domainName = this.apiService.GetHeader();

      if (this.editMode) {
        await firstValueFrom(this.medicalHistoryService.Edit(this.medicalHistory, domainName));
      } else {
        await firstValueFrom(this.medicalHistoryService.Add(this.medicalHistory, domainName));
      }

      this.loadMedicalHistories();
      this.closeModal();
    } catch (error) {
      console.error('Error saving medical history:', error);
      Swal.fire('Error', 'Failed to save medical history. Please try again later.', 'error');
    }
  }
}

isFormValid(): boolean {
  let isValid = true;

  // Reset validation errors
  this.validationErrors = {};

  // Validate School
  if (!this.medicalHistory.schoolId || this.medicalHistory.schoolId === 0) {
    this.validationErrors['schoolId'] = '*School is required';
    isValid = false;
  }

  // Validate Grade
  if (!this.medicalHistory.gradeId || this.medicalHistory.gradeId === 0) {
    this.validationErrors['gradeId'] = '*Grade is required';
    isValid = false;
  }

  // Validate Class
  if (!this.medicalHistory.classRoomID || this.medicalHistory.classRoomID === 0) {
    this.validationErrors['classRoomID'] = '*Class is required';
    isValid = false;
  }

  // Validate Student
  if (!this.medicalHistory.studentId || this.medicalHistory.studentId === 0) {
    this.validationErrors['studentId'] = '*Student is required';
    isValid = false;
  }

  return isValid;
}

  deleteMedicalHistory(row: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this medical history!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#2E3646',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.isConfirmed) {
        const domainName = this.apiService.GetHeader();
        this.medicalHistoryService.Delete(row.id, domainName).subscribe({
          next: () => {
            this.loadMedicalHistories();
            Swal.fire('Deleted!', 'The medical history has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting medical history:', error);
            Swal.fire('Error', 'Failed to delete medical history. Please try again later.', 'error');
          },
        });
      }
    });
  }
}