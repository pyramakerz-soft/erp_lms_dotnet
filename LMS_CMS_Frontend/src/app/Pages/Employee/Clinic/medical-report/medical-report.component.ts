import { Component, OnInit } from '@angular/core';
import { HygieneFormService } from '../../../../Services/Employee/Clinic/hygiene-form.service';
import { FollowUpService } from '../../../../Services/Employee/Clinic/follow-up.service';
import { ApiService } from '../../../../Services/api.service';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { TableComponent } from "../../../../Component/reuse-table/reuse-table.component";
import { FollowUpComponent } from "../follow-up/follow-up.component";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HygieneFormComponent } from "../hygiene_form/hygiene-form/hygiene-form.component";
import { CreateHygieneFormComponent } from "../hygiene_form/create-hygiene-form/create-hygiene-form.component";
import { HygieneFormTableComponent } from "../hygiene_form/hygiene-form-table/hygiene-form-table.component";

@Component({
  selector: 'app-medical-report',
  templateUrl: './medical-report.component.html',
  styleUrls: ['./medical-report.component.css'],
  imports: [TableComponent, FollowUpComponent, CommonModule, FormsModule, HygieneFormComponent, CreateHygieneFormComponent, HygieneFormTableComponent],
  standalone:true
})
export class MedicalReportComponent implements OnInit {
  // Tabs
  tabs = ['MH By Parent', 'MH By Doctor', 'Hygiene Form', 'Follow Up'];
  selectedTab = this.tabs[0]; // Default selected tab

  // Mock Data for MH By Parent
  mhByParentData = [
    {
      date: '2023-10-01',
      description: 'Parent Report 1',
      insertDate: '2023-10-01',
      lastModified: '2023-10-02',
      actions: { delete: true, edit: true },
    },
    {
      date: '2023-10-02',
      description: 'Parent Report 2',
      insertDate: '2023-10-02',
      lastModified: '2023-10-03',
      actions: { delete: true, edit: true },
    },
  ];

  // Mock Data for MH By Doctor
  mhByDoctorData = [
    {
      date: '2023-10-01',
      description: 'Doctor Report 1',
      insertDate: '2023-10-01',
      lastModified: '2023-10-02',
      actions: { delete: true, edit: true },
    },
    {
      date: '2023-10-02',
      description: 'Doctor Report 2',
      insertDate: '2023-10-02',
      lastModified: '2023-10-03',
      actions: { delete: true, edit: true },
    },
  ];

  // Data for Hygiene Form
  hygieneForms: any[] = [];

  // Data for Follow Up
  followUps: any[] = [];

  constructor(
    private hygieneFormService: HygieneFormService,
    private followUpService: FollowUpService,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    this.loadHygieneForms();
    this.loadFollowUps();
  }


  

// hygiene-form.component.ts
async loadHygieneForms() {
  try {
    const domainName = this.apiService.GetHeader();
    const data = await firstValueFrom(this.hygieneFormService.Get(domainName));

    this.hygieneForms = data.map((item) => ({
      ...item,
      school: item.school,
      grade: item.grade,
      classRoom: item.classRoom,
      date: new Date(item.date).toLocaleDateString(),
      actions: { delete: true, edit: true, view: true } // Enable view action
    }));
  } catch (error) {
    console.error('Error fetching hygiene forms:', error);
    // Swal.fire('Error', 'Failed to load hygiene forms. Please try again later.', 'error');
  }
}

  // Load Follow Ups from Backend
async loadFollowUps() {
  try {
    const domainName = this.apiService.GetHeader();
    const data = await firstValueFrom(this.followUpService.Get(domainName));
    console.log(data)
    
    this.followUps = data.map((item) => {
      return {
        id: item.id,
        schoolName: item.school || 'N/A', // Map 'school' to 'schoolName'
        gradeName: item.grade || 'N/A', // Map 'grade' to 'gradeName'
        className: item.classroom || 'N/A', // Map 'classroom' to 'className'
        studentName: item.student || 'N/A', // Map 'student' to 'studentName'
        complaints: item.complains || "No Complaints", // Map 'complains' to 'complaints'
        // diagnosisName: this.diagnoses.find(d => d.id === item.diagnosisId)?.name || 'N/A', // Map diagnosis ID to name
        recommendation: item.recommendation || "No Recommendation", // Map 'recommendation'
        actions: { edit: true, delete: true } // Add actions property
      };
    });
  } catch (error) {
    console.error('Error fetching follow-ups:', error);
    // Swal.fire('Error', 'Failed to load follow-ups.', 'error');
  }
}

  // Delete Hygiene Form
  deleteHygieneForm(row: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this hygiene form!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#2E3646',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.isConfirmed) {
        this.hygieneForms = this.hygieneForms.filter((item) => item.id !== row.id);
        Swal.fire('Deleted!', 'The hygiene form has been deleted.', 'success');
      }
    });
  }

  // Delete Follow Up
  deleteFollowUp(row: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this follow-up!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#2E3646',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.isConfirmed) {
        this.followUps = this.followUps.filter((item) => item.id !== row.id);
        Swal.fire('Deleted!', 'The follow-up has been deleted.', 'success');
      }
    });
  }

  // Other methods (selectTab, exportToExcel, exportToPDF, printTable, etc.)
  selectTab(tab: string) {
    this.selectedTab = tab;
  }

  exportToExcel() {
    console.log('Exporting to Excel:', this.selectedTab);
  }

  exportToPDF() {
    console.log('Exporting to PDF:', this.selectedTab);
  }

  printTable() {
    console.log('Printing Table:', this.selectedTab);
  }
}