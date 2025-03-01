import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ModalComponent } from '../../../../Component/modal/modal.component';
import Swal from 'sweetalert2';
import { TableComponent } from '../../../../Component/reuse-table/reuse-table.component';
import { ApiService } from '../../../../Services/api.service';
import { DrugService } from '../../../../Services/Employee/Clinic/drug.service';
import { Drug } from '../../../../Models/Clinic/drug';

@Component({
  selector: 'app-drugs',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, ModalComponent, TableComponent],
  templateUrl: './drugs.component.html',
  styleUrls: ['./drugs.component.css'],
})
export class DrugsComponent implements OnInit {
  drug: Drug = new Drug(0, '', new Date());
  editDrug = false;
  validationErrors: { [key: string]: string } = {};
  keysArray: string[] = ['id', 'name', 'insertedAt'];
  key: string = "id";
  value: any = "";
  isModalVisible = false;
  drugs: Drug[] = [];
  DomainName: string = '';

  constructor(
    private drugService: DrugService,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    this.DomainName = this.apiService.GetHeader(); // Get the domain name from ApiService
    this.getDrugs();
  }

  // Fetch drugs
  async getDrugs() {
    try {
      const data = await firstValueFrom(this.drugService.Get(this.DomainName));
      this.drugs = data.map((item) => ({
        ...item,
        actions: { delete: true, edit: true }, // Add actions dynamically
      }));
    } catch (error) {
      console.error('Error loading data:', error);
      this.drugs = []; // Clear the table if there's an error
    }
  }

  // Open modal for create/edit
  openModal(id?: number) {
    if (id) {
      this.editDrug = true;
      this.drug = this.drugs.find((drug) => drug.id === id)!;
    } else {
      this.drug = new Drug(0, '', new Date()); // Reset form for new entry
      this.editDrug = false;
    }
    this.isModalVisible = true; // Show the modal
  }

  // Close modal
  closeModal() {
    this.isModalVisible = false; // Hide the modal
    this.drug = new Drug(0, '', new Date()); // Reset form
    this.editDrug = false;
    this.validationErrors = {};
  }

  // Save or update drug
  saveDrug() {
    if (this.validateForm()) {
      if (this.editDrug) {
        this.drugService.Edit(this.drug, this.DomainName).subscribe(() => {
          this.getDrugs();
          this.closeModal();
        });
      } else {
        // Set the current date for new drugs
        this.drug.insertedAt = new Date().toISOString();
        this.drugService.Add(this.drug, this.DomainName).subscribe(() => {
          this.getDrugs();
          this.closeModal();
        });
      }
    }
  }

  // Delete drug
  deleteDrug(row: any) {
    Swal.fire({
      title: 'Are you sure you want to delete this drug?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.drugService.Delete(row.id, this.DomainName).subscribe({
          next: (response) => {
            // Log the plain text response (optional)
            console.log('Delete response:', response);

            // Refresh the table after successful deletion
            this.getDrugs();
            Swal.fire('Deleted!', 'The drug has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting drug:', error);
            Swal.fire('Error!', 'Failed to delete the drug.', 'error');
          },
        });
      }
    });
  }

  // Validate form
  validateForm(): boolean {
    let isValid = true;
    if (!this.drug.name) {
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
    (this.drug as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  // Handle search
  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    await this.getDrugs();
    if (this.value != "") {
      const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);

      this.drugs = this.drugs.filter(t => {
        const fieldValue = t[this.key as keyof typeof t];
        if (typeof fieldValue === 'string') {
          return fieldValue.toLowerCase().includes(this.value.toLowerCase());
        }
        if (typeof fieldValue === 'number') {
          return fieldValue === numericValue;
        }
        return fieldValue == this.value;
      });
    }
  }
}