// medical-history.component.ts
import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
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
  imports: [FormsModule, CommonModule, SearchComponent, TableComponent],
  templateUrl: './medical-history.component.html',
  styleUrls: ['./medical-history.component.css'],
})
export class MedicalHistoryComponent implements OnInit {
  // // Table Headers
  // headers: string[] = ['ID', 'Grade', 'Class', 'Student', 'Attached', 'Text', 'Date', 'Actions'];

  // // Table Data
  // medicalHistories: MedicalHistory[] = [];
  // isModalVisible = false;

  // // Define keys for table columns
  // keys: string[] = ['id', 'gradeId', 'classRoomID', 'studentId', 'attached', 'details', 'date'];

  // // Modal Data
  // medicalHistory: MedicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);

  // // Dropdown Options
  // schools: any[] = [];
  // grades: any[] = [];
  // classes: any[] = [];
  // students: any[] = [];

  // // Define keysArray for search component
  // keysArray: string[] = ['gradeId', 'classRoomID', 'studentId', 'attached', 'details', 'date'];

  // constructor(
  //   private medicalHistoryService: MedicalHistoryService,
  //   private apiService: ApiService,
  //   private schoolService: SchoolService,
  //   private gradeService: GradeService,
  //   private classroomService: ClassroomService,
  //   private studentService: StudentService
  // ) {}

  // ngOnInit(): void {
  //   this.loadMedicalHistories();
  //   this.loadDropdownOptions();
  // // }

  // async loadMedicalHistories() {
  //   try {
  //     const domainName = this.apiService.GetHeader();
  //     const data = await firstValueFrom(this.medicalHistoryService.Get(domainName));
  //     this.medicalHistories = data.map(item => ({
  //       ...item,
  //       // formattedDate: this.formatDate(item.insertedAt),
  //       attachedFiles: item.firstReport || item.secReport ? '2' : item.firstReport || item.secReport === null ? '-' : '1',
  //     }));
  //   } catch (error) {
  //     console.error('Error loading medical histories:', error);
  //     Swal.fire('Error', 'Failed to load medical histories. Please try again later.', 'error');
  //   }
  // }
  //   formatDate(dateString: string): string {
  //   const date = new Date(dateString);
  //   return date.toLocaleDateString('en-US', { month: 'long', day: 'numeric', year: 'numeric' });
  // }

  // async loadDropdownOptions() {
  //   try {
  //     const domainName = this.apiService.GetHeader();
  //     this.schools = await firstValueFrom(this.schoolService.Get(domainName));
  //     this.grades = await firstValueFrom(this.gradeService.Get(domainName));
  //     this.classes = await firstValueFrom(this.classroomService.Get(domainName));
  //     const studentsData = await firstValueFrom(this.studentService.GetAll(domainName));
  //     this.students = studentsData.map(student => ({ id: student.id, name: student.en_name })); // Use English name for display
  //   } catch (error) {
  //     console.error('Error loading dropdown options:', error);
  //     Swal.fire('Error', 'Failed to load dropdown options. Please try again later.', 'error');
  //   }
  // }
  // openCreateModal() {
  //   this.isModalVisible = true;
  //   this.resetForm();
  // }

  // openEditModal(id?: number) {
  //   this.isModalVisible = true;
  //   if (id) {
  //     const existingHistory = this.medicalHistories.find(mh => mh.id === id);
  //     if (existingHistory) {
  //       this.medicalHistory = { ...existingHistory };
  //     }
  //   } else {
  //     this.resetForm();
  //   }
  // }

  // closeModal() {
  //   this.isModalVisible = false;
  //   this.resetForm();
  // }

  // async saveMedicalHistory() {
  //   try {
  //     const domainName = this.apiService.GetHeader();
  //     const formData = new FormData();
  //     formData.append('Details', this.medicalHistory.details);
  //     formData.append('PermanentDrug', this.medicalHistory.permanentDrug);
  //     formData.append('ClassRoomID', this.medicalHistory.classRoomID.toString());
  //     formData.append('Date', this.medicalHistory.date);
  //     formData.append('SchoolId', this.medicalHistory.schoolId.toString());
  //     formData.append('GradeId', this.medicalHistory.gradeId.toString());
  //     formData.append('StudentId', this.medicalHistory.studentId.toString());
  //     if (this.medicalHistory.firstReport) {
  //       formData.append('FirstReport', this.medicalHistory.firstReport);
  //     }
  //     if (this.medicalHistory.secReport) {
  //       formData.append('SecReport', this.medicalHistory.secReport);
  //     }

  //     if (this.medicalHistory.id) {
  //       await firstValueFrom(this.medicalHistoryService.Edit(this.medicalHistory, domainName));
  //     } else {
  //       await firstValueFrom(this.medicalHistoryService.Add(formData, domainName));
  //     }
  //     this.loadMedicalHistories();
  //     this.closeModal();
  //   } catch (error) {
  //     console.error('Error saving medical history:', error);
  //     Swal.fire('Error', 'Failed to save medical history. Please try again later.', 'error');
  //   }
  // }

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
next: (response) => {
            // Log the plain text response (optional)
            console.log('Delete response:', response);

            // Refresh the table after successful deletion
            this.loadMedicalHistories();
            Swal.fire('Deleted!', 'The drug has been deleted.', 'success');        },
        error: (error) => {
          console.error('Error deleting medical history:', error);
          Swal.fire('Error', 'Failed to delete medical history. Please try again later.', 'error');
        },
      });
    }
  });
}

  // resetForm() {
  //   this.medicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);
  // }

  onFileChange(event: any, inputNumber: number) {
    if (event.target.files && event.target.files.length > 0) {
      if (inputNumber === 1) {
        this.medicalHistory.firstReport = event.target.files[0];
      } else if (inputNumber === 2) {
        this.medicalHistory.secReport = event.target.files[0];
      }
    }
  }

  onSearchEvent(event: { key: string, value: any }) {
    const { key, value } = event;
    if (value) {
      this.medicalHistories = this.medicalHistories.filter(mh => mh[key].toString().toLowerCase().includes(value.toLowerCase()));
    } else {
      this.loadMedicalHistories();
    }
  }


    headers: string[] = ['ID', 'School', 'Grade', 'Class', 'Student', 'Attached Files', 'Details', 'Permanent Drug', 'Date', 'Actions'];
  medicalHistories: any[] = [];
  isModalVisible = false;

  keys: string[] = ['id', 'school', 'grade', 'classRoom', 'student', 'attachedFiles', 'details', 'permanentDrug', 'formattedDate'];

  medicalHistory: MedicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);

  schools: any[] = [];
  grades: any[] = [];
  classes: any[] = [];
  students: any[] = [];

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
      console.log(data)
      this.medicalHistories = data.map(item => ({
        ...item,
        formattedDate: this.formatDate(item.insertedAt), // Format insertedAt date
        attachedFiles: item.firstReport || item.secReport ? '2' : item.firstReport || item.secReport === null ? '-' : '1',
        actions: { edit: true, delete: true }, // Ensure action buttons appear
      }));
    } catch (error) {
      console.error('Error loading medical histories:', error);
      Swal.fire('Error', 'Failed to load medical histories. Please try again later.', 'error');
    }
  }

  async loadDropdownOptions() {
    try {
      const domainName = this.apiService.GetHeader();
      this.schools = await firstValueFrom(this.schoolService.Get(domainName));
      this.grades = await firstValueFrom(this.gradeService.Get(domainName));
      this.classes = await firstValueFrom(this.classroomService.Get(domainName));
      const studentsData = await firstValueFrom(this.studentService.GetAll(domainName));
      this.students = studentsData.map(student => ({ id: student.id, name: student.en_name })); // Use English name for display
    } catch (error) {
      console.error('Error loading dropdown options:', error);
      Swal.fire('Error', 'Failed to load dropdown options. Please try again later.', 'error');
    }
  }

  openCreateModal() {
    this.isModalVisible = true;
    this.resetForm();
  }

  openEditModal(id?: number) {
    this.isModalVisible = true;
    if (id) {
      const existingHistory = this.medicalHistories.find(mh => mh.id === id);
      if (existingHistory) {
        this.medicalHistory = { ...existingHistory };
      }
    } else {
      this.resetForm();
    }
  }

  closeModal() {
    this.isModalVisible = false;
    this.resetForm();
  }

  async saveMedicalHistory() {
    try {
      const domainName = this.apiService.GetHeader();
      const formData = new FormData();
      formData.append('Details', this.medicalHistory.details);
      formData.append('PermanentDrug', this.medicalHistory.permanentDrug);
      formData.append('ClassRoomID', this.medicalHistory.classRoomID.toString());
      formData.append('Date', this.medicalHistory.insertedAt);
      formData.append('SchoolId', this.medicalHistory.schoolId.toString());
      formData.append('GradeId', this.medicalHistory.gradeId.toString());
      formData.append('StudentId', this.medicalHistory.studentId.toString());
      if (this.medicalHistory.firstReport) {
        formData.append('FirstReport', this.medicalHistory.firstReport);
      }
      if (this.medicalHistory.secReport) {
        formData.append('SecReport', this.medicalHistory.secReport);
      }

      if (this.medicalHistory.id) {
        // await firstValueFrom(this.medicalHistoryService.Edit(, domainName));
      } else {
        await firstValueFrom(this.medicalHistoryService.Add(formData, domainName));
      }
      this.loadMedicalHistories();
      this.closeModal();
    } catch (error) {
      console.error('Error saving medical history:', error);
      Swal.fire('Error', 'Failed to save medical history. Please try again later.', 'error');
    }
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { month: 'long', day: 'numeric', year: 'numeric' });
  }

  resetForm() {
    this.medicalHistory = new MedicalHistory(0, 0, 0, 0, 0, '', '', new Date().toISOString(), null, null);
  }
}