import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ModalComponent } from '../../../../Component/modal/modal.component';
import Swal from 'sweetalert2';
import { TableComponent } from '../../../../Component/reuse-table/reuse-table.component';
import { ApiService } from '../../../../Services/api.service';
import { HygieneTypesService } from '../../../../Services/Employee/Clinic/hygiene-types.service';
import { HygieneTypes } from '../../../../Models/Clinic/hygiene-types';

@Component({
  selector: 'app-hygiene-types',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, ModalComponent, TableComponent],
  templateUrl: './hygiene-types.component.html',
  styleUrls: ['./hygiene-types.component.css'],
})
export class HygieneTypesComponent implements OnInit {
  hygieneType: HygieneTypes = new HygieneTypes(0, '', new Date(), 0);
  editHygieneType = false;
  validationErrors: { [key: string]: string } = {};
  keysArray: string[] = ['id', 'type'];
  key: string = "id";
  value: any = "";
  isModalVisible = false;
  hygieneTypes: HygieneTypes[] = [];
  DomainName: string = '';

  constructor(
    private hygieneTypesService: HygieneTypesService,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    this.DomainName = this.apiService.GetHeader(); // Get the domain name from ApiService
    this.getHygieneTypes();
  }

  // Fetch hygiene types
//  async getHygieneTypes() {
//     this.hygieneTypesService.Get(this.DomainName).subscribe(
//       (data) => {
//         this.hygieneTypes = data;
//         console.log(this.hygieneTypes)
//       },
//       (error) => {
//         console.error('Error fetching hygiene types:', error);
//       }
//     );
//   }

async getHygieneTypes() {
  try {
    const data = await firstValueFrom(this.hygieneTypesService.Get(this.DomainName));
    this.hygieneTypes = data.map((item) => ({
      ...item,
      actions: { delete: true, edit: true }, // Add actions dynamically
    }));
  } catch (error) {
    this.hygieneTypes = [];
    console.log('Error loading data:', error);
  }
}

  // Open modal for create/edit
openModal(id?: number) {
  if (id) {
    this.editHygieneType = true;
    this.hygieneType = this.hygieneTypes.find((ht) => ht.id === id)!;
  } else {
    this.hygieneType = new HygieneTypes(0, '', new Date(), 0); // Reset form for new entry
    this.editHygieneType = false;
  }
  this.isModalVisible = true; // Show the modal
}

  // Close modal
  closeModal() {
    this.isModalVisible = false; // Hide the modal
    this.hygieneType = new HygieneTypes(0, '', new Date(), 0); // Reset form
    this.editHygieneType = false;
    this.validationErrors = {};
  }

  // Save or update hygiene type
  saveHygieneType() {
    if (this.validateForm()) {
      if (this.editHygieneType) {
        this.hygieneTypesService.Edit(this.hygieneType, this.DomainName).subscribe(() => {
          this.getHygieneTypes();
          this.closeModal();
        });
      } else {
        this.hygieneTypesService.Add(this.hygieneType, this.DomainName).subscribe(() => {
          this.getHygieneTypes();
          this.closeModal();
        });
      }
    }
  }

  // Delete hygiene type
deleteHygieneType(row: any) {
  Swal.fire({
    title: 'Are you sure?',
    text: 'You will not be able to recover this hygiene type!',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#FF7519',
    cancelButtonColor: '#2E3646',
    confirmButtonText: 'Yes, delete it!',
    cancelButtonText: 'No, keep it',
  }).then((result) => {
    if (result.isConfirmed) {
      this.hygieneTypesService.Delete(row.id, this.DomainName).subscribe(() => {
        this.getHygieneTypes(); // Refresh the table after deletion
        Swal.fire('Deleted!', 'The hygiene type has been deleted.', 'success');
      });
    }
  });
}

  // Validate form
  validateForm(): boolean {
    let isValid = true;
    if (!this.hygieneType.type) {
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
    (this.hygieneType as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    await this.getHygieneTypes();
    if (this.value != "") {
      const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);

      this.hygieneTypes = this.hygieneTypes.filter(t => {
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