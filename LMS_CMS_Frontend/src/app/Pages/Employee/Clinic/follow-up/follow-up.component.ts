import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-follow-up',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './follow-up.component.html',
  styleUrls: ['./follow-up.component.css'],
})
export class FollowUpComponent {
  // Table Headers
  headers: string[] = ['ID', 'School', 'Grade', 'Class', 'Student', 'Complaints', 'Diagnosis', 'Drugs', 'Dose', 'Recommendation', 'Actions'];

  // Table Data
  followUps: any[] = [];
  isModalVisible = false;

  // Define keys for table columns
  keys: string[] = ['id', 'school', 'grade', 'class', 'student', 'complaints', 'diagnosis', 'drugs', 'dose', 'recommendation'];

  // Modal Data
  followUp: any = {
    school: '',
    grade: '',
    class: '',
    student: '',
    complaints: '',
    diagnosis: '',
    drugs: '',
    dose: '',
    recommendation: '',
    sendSms: false,
  };

  // Dropdown Options
  schools = ['School A', 'School B'];
  grades = ['Grade 1', 'Grade 2'];
  classes = ['Class A', 'Class B'];
  students = ['Student 1', 'Student 2'];
  diagnoses = ['Diagnosis 1', 'Diagnosis 2'];
  drugs = ['Drug 1', 'Drug 2'];
  doses = ['Once', 'Twice'];

  // Drugs and Dose Table Data
  drugDoses: { drug: string, dose: string }[] = [];

  // Define keysArray for search component
  keysArray: string[] = ['school', 'grade', 'class', 'student', 'complaints', 'diagnosis', 'drugs', 'dose', 'recommendation'];

  openModal(id?: number) {
    this.isModalVisible = true;
    if (id) {
      // Load existing follow-up data for editing
      const existingFollowUp = this.followUps.find(f => f.id === id);
      if (existingFollowUp) {
        this.followUp = { ...existingFollowUp };
      }
    } else {
      // Reset form for new entry
      this.followUp = {
        school: '',
        grade: '',
        class: '',
        student: '',
        complaints: '',
        diagnosis: '',
        drugs: '',
        dose: '',
        recommendation: '',
        sendSms: false,
      };
    }
  }

  closeModal() {
    this.isModalVisible = false;
  }

  saveFollowUp() {
    if (this.followUp.id) {
      // Update existing follow-up
      const index = this.followUps.findIndex(f => f.id === this.followUp.id);
      this.followUps[index] = { ...this.followUp };
    } else {
      // Add new follow-up
      this.followUp.id = this.followUps.length + 1;
      this.followUps.push({ ...this.followUp });
    }
    this.closeModal();
  }
  

deleteFollowUp(row: any) {
  Swal.fire({
    title: 'Are you sure?',
    text: 'You will not be able to recover this hygiene form!',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#FF7519', // Use your secondary color
    cancelButtonColor: '#2E3646', // Use your primary color
    confirmButtonText: 'Yes, delete it!',
    cancelButtonText: 'No, keep it'
  }).then((result) => {
    if (result.isConfirmed) {
      // Delete the hygiene form if confirmed
      this.followUps = this.followUps.filter((hf: any) => hf.id !== row.id); // Explicitly type 'hf'
      Swal.fire(
        'Deleted!',
        'The follow up has been deleted.',
        'success'
      );
    }
  });
}

  onSearchEvent(event: { key: string, value: any }) {
    const { key, value } = event;
    if (value) {
      this.followUps = this.followUps.filter(f => f[key].toString().toLowerCase().includes(value.toLowerCase()));
    } else {
      this.followUps = [...this.followUps];
    }
  }

  addDrugDose() {
    if (this.followUp.drugs && this.followUp.dose) {
      this.drugDoses.push({ drug: this.followUp.drugs, dose: this.followUp.dose });
      this.followUp.drugs = '';
      this.followUp.dose = '';
    }
  }
}