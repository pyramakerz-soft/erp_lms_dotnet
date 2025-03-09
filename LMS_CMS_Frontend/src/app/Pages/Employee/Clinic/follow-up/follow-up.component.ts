// follow-up.component.ts
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
import { DrugService } from '../../../../Services/Employee/Clinic/drug.service';
import { DoseService } from '../../../../Services/Employee/Clinic/dose.service';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { FollowUp } from '../../../../Models/Clinic/FollowUp';

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
  keys: string[] = ['id', 'school', 'grade', 'classroom', 'student', 'complaints', 'diagnosis', 'recommendation'];

  // Modal Data
  followUp: FollowUp = new FollowUp()

  // Dropdown Options (Fetched from Backend)
  schools: any[] = [];
  grades: any[] = [];
  classroom: any[] = [];
  classes: any[] = [];
  students: any[] = [];
  diagnoses: any[] = [];
  drugs: any[] = [];
  doses: any[] = [];

  // Selected Drug and Dose
  selectedDrugId: number | null = null;
  selectedDoseId: number | null = null;

  // List of selected drugs and doses
  drugDoseList: any[] = [];

  // Define keysArray for search component
  keysArray: string[] = ['school', 'grade', 'classroom', 'studentName', 'complaints', 'diagnosisName', 'recommendation'];
editMode: any;

  constructor(
    private followUpService: FollowUpService,
    private schoolService: SchoolService,
    private gradeService: GradeService,
    private classroomService: ClassroomService,
    private studentService: StudentService,
    private diagnosisService: DiagnosisService,
    private drugService: DrugService,
    private doseService: DoseService,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    this.loadFollowUps();
    this.loadDropdownOptions();
  }

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
        diagnosisName: this.diagnoses.find(d => d.id === item.diagnosisId)?.name || 'N/A', // Map diagnosis ID to name
        recommendation: item.recommendation || "No Recommendation", // Map 'recommendation'
        actions: { edit: true, delete: true } // Add actions property
      };
    });
  } catch (error) {
    console.error('Error fetching follow-ups:', error);
    Swal.fire('Error', 'Failed to load follow-ups.', 'error');
  }
}



async loadDropdownOptions() {
  try {
    const domainName = this.apiService.GetHeader();
    
    // Fetch necessary data
    this.schools = await firstValueFrom(this.schoolService.Get(domainName));
    this.grades = await firstValueFrom(this.gradeService.Get(domainName));
    this.diagnoses = await firstValueFrom(this.diagnosisService.Get(domainName));
    this.drugs = await firstValueFrom(this.drugService.Get(domainName));
    this.doses = await firstValueFrom(this.doseService.Get(domainName));

    // Fetch classrooms
    this.classes = await firstValueFrom(this.classroomService.Get(domainName));

    // Fetch students with correct property mapping
    const studentsData = await firstValueFrom(this.studentService.GetAll(domainName));
    this.students = studentsData.map(student => ({ id: student.id, name: student.en_name }));

  } catch (error) {
    console.error('Error loading dropdown options:', error);
    Swal.fire('Error', 'Failed to load dropdown options.', 'error');
  }
}


// // Fetch classes when grade is selected
// async loadClasses() {
//   try {
//     const domainName = this.apiService.GetHeader();
//     this.classes = await firstValueFrom(this.classroomService.GetByGradeId(this.followUp.gradeId, domainName));
//   } catch (error) {
//     console.error('Error loading classes:', error);
//   }
// }

// // Fetch students when class is selected
// async loadStudents() {
//   try {
//     const domainName = this.apiService.GetHeader();
//     this.students = await firstValueFrom(this.studentService.GetByClassID(this.followUp.classId, domainName));
//   } catch (error) {
//     console.error('Error loading students:', error);
//   }
// }


async loadDrugsAndDoses() {
  try {
    const domainName = this.apiService.GetHeader();
    this.drugs = await firstValueFrom(this.drugService.Get(domainName));
    this.doses = await firstValueFrom(this.doseService.Get(domainName));
  } catch (error) {
    console.error('Error loading drugs and doses:', error);
    Swal.fire('Error', 'Failed to load drug and dose options.', 'error');
  }
}

addDrugAndDose() {
  if (this.selectedDrugId && this.selectedDoseId) {
    // Add the selected drug and dose to the followUpDrugs array
    this.followUp.followUpDrugs.push({
      drugId: this.selectedDrugId,
      doseId: this.selectedDoseId
    });
          console.log('test')


    // Reset selections
    this.selectedDrugId = null;
    this.selectedDoseId = null;
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
      this.followUp = new FollowUp()
    }
  }

  // Close modal
  closeModal() {
    this.isModalVisible = false;
  }

async saveFollowUp() {
  try {
    const domainName = this.apiService.GetHeader();

    // Validate required fields
    if (!this.followUp.schoolId || !this.followUp.gradeId || !this.followUp.classroomId || !this.followUp.studentId) {
      Swal.fire('Error', 'Please fill all required fields.', 'error');
      return;
    }

    // Log the followUp object for debugging
    console.log('FollowUp Object:', this.followUp);

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