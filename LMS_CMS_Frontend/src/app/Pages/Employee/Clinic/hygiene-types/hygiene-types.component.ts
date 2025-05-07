import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule, formatDate } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ModalComponent } from '../../../../Component/modal/modal.component';
import Swal from 'sweetalert2';
import { TableComponent } from '../../../../Component/reuse-table/reuse-table.component';
import { ApiService } from '../../../../Services/api.service';
import { HygieneTypesService } from '../../../../Services/Employee/Clinic/hygiene-type.service';
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
    this.DomainName = this.apiService.GetHeader(); 
    this.getHygieneTypes();
  }

  
async getHygieneTypes() {
  try {
    const data = await firstValueFrom(this.hygieneTypesService.Get(this.DomainName));
    this.hygieneTypes = data.map((item) => {
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
    this.hygieneTypes = []; 
  }
}


  
openModal(id?: number) {
  if (id) {
    this.editHygieneType = true;
    
    const originalHygieneType = this.hygieneTypes.find((ht) => ht.id === id)!;
    this.hygieneType = new HygieneTypes(
      originalHygieneType.id,
      originalHygieneType.type,
      new Date(originalHygieneType.insertedAt),
      originalHygieneType.insertedByUserId 
    );
  } else {
    this.hygieneType = new HygieneTypes(0, '', new Date(), 0);
    this.editHygieneType = false;
  }
  this.isModalVisible = true;
}

  
  closeModal() {
    this.isModalVisible = false; 
    this.hygieneType = new HygieneTypes(0, '', new Date(), 0); 
    this.editHygieneType = false;
    this.validationErrors = {};
  }

  
saveHygieneType() {
  if (this.validateForm()) {
    if (this.editHygieneType) {
      this.hygieneTypesService.Edit(this.hygieneType, this.DomainName).subscribe({
        next: () => {
          
          this.getHygieneTypes();
          this.closeModal();
        },
        error: (err) => {
          console.error('Error updating hygiene type:', err);
          
        }
      });
    } else {
      this.hygieneTypesService.Add(this.hygieneType, this.DomainName).subscribe({
        next: () => {
          
          this.getHygieneTypes();
          this.closeModal();
        },
        error: (err) => {
          console.error('Error adding hygiene type:', err);
          
        }
      });
    }
  }
}
private formatDate(date: Date | string): string {
  const dateObj = typeof date === 'string' ? new Date(date) : date;
  const options: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
  };
  return dateObj.toLocaleDateString(undefined, options);
}
  
  deleteHygieneType(row: any) {
    Swal.fire({
      title: 'Are you sure you want to delete this Hygiene Type?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.hygieneTypesService.Delete(row.id, this.DomainName).subscribe({
          next: (response) => {
            
            console.log('Delete response:', response);

            
            this.getHygieneTypes();
          },
          error: (error) => {
            console.error('Error deleting Hygiene Type:', error);
            Swal.fire('Error!', 'Failed to delete the Hygiene Type.', 'error');
          },
        });
      }
    });
  }

  
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
        this.hygieneTypes = this.hygieneTypes.filter(t => {
            const fieldValue = t[this.key as keyof typeof t];
            
            
            const searchString = this.value.toString().toLowerCase();
            const fieldString = fieldValue?.toString().toLowerCase() || '';
            
            
            return fieldString.includes(searchString);
        });
    }
}

} 