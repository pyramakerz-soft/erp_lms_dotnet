import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ModalComponent } from '../../../../Component/modal/modal.component';
import Swal from 'sweetalert2';
import { TableComponent } from "../../../../Component/reuse-table/reuse-table.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-hygiene-form',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, ModalComponent, TableComponent],
  templateUrl: './hygiene-form.component.html',
  styleUrls: ['./hygiene-form.component.css']
})
export class HygieneFormComponent {
  hygieneForm: any = { id: null, grade: '', classes: '' };
  editHygieneForm = false;
  validationErrors: { [key: string]: string } = {};
  keysArray: string[] = ['id', 'grade', 'classes'];
  isModalVisible = false;

  hygieneForms: any[] = [
    { id: 1, grade: 'A', classes: 'Math, Science', date: '2023-10-01' },
    { id: 2, grade: 'B', classes: 'History, English', date: '2023-10-02' },
    { id: 3, grade: 'C', classes: 'Physics, Chemistry', date: '2023-10-03' },
  ];

  constructor(private router: Router) {
    this.addActionsToHygieneForms(); // Add actions to hygiene forms on initialization
  }
  ngOnInit() {
  const savedForms = localStorage.getItem('hygieneForms');
  if (savedForms) {
    this.hygieneForms = JSON.parse(savedForms);
  }
}

  navigateToCreateHygieneForm() {
  this.router.navigate(['/Employee/Create Hygiene Form']);
}

  // Function to add actions to each hygiene form
  addActionsToHygieneForms() {
    this.hygieneForms = this.hygieneForms.map(hf => ({
      ...hf,
      actions: { delete: true, edit: true } // Add actions dynamically
    }));
  }

  openModal(id?: number) {
    if (id) {
      this.editHygieneForm = true;
      this.hygieneForm = this.hygieneForms.find(hf => hf.id === id);
    } else {
      this.hygieneForm = { id: null, grade: '', classes: '' }; // Reset form for new entry
      this.editHygieneForm = false;
    }
    this.isModalVisible = true; // Show the modal
  }

  closeModal() {
    this.isModalVisible = false; // Hide the modal
    this.hygieneForm = { id: null, grade: '', classes: '' }; // Reset form
    this.editHygieneForm = false;
    this.validationErrors = {};
  }

  saveHygieneForm() {
    if (this.validateForm()) {
      if (this.editHygieneForm) {
        const index = this.hygieneForms.findIndex(hf => hf.id === this.hygieneForm.id);
        this.hygieneForms[index] = { ...this.hygieneForm, actions: { delete: true, edit: true } }; // Update existing item
      } else {
        this.hygieneForm.id = this.hygieneForms.length + 1; // Generate new ID
        this.hygieneForms.push({ ...this.hygieneForm, date: new Date().toISOString().split('T')[0], actions: { delete: true, edit: true } }); // Add new item with actions
      }
      this.closeModal(); // Close the modal after saving
    }
  }

  deleteHygieneForm(row: any) {
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
        this.hygieneForms = this.hygieneForms.filter(hf => hf.id !== row.id);
        Swal.fire(
          'Deleted!',
          'The hygiene form has been deleted.',
          'success'
        );
      }
    });
  }

  validateForm(): boolean {
    let isValid = true;
    if (!this.hygieneForm.grade) {
      this.validationErrors['grade'] = '*Grade is required';
      isValid = false;
    } else {
      this.validationErrors['grade'] = '';
    }
    if (!this.hygieneForm.classes) {
      this.validationErrors['classes'] = '*Classes are required';
      isValid = false;
    } else {
      this.validationErrors['classes'] = '';
    }
    return isValid;
  }

  onInputValueChange(event: { field: string, value: any }) {
    const { field, value } = event;
    this.hygieneForm[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  onSearchEvent(event: { key: string, value: any }) {
    const { key, value } = event;
    if (value) {
      this.hygieneForms = this.hygieneForms.filter(hf => hf[key].toString().toLowerCase().includes(value.toLowerCase()));
    } else {
      this.hygieneForms = [...this.hygieneForms];
    }
  }
}