import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';

@Component({
  selector: 'app-drugs',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './drugs.component.html',
  styleUrls: ['./drugs.component.css']
})
export class DrugsComponent {
  drugs: any[] = [];
  drug: any = { id: null, name: '', date: '' };
  editDrug = false;
  validationErrors: { [key: string]: string } = {};
  keysArray: string[] = ['id', 'name', 'date'];

  openModal(id?: number) {
    if (id) {
      this.editDrug = true;
      this.drug = this.drugs.find(drug => drug.id === id);
    } else {
      this.drug = { id: null, name: '', date: '' };
    }
    document.getElementById("Add_Drug_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Drug_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Drug_Modal")?.classList.remove("flex");
    document.getElementById("Add_Drug_Modal")?.classList.add("hidden");
    this.drug = { id: null, name: '', date: '' };
    this.editDrug = false;
    this.validationErrors = {};
  }

  saveDrug() {
    if (this.validateForm()) {
      if (this.editDrug) {
        const index = this.drugs.findIndex(drug => drug.id === this.drug.id);
        this.drugs[index] = { ...this.drug };
      } else {
        this.drug.id = this.drugs.length + 1;
        this.drugs.push({ ...this.drug });
      }
      this.closeModal();
    }
  }

  deleteDrug(id: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this drug!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#2E3646',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.isConfirmed) {
        this.drugs = this.drugs.filter(drug => drug.id !== id);
        Swal.fire('Deleted!', 'The drug has been deleted.', 'success');
      }
    });
  }

  validateForm(): boolean {
    let isValid = true;
    if (!this.drug.name) {
      this.validationErrors['name'] = '*Name is required';
      isValid = false;
    } else {
      this.validationErrors['name'] = '';
    }
    if (!this.drug.date) {
      this.validationErrors['date'] = '*Date is required';
      isValid = false;
    } else {
      this.validationErrors['date'] = '';
    }
    return isValid;
  }

  onInputValueChange(event: { field: string, value: any }) {
    const { field, value } = event;
    this.drug[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  onSearchEvent(event: { key: string, value: any }) {
    const { key, value } = event;
    if (value) {
      this.drugs = this.drugs.filter(drug => drug[key].toString().toLowerCase().includes(value.toLowerCase()));
    } else {
      this.drugs = [...this.drugs];
    }
  }
}