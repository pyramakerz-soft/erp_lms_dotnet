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
import { SchoolService } from '../../../../Services/Employee/school.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { StudentService } from '../../../../Services/student.service';
import { MedicalReportService } from '../../../../Services/Employee/Clinic/medical-report.service';

@Component({
  selector: 'app-medical-report',
  templateUrl: './medical-report.component.html',
  styleUrls: ['./medical-report.component.css'],
  imports: [TableComponent, FollowUpComponent, CommonModule, FormsModule, HygieneFormComponent, CreateHygieneFormComponent, HygieneFormTableComponent],
  standalone:true
})
export class MedicalReportComponent implements OnInit {
onView($event: any) {
throw new Error('Method not implemented.');
}
  // Tabs
  tabs = ['MH By Parent', 'MH By Doctor', 'Hygiene Form', 'Follow Up'];
  selectedTab = this.tabs[0]; // Default selected tab
  mhByParentData: any[] = [];
  mhByDoctorData: any[] = [];


  // Data for Hygiene Form
  hygieneForms: any[] = [];

  // Data for Follow Up
  followUps: any[] = [];

  constructor(
    private hygieneFormService: HygieneFormService,
    private followUpService: FollowUpService,
    private apiService: ApiService,
    private medicalreportService: MedicalReportService,
    private schoolService: SchoolService,
    private gradeService: GradeService,
    private classroomService: ClassroomService,
    private studentService: StudentService
  ) {}

 ngOnInit(): void {
  this.loadHygieneForms();
  this.loadFollowUps();
  this.loadMHByParentData();
  this.loadSchools(); // Load schools when the component initializes
  this.loadAllHygieneForms(); // Load all hygiene forms
  this.loadMHByDoctorData(); // Load MH By Doctor data

}

  schools: any[] = [];
  grades: any[] = [];
  classes: any[] = [];
  students: any[] = [];
  hygieneTypes: any[] = [];
  allHygieneForms: any[] = [];

  selectedSchool: number | null = null;
  selectedGrade: number | null = null;
  selectedClass: number | null = null;
  selectedDate: string = '';
  
  // Add a new property to store filtered follow-ups
filteredFollowUps: any[] = [];

filterFollowUps() {
  if (this.selectedSchool && this.selectedGrade && this.selectedClass) {
    // console.log('Filtering follow-ups...');
    console.log('Selected School ID:', this.selectedSchool);
    // console.log('Selected Grade ID:', this.selectedGrade);
    // console.log('Selected Class ID:', this.selectedClass);

    // console.log(this.followUps)
    this.filteredFollowUps = this.followUps.filter(followUp =>
      followUp.schoolId == this.selectedSchool &&
      followUp.gradeId == this.selectedGrade &&
      followUp.classRoomID == this.selectedClass
    );

    console.log('Filtered Follow Ups:', this.filteredFollowUps); // Log the filtered data
    if (this.filteredFollowUps.length === 0) {
      console.log('No follow-ups found for the selected criteria.');
    }
  } else {
    console.log('Please select all filtration criteria (School, Grade, and Class).');
  }
}

  // Add these methods for the filtration system
  async loadSchools() {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.schoolService.Get(domainName));
      // console.log(data)
      this.schools = data;
    } catch (error) {
      console.error('Error loading schools:', error);
      // this.errorMessage = 'Failed to load schools.';
    }
  }

  async loadGrades() {
    if (this.selectedSchool) {
      try {
        const domainName = this.apiService.GetHeader();
        const data = await firstValueFrom(this.gradeService.GetBySchoolId(this.selectedSchool, domainName));
        this.grades = data;
      } catch (error) {
        console.error('Error loading grades:', error);
        // this.errorMessage = 'Failed to load grades.';
      }
    }
  }

  async loadClasses() {
    if (this.selectedGrade) {
      try {
        const domainName = this.apiService.GetHeader();
        const data = await firstValueFrom(this.classroomService.GetByGradeId(this.selectedGrade, domainName));
        this.classes = data;
      } catch (error) {
        console.error('Error loading classes:', error);
        // this.errorMessage = 'Failed to load classes.';
      }
    }
  }

  async loadStudents() {
    if (this.selectedClass) {
      try {
        const domainName = this.apiService.GetHeader();
        const data = await firstValueFrom(this.studentService.GetByClassID(this.selectedClass, domainName));
        // console.log(data)
        // console.log('test')
        this.students = data.map(student => ({
          ...student,
          attendance: null,
          comment: '',
          actionTaken: ''
        }));
      } catch (error) {
        console.error('Error loading students:', error);
        // this.errorMessage = 'Failed to load students.';
      }
    }
  }
  

onSchoolChange() {
  // console.log('School changed');
  this.selectedGrade = null;
  this.selectedClass = null;
  this.grades = [];
  this.classes = [];
  this.filteredFollowUps = this.followUps; // Reset filtered follow-ups
  this.loadGrades();
}

onGradeChange() {
  // console.log('Grade changed');
  this.selectedClass = null;
  this.classes = [];
  this.filteredFollowUps = this.followUps; // Reset filtered follow-ups
  this.loadClasses();
}

onClassChange() {
  // console.log('Class changed');
  this.filteredFollowUps = this.followUps; // Reset filtered follow-ups
}

    async loadMHByParentData() {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.medicalreportService.getAllMHByParent(domainName));

      this.mhByParentData = data.map((item) => ({
        date: new Date(item.insertedAt).toLocaleDateString(),
        description: item.details || 'No details',
        insertDate: new Date(item.insertedAt).toLocaleDateString(),
        lastModified: item.updatedAt ? new Date(item.updatedAt).toLocaleDateString() : 'Not modified',
        actions: { delete: true, edit: true ,view: true  }
      }));
    } catch (error) {
      console.error('Error fetching MH By Parent data:', error);
      // Swal.fire('Error', 'Failed to load MH By Parent data. Please try again later.', 'error');
    }
  }
  
async loadMHByDoctorData() {
  try {
    const domainName = this.apiService.GetHeader();
    const data = await firstValueFrom(this.medicalreportService.getAllMHByDoctor(domainName));

    this.mhByDoctorData = data.map((item: any) => ({
      date: new Date(item.insertedAt).toLocaleDateString(), // Ensure date is formatted correctly
      description: item.details || 'No details', // Ensure description is mapped
      insertDate: new Date(item.insertedAt).toLocaleDateString(), // Ensure insertDate is formatted
      lastModified: item.updatedAt ? new Date(item.updatedAt).toLocaleDateString() : 'Not modified', // Ensure lastModified is formatted
      actions: { delete: true, edit: true ,view: true  } // Add actions if needed
    }));

    // console.log('MH By Doctor Data:', this.mhByDoctorData); // Log the mapped data for debugging
  } catch (error) {
    console.error('Error fetching MH By Doctor data:', error);
  }
}

async loadAllHygieneForms() {
  try {
    const domainName = this.apiService.GetHeader();
    const data = await firstValueFrom(this.medicalreportService.getAllHygieneForms(domainName));
    console.log(data)
    this.allHygieneForms = data;

    // Map the data to the expected structure for the hygiene-form-table
    this.students = this.allHygieneForms.flatMap(form => 
      form.studentHygieneTypes.map((student: any) => ({
        id: student.studentId,
        en_name: student.student, // Assuming 'student' contains the name
        attendance: student.attendance,
        comment: student.comment,
        actionTaken: student.actionTaken,
        // Add hygiene type properties if needed
        hygieneType_1: student.hygieneTypeId === 1, // Example for hygiene type 1
        hygieneType_2: student.hygieneTypeId === 2, // Example for hygiene type 2
        // Add more hygiene types as needed
      }))
    );
  } catch (error) {
    console.error('Error loading hygiene forms:', error);
  }
}

  
filterStudents() {
  // if (this.selectedSchool && this.selectedGrade && this.selectedClass) {
    const filteredForms = this.allHygieneForms.filter(form =>
      form.schoolId == this.selectedSchool &&
      form.gradeId == this.selectedGrade &&
      form.classRoomID == this.selectedClass
    );

    if (filteredForms.length > 0) {
      this.students = filteredForms[0].studentHygieneTypes.map((student: any) => ({
        ...student,
        attendance: student.attendance,
        comment: student.comment,
        actionTaken: student.actionTaken
      }));
    } else {
      this.students = [];
      console.log('No students found for the selected criteria.');
    }
  // } else {
  //   console.log('Please select all filtration criteria (School, Grade, Class, and Date).');
  // }
}
  

// hygiene-form.component.ts
async loadHygieneForms() {
  try {
    const domainName = this.apiService.GetHeader();
    const data = await firstValueFrom(this.hygieneFormService.Get(domainName));
    // console.log('start')
    // console.log(data)
    // console.log('end')

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

async loadFollowUps() {
  try {
    const domainName = this.apiService.GetHeader();
    const data = await firstValueFrom(this.followUpService.Get(domainName));
    // console.log('Fetched Follow Ups:', data); // Log the fetched data

    // Map the data to the expected structure
    this.followUps = data.map((item) => ({
      id: item.id,
      schoolId: item.schoolId, // Add schoolId
      schoolName: item.school || 'N/A',
      gradeId: item.gradeId, // Add gradeId
      gradeName: item.grade || 'N/A',
      classRoomID: item.classroomId, // Add classRoomID
      className: item.classroom || 'N/A',
      studentName: item.student || 'N/A',
      complaints: item.complains || "No Complaints",
      diagnosisName: item.diagnosis || 'N/A', // Map diagnosis if available
      recommendation: item.recommendation || "No Recommendation",
      actions: { edit: true, delete: true }
    }));

    // Initialize filteredFollowUps with the full list of follow-ups
    this.filteredFollowUps = this.followUps;
  } catch (error) {
    console.error('Error fetching follow-ups:', error);
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