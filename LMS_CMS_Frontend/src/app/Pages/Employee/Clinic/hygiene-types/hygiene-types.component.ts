import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ModalComponent } from '../../../../Component/modal/modal.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-hygiene-types',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, ModalComponent],
  templateUrl: './hygiene-types.component.html',
  styleUrls: ['./hygiene-types.component.css']
})
export class HygieneTypesComponent {
  hygieneTypes: any[] = [];
  hygieneType: any = { id: null, name: '' };
  editHygieneType = false;
  validationErrors: { [key: string]: string } = {};
  keysArray: string[] = ['id', 'name'];
  isModalVisible = false; // Add this property to control modal visibility

  openModal(id?: number) {
    if (id) {
      this.editHygieneType = true;
      this.hygieneType = this.hygieneTypes.find(ht => ht.id === id);
    } else {
      this.hygieneType = { id: null, name: '' }; // Reset form for new entry
      this.editHygieneType = false;
    }
    this.isModalVisible = true; // Show the modal
  }

  closeModal() {
    this.isModalVisible = false; // Hide the modal
    this.hygieneType = { id: null, name: '' }; // Reset form
    this.editHygieneType = false;
    this.validationErrors = {};
  }

  saveHygieneType() {
    if (this.validateForm()) {
      if (this.editHygieneType) {
        const index = this.hygieneTypes.findIndex(ht => ht.id === this.hygieneType.id);
        this.hygieneTypes[index] = { ...this.hygieneType }; // Update existing item
      } else {
        this.hygieneType.id = this.hygieneTypes.length + 1; // Generate new ID
        this.hygieneTypes.push({ ...this.hygieneType }); // Add new item
      }
      this.closeModal(); // Close the modal after saving
    }
  }

  deleteHygieneType(id: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this hygiene type!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#2E3646',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.isConfirmed) {
        this.hygieneTypes = this.hygieneTypes.filter(ht => ht.id !== id); // Remove item
        Swal.fire('Deleted!', 'The hygiene type has been deleted.', 'success');
      }
    });
  }

  validateForm(): boolean {
    let isValid = true;
    if (!this.hygieneType.name) {
      this.validationErrors['name'] = '*Name is required';
      isValid = false;
    } else {
      this.validationErrors['name'] = '';
    }
    return isValid;
  }

  onInputValueChange(event: { field: string, value: any }) {
    const { field, value } = event;
    this.hygieneType[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  onSearchEvent(event: { key: string, value: any }) {
    const { key, value } = event;
    if (value) {
      this.hygieneTypes = this.hygieneTypes.filter(ht => ht[key].toString().toLowerCase().includes(value.toLowerCase()));
    } else {
      this.hygieneTypes = [...this.hygieneTypes];
    }
  }
}