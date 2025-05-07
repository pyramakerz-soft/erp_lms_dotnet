import { Component, Input, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../../Component/search/search.component';
import Swal from 'sweetalert2';
import { TableComponent } from "../../../../../Component/reuse-table/reuse-table.component";
import { Router } from '@angular/router';
import { HygieneFormService } from '../../../../../Services/Employee/Clinic/hygiene-form.service';
import { ApiService } from '../../../../../Services/api.service';
import { HygieneForm } from '../../../../../Models/Clinic/HygieneForm';

@Component({
  selector: 'app-hygiene-form',
  standalone: true,
  imports: [FormsModule, CommonModule, TableComponent, SearchComponent],
  templateUrl: './hygiene-form.component.html',
  styleUrls: ['./hygiene-form.component.css']
})
export class HygieneFormComponent implements OnInit {
  @Input() students: any[] = [];
  @Input() hygieneTypes: any[] = [];
  
  hygieneForms: HygieneForm[] = []; 
  originalHygieneForms: HygieneForm[] = []; 
  hygieneForm: any = { id: null, grade: '', classes: '' };
  editHygieneForm = false;
  validationErrors: { [key: string]: string } = {};
  isModalVisible = false;
  DomainName: string = '';
  key: string = 'id'; 
  value: any = ''; 
  keysArray: string[] = ['id', 'school', 'grade', 'classRoom', 'date']; 

  constructor(
    private router: Router,
    private hygieneFormService: HygieneFormService,
    private apiService: ApiService
  ) {}

  ngOnInit() {
      this.DomainName = this.apiService.GetHeader(); 
    this.loadHygieneForms();
  }

  async loadHygieneForms() {
      try {
          const domainName = this.apiService.GetHeader();
          const data = await firstValueFrom(this.hygieneFormService.Get(domainName));          
          this.originalHygieneForms = data.map((item) => ({
              ...item,
              school: item.school,
              grade: item.grade,
              classRoom: item.classRoom,
              date: new Date(item.date).toLocaleDateString(),
              actions: { delete: true, edit: true, view: true }
          }));

          
          this.hygieneForms = [...this.originalHygieneForms];
      } catch (error) {
          console.error('Error fetching hygiene forms:', error);
      }
  }

async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    
    // Always start with original data
    this.hygieneForms = [...this.originalHygieneForms];
    
    if (this.value) {
        this.hygieneForms = this.hygieneForms.filter(form => {
            const fieldValue = form[this.key as keyof typeof form]?.toString().toLowerCase() || '';
            return fieldValue.includes(this.value.toString().toLowerCase());
        });
    }
}

  
  // applySearchFilter() {
  //   const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);

  //   this.hygieneForms = this.hygieneForms.filter((form) => {
  //     const fieldValue = form[this.key as keyof HygieneForm];
  //     if (typeof fieldValue === 'string') {
  //       return fieldValue.toLowerCase().includes(this.value.toLowerCase());
  //     }
  //     if (typeof fieldValue === 'number') {
  //       return fieldValue === numericValue;
  //     }
  //     return false;
  //   });
  // }

  navigateToCreateHygieneForm() {
    this.router.navigate(['/Employee/Create Hygiene Form']);
  }

  openModal(id?: number) {
    if (id) {
      this.editHygieneForm = true;
      this.hygieneForm = this.hygieneForms.find(hf => hf.id === id);
    } else {
      this.hygieneForm = { id: null, grade: '', classes: '' };
      this.editHygieneForm = false;
    }
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
    this.hygieneForm = { id: null, grade: '', classes: '' };
    this.editHygieneForm = false;
    this.validationErrors = {};
  }

  saveHygieneForm() {
    if (this.validateForm()) {
      if (this.editHygieneForm) {
        const index = this.hygieneForms.findIndex(hf => hf.id === this.hygieneForm.id);
        this.hygieneForms[index] = { ...this.hygieneForm, actions: { delete: true, edit: true } };
      } else {
        this.hygieneForm.id = this.hygieneForms.length + 1;
        this.hygieneForms.push({ ...this.hygieneForm, date: new Date().toISOString().split('T')[0], actions: { delete: true, edit: true } });
      }
      this.closeModal();
    }
  }

deleteHygieneForm(row: any) {
  Swal.fire({
    title: 'Are you sure?',
    text: 'You will not be able to recover this hygiene form!',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#FF7519',
    cancelButtonColor: '#2E3646',
    confirmButtonText: 'Yes, delete it!',
    cancelButtonText: 'No, keep it'
  }).then((result) => {
    if (result.isConfirmed) {
      this.hygieneFormService.Delete(row.id, this.DomainName).subscribe({
        next: (response) => {
          console.log('Delete response:', response);
          // Check if this was the last item
          if (this.hygieneForms.length === 1) {
            this.hygieneForms = []; // Clear the array immediately
          }
          this.loadHygieneForms(); // Refresh the data
          Swal.fire('Deleted!', 'The hygiene form has been deleted.', 'success');
        },
        error: (error) => {
          console.error('Error deleting hygiene form:', error);
          Swal.fire('Error!', 'Failed to delete the hygiene form.', 'error');
        },
      });
    }
  });
}

  validateForm(): boolean {
    let isValid = true;
    if (!this.hygieneForm.grade) {
      this.validationErrors['grade'] = '*Grade is required';
      isValid = false;
    } else {
      this.validationErrors['grade'] = '';
    }
    if (!this.hygieneForm.classes) {
      this.validationErrors['classes'] = '*Classes are required';
      isValid = false;
    } else {
      this.validationErrors['classes'] = '';
    }
    return isValid;
  }

  onInputValueChange(event: { field: string, value: any }) {
    const { field, value } = event;
    this.hygieneForm[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  onView(row: any) {
    this.router.navigate(['/Employee/view hygiene form', row.id]);
  }
}