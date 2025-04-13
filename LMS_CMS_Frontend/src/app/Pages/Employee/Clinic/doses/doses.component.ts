import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
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
    this.DomainName = this.apiService.GetHeader();
    this.getDoses();
  }

  async getDoses() {
    try {
      const data = await firstValueFrom(this.doseService.Get(this.DomainName));
      this.doses = data.map((item) => {
        const insertedAtDate = new Date(item.insertedAt);
        const options: Intl.DateTimeFormatOptions = {
          year: 'numeric',
          month: 'long',
          day: 'numeric',
        };
        const formattedDate: string = insertedAtDate.toLocaleDateString(undefined, options);
        return {
          ...item,
          insertedAt: formattedDate,
          actions: { delete: true, edit: true },
        };
      });
    } catch (error) {
      this.handleError('Error loading doses:', error);
    }
  }

  closeModal() {
    this.isModalVisible = false;
    this.dose = new Dose(0, '', '');
    this.editDose = false;
    this.validationErrors = {};
  }

  openModal(id?: number) {
    if (id) {
      this.editDose = true;
      const originalDose = this.doses.find((d) => d.id === id)!;
      this.dose = new Dose(
        originalDose.id,
        originalDose.doseTimes,
        originalDose.insertedAt
      );
    } else {
      this.dose = new Dose(0, '', '');
      this.editDose = false;
    }
    this.isModalVisible = true;
  }

  saveDose() {
    if (this.validateForm()) {
      const operation = this.editDose 
        ? this.doseService.Edit(this.dose, this.DomainName)
        : this.doseService.Add(this.dose, this.DomainName);

      operation.subscribe({
        next: () => {
          this.getDoses();
          this.closeModal();
          Swal.fire('Success', `Dose ${this.editDose ? 'updated' : 'created'} successfully`, 'success');
        },
        error: (error) => {
          this.handleError(`Error ${this.editDose ? 'updating' : 'creating'} dose:`, error);
        }
      });
    }
  }

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
            // Swal.fire('Deleted!', 'The dose has been deleted.', 'success');
          },
          error: (error) => {
            this.handleError('Error deleting dose:', error);
          },
        });
      }
    });
  }

  validateForm(): boolean {
    this.validationErrors = {};
    if (!this.dose.doseTimes) {
      this.validationErrors['doseTimes'] = '*Dose Times is required';
      return false;
    }
    return true;
  }

  onInputValueChange(event: { field: string; value: any }) {
    const { field, value } = event;
    (this.dose as any)[field] = value;
    if (value && this.validationErrors[field]) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    await this.getDoses();
    
    if (this.value) {
      this.doses = this.doses.filter(dose => {
        const fieldValue = dose[this.key as keyof typeof dose]?.toString().toLowerCase() || '';
        return fieldValue.includes(this.value.toString().toLowerCase());
      });
    }
  }

  private handleError(message: string, error: any) {
    console.error(message, error);
    // Swal.fire('Error!', 'An error occurred. Please try again.', 'error');
  }
}