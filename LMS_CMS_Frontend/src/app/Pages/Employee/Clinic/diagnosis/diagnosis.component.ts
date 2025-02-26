import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { SearchComponent } from "../../../../Component/search/search.component";

@Component({
  selector: 'app-diagnosis',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './diagnosis.component.html',
  styleUrls: ['./diagnosis.component.css']
})
export class DiagnosisComponent {
  diagnoses: any[] = [];
  diagnosis: any = { id: null, name: '', date: '' };
  editDiagnosis = false;
  validationErrors: { [key: string]: string } = {};
  keysArray: string[] = ['id', 'name', 'date'];

  openModal(id?: number) {
    if (id) {
      this.editDiagnosis = true;
      this.diagnosis = this.diagnoses.find(diag => diag.id === id);
    } else {
      this.diagnosis = { id: null, name: '', date: '' };
    }
    document.getElementById("Add_Diagnosis_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Diagnosis_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Diagnosis_Modal")?.classList.remove("flex");
    document.getElementById("Add_Diagnosis_Modal")?.classList.add("hidden");
    this.diagnosis = { id: null, name: '', date: '' };
    this.editDiagnosis = false;
    this.validationErrors = {};
  }

  saveDiagnosis() {
    if (this.validateForm()) {
      if (this.editDiagnosis) {
        const index = this.diagnoses.findIndex(diag => diag.id === this.diagnosis.id);
        this.diagnoses[index] = { ...this.diagnosis };
      } else {
        this.diagnosis.id = this.diagnoses.length + 1;
        this.diagnoses.push({ ...this.diagnosis });
      }
      this.closeModal();
    }
  }

  deleteDiagnosis(id: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this diagnosis!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#2E3646',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.isConfirmed) {
        this.diagnoses = this.diagnoses.filter(diag => diag.id !== id);
        Swal.fire('Deleted!', 'The diagnosis has been deleted.', 'success');
      }
    });
  }

  validateForm(): boolean {
    let isValid = true;
    if (!this.diagnosis.name) {
      this.validationErrors['name'] = '*Name is required';
      isValid = false;
    } else {
      this.validationErrors['name'] = '';
    }
    if (!this.diagnosis.date) {
      this.validationErrors['date'] = '*Date is required';
      isValid = false;
    } else {
      this.validationErrors['date'] = '';
    }
    return isValid;
  }

  onInputValueChange(event: { field: string, value: any }) {
    const { field, value } = event;
    this.diagnosis[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  onSearchEvent(event: { key: string, value: any }) {
    const { key, value } = event;
    if (value) {
      this.diagnoses = this.diagnoses.filter(diag => diag[key].toString().toLowerCase().includes(value.toLowerCase()));
    } else {
      this.diagnoses = [...this.diagnoses];
    }
  }
}