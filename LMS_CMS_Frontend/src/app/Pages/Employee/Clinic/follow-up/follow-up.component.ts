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
import { Drug } from '../../../../Models/Clinic/drug';
import { Dose } from '../../../../Models/Clinic/dose';

@Component({
  selector: 'app-follow-up',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, TableComponent],
  templateUrl: './follow-up.component.html',
  styleUrls: ['./follow-up.component.css'],
})
export class FollowUpComponent implements OnInit {
addDose() {
throw new Error('Method not implemented.');
}
addDrug() {
throw new Error('Method not implemented.');
}
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

  // For Drug Modal
isDrugModalVisible: boolean = false;
drug: Drug = new Drug(0, '', new Date().toISOString());
editDrug: boolean = false;
drugValidationErrors: { [key: string]: string } = {};

// For Dose Modal
isDoseModalVisible: boolean = false;
dose: Dose = new Dose(0, '', new Date().toISOString());
editDose: boolean = false;
doseValidationErrors: { [key: string]: string } = {};

// Open Drug Modal
openDrugModal() {
    this.isDrugModalVisible = true;
    this.drug = new Drug(0, '', new Date().toISOString()); // Reset drug form
    this.editDrug = false;
    this.drugValidationErrors = {};
}

// Close Drug Modal
closeDrugModal() {
    this.isDrugModalVisible = false;
}


// Save Drug
async saveDrug() {
    if (!this.drug.name) {
        this.drugValidationErrors['name'] = '*Name is required';
        return;
    }

    try {
        const domainName = this.apiService.GetHeader();
        await firstValueFrom(this.drugService.Add(this.drug, domainName));
        this.loadDropdownOptions(); // Reload drugs dropdown
        this.closeDrugModal();
    } catch (error) {
        console.error('Error saving drug:', error);
        Swal.fire('Error', 'Failed to save drug.', 'error');
    }
}

// Open Dose Modal
openDoseModal() {
    this.isDoseModalVisible = true;
    this.dose = new Dose(0, '', new Date().toISOString()); // Reset dose form
    this.editDose = false;
    this.doseValidationErrors = {};
}

// Close Dose Modal
closeDoseModal() {
    this.isDoseModalVisible = false;
}

// Save Dose
async saveDose() {
    if (!this.dose.doseTimes) {
        this.doseValidationErrors['doseTimes'] = '*Dose Times is required';
        return;
    }

    try {
        const domainName = this.apiService.GetHeader();
        await firstValueFrom(this.doseService.Add(this.dose, domainName));
        this.loadDropdownOptions(); // Reload doses dropdown
        this.closeDoseModal();
    } catch (error) {
        console.error('Error saving dose:', error);
        Swal.fire('Error', 'Failed to save dose.', 'error');
    }
}



  ngOnInit(): void {
    this.loadFollowUps();
    this.loadDropdownOptions();
  }

// Method to handle school selection change
onSchoolChange(event: Event) {
  const selectedSchoolId = (event.target as HTMLSelectElement).value;
  this.followUp.gradeId = 0; // Reset grade
  this.followUp.classroomId = 0; // Reset class
  this.followUp.studentId = 0; // Reset student
  this.grades = []; // Clear grades
  this.classes = []; // Clear classes
  this.students = []; // Clear students

  if (selectedSchoolId) {
    this.loadGrades(Number(selectedSchoolId)); // Load grades for the selected school
  }
}

// Method to handle grade selection change
onGradeChange(event: Event) {
  const selectedGradeId = (event.target as HTMLSelectElement).value;
  this.followUp.classroomId = 0; // Reset class
  this.followUp.studentId = 0; // Reset student
  this.classes = []; // Clear classes
  this.students = []; // Clear students

  if (selectedGradeId) {
    this.loadClasses(Number(selectedGradeId)); // Load classes for the selected grade
  }
}

// Method to handle class selection change
onClassChange(event: Event) {
  const selectedClassId = (event.target as HTMLSelectElement).value;
  this.followUp.studentId = 0; // Reset student
  this.students = []; // Clear students

  if (selectedClassId) {
    this.loadStudents(Number(selectedClassId)); // Load students for the selected class
  }
}

// Method to load grades for a selected school
async loadGrades(schoolId: number) {
  try {
    const domainName = this.apiService.GetHeader();
    this.grades = await firstValueFrom(this.gradeService.GetBySchoolId(schoolId, domainName));
  } catch (error) {
    console.error('Error loading grades:', error);
    Swal.fire('Error', 'Failed to load grades.', 'error');
  }
}

// Method to load classes for a selected grade
async loadClasses(gradeId: number) {
  try {
    const domainName = this.apiService.GetHeader();
    this.classes = await firstValueFrom(this.classroomService.GetByGradeId(gradeId, domainName));
  } catch (error) {
    console.error('Error loading classes:', error);
    Swal.fire('Error', 'Failed to load classes.', 'error');
  }
}

// Method to load students for a selected class
async loadStudents(classId: number) {
  try {
    const domainName = this.apiService.GetHeader();
    const studentsData = await firstValueFrom(this.studentService.GetByClassID(classId, domainName));
    this.students = studentsData.map(student => ({ id: student.id, name: student.en_name }));
  } catch (error) {
    console.error('Error loading students:', error);
    Swal.fire('Error', 'Failed to load students.', 'error');
  }
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

// Method to add a drug and dose to the table
addDrugAndDose() {
  console.log('Selected Drug ID:', this.selectedDrugId); // Debug: Log selected drug ID
  console.log('Selected Dose ID:', this.selectedDoseId); // Debug: Log selected dose ID
  let selectedDrug = new Drug()
  let selectedDose = new Dose()
  if (this.selectedDrugId && this.selectedDoseId) {
    // Find the selected drug and dose from their respective lists
    console.log(this.drugs)
    this.drugs.forEach(element => {
      if(element.id == this.selectedDrugId){
        selectedDrug = element
      }
    });
    this.doses.forEach(element => {
      if(element.id == this.selectedDoseId){
        selectedDose = element
      }
    });
    // const selectedDrug = this.drugs.find(drug => drug.id === this.selectedDrugId);
    // const selectedDose = this.doses.find(dose => dose.id === this.selectedDoseId);

    console.log('Selected Drug:', selectedDrug); // Debug: Log selected drug
    console.log('Selected Dose:', selectedDose); // Debug: Log selected dose

    if (selectedDrug && selectedDose) {
      // Add the drug and dose to the display table
      this.drugDoseList.push({
        drugName: selectedDrug.name, // Use the drug name for display
        doseTimes: selectedDose.doseTimes // Use the dose times for display
      });

      // Add the drug and dose IDs to the followUp.followUpDrugs array
      this.followUp.followUpDrugs.push({
        drugId: selectedDrug.id, // Use the drug ID for saving
        doseId: selectedDose.id // Use the dose ID for saving
      });

      console.log('Updated drugDoseList:', this.drugDoseList); // Debug: Log updated display list
      console.log('Updated followUp.followUpDrugs:', this.followUp.followUpDrugs); // Debug: Log updated save list

      // Reset the selected drug and dose
      this.selectedDrugId = null;
      this.selectedDoseId = null;
    } else {
      console.error('Selected drug or dose not found in the list.'); // Debug: Log error
    }
  } else {
    console.error('Please select both a drug and a dose.'); // Debug: Log error
    Swal.fire('Error', 'Please select both a drug and a dose.', 'error');
  }
}


openModal(id?: number) {
  this.isModalVisible = true;
  if (id) {
    // Load existing follow-up data for editing
    const existingFollowUp = this.followUps.find(f => f.id === id);
    if (existingFollowUp) {
      this.followUp = { ...existingFollowUp };

      // Load the related dropdowns based on the existing data
      if (this.followUp.schoolId) {
        this.loadGrades(this.followUp.schoolId);
      }
      if (this.followUp.gradeId) {
        this.loadClasses(this.followUp.gradeId);
      }
      if (this.followUp.classroomId) {
        this.loadStudents(this.followUp.classroomId);
      }

      // Populate the drugDoseList for display
      this.drugDoseList = this.followUp.followUpDrugs.map(fd => {
        const drug = this.drugs.find(d => d.id === fd.drugId);
        const dose = this.doses.find(d => d.id === fd.doseId);
        return {
          drugName: drug ? drug.name : 'N/A',
          doseTimes: dose ? dose.doseTimes : 'N/A'
        };
      });
    }
  } else {
    // Reset form for new entry
    this.followUp = new FollowUp();
    this.followUp.schoolId = 0; // Ensure schoolId is 0
    this.followUp.gradeId = 0; // Ensure gradeId is 0
    this.followUp.classroomId = 0; // Ensure classroomId is 0
    this.followUp.studentId = 0; // Ensure studentId is 0
    this.grades = []; // Clear grades
    this.classes = []; // Clear classes
    this.students = []; // Clear students
    this.drugDoseList = []; // Clear drugDoseList
  }
}

closeModal() {
  this.isModalVisible = false;
  this.drugDoseList = []; // Reset the display table
  this.followUp = new FollowUp(); // Reset the form and followUp.followUpDrugs
  this.grades = []; // Clear grades
  this.classes = []; // Clear classes
  this.students = []; // Clear students
  console.log('Modal closed. drugDoseList and followUp.followUpDrugs reset.'); // Debug: Log reset
}

async saveFollowUp() {
  // Validate required fields
  if (!this.followUp.schoolId || !this.followUp.gradeId || !this.followUp.classroomId || !this.followUp.studentId) {
    Swal.fire('Error', 'Please fill all required fields (School, Grade, Class, and Student).', 'error');
    return;
  }

  try {
    const domainName = this.apiService.GetHeader();

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

// Method to delete a drug and dose row
deleteDrugDoseRow(index: number) {
  // Remove the row from the display table
  this.drugDoseList.splice(index, 1);

  // Remove the corresponding entry from the followUp.followUpDrugs array
  this.followUp.followUpDrugs.splice(index, 1);

  console.log('Deleted row at index:', index); // Debug: Log deletion
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