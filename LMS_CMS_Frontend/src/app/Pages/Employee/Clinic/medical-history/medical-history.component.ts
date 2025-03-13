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
      if (fileType.startsWith('image/') || fileType.startsWith('video/')) {
        this.medicalHistory[field] = file; // Store the new file
        if (field === 'firstReport') {
          this.firstReportPreview = URL.createObjectURL(file); // Update the preview
        } else if (field === 'secReport') {
          this.secReportPreview = URL.createObjectURL(file); // Update the preview
        }
      } else {
        alert('Invalid file type. Please upload an image or video.');
      }
    }
  }

  async saveMedicalHistory() {
    // if (this.isFormValid()) {
      try {
        const domainName = this.apiService.GetHeader();
        const formData = new FormData();

        // Append all fields to FormData
        formData.append('Id', this.medicalHistory.id.toString());
        formData.append('SchoolId', this.medicalHistory.schoolId.toString());
        formData.append('GradeId', this.medicalHistory.gradeId.toString());
        formData.append('ClassRoomID', this.medicalHistory.classRoomID.toString());
        formData.append('StudentId', this.medicalHistory.studentId.toString());
        formData.append('Details', this.medicalHistory.details);
        formData.append('PermanentDrug', this.medicalHistory.permanentDrug);
        formData.append('Date', this.medicalHistory.insertedAt);

        // Handle FirstReport
        if (this.medicalHistory.firstReport instanceof File) {
          formData.append('FirstReportFile', this.medicalHistory.firstReport, this.medicalHistory.firstReport.name);
          formData.append('FirstReport', ''); // Set FirstReport to null when a new file is uploaded
        } else if (this.medicalHistory.firstReport === null) {
          formData.append('FirstReport', ''); // Set FirstReport to null if the file is deleted
        } else {
          formData.append('FirstReport', this.medicalHistory.firstReport); // Retain the existing FirstReport if no new file is uploaded
        }

        // Handle SecReport
        if (this.medicalHistory.secReport instanceof File) {
          formData.append('SecReportFile', this.medicalHistory.secReport, this.medicalHistory.secReport.name);
          formData.append('SecReport', ''); // Set SecReport to null when a new file is uploaded
        } else if (this.medicalHistory.secReport === null) {
          formData.append('SecReport', ''); // Set SecReport to null if the file is deleted
        } else {
          formData.append('SecReport', this.medicalHistory.secReport); // Retain the existing SecReport if no new file is uploaded
        }

        if (this.editMode) {
          await firstValueFrom(this.medicalHistoryService.Edit(formData, domainName));
        } else {
          await firstValueFrom(this.medicalHistoryService.Add(formData, domainName));
        }

        this.loadMedicalHistories();
        this.closeModal();
      } catch (error) {
        console.error('Error saving medical history:', error);
        Swal.fire('Error', 'Failed to save medical history. Please try again later.', 'error');
      }
    // }
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.medicalHistory) {
      if (this.medicalHistory.hasOwnProperty(key)) {
        if (!this.medicalHistory[key]) {
          this.validationErrors[key] = `*${this.capitalizeField(key)} is required`;
          isValid = false;
        }
      }
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