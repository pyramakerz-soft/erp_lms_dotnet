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
    this.DomainName = this.apiService.GetHeader(); 
    this.getDrugs();
  }

  
  async getDrugs() {
    try {
      const data = await firstValueFrom(this.drugService.Get(this.DomainName));
      this.drugs = data.map((item) => {
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
      console.error('Error loading data:', error);
      this.drugs = []; 
    }
  }



  
  closeModal() {
    this.isModalVisible = false; 
    this.drug = new Drug(0, '', new Date()); 
    this.editDrug = false;
    this.validationErrors = {};
  }
  
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


saveDrug() {
  if (this.validateForm()) {
    if (this.editDrug) {
      this.drugService.Edit(this.drug, this.DomainName).subscribe({
        next: () => {
          this.getDrugs(); 
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
          this.getDrugs(); 
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
            
            console.log('Delete response:', response);

            
            this.getDrugs();
            
          },
          error: (error) => {
            console.error('Error deleting drug:', error);
            Swal.fire('Error!', 'Failed to delete the drug.', 'error');
          },
        });
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
    return isValid;
  }

  
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
    
    
    await this.getDrugs(); 
    
    if (this.value != "") {
        this.drugs = this.drugs.filter((item: any) => { 
            const fieldValue = item[this.key as keyof typeof item];
            
            
            const searchString = this.value.toString().toLowerCase();
            const fieldString = fieldValue?.toString().toLowerCase() || '';
            
            
            return fieldString.includes(searchString);
        });
    }
}
}