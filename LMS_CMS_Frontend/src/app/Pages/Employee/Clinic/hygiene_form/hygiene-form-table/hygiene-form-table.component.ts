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
  @Input() hygieneForms: any[] = []; // Add this line

  // Set Attendance
setAttendance(student: Student, value: boolean) {
  student['attendance'] = value; // Use bracket notation
}

// Set Hygiene Type
setHygieneType(student: Student, hygieneTypeId: number, value: boolean) {
  student[`hygieneType_${hygieneTypeId}`] = value; // This is already using bracket notation
}
}