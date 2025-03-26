import { Component, OnInit } from '@angular/core';
import { HygieneFormService } from '../../../../../Services/Employee/Clinic/hygiene-form.service';
import { FollowUpService } from '../../../../../Services/Employee/Clinic/follow-up.service';
import { ApiService } from '../../../../../Services/api.service';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { TableComponent } from "../../../../../Component/reuse-table/reuse-table.component";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HygieneFormTableComponent } from "../../hygiene_form/hygiene-form-table/hygiene-form-table.component";
import { SchoolService } from '../../../../../Services/Employee/school.service';
import { GradeService } from '../../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../../Services/Employee/LMS/classroom.service';
import { StudentService } from '../../../../../Services/student.service';
import { MedicalReportService } from '../../../../../Services/Employee/Clinic/medical-report.service';
import { Router } from '@angular/router';
import * as XLSX from 'xlsx';
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';
import { MedicalHistoryService } from '../../../../../Services/Employee/Clinic/medical-history.service';
import { SearchComponent } from "../../../../../Component/search/search.component";
import { MedicalHistoryModalComponent } from "../../medical-history/medical-history-modal/medical-history-modal.component";

@Component({
  selector: 'app-medical-report',
  templateUrl: './medical-report.component.html',
  styleUrls: ['./medical-report.component.css'],
  imports: [TableComponent, CommonModule, FormsModule, HygieneFormTableComponent, SearchComponent, MedicalHistoryModalComponent],
  standalone: true
})
export class MedicalReportComponent implements OnInit {
  
onView($event: any) {
throw new Error('Method not implemented.');
}
  
  tabs = ['MH By Parent', 'MH By Doctor', 'Hygiene Form', 'Follow Up'];
  selectedTab = this.tabs[0]; 

  
  mhByParentData: any[] = [];
  mhByDoctorData: any[] = [];
  filteredMHByDoctorData: any[] = [];
  hygieneForms: any[] = [];
  followUps: any[] = [];
  filteredFollowUps: any[] = [];
  

  
  searchKey: string = 'id';
  searchValue: any = '';
  searchKeysArray: string[] = ['id', 'school', 'grade', 'class', 'student', 'date'];

  
  schools: any[] = [];
  grades: any[] = [];
  classes: any[] = [];
  students: any[] = [];
  hygieneTypes: any[] = [];
  allHygieneForms: any[] = [];

  selectedSchool: number | null = null;
  selectedGrade: number | null = null;
  selectedClass: number | null = null;
  selectedStudent: number | null = null; 
  selectedDate: string = '';

  studentSearchTerm: string = '';
  filteredHygieneForms: any[] = [];

  constructor(
    
    private hygieneFormService: HygieneFormService,
    private medicalHistoryService: MedicalHistoryService,
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
    this.loadSchools();
    this.loadAllHygieneForms();
    this.loadMHByDoctorData();
  }

  
  onSearchEvent(event: { key: string, value: any }) {
    this.searchKey = event.key;
    this.searchValue = event.value;

    switch (this.selectedTab) {
      case 'MH By Parent':
        this.mhByParentData = this.applySearchFilter(this.mhByParentData);
        break;
      case 'MH By Doctor':
        this.filteredMHByDoctorData = this.applySearchFilter(this.mhByDoctorData);
        break;
      case 'Hygiene Form':
        this.students = this.applySearchFilter(this.students);
        break;
      case 'Follow Up':
        this.filteredFollowUps = this.applySearchFilter(this.followUps);
        break;
    }
  }

  applySearchFilter(data: any[]): any[] {
    if (!this.searchValue) return data;

    const numericValue = isNaN(Number(this.searchValue)) ? this.searchValue : parseInt(this.searchValue, 10);

    return data.filter((item) => {
      const fieldValue = item[this.searchKey as keyof typeof item];
      if (typeof fieldValue === 'string') {
        return fieldValue.toLowerCase().includes(this.searchValue.toLowerCase());
      }
      if (typeof fieldValue === 'number') {
        return fieldValue === numericValue;
      }
      return false;
    });
  }

  
  async loadSchools() {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.schoolService.Get(domainName));
      this.schools = data;
    } catch (error) {
      console.error('Error loading schools:', error);
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
      }
    }
  }


async loadStudents() {
    if (this.selectedClass) {
        try {
            const domainName = this.apiService.GetHeader();
            const data = await firstValueFrom(this.studentService.GetByClassID(this.selectedClass, domainName));
            this.students = data.map(student => ({
                id: student.id,
                en_name: student.en_name
            }));
        } catch (error) {
            console.error('Error loading students:', error);
        }
    }
}

  onSchoolChange() {
    this.selectedGrade = null;
    this.selectedClass = null;
    this.selectedStudent = null;
    this.grades = [];
    this.classes = [];
    
    this.loadGrades();
  }

  onGradeChange() {
    this.selectedClass = null;
    this.selectedStudent = null;
    this.classes = [];
    
    this.loadClasses();
  }

onClassChange() {
    this.selectedStudent = null;
    this.loadStudents();
}


  // onStudentChange() {
  //   this.filteredMHByDoctorData = this.mhByDoctorData;
  //   this.filteredFollowUps = this.followUps;
  // }

  isEditModalVisible = false;

selectedMedicalHistory: any = null;

openEditModal(row: any) {
    this.selectedMedicalHistory = row;
    this.isEditModalVisible = true;
}



  
  async loadMHByParentData() {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.medicalreportService.getAllMHByParent(domainName));
      
      this.mhByParentData = data.map((item) => ({
        id: item.id,
        date: new Date(item.insertedAt).toLocaleDateString(),
        description: item.details || 'No details',
        insertDate: new Date(item.insertedAt).toLocaleDateString(),
        lastModified: item.updatedAt ? new Date(item.updatedAt).toLocaleDateString() : 'Not modified',
        actions: { delete: true, edit: true, view: true }
      }));
    } catch (error) {
      console.error('Error fetching MH By Parent data:', error);
    }
  }

  async loadMHByDoctorData() {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.medicalHistoryService.GetByDoctor(domainName));
      console.log(data)
      this.mhByDoctorData = data.map((item: any) => ({
        id: item.id,
        date: new Date(item.insertedAt).toLocaleDateString(),
        description: item.details || 'No details',
        insertDate: new Date(item.insertedAt).toLocaleDateString(),
        lastModified: item.updatedAt ? new Date(item.updatedAt).toLocaleDateString() : 'Not modified',
        schoolId: item.schoolId,
        gradeId: item.gradeId,
        classRoomID: item.classRoomID,
        studentId: item.studentId,
        actions: { delete: true, edit: true, view: true }
      }));
      this.filteredMHByDoctorData = this.mhByDoctorData;
    } catch (error) {
      console.error('Error fetching MH By Doctor data:', error);
    }
  }

async loadAllHygieneForms() {
  try {
    const domainName = this.apiService.GetHeader();
    const data = await firstValueFrom(this.hygieneFormService.Get(domainName));
    this.allHygieneForms = data;
    this.filteredHygieneForms = [...data];
    this.prepareStudentsData();
  } catch (error) {
    console.error('Error loading hygiene forms:', error);
  }
}

  async loadHygieneForms() {
    try {
      const domainName = this.apiService.GetHeader();
      const data = await firstValueFrom(this.hygieneFormService.Get(domainName));
      this.allHygieneForms = data;

      
      this.students = data.flatMap(form => 
        form.studentHygieneTypes.map((student: any) => ({
          id: student.studentId,
          en_name: student.student || 'N/A',
          attendance: student.attendance,
          comment: student.comment || '',
          actionTaken: student.actionTaken || '',
          hygieneType_1: student.hygieneTypeId === 1,
          hygieneType_2: student.hygieneTypeId === 2,
        }))
      );
    } catch (error) {
      console.error('Error loading hygiene forms:', error);
    }
  }

async loadFollowUps() {
    try {
        const domainName = this.apiService.GetHeader();
        const data = await firstValueFrom(this.followUpService.Get(domainName));
        
        this.followUps = data.map((item) => ({
            id: item.id,
            schoolId: item.schoolId,
            schoolName: item.school || 'N/A',
            gradeId: item.gradeId,
            gradeName: item.grade || 'N/A',
            classRoomID: item.classroomId,
            className: item.classroom || 'N/A',
            studentId: item.studentId, // Make sure this is included
            studentName: item.student || 'N/A',
            complaints: item.complains || "No Complaints",
            diagnosisName: item.diagnosis || 'N/A',
            recommendation: item.recommendation || "No Recommendation",
            actions: { edit: true, delete: true }
        }));
        this.filteredFollowUps = this.followUps;
    } catch (error) {
        console.error('Error fetching follow-ups:', error);
    }
}

  
  filterMHByDoctor() {
    let filteredData = this.mhByDoctorData;

    if (this.selectedSchool) {
      filteredData = filteredData.filter(item => item.schoolId == this.selectedSchool);
    }

    if (this.selectedGrade) {
      filteredData = filteredData.filter(item => item.gradeId == this.selectedGrade);
    }

    if (this.selectedClass) {
      filteredData = filteredData.filter(item => item.classRoomID == this.selectedClass);
    }

    if (this.selectedStudent) {
      filteredData = filteredData.filter(item => item.studentId == this.selectedStudent);
    }

    console.log('Filtered MH By Doctor Data:', filteredData);
    this.filteredMHByDoctorData = filteredData;
  }

filterHygieneForms() {
  
  if (this.allHygieneForms.length === 0) return;

  
  let filteredForms = [...this.allHygieneForms];

  
  if (this.selectedSchool) {
    filteredForms = filteredForms.filter(
      form => form.schoolId == this.selectedSchool
    );
  }

  
  if (this.selectedGrade) {
    filteredForms = filteredForms.filter(
      form => form.gradeId == this.selectedGrade
    );
  }

  
  if (this.selectedClass) {
    filteredForms = filteredForms.filter(
      form => form.classRoomID == this.selectedClass
    );
  }

  
  if (this.selectedStudent) {
    filteredForms = filteredForms.filter(form => {
      return form.studentHygieneTypes.some((student: any) => 
        student.studentId == this.selectedStudent
      );
    });
  }

  
  this.filteredHygieneForms = filteredForms;
  this.prepareStudentsData();
}

filterFollowUps() {
    let filteredFollowUps = this.followUps;

    if (this.selectedSchool) {
        filteredFollowUps = filteredFollowUps.filter(followUp => followUp.schoolId == this.selectedSchool);
    }

    if (this.selectedGrade) {
        filteredFollowUps = filteredFollowUps.filter(followUp => followUp.gradeId == this.selectedGrade);
    }

    if (this.selectedClass) {
        filteredFollowUps = filteredFollowUps.filter(followUp => followUp.classRoomID == this.selectedClass);
    }

    if (this.selectedStudent) {
        filteredFollowUps = filteredFollowUps.filter(followUp => followUp.studentId == this.selectedStudent);
    }

    console.log('Filtered Follow Ups:', filteredFollowUps);
    this.filteredFollowUps = filteredFollowUps;
}


prepareStudentsData() {
  this.students = this.filteredHygieneForms.flatMap(form => 
    form.studentHygieneTypes.map((student: any) => ({
      id: student.studentId,
      en_name: student.student || 'N/A',
      attendance: student.attendance,
      comment: student.comment || '',
      actionTaken: student.actionTaken || '',
      hygieneType_1: student.hygieneTypeId === 1,
      hygieneType_2: student.hygieneTypeId === 2,
      formId: form.id,
      formDate: new Date(form.date).toLocaleDateString(),
      schoolName: form.school,
      gradeName: form.grade,
      className: form.classRoom
    }))
  );
}

  
  deleteMH(row: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this medical history!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#2E3646',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.isConfirmed) {
        const domainName = this.apiService.GetHeader();
        this.medicalHistoryService.Delete(row.id, domainName).subscribe({
          next: (response) => {
            console.log('Delete response:', response);
            this.loadMHByParentData();
            this.loadMHByDoctorData();
            Swal.fire('Deleted!', 'The medical history has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting medical history:', error);
            Swal.fire('Error!', 'Failed to delete the medical history.', 'error');
          },
        });
      }
    });
  }

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
        const domainName = this.apiService.GetHeader();
        this.followUpService.Delete(row.id, domainName).subscribe({
          next: () => {
            this.loadFollowUps();
          },
          error: (error) => {
            console.error('Error deleting follow-up:', error);
            Swal.fire('Error', 'Failed to delete follow-up. Please try again later.', 'error');
          },
        });
      }
    });
  }

  
selectTab(tab: string) {
    this.selectedTab = tab;
    
    // Reset all filters
    this.selectedSchool = null;
    this.selectedGrade = null;
    this.selectedClass = null;
    this.selectedStudent = null;
    this.studentSearchTerm = '';
    
    // Reset dropdowns
    this.grades = [];
    this.classes = [];
    this.students = [];
    
    // Reset filtered data to show all
    switch(tab) {
        case 'MH By Doctor':
            this.filteredMHByDoctorData = [...this.mhByDoctorData];
            break;
        case 'Hygiene Form':
            this.filteredHygieneForms = [...this.allHygieneForms];
            this.prepareStudentsData();
            break;
        case 'Follow Up':
            this.filteredFollowUps = [...this.followUps];
            break;
    }
    
    // Load fresh data if needed
    if (tab === 'Hygiene Form') {
        this.loadAllHygieneForms();
    } else if (tab === 'MH By Doctor') {
        this.loadMHByDoctorData();
    } else if (tab === 'Follow Up') {
        this.loadFollowUps();
    }
}

  exportToExcel() {
    let data: any[] = [];
    let fileName = '';

    switch (this.selectedTab) {
      case 'MH By Parent':
        data = this.mhByParentData;
        fileName = 'MH_By_Parent.xlsx';
        break;
      case 'MH By Doctor':
        data = this.filteredMHByDoctorData;
        fileName = 'MH_By_Doctor.xlsx';
        break;
      case 'Hygiene Form':
        data = this.students;
        fileName = 'Hygiene_Form.xlsx';
        break;
      case 'Follow Up':
        data = this.filteredFollowUps;
        fileName = 'Follow_Up.xlsx';
        break;
      default:
        Swal.fire('Error', 'No data available for export.', 'error');
        return;
    }

    if (data.length === 0) {
      Swal.fire('Error', 'No data available for export.', 'error');
      return;
    }

    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, fileName);
  }


exportToPDF() {
  let data: any[] = [];
  let headers: string[] = [];
  let fileName = '';

  switch (this.selectedTab) {
    case 'MH By Parent':
      data = this.mhByParentData;
      headers = ['Date', 'Description', 'Insert Date', 'Last Modified'];
      fileName = 'MH_By_Parent.pdf';
      break;
    case 'MH By Doctor':
      data = this.filteredMHByDoctorData;
      headers = ['Date', 'Description', 'Insert Date', 'Last Modified'];
      fileName = 'MH_By_Doctor.pdf';
      break;
    case 'Hygiene Form':
      data = this.students;
      headers = ['Student', 'Attendance', 'Comment', 'Action Taken'];
      fileName = 'Hygiene_Form.pdf';
      break;
    case 'Follow Up':
      data = this.filteredFollowUps;
      headers = ['ID', 'School', 'Grade', 'Class', 'Student', 'Complaints', 'Diagnosis', 'Recommendation'];
      fileName = 'Follow_Up.pdf';
      break;
    default:
      Swal.fire('Error', 'No data available for export.', 'error');
      return;
  }

  if (data.length === 0) {
    Swal.fire('Error', 'No data available for export.', 'error');
    return;
  }

  
  const doc = new jsPDF();

  
  const tableData = data.map((item) => {
    return headers.map((header) => {
      
      const key = header.toLowerCase().replace(/ /g, '_');
      return item[key] || ''; 
    });
  });

  
  autoTable(doc, {
    head: [headers], 
    body: tableData, 
  });

  
  doc.save(fileName);
}

  
  printTable() {
    let data: any[] = [];
    let headers: string[] = [];

    switch (this.selectedTab) {
      case 'MH By Parent':
        data = this.mhByParentData;
        headers = ['Date', 'Description', 'Insert Date', 'Last Modified'];
        break;
      case 'MH By Doctor':
        data = this.filteredMHByDoctorData;
        headers = ['Date', 'Description', 'Insert Date', 'Last Modified'];
        break;
      case 'Hygiene Form':
        data = this.students;
        headers = ['Student', 'Attendance', 'Comment', 'Action Taken'];
        break;
      case 'Follow Up':
        data = this.filteredFollowUps;
        headers = ['ID', 'School', 'Grade', 'Class', 'Student', 'Complaints', 'Diagnosis', 'Recommendation'];
        break;
      default:
        Swal.fire('Error', 'No data available for printing.', 'error');
        return;
    }

    if (data.length === 0) {
      Swal.fire('Error', 'No data available for printing.', 'error');
      return;
    }

    const printWindow = window.open('', '', 'height=600,width=800');
    if (printWindow) {
      printWindow.document.write(`
        <html>
          <head>
            <title>Print Table</title>
            <style>
              table { width: 100%; border-collapse: collapse; }
              th, td { border: 1px solid #000; padding: 8px; text-align: left; }
              th { background-color: #f2f2f2; }
            </style>
          </head>
          <body>
            <table>
              <thead>
                <tr>${headers.map((header) => `<th>${header}</th>`).join('')}</tr>
              </thead>
              <tbody>
                ${data
                  .map(
                    (item) =>
                      `<tr>${headers
                        .map((header) => `<td>${item[header.toLowerCase().replace(/ /g, '')]}</td>`)
                        .join('')}</tr>`
                  )
                  .join('')}
              </tbody>
            </table>
          </body>
        </html>
      `);
      printWindow.document.close();
      printWindow.print();
    } else {
      Swal.fire('Error', 'Unable to open print window.', 'error');
    }
  }
    }