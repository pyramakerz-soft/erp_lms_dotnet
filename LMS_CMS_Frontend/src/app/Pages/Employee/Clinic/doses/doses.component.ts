import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule, formatDate } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ModalComponent } from '../../../../Component/modal/modal.component';
import { TableComponent } from '../../../../Component/reuse-table/reuse-table.component';

@Component({
  selector: 'app-doses',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, ModalComponent, TableComponent],
  templateUrl: './doses.component.html',
  styleUrls: ['./doses.component.css'],
})
export class DosesComponent implements OnInit {
  dose: any = { id: 0, name: '', insertedAt: new Date() }; // Simplified dose object
  editDose = false;
  validationErrors: { [key: string]: string } = {};
  keysArray: string[] = ['id', 'name'];
  key: string = "id";
  value: any = "";
  isModalVisible = false;
  doses: any[] = []; // Simplified doses array

  constructor() {}

  ngOnInit(): void {
    this.getDoses(); // Fetch doses (mocked for now)
  }

  // Mocked function to fetch doses
  getDoses() {
    this.doses = [
      { id: 1, name: 'Dose 1', insertedAt: new Date().toLocaleDateString() },
      { id: 2, name: 'Dose 2', insertedAt: new Date().toLocaleDateString() },
    ];
  }

  // Open modal for create/edit
  openModal(id?: number) {
    if (id) {
      this.editDose = true;
      this.dose = this.doses.find((d) => d.id === id)!;
    } else {
      this.dose = { id: 0, name: '', insertedAt: new Date() }; // Reset form for new entry
      this.editDose = false;
    }
    this.isModalVisible = true; // Show the modal
  }

  // Close modal
  closeModal() {
    this.isModalVisible = false; // Hide the modal
    this.dose = { id: 0, name: '', insertedAt: new Date() }; // Reset form
    this.editDose = false;
    this.validationErrors = {};
  }

  // Save or update dose (mocked for now)
  saveDose() {
    if (this.validateForm()) {
      if (this.editDose) {
        // Mocked edit functionality
        const index = this.doses.findIndex((d) => d.id === this.dose.id);
        this.doses[index] = { ...this.dose };
      } else {
        // Mocked create functionality
        this.dose.id = this.doses.length + 1;
        this.doses.push({ ...this.dose });
      }
      this.closeModal();
    }
  }

  // Delete dose (mocked for now)
  deleteDose(row: any) {
    if (confirm('Are you sure you want to delete this dose?')) {
      this.doses = this.doses.filter((d) => d.id !== row.id);
    }
  }

  // Validate form
  validateForm(): boolean {
    let isValid = true;
    if (!this.dose.name) {
      this.validationErrors['name'] = '*Name is required';
      isValid = false;
    } else {
      this.validationErrors['name'] = '';
    }
    return isValid;
  }

  // Handle input changes
  onInputValueChange(event: { field: string; value: any }) {
    const { field, value } = event;
    this.dose[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  // Mocked search functionality
  onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    if (this.value !== "") {
      this.doses = this.doses.filter((d) => {
        const fieldValue = d[this.key as keyof typeof d];
        if (typeof fieldValue === 'string') {
          return fieldValue.toLowerCase().includes(this.value.toLowerCase());
        }
        if (typeof fieldValue === 'number') {
          return fieldValue === Number(this.value);
        }
        return fieldValue == this.value;
      });
    } else {
      this.getDoses(); // Reset to original data if search is empty
    }
  }
}