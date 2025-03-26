import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import Swal from 'sweetalert2';
import { firstValueFrom } from 'rxjs';
import { MedicalHistory } from '../../../../../Models/Clinic/MedicalHistory';
import { MedicalHistoryService } from '../../../../../Services/Employee/Clinic/medical-history.service';
import { ApiService } from '../../../../../Services/api.service';
import { SchoolService } from '../../../../../Services/Employee/school.service';
import { GradeService } from '../../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../../Services/Employee/LMS/classroom.service';
import { StudentService } from '../../../../../Services/student.service';

@Component({
  selector: 'app-medical-history-modal',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './medical-history-modal.component.html',
  styleUrls: ['./medical-history-modal.component.css']
})
export class MedicalHistoryModalComponent {
  @Input() isVisible = false;
  @Input() medicalHistoryData: MedicalHistory | null = null;
  @Output() isVisibleChange = new EventEmitter<boolean>();
  @Output() onSave = new EventEmitter<void>();

  firstReportPreview: File | null = null;
  secReportPreview: File | null = null;
  
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

 ngOnChanges(changes: SimpleChanges) {
    if (changes['medicalHistoryData'] && changes['medicalHistoryData'].currentValue) {
        this.editMode = true;
        this.medicalHistory = { ...changes['medicalHistoryData'].currentValue };
        // Handle file previews if needed
        this.firstReportPreview = this.medicalHistory.firstReport;
        this.secReportPreview = this.medicalHistory.secReport;
    } else {
        this.editMode = false;
        this.medicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);
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

  openModal(id?: number) {
    this.isVisible = true;
    this.isVisibleChange.emit(true);
    
    if (id) {
      this.editMode = true;
      this.loadMedicalHistory(id);
    } else {
      this.editMode = false;
      this.medicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);
    }
  }

  async loadMedicalHistory(id: number) {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.medicalHistoryService.GetByIdByDoctor(id, domainName));
      this.medicalHistory = data;
      this.firstReportPreview = data.firstReport;
      this.secReportPreview = data.secReport;
    } catch (error) {
      console.error('Error loading medical history:', error);
      Swal.fire('Error', 'Failed to load medical history details.', 'error');
    }
  }

  closeModal() {
    this.isVisible = false;
    this.isVisibleChange.emit(false);
    this.medicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);
    this.validationErrors = {};
    this.firstReportPreview = null;
    this.secReportPreview = null;
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

      if (fileType.startsWith('image/') || fileType.startsWith('video/')) {
        this.medicalHistory[field] = file;

        const reader = new FileReader();
        reader.onload = (e: any) => {
          if (field === 'firstReport') {
            this.firstReportPreview = e.target.result;
          } else if (field === 'secReport') {
            this.secReportPreview = e.target.result;
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
          await firstValueFrom(this.medicalHistoryService.UpdateByDoctorAsync(this.medicalHistory, domainName));
        } else {
          await firstValueFrom(this.medicalHistoryService.AddByDoctor(this.medicalHistory, domainName));
        }

        this.onSave.emit();
        this.closeModal();
        Swal.fire('Success', 'Medical history saved successfully!', 'success');
      } catch (error) {
        console.error('Error saving medical history:', error);
        Swal.fire('Error', 'Failed to save medical history. Please try again later.', 'error');
      }
    }
  }

  isFormValid(): boolean {
    let isValid = true;
    this.validationErrors = {};

    if (!this.medicalHistory.schoolId || this.medicalHistory.schoolId === 0) {
      this.validationErrors['schoolId'] = '*School is required';
      isValid = false;
    }

    if (!this.medicalHistory.gradeId || this.medicalHistory.gradeId === 0) {
      this.validationErrors['gradeId'] = '*Grade is required';
      isValid = false;
    }

    if (!this.medicalHistory.classRoomID || this.medicalHistory.classRoomID === 0) {
      this.validationErrors['classRoomID'] = '*Class is required';
      isValid = false;
    }

    if (!this.medicalHistory.studentId || this.medicalHistory.studentId === 0) {
      this.validationErrors['studentId'] = '*Student is required';
      isValid = false;
    }

    return isValid;
  }
}