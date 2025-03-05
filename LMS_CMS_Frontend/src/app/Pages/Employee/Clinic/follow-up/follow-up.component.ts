import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '../../../../Component/search/search.component';
import Swal from 'sweetalert2';
import { FollowUpService } from '../../../../Services/Employee/Clinic/follow-up.service';

import { DiagnosisService } from '../../../../Services/Employee/Clinic/diagnosis.service';
import { ApiService } from '../../../../Services/api.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { StudentService } from '../../../../Services/student.service';
import { TableComponent } from "../../../../Component/reuse-table/reuse-table.component";

@Component({
  selector: 'app-follow-up',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, TableComponent],
  templateUrl: './follow-up.component.html',
  styleUrls: ['./follow-up.component.css'],
})
export class FollowUpComponent implements OnInit {
  // Table Headers
  headers: string[] = ['ID', 'School', 'Grade', 'Class', 'Student', 'Complaints', 'Diagnosis', 'Recommendation', 'Actions'];

  // Table Data
  followUps: any[] = [];
  isModalVisible = false;

  // Define keys for table columns
  keys: string[] = ['id', 'schoolName', 'gradeName', 'className', 'studentName', 'complaints', 'diagnosisName', 'recommendation'];

  // Modal Data
  followUp: any = {
    schoolId: null,
    gradeId: null,
    classId: null,
    studentId: null,
    complaints: '',
    diagnosisId: null,
    recommendation: '',
    sendSms: false,
  };

  // Dropdown Options (Fetched from Backend)
  schools: any[] = [];
  grades: any[] = [];
  classes: any[] = [];
  students: any[] = [];
  diagnoses: any[] = [];

  // Define keysArray for search component
  keysArray: string[] = ['schoolName', 'gradeName', 'className', 'studentName', 'complaints', 'diagnosisName', 'recommendation'];

  constructor(
    private followUpService: FollowUpService,
    private schoolService: SchoolService,
    private gradeService: GradeService,
    private studentService: StudentService,
    private diagnosisService: DiagnosisService,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    this.loadFollowUps();
    this.loadDropdownOptions();
  }

async loadFollowUps() {
  try {
    const domainName = this.apiService.GetHeader(); // Get the domain name from ApiService
    const data = await firstValueFrom(this.followUpService.Get(domainName));

    // Map the data to match the keys array
    this.followUps = data.map((item) => {
      // Find the school name using schoolId
      const school = this.schools.find(s => s.id === item.schoolId);
      const grade = this.grades.find(g => g.id === item.gradeId);
      const student = this.students.find(s => s.id === item.studentId);
      const diagnosis = this.diagnoses.find(d => d.id === item.diagnosisId);

      return {
        id: item.id,
        schoolName: school?.name || 'N/A', // Use the school name
        gradeName: grade?.name || 'N/A', // Use the grade name
        studentName: student?.name || 'N/A', // Use the student name
        complaints: item.complains,
        diagnosisName: diagnosis?.name || 'N/A', // Use the diagnosis name
        recommendation: item.recommendation,
        actions: { delete: true, edit: true }, // Add actions dynamically
      };
    });
  } catch (error) {
    console.error('Error fetching follow-ups:', error);
    Swal.fire('Error', 'Failed to load follow-ups. Please try again later.', 'error');
  }
}

  // Load dropdown options from backend
  async loadDropdownOptions() {
    try {
      const domainName = this.apiService.GetHeader(); // Get the domain name from ApiService

      // Fetch schools
      this.schools = await firstValueFrom(this.schoolService.Get(domainName));

      // Fetch grades
      this.grades = await firstValueFrom(this.gradeService.Get(domainName));

      // Fetch classes

      // Fetch students
      this.students = await firstValueFrom(this.studentService.GetAll(domainName));

      // Fetch diagnoses
      this.diagnoses = await firstValueFrom(this.diagnosisService.Get(domainName));
    } catch (error) {
      console.error('Error loading dropdown options:', error);
      Swal.fire('Error', 'Failed to load dropdown options. Please try again later.', 'error');
    }
  }

  // Open modal for create/edit
  openModal(id?: number) {
    this.isModalVisible = true;
    if (id) {
      // Load existing follow-up data for editing
      const existingFollowUp = this.followUps.find(f => f.id === id);
      if (existingFollowUp) {
        this.followUp = { ...existingFollowUp };
      }
    } else {
      // Reset form for new entry
      this.followUp = {
        schoolId: null,
        gradeId: null,
        classId: null,
        studentId: null,
        complaints: '',
        diagnosisId: null,
        recommendation: '',
        sendSms: false,
      };
    }
  }

  // Close modal
  closeModal() {
    this.isModalVisible = false;
  }

  // Save or update follow-up
  async saveFollowUp() {
    try {
      const domainName = this.apiService.GetHeader(); // Get the domain name from ApiService
      if (this.followUp.id) {
        // Update existing follow-up
        await firstValueFrom(this.followUpService.Edit(this.followUp, domainName));
      } else {
        // Add new follow-up
        await firstValueFrom(this.followUpService.Add(this.followUp, domainName));
      }
      this.loadFollowUps(); // Refresh the table
      this.closeModal();
    } catch (error) {
      console.error('Error saving follow-up:', error);
      Swal.fire('Error', 'Failed to save follow-up. Please try again later.', 'error');
    }
  }

  // Delete follow-up
  deleteFollowUp(row: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this follow-up!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#2E3646',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.isConfirmed) {
        const domainName = this.apiService.GetHeader(); // Get the domain name from ApiService
        this.followUpService.Delete(row.id, domainName).subscribe({
          next: () => {
            this.loadFollowUps(); // Refresh the table
            Swal.fire('Deleted!', 'The follow-up has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting follow-up:', error);
            Swal.fire('Error', 'Failed to delete follow-up. Please try again later.', 'error');
          },
        });
      }
    });
  }

  // Handle search event
  onSearchEvent(event: { key: string, value: any }) {
    const { key, value } = event;
    if (value) {
      this.followUps = this.followUps.filter(f => f[key].toString().toLowerCase().includes(value.toLowerCase()));
    } else {
      this.loadFollowUps(); // Reset to full list if search is empty
    }
  }
}