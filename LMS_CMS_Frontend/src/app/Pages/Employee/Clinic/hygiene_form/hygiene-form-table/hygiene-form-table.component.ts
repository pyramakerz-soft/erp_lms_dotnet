import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Student } from '../../../../../Models/student';

@Component({
  selector: 'app-hygiene-form-table',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './hygiene-form-table.component.html',
  styleUrls: ['./hygiene-form-table.component.css'],
})
export class HygieneFormTableComponent {
  @Input() students: any[] = [];
  @Input() hygieneTypes: any[] = [];
  @Input() isViewOnly: boolean = false; // Add this input
  @Input() showSelectAll: boolean = true; // New input property to control visibility of "Select All" column



  // Set Hygiene Type for a Specific Student
  setHygieneType(student: Student, hygieneTypeId: number, value: boolean) {
    student[`hygieneType_${hygieneTypeId}`] = value;
  }

  // Set All Hygiene Types for a Specific Student
  setAllHygieneTypesForStudent(student: Student, value: boolean) {
    this.hygieneTypes.forEach(hygieneType => {
      student[`hygieneType_${hygieneType.id}`] = value;
    });
  }
}