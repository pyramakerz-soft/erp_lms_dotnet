import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MedicalHistoryService } from '../../../../../Services/Employee/Clinic/medical-history.service';
import { ApiService } from '../../../../../Services/api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-medical-history-by-doctor',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './medical-history-by-doctor.component.html',
  styleUrls: ['./medical-history-by-doctor.component.css']
})
export class MedicalHistoryByDoctorComponent implements OnInit {
  medicalHistory: any = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private medicalHistoryService: MedicalHistoryService,
    private apiService: ApiService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadMedicalHistory(Number(id));
    }
  }

  async loadMedicalHistory(id: number) {
    try {
      const domainName = this.apiService.GetHeader();
      this.medicalHistoryService.GetByIdByDoctor(id, domainName).subscribe({
        next: (data: any) => {
          this.medicalHistory = data;
        },
        error: (error) => {
          console.error('Error loading medical history:', error);
        }
      });
    } catch (error) {
      console.error('Error in loadMedicalHistory:', error);
    }
  }

  hasValidFile(fileUrl: string | null): boolean {
    if (!fileUrl || fileUrl.toLowerCase() === 'string') {
      return false;
    }
    return true;
  }

  getFileName(url: string): string {
    if (!url) return 'Document';
    const parts = url.split('/');
    return parts[parts.length - 1] || 'Document';
  }

  deleteFile(fileType: 'firstReport' | 'secReport') {
    if (confirm('Are you sure you want to delete this file?')) {
      if (this.medicalHistory) {
        this.medicalHistory[fileType] = null;
      }
      
    }
  }

  goBack() {
    this.router.navigate(['/Employee/Medical Report']);
  }
}