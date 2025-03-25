import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ModalComponent } from '../../../../Component/modal/modal.component';
import Swal from 'sweetalert2';
import { TableComponent } from '../../../../Component/reuse-table/reuse-table.component';
import { ApiService } from '../../../../Services/api.service';
import { DiagnosisService } from '../../../../Services/Employee/Clinic/diagnosis.service';
import { Diagnosis } from '../../../../Models/Clinic/diagnosis';

@Component({
  selector: 'app-diagnosis',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, ModalComponent, TableComponent],
  templateUrl: './diagnosis.component.html',
  styleUrls: ['./diagnosis.component.css'],
})
export class DiagnosisComponent implements OnInit {
  diagnosis: Diagnosis = new Diagnosis(0, '', new Date(), 0);
  editDiagnosis = false;
  validationErrors: { [key: string]: string } = {};
  keysArray: string[] = ['id', 'name'];
  key: string = "id";
  value: any = "";
  isModalVisible = false;
  diagnoses: Diagnosis[] = [];
  DomainName: string = '';

  constructor(
    private diagnosisService: DiagnosisService,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    this.DomainName = this.apiService.GetHeader(); 
    this.getDiagnoses();
  }

 
async getDiagnoses() {
  try {
    const data = await firstValueFrom(this.diagnosisService.Get(this.DomainName));
    this.diagnoses = data.map((item) => {
      const insertedAtDate = new Date(item.insertedAt);

      // Format the date
      const options: Intl.DateTimeFormatOptions = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      };
      const formattedDate: string = insertedAtDate.toLocaleDateString(undefined, options);

      // Create a new Diagnosis object with the formatted date and actions
      return {
        ...item,
        insertedAt: formattedDate, // Pass the formatted date as a string
        actions: { delete: true, edit: true }, // Add actions dynamically
      };
    });
  } catch (error) {
    console.error('Error loading data:', error);
    this.diagnoses = []; // Clear the table if there's an error
  }
}



  
  closeModal() {
    this.isModalVisible = false; 
    this.diagnosis = new Diagnosis(0, '', new Date(), 0); 
    this.editDiagnosis = false;
    this.validationErrors = {};
  }
  // Update openModal to create a copy of the diagnosis
openModal(id?: number) {
  if (id) {
    this.editDiagnosis = true;
    const originalDiagnosis = this.diagnoses.find((diag) => diag.id === id)!;
    this.diagnosis = new Diagnosis(
      originalDiagnosis.id,
      originalDiagnosis.name,
      new Date(originalDiagnosis.insertedAt),
      originalDiagnosis.insertedByUserId
    );
  } else {
    this.diagnosis = new Diagnosis(0, '', new Date(), 0);
    this.editDiagnosis = false;
  }
  this.isModalVisible = true;
}

// Update saveDiagnosis to handle errors and loading state
saveDiagnosis() {
  if (this.validateForm()) {
    if (this.editDiagnosis) {
      this.diagnosisService.Edit(this.diagnosis, this.DomainName).subscribe({
        next: () => {
          this.getDiagnoses(); // Refresh from server
          this.closeModal();
          Swal.fire('Success', 'Diagnosis saved successfully', 'success');
        },
        error: (err) => {
          console.error('Error updating diagnosis:', err);
          Swal.fire('Error', 'Failed to save diagnosis', 'error');
        }
      });
    } else {
      this.diagnosis.insertedAt = new Date().toISOString();
      this.diagnosisService.Add(this.diagnosis, this.DomainName).subscribe({
        next: () => {
          this.getDiagnoses(); // Refresh from server
          this.closeModal();
          Swal.fire('Success', 'Diagnosis created successfully', 'success');
        },
        error: (err) => {
          console.error('Error creating diagnosis:', err);
          Swal.fire('Error', 'Failed to create diagnosis', 'error');
        }
      });
    }
  }
}


deleteDiagnosis(row: any) {
  Swal.fire({
    title: 'Are you sure you want to delete this diagnosis?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#FF7519',
    cancelButtonColor: '#17253E',
    confirmButtonText: 'Delete',
    cancelButtonText: 'Cancel'
  }).then((result) => {
    if (result.isConfirmed) {
      this.diagnosisService.Delete(row.id, this.DomainName).subscribe({
        next: (response) => {
          console.log('Delete response:', response);
          this.getDiagnoses();
          // Swal.fire('Deleted!', 'The diagnosis has been deleted.', 'success');
        },
        error: (error) => {
          console.error('Error deleting diagnosis:', error);
          Swal.fire('Error!', 'Failed to delete the diagnosis.', 'error');
        },
      });
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
    return isValid;
  }

  
  onInputValueChange(event: { field: string; value: any }) {
    const { field, value } = event;
    (this.diagnosis as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  
async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    
    // First get all data from server
    await this.getDiagnoses(); // Change this to the appropriate method for each component (getDrugs, getDiagnoses, getDoses)
    
    if (this.value != "") {
        this.diagnoses = this.diagnoses.filter((item: any) => { // Change data to the appropriate array (drugs, diagnoses, doses)
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