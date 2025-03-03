import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { TableComponent } from '../../../../Component/reuse-table/reuse-table.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-medical-history',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, TableComponent],
  templateUrl: './medical-history.component.html',
  styleUrls: ['./medical-history.component.css'],
})
export class MedicalHistoryComponent {
  // Table Headers
  headers: string[] = ['ID', 'Grade', 'Class', 'Student', 'Attached', 'Text', 'Date', 'Actions'];

  // Table Data
  medicalHistories: any[] = [];
  isModalVisible = false;

  // Define keys for table columns
  keys: string[] = ['id', 'grade', 'class', 'student', 'attached', 'text', 'date'];

  // Modal Data
  medicalHistory: any = {
    school: '',
    grade: '',
    class: '',
    student: '',
    details: '',
    permanentDrug: '',
    attachedFiles: [],
  };

  // Dropdown Options
  schools = ['School A', 'School B'];
  grades = ['Grade 1', 'Grade 2'];
  classes = ['Class A', 'Class B'];
  students = ['Student 1', 'Student 2'];

  // Define keysArray for search component
  keysArray: string[] = ['grade', 'class', 'student', 'attached', 'text', 'date'];

  openCreateModal() {
    this.isModalVisible = true;
    this.resetForm();
  }

  openEditModal(id?: number) {
    this.isModalVisible = true;
    if (id) {
      // Load existing medical history data for editing
      const existingHistory = this.medicalHistories.find(mh => mh.id === id);
      if (existingHistory) {
        this.medicalHistory = { ...existingHistory };
      }
    } else {
      // Reset form for new entry
      this.resetForm();
    }
  }

  closeModal() {
    this.isModalVisible = false;
    this.resetForm();
  }

  saveMedicalHistory() {
    if (this.medicalHistory.id) {
      // Update existing medical history
      const index = this.medicalHistories.findIndex(mh => mh.id === this.medicalHistory.id);
      this.medicalHistories[index] = { ...this.medicalHistory, actions: { edit: true, delete: true } };
    } else {
      // Add new medical history
      this.medicalHistory.id = this.medicalHistories.length + 1;
      this.medicalHistories.push({
        ...this.medicalHistory,
        actions: { edit: true, delete: true }
      });
    }
    this.closeModal();
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
        this.medicalHistories = this.medicalHistories.filter(mh => mh.id !== row.id);
        Swal.fire('Deleted!', 'The medical history has been deleted.', 'success');
      }
    });
  }

  resetForm() {
    this.medicalHistory = {
      school: '',
      grade: '',
      class: '',
      student: '',
      details: '',
      permanentDrug: '',
      attachedFiles: [],
    };
  }

  onFileChange(event: any, inputNumber: number) {
    if (event.target.files && event.target.files.length > 0) {
      if (inputNumber === 1) {
        this.medicalHistory.attachedFiles1 = event.target.files;
      } else if (inputNumber === 2) {
        this.medicalHistory.attachedFiles2 = event.target.files;
      }
    }
  }

  onSearchEvent(event: { key: string, value: any }) {
    const { key, value } = event;
    if (value) {
      this.medicalHistories = this.medicalHistories.filter(mh => mh[key].toString().toLowerCase().includes(value.toLowerCase()));
    } else {
      this.medicalHistories = [...this.medicalHistories];
    }
  }
}