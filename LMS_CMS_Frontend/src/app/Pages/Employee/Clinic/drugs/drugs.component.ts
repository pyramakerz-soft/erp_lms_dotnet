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
  keysArray: string[] = ['id', 'name'];
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
      this.drugs = data.map((item) => {
        const insertedAtDate = new Date(item.insertedAt);

        // Format the date
        const options: Intl.DateTimeFormatOptions = {
          year: 'numeric',
          month: 'long',
          day: 'numeric',
        };
        const formattedDate: string = insertedAtDate.toLocaleDateString(undefined, options);

        // Create a new Drug object with the formatted date and actions
        return {
          ...item,
          insertedAt: formattedDate, // Pass the formatted date as a string
          actions: { delete: true, edit: true }, // Add actions dynamically
        };
      });
    } catch (error) {
      console.error('Error loading data:', error);
      this.drugs = []; // Clear the table if there's an error
    }
  }



  // Close modal
  closeModal() {
    this.isModalVisible = false; // Hide the modal
    this.drug = new Drug(0, '', new Date()); // Reset form
    this.editDrug = false;
    this.validationErrors = {};
  }
  // Update openModal to create a copy of the drug
openModal(id?: number) {
  if (id) {
    this.editDrug = true;
    const originalDrug = this.drugs.find((drug) => drug.id === id)!;
    this.drug = new Drug(
      originalDrug.id,
      originalDrug.name,
      new Date(originalDrug.insertedAt)
    );
  } else {
    this.drug = new Drug(0, '', new Date());
    this.editDrug = false;
  }
  this.isModalVisible = true;
}

// Update saveDrug to handle errors and loading state
saveDrug() {
  if (this.validateForm()) {
    if (this.editDrug) {
      this.drugService.Edit(this.drug, this.DomainName).subscribe({
        next: () => {
          this.getDrugs(); // Refresh from server
          this.closeModal();
          Swal.fire('Success', 'Drug saved successfully', 'success');
        },
        error: (err) => {
          console.error('Error updating drug:', err);
          Swal.fire('Error', 'Failed to save drug', 'error');
        }
      });
    } else {
      this.drug.insertedAt = new Date().toISOString();
      this.drugService.Add(this.drug, this.DomainName).subscribe({
        next: () => {
          this.getDrugs(); // Refresh from server
          this.closeModal();
          Swal.fire('Success', 'Drug created successfully', 'success');
        },
        error: (err) => {
          console.error('Error creating drug:', err);
          Swal.fire('Error', 'Failed to create drug', 'error');
        }
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
            // Swal.fire('Deleted!', 'The drug has been deleted.', 'success');
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

async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    
    // First get all data from server
    await this.getDrugs(); // Change this to the appropriate method for each component (getDrugs, getDiagnoses, getDoses)
    
    if (this.value != "") {
        this.drugs = this.drugs.filter((item: any) => { // Change data to the appropriate array (drugs, diagnoses, doses)
            const fieldValue = item[this.key as keyof typeof item];
            
            // Convert both values to string for consistent comparison
            const searchString = this.value.toString().toLowerCase();
            const fieldString = fieldValue?.toString().toLowerCase() || '';
            
            // Check if the field string includes the search string
            return fieldString.includes(searchString);
        });
    }
}
}