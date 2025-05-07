import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MedicalHistoryByParent } from '../../../../../Models/Clinic/mh-by-parent';
import { MedicalReportService } from '../../../../../Services/Employee/Clinic/medical-report.service';
import { ApiService } from '../../../../../Services/api.service';

@Component({
  selector: 'app-medical-history-by-parent',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './medical-history-by-parent.component.html',
  styleUrl: './medical-history-by-parent.component.css'
})
export class MedicalHistoryByParentComponent implements OnInit {
  medicalHistory: MedicalHistoryByParent | null = null; 

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private medicalReportService: MedicalReportService, 
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
      this.medicalReportService.getMHByParentById(id, domainName).subscribe({
        next: (data: MedicalHistoryByParent) => {
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

//   getFileName(url: string): string {
//     if (!url) return 'Document';
//     const parts = url.split('/');
//     return parts[parts.length - 1] || 'Document';
// }

// getFileSize(): number {
//     // You might want to implement actual file size calculation
//     // This is just a placeholder
//     return Math.floor(Math.random() * 500) + 100; // Random size between 100-600 KB
// }

getFileName(url: string): string {
    if (!url) return 'Document';
    const parts = url.split('/');
    return parts[parts.length - 1] || 'Document';
}

// deleteFile(fileType: 'firstReport' | 'secReport') {
//     // Confirm before deleting
//     if (confirm('Are you sure you want to delete this file?')) {
//         // Here you would typically call a service to delete the file
//         // For now, we'll just remove it from the UI
//         if (this.medicalHistory) {
//             this.medicalHistory[fileType] = null;
//         }
        
//         // TODO: Add API call to actually delete the file from the server
//         // this.medicalReportService.deleteFile(this.medicalHistory?.id, fileType)
//         //   .subscribe(...);
//     }
// }

  
  goBack() {
    this.router.navigate(['/Employee/Medical Report']);
  }
}