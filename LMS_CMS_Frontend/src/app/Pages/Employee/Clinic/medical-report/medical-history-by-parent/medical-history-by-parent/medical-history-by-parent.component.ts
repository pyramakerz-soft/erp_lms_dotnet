import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MedicalReportService } from '../../../../../../Services/Employee/Clinic/medical-report.service';
import { ApiService } from '../../../../../../Services/api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MedicalHistoryByParent } from '../../../../../../Models/Clinic/mh-by-parent'; // Import the model

@Component({
  selector: 'app-medical-history-by-parent',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './medical-history-by-parent.component.html',
  styleUrl: './medical-history-by-parent.component.css'
})
export class MedicalHistoryByParentComponent implements OnInit {
  medicalHistory: MedicalHistoryByParent | null = null; // Corrected type and initialization

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private medicalReportService: MedicalReportService, // Service for fetching data
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
      const domainName = this.apiService.GetHeader(); // Get the domain name
      this.medicalReportService.getMHByParentById(id, domainName).subscribe({
        next: (data: MedicalHistoryByParent) => {
          this.medicalHistory = data; // Assign the fetched data to the correct variable
        },
        error: (error) => {
          console.error('Error loading medical history:', error);
        }
      });
    } catch (error) {
      console.error('Error in loadMedicalHistory:', error);
    }
  }

  // Method to navigate back to the medical report page
  goBack() {
    this.router.navigate(['/Employee/medical-report']);
  }
}