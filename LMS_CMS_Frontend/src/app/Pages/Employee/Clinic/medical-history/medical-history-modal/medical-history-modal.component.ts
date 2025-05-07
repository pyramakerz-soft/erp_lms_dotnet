import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges, OnInit } from '@angular/core';

import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MedicalHistory } from '../../../../../Models/Clinic/MedicalHistory';
import { SchoolService } from '../../../../../Services/Employee/school.service';
import { GradeService } from '../../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../../Services/Employee/LMS/classroom.service';
import { StudentService } from '../../../../../Services/student.service';
import { MedicalHistoryService } from '../../../../../Services/Employee/Clinic/medical-history.service';
import { ApiService } from '../../../../../Services/api.service';

@Component({
  selector: 'app-medical-history-modal',
    imports: [FormsModule, CommonModule],
  standalone: true,
  templateUrl: './medical-history-modal.component.html',
  styleUrls: ['./medical-history-modal.component.css']
})
export class MedicalHistoryModalComponent implements OnInit, OnChanges {
  @Input() isVisible = false;
  @Input() medicalHistoryData: MedicalHistory | null = null;
  @Output() isVisibleChange = new EventEmitter<boolean>();
  @Output() onSave = new EventEmitter<void>();

  editMode = false;
  medicalHistory: MedicalHistory = new MedicalHistory(0, 0,'', 0,'', 0,'', 0,'', '', '', new Date().toISOString(), null, null);
  firstReportPreview: File | null = null;
  secReportPreview: File | null = null;
  validationErrors: { [key: string]: string } = {};

  schools: any[] = [];
  grades: any[] = [];
  classes: any[] = [];
  students: any[] = [];

  constructor(
    private schoolService: SchoolService,
    private gradeService: GradeService,
    private classroomService: ClassroomService,
    private studentService: StudentService,
    private medicalHistoryService: MedicalHistoryService,
    private apiService: ApiService
  ) {}

  async ngOnInit() {
    await this.loadSchools();
  }

  async ngOnChanges(changes: SimpleChanges) {
    if (changes['medicalHistoryData']) {
      if (this.medicalHistoryData) {
        this.editMode = true;
        this.medicalHistory = { ...this.medicalHistoryData };
        await this.loadSchools();
        if (this.medicalHistory.schoolId) {
          await this.loadGrades(this.medicalHistory.schoolId);
          if (this.medicalHistory.gradeId) {
            await this.loadClasses(this.medicalHistory.gradeId);
          }
        }
        this.firstReportPreview = this.medicalHistory.firstReport;
        this.secReportPreview = this.medicalHistory.secReport;
      } else {
        this.editMode = false;
        this.medicalHistory = new MedicalHistory(0, 0,'', 0,'', 0,'', 0,'', '', '', new Date().toISOString(), null, null);
        this.firstReportPreview = null;
        this.secReportPreview = null;
      }
    }
  }

  async loadSchools() {
    try {
      const domainName = this.apiService.GetHeader();
      this.schools = await firstValueFrom(this.schoolService.Get(domainName));
    } catch (error) {
      console.error('Error loading schools:', error);
    }
  }

  async loadGrades(schoolId: number) {
    try {
      const domainName = this.apiService.GetHeader();
      this.grades = await firstValueFrom(this.gradeService.GetBySchoolId(schoolId, domainName));
    } catch (error) {
      console.error('Error loading grades:', error);
    }
  }

  async loadClasses(gradeId: number) {
    try {
      const domainName = this.apiService.GetHeader();
      this.classes = await firstValueFrom(this.classroomService.GetByGradeId(gradeId, domainName));
    } catch (error) {
      console.error('Error loading classes:', error);
    }
  }

  async loadStudents(classId: number) {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.studentService.GetByClassID(classId, domainName));
      this.students = data.map(student => ({ id: student.id, name: student.en_name }));
    } catch (error) {
      console.error('Error loading students:', error);
    }
  }

  onSchoolChange(event: Event) {
    const selectedSchoolId = Number((event.target as HTMLSelectElement).value);
    this.medicalHistory.schoolId = selectedSchoolId;
    this.medicalHistory.gradeId = 0;
    this.medicalHistory.classRoomID = 0;
    this.medicalHistory.studentId = 0;
    this.grades = [];
    this.classes = [];
    this.students = [];
    if (selectedSchoolId) {
      this.loadGrades(selectedSchoolId);
    }
  }

  onGradeChange(event: Event) {
    const selectedGradeId = Number((event.target as HTMLSelectElement).value);
    this.medicalHistory.gradeId = selectedGradeId;
    this.medicalHistory.classRoomID = 0;
    this.medicalHistory.studentId = 0;
    this.classes = [];
    this.students = [];
    if (selectedGradeId) {
      this.loadClasses(selectedGradeId);
    }
  }

  onClassChange(event: Event) {
    const selectedClassId = Number((event.target as HTMLSelectElement).value);
    this.medicalHistory.classRoomID = selectedClassId;
    this.medicalHistory.studentId = 0;
    this.students = [];
    if (selectedClassId) {
      this.loadStudents(selectedClassId);
    }
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
          } else {
            this.secReportPreview = e.target.result;
          }
        };
        reader.readAsDataURL(file);
      } else {
        alert('Invalid file type. Please upload an image or video.');
      }
    }
  }

  isFormValid(): boolean {
    this.validationErrors = {};
    let isValid = true;

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

  async saveMedicalHistory() {
    if (this.isFormValid()) {
      try {
        const domainName = this.apiService.GetHeader();
        
        if (this.editMode) {
          await firstValueFrom(
            this.medicalHistoryService.UpdateByDoctorAsync(this.medicalHistory, domainName)
          );
          Swal.fire('Success', 'Medical history updated successfully!', 'success');
        } else {
          await firstValueFrom(
            this.medicalHistoryService.AddByDoctor(this.medicalHistory, domainName)
          );
          Swal.fire('Success', 'Medical history created successfully!', 'success');
        }

        this.onSave.emit();
        this.closeModal();
      } catch (error) {
        console.error('Error saving medical history:', error);
        Swal.fire('Error', 'Failed to save medical history. Please try again later.', 'error');
      }
    }
  }

  closeModal() {
    this.isVisible = false;
    this.isVisibleChange.emit(false);
  }
}