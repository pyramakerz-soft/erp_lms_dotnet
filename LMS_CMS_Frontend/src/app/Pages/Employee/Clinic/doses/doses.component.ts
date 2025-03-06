import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule, formatDate } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ModalComponent } from '../../../../Component/modal/modal.component';
import { TableComponent } from '../../../../Component/reuse-table/reuse-table.component';
import { DoseService } from '../../../../Services/Employee/Clinic/dose.service';
import { ApiService } from '../../../../Services/api.service';
import Swal from 'sweetalert2';
import { Dose } from '../../../../Models/Clinic/dose';

@Component({
  selector: 'app-doses',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, ModalComponent, TableComponent],
  templateUrl: './doses.component.html',
  styleUrls: ['./doses.component.css'],
})
export class DosesComponent implements OnInit {
  dose: Dose = new Dose(0, '', '');
  editDose = false;
  validationErrors: { [key: string]: string } = {};
  keysArray: string[] = ['id', 'doseTimes', 'insertedAt'];
  key: string = 'id';
  value: any = '';
  isModalVisible = false;
  doses: Dose[] = [];
  DomainName: string = '';

  constructor(private doseService: DoseService, private apiService: ApiService) {}

  ngOnInit(): void {
    this.DomainName = this.apiService.GetHeader(); // Get the domain name from ApiService
    this.getDoses();
  }

async getDoses() {
  try {
    const data = await firstValueFrom(this.doseService.Get(this.DomainName));
    this.doses = data.map((item) => {
      const insertedAtDate = new Date(item.insertedAt);

      // Format the date as "Month Day, Year"
      const options: Intl.DateTimeFormatOptions = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      };
      const formattedDate: string = insertedAtDate.toLocaleDateString(undefined, options);

      return {
        ...item,
        insertedAt: formattedDate, // Replace the raw date with the formatted date
        actions: { delete: true, edit: true }, // Add actions dynamically
      };
    });
  } catch (error) {
    console.error('Error loading doses:', error);
    this.doses = []; // Clear the table if there's an error
  }
}

  // Open modal for create/edit
  openModal(id?: number) {
    if (id) {
      this.editDose = true;
      this.dose = this.doses.find((d) => d.id === id)!;
    } else {
      this.dose = new Dose(0, '', ''); // Reset form for new entry
      this.editDose = false;
    }
    this.isModalVisible = true; // Show the modal
  }

  // Close modal
  closeModal() {
    this.isModalVisible = false; // Hide the modal
    this.dose = new Dose(0, '', ''); // Reset form
    this.editDose = false;
    this.validationErrors = {};
  }

  // Save or update dose
  saveDose() {
    if (this.validateForm()) {
      if (this.editDose) {
        this.doseService.Edit(this.dose, this.DomainName).subscribe(() => {
          this.getDoses();
          this.closeModal();
        });
      } else {
        this.doseService.Add(this.dose, this.DomainName).subscribe(() => {
          this.getDoses();
          this.closeModal();
        });
      }
    }
  }

  // Delete dose
  deleteDose(row: Dose) {
    Swal.fire({
      title: 'Are you sure you want to delete this dose?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.doseService.Delete(row.id, this.DomainName).subscribe({
          next: () => {
            this.getDoses();
            Swal.fire('Deleted!', 'The dose has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting dose:', error);
            Swal.fire('Error!', 'Failed to delete the dose.', 'error');
          },
        });
      }
    });
  }

  // Validate form
  validateForm(): boolean {
    let isValid = true;
    if (!this.dose.doseTimes) {
      this.validationErrors['doseTimes'] = '*Dose Times is required';
      isValid = false;
    } else {
      this.validationErrors['doseTimes'] = '';
    }
    return isValid;
  }

  // Handle input changes
  onInputValueChange(event: { field: string; value: any }) {
    const { field, value } = event;
    (this.dose as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

async onSearchEvent(event: { key: string; value: any }) {
  this.key = event.key;
  this.value = event.value;
  if (this.value !== '') {
    try {
      const data = await firstValueFrom(
        this.doseService.Search(this.key, this.value, this.DomainName)
      );
      this.doses = data.map((item) => {
        const insertedAtDate = new Date(item.insertedAt);

        // Format the date as "Month Day, Year"
        const options: Intl.DateTimeFormatOptions = {
          year: 'numeric',
          month: 'long',
          day: 'numeric',
        };
        const formattedDate: string = insertedAtDate.toLocaleDateString(undefined, options);

        return {
          ...item,
          insertedAt: formattedDate, // Replace the raw date with the formatted date
          actions: { delete: true, edit: true }, // Add actions dynamically
        };
      });
    } catch (error) {
      console.error('Error searching doses:', error);
      this.doses = [];
    }
  } else {
    this.getDoses(); // Reset to original data if search is empty
  }
}
}