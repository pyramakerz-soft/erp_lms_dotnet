import { Component } from '@angular/core';
import { HygieneFormComponent } from '../hygiene-form/hygiene-form.component';
import { FollowUpComponent } from '../follow-up/follow-up.component';
import { TableComponent } from '../../../../Component/reuse-table/reuse-table.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-medical-report',
  standalone: true,
  imports: [HygieneFormComponent, FollowUpComponent, TableComponent , CommonModule],
  templateUrl: './medical-report.component.html',
  styleUrls: ['./medical-report.component.css'],
})
export class MedicalReportComponent {
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

  // Select Tab
  selectTab(tab: string) {
    this.selectedTab = tab;
  }

  // Delete MH By Parent
  deleteMHParent(row: any) {
    this.mhByParentData = this.mhByParentData.filter((item) => item !== row);
  }

  // Edit MH By Parent
  editMHParent(row: any) {
    console.log('Edit MH By Parent:', row);
  }

  // Delete MH By Doctor
  deleteMHDoctor(row: any) {
    this.mhByDoctorData = this.mhByDoctorData.filter((item) => item !== row);
  }

  // Edit MH By Doctor
  editMHDoctor(row: any) {
    console.log('Edit MH By Doctor:', row);
  }

  // Export to Excel
  exportToExcel() {
    console.log('Exporting to Excel:', this.selectedTab);
    // Add logic to export table data to Excel
  }

  // Export to PDF
  exportToPDF() {
    console.log('Exporting to PDF:', this.selectedTab);
    // Add logic to export table data to PDF
  }

  // Print Table
  printTable() {
    console.log('Printing Table:', this.selectedTab);
    // Add logic to print the table
  }
}