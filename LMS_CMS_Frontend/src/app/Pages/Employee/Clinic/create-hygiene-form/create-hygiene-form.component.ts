import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-hygiene-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './create-hygiene-form.component.html',
  styleUrls: ['./create-hygiene-form.component.css'],
})
export class CreateHygieneFormComponent {
  headers = ['Student', 'Attendance', 'Select All', 'Hair', 'Nails', 'Comment', 'Action Taken'];
  students = [
    { id: 1, name: 'John Doeeeeeeeeeeeeeeeeeeeeee', attendance: null, selectAll: null, hair: null, nails: null, comment: '', actionTaken: '' },
    { id: 2, name: 'Jane Smith', attendance: null, selectAll: null, hair: null, nails: null, comment: '', actionTaken: '' },
  ];

  schools = [
    { id: 1, name: 'School A' },
    { id: 2, name: 'School B' },
  ];

  grades = [
    { id: 1, name: 'Grade 1' },
    { id: 2, name: 'Grade 2' },
  ];

  classes = [
    { id: 1, name: 'Class A' },
    { id: 2, name: 'Class B' },
  ];

  selectedSchool: number | null = null;
  selectedGrade: number | null = null;
  selectedClass: number | null = null;
  selectedDate: string = '';
  errorMessage: any;

  constructor(private router: Router) {}

// Toggle Attendance
toggleAttendance(student: any) {
  student.attendance = !student.attendance;
}

  // Set Select All
  setSelectAll(student: any, value: boolean) {
    student.selectAll = value;
    student.hair = value;
    student.nails = value;
  }

  // Set Hair
setHair(student: any, value: boolean) {
  student.hair = value;
}

// Set Nails
setNails(student: any, value: boolean) {
  student.nails = value;
}
  // Toggle Hair
  toggleHair(student: any) {
    if (student.hair === null) student.hair = true;
    else if (student.hair === true) student.hair = false;
    else student.hair = null;
  }

  // Toggle Nails
  toggleNails(student: any) {
    if (student.nails === null) student.nails = true;
    else if (student.nails === true) student.nails = false;
    else student.nails = null;
  }

// Save Hygiene Form
saveHygieneForm() {//not paired to backend yet
  const newHygieneForm = {
    id: this.students.length + 1,
    school: this.schools.find(s => s.id === this.selectedSchool)?.name,
    grade: this.grades.find(g => g.id === this.selectedGrade)?.name,
    class: this.classes.find(c => c.id === this.selectedClass)?.name,
    date: this.selectedDate,
    students: this.students,
  };

  // Retrieve existing hygiene forms from localStorage
  const savedForms = localStorage.getItem('hygieneForms');
  let existingForms = [];

  if (savedForms) {
    try {
      // Parse the saved forms and ensure it's an array
      existingForms = JSON.parse(savedForms);
      if (!Array.isArray(existingForms)) {
        console.error('Saved forms is not an array. Resetting to an empty array.');
        existingForms = [];
      }
    } catch (error) {
      console.error('Error parsing saved forms. Resetting to an empty array.', error);
      existingForms = [];
    }
  }

  // Add the new form to the list
  existingForms.push(newHygieneForm);

  // Save the updated list back to localStorage
  localStorage.setItem('hygieneForms', JSON.stringify(existingForms));

  // Navigate back to the main Hygiene Form page
  this.router.navigate(['/Employee/Hygiene Form']);
}
}