import { Component, OnInit, ViewChild } from '@angular/core';
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
import { MedicalHistoryService } from '../../../../../Services/Employee/Clinic/medical-history.service';
import { SearchComponent } from "../../../../../Component/search/search.component";
import { MedicalHistoryModalComponent } from "../../medical-history/medical-history-modal/medical-history-modal.component";
import { PdfPrintComponent } from "../../../../../Component/pdf-print/pdf-print.component";

@Component({
  selector: 'app-medical-report',
  templateUrl: './medical-report.component.html',
  styleUrls: ['./medical-report.component.css'],
  imports: [TableComponent, CommonModule, FormsModule, HygieneFormTableComponent, MedicalHistoryModalComponent, SearchComponent, PdfPrintComponent],
  standalone: true
})
export class MedicalReportComponent implements OnInit {
  
onView(row: any) {
    if (this.selectedTab === 'MH By Parent') {
        this.router.navigate(['/Employee/mh by parent/', row.id]);
    }
    if (this.selectedTab === 'MH By Doctor') {
        this.router.navigate(['/Employee/mh by doctor/', row.id]);
    }
}
  
  tabs = ['MH By Parent', 'MH By Doctor', 'Hygiene Form', 'Follow Up'];
  selectedTab = this.tabs[0]; 
  
  mhByParentData: any[] = [];
  mhByDoctorData: any[] = [];
  filteredMHByDoctorData: any[] = [];
  hygieneForms: any[] = [];
    filteredHygieneForms: any[] = [];
  followUps: any[] = [];
  filteredFollowUps: any[] = [];
  
  searchKey: string = 'id';
  searchValue: any = '';
  searchKeysArray: string[] = ['id'];
  
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

  // PDF Print properties
showPDFView = false;
pdfSchoolData: any = {
    reportHeaderOneEn: 'Medical Report',
    reportHeaderTwoEn: 'Detailed Medical Information',
    reportHeaderOneAr: 'التقرير الطبي',
    reportHeaderTwoAr: 'معلومات طبية مفصلة'
};
pdfFileName = 'Medical_Report';
pdfTitle = '';
pdfInfoRows: any[] = [];
pdfTableHeaders: string[] = [];
pdfTableData: any[] = [];
autoDownloadPDF = false;
@ViewChild('pdfComponentRef') pdfComponentRef?: PdfPrintComponent;


showPrintView = false;
printData: any[] = [];
printHeaders: string[] = [];
getPrintValue(item: any, header: string): string {
  const key = header.toLowerCase().replace(/ /g, '');
  return item[key] || '';
}

  constructor(
        private router: Router,
    
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

  
async onSearchEvent(event: { key: string, value: any }) {
  this.searchKey = event.key;
  this.searchValue = event.value;

  switch (this.selectedTab) {
    case 'MH By Parent':
      
      await this.loadMHByParentData();
      break;
    case 'MH By Doctor':
      this.filterMHByDoctor();
      break;
    case 'Hygiene Form':
      this.filterHygieneForms();
      break;
    case 'Follow Up':
      this.filterFollowUps();
      break;
  }
}

applySearchFilter(data: any[]): any[] {
  if (!this.searchValue) return data;

  return data.filter((item) => {
    const fieldValue = item[this.searchKey as keyof typeof item]?.toString().toLowerCase() || '';
    
    if (this.searchKey === 'id') {
      return fieldValue.includes(this.searchValue.toString().toLowerCase());
    }
    return fieldValue.includes(this.searchValue.toString().toLowerCase());
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
  this.filterMHByDoctor(); 
}

onGradeChange() {
  this.selectedClass = null;
  this.selectedStudent = null;
  this.classes = [];
  this.loadClasses();
  this.filterMHByDoctor(); 
}

onClassChange() {
  this.selectedStudent = null;
  this.loadStudents();
  this.filterMHByDoctor(); 
}

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
    console.log(data)
    this.mhByParentData = data.map((item) => ({
      id: item.id,
      date: new Date(item.insertedAt).toLocaleDateString(),
      description: item.details || 'No details',
      insertDate: new Date(item.insertedAt).toLocaleDateString(),
      lastModified: item.updatedAt ? new Date(item.updatedAt).toLocaleDateString() : 'Not modified',
      actions: { delete: true, edit: true, view: true }
    }));

    if (this.searchValue) {
      this.mhByParentData = this.mhByParentData.filter(mh => 
        mh.id.toString().includes(this.searchValue.toString())
      );
    }
  } catch (error) {
    console.error('Error fetching MH By Parent data:', error);
  }
}

async loadMHByDoctorData() {
  try {
    const domainName = this.apiService.GetHeader();
    const data = await firstValueFrom(this.medicalHistoryService.GetByDoctor(domainName));
    this.mhByDoctorData = data.map((item: any) => ({
      id: item.id,
      date: new Date(item.insertedAt).toLocaleDateString(),
      description: item.details || 'No details',
      insertDate: new Date(item.insertedAt).toLocaleDateString(),
      lastModified: item.updatedAt ? new Date(item.updatedAt).toLocaleDateString() : 'Not modified',
      schoolId: item.schoolId,
      schoolName: item.school || '',
      gradeId: item.gradeId,
      gradeName: item.grade || '',
      classRoomID: item.classRoomID,
      className: item.classRoom || '',
      studentId: item.studentId,
      studentName: item.student || '',
      actions: { delete: true, edit: true, view: true }
    }));

    if (this.searchValue) {
      this.mhByDoctorData = this.mhByDoctorData.filter(mh => {
        const fieldValue = mh[this.searchKey as keyof typeof mh]?.toString().toLowerCase() || '';
        return fieldValue.includes(this.searchValue.toString().toLowerCase());
      });
    }
    
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
        console.log(data)
        this.followUps = data.map((item) => ({
            id: item.id,
            schoolId: item.schoolId,
            schoolName: item.school || 'N/A',
            gradeId: item.gradeId,
            gradeName: item.grade || 'N/A',
            classRoomID: item.classroomId,
            className: item.classroom || 'N/A',
            studentId: item.studentId, 
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
  let filteredData = [...this.mhByDoctorData];

  
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

  
  if (this.searchValue) {
    filteredData = filteredData.filter(item => {
      const fieldValue = item[this.searchKey as keyof typeof item]?.toString().toLowerCase() || '';
      return fieldValue.includes(this.searchValue.toString().toLowerCase());
    });
  }

  this.filteredMHByDoctorData = filteredData;
}

// async onSearchEvent(event: { key: string; value: any }) {
//     this.key = event.key;
//     this.value = event.value;
//     await this.GetTableData();
//     if (this.value != '') {
//       const numericValue = isNaN(Number(this.value))
//         ? this.value
//         : parseInt(this.value, 10);

//       this.TableData = this.TableData.filter((t) => {
//         const fieldValue = t[this.key as keyof typeof t];
//         if (typeof fieldValue === 'string') {
//           return fieldValue.toLowerCase().includes(this.value.toLowerCase());
//         }
//         if (typeof fieldValue === 'number') {
//           return fieldValue === numericValue;
//         }
//         return fieldValue == this.value;
//       });
//     }
//   }

filterHygieneForms() {
  let filteredForms = [...this.allHygieneForms];
  
  if (this.selectedSchool) {
    filteredForms = filteredForms.filter(form => form.schoolId == this.selectedSchool);
  }
  if (this.selectedGrade) {
    filteredForms = filteredForms.filter(form => form.gradeId == this.selectedGrade);
  }
  if (this.selectedClass) {
    filteredForms = filteredForms.filter(form => form.classRoomID == this.selectedClass);
  }

  if (this.searchValue) {
    filteredForms = filteredForms.filter(form => {
      const searchField = this.getSearchFieldValue(form);
      return searchField?.toString().toLowerCase().includes(this.searchValue.toLowerCase());
    });
  }

  this.filteredHygieneForms = filteredForms;
  this.prepareStudentsData();
}

private getSearchFieldValue(form: any): string {
  switch(this.searchKey) {
    case 'id':
      return form.id?.toString();
    case 'schoolName':
      return form.school;
    case 'gradeName':
      return form.grade;
    case 'className':
      return form.classRoom;
    case 'formDate':
      return new Date(form.date).toLocaleDateString();
    default:
      return '';
  }
}

filterFollowUps() {
  let filteredFollowUps = [...this.followUps];
  
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

  if (this.searchValue) {
    filteredFollowUps = this.applySearchFilter(filteredFollowUps);
  }

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

async selectTab(tab: string) {
  
  this.selectedTab = tab;
  this.selectedSchool = null;
  this.selectedGrade = null;
  this.selectedClass = null;
  this.selectedStudent = null;
  this.studentSearchTerm = '';
  this.searchValue = '';
  this.searchKey = 'id';
  this.grades = [];
  this.classes = [];
  this.students = [];
  
  switch(tab) {
    case 'MH By Parent':
      this.searchKeysArray = ['id'];
      await this.loadMHByParentData();
      break;
    case 'MH By Doctor':
      this.searchKeysArray = ['id', 'schoolName', 'gradeName', 'className', 'studentName'];
      await this.loadMHByDoctorData();
      break;
    case 'Hygiene Form':
      this.searchKeysArray = ['id', 'schoolName', 'gradeName', 'className', 'formDate'];
      await this.loadAllHygieneForms(); 
      this.filteredHygieneForms = [...this.allHygieneForms];
      this.prepareStudentsData();
      break;
    case 'Follow Up':
      this.searchKeysArray = ['id', 'schoolName', 'gradeName', 'className', 'studentName'];
      await this.loadFollowUps(); 
      this.filteredFollowUps = [...this.followUps];
      break;
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
  this.showPDFView = true;
  this.autoDownloadPDF = true;
  
  // Set common PDF properties
  this.pdfTitle = `${this.selectedTab} Report`;
  this.pdfFileName = `${this.selectedTab.replace(/ /g, '_')}_Report.pdf`;
  
  // Set data based on selected tab
  switch (this.selectedTab) {
    case 'MH By Parent':
      this.pdfTableHeaders = ['Date', 'Description', 'Insert Date', 'Last Modified'];
      this.pdfTableData = this.mhByParentData.map(item => ({
        Date: item.date,
        Description: item.description,
        'Insert Date': item.insertDate,
        'Last Modified': item.lastModified
      }));
      break;
    case 'MH By Doctor':
      this.pdfTableHeaders = ['Date', 'Description', 'Insert Date', 'Last Modified', 'School', 'Grade', 'Class', 'Student'];
      this.pdfTableData = this.filteredMHByDoctorData.map(item => ({
        Date: item.date,
        Description: item.description,
        'Insert Date': item.insertDate,
        'Last Modified': item.lastModified,
        School: item.schoolName,
        Grade: item.gradeName,
        Class: item.className,
        Student: item.studentName
      }));
      break;
    case 'Hygiene Form':
      this.pdfTableHeaders = ['Student', 'Attendance', 'Comment', 'Action Taken', 'Hygiene Type'];
      this.pdfTableData = this.students.map(item => ({
        Student: item.en_name,
        Attendance: item.attendance,
        Comment: item.comment,
        'Action Taken': item.actionTaken,
        'Hygiene Type': item.hygieneType_1 ? 'Type 1' : item.hygieneType_2 ? 'Type 2' : 'N/A'
      }));
      break;
    case 'Follow Up':
      this.pdfTableHeaders = ['ID', 'School', 'Grade', 'Class', 'Student', 'Complaints', 'Diagnosis', 'Recommendation'];
      this.pdfTableData = this.filteredFollowUps.map(item => ({
        ID: item.id,
        School: item.schoolName,
        Grade: item.gradeName,
        Class: item.className,
        Student: item.studentName,
        Complaints: item.complaints,
        Diagnosis: item.diagnosisName,
        Recommendation: item.recommendation
      }));
      break;
    default:
      Swal.fire('Error', 'No data available for PDF export.', 'error');
      this.showPDFView = false;
      return;
  }

  // Set info rows based on filters
  this.pdfInfoRows = [];
  
  if (this.selectedSchool) {
    const school = this.schools.find(s => s.id === this.selectedSchool);
    this.pdfInfoRows.push({ 
      keyEn: 'School: ' + (school?.name || 'N/A'),
      keyAr: 'المدرسة: ' + (school?.name || 'غير متوفر')
    });
  }
  
  if (this.selectedGrade) {
    const grade = this.grades.find(g => g.id === this.selectedGrade);
    this.pdfInfoRows.push({ 
      keyEn: 'Grade: ' + (grade?.name || 'N/A'),
      keyAr: 'الصف: ' + (grade?.name || 'غير متوفر')
    });
  }
  
  if (this.selectedClass) {
    const classRoom = this.classes.find(c => c.id === this.selectedClass);
    this.pdfInfoRows.push({ 
      keyEn: 'Class: ' + (classRoom?.name || 'N/A'),
      keyAr: 'الفصل: ' + (classRoom?.name || 'غير متوفر')
    });
  }
  
  if (this.selectedStudent) {
    const student = this.students.find(s => s.id === this.selectedStudent);
    this.pdfInfoRows.push({ 
      keyEn: 'Student: ' + (student?.en_name || 'N/A'),
      keyAr: 'الطالب: ' + (student?.en_name || 'غير متوفر')
    });
  }

  // Add date info
  this.pdfInfoRows.push({
    keyEn: 'Report Date: ' + new Date().toLocaleDateString(),
    keyAr: 'تاريخ التقرير: ' + new Date().toLocaleDateString()
  });

  // Trigger the PDF generation after a small delay to ensure the view is updated
  setTimeout(() => {
    if (this.pdfComponentRef) {
      this.pdfComponentRef.downloadPDF();
    }
    this.showPDFView = false;
  }, 100);
}

printTable() {
  this.showPrintView = true;
  this.autoDownloadPDF = false; // Ensure we don't auto-download
  
  // Set common PDF properties
  this.pdfTitle = `${this.selectedTab} Report`;
  this.pdfFileName = `${this.selectedTab.replace(/ /g, '_')}_Report.pdf`;
  
  // Set data based on selected tab
  switch (this.selectedTab) {
    case 'MH By Parent':
      this.pdfTableHeaders = ['Date', 'Description', 'Insert Date', 'Last Modified'];
      this.pdfTableData = this.mhByParentData.map(item => ({
        Date: item.date,
        Description: item.description,
        'Insert Date': item.insertDate,
        'Last Modified': item.lastModified
      }));
      break;
    case 'MH By Doctor':
      this.pdfTableHeaders = ['Date', 'Description', 'Insert Date', 'Last Modified', 'School', 'Grade', 'Class', 'Student'];
      this.pdfTableData = this.filteredMHByDoctorData.map(item => ({
        Date: item.date,
        Description: item.description,
        'Insert Date': item.insertDate,
        'Last Modified': item.lastModified,
        School: item.schoolName,
        Grade: item.gradeName,
        Class: item.className,
        Student: item.studentName
      }));
      break;
    case 'Hygiene Form':
      this.pdfTableHeaders = ['Student', 'Attendance', 'Comment', 'Action Taken', 'Hygiene Type'];
      this.pdfTableData = this.students.map(item => ({
        Student: item.en_name,
        Attendance: item.attendance,
        Comment: item.comment,
        'Action Taken': item.actionTaken,
        'Hygiene Type': item.hygieneType_1 ? 'Type 1' : item.hygieneType_2 ? 'Type 2' : 'N/A'
      }));
      break;
    case 'Follow Up':
      this.pdfTableHeaders = ['ID', 'School', 'Grade', 'Class', 'Student', 'Complaints', 'Diagnosis', 'Recommendation'];
      this.pdfTableData = this.filteredFollowUps.map(item => ({
        ID: item.id,
        School: item.schoolName,
        Grade: item.gradeName,
        Class: item.className,
        Student: item.studentName,
        Complaints: item.complaints,
        Diagnosis: item.diagnosisName,
        Recommendation: item.recommendation
      }));
      break;
    default:
      Swal.fire('Error', 'No data available for printing.', 'error');
      this.showPrintView = false;
      return;
  }

  // Set info rows based on filters
  this.pdfInfoRows = [];
  
  if (this.selectedSchool) {
    const school = this.schools.find(s => s.id === this.selectedSchool);
    this.pdfInfoRows.push({ 
      keyEn: 'School: ' + (school?.name || 'N/A'),
      keyAr: 'المدرسة: ' + (school?.name || 'غير متوفر')
    });
  }
  
  if (this.selectedGrade) {
    const grade = this.grades.find(g => g.id === this.selectedGrade);
    this.pdfInfoRows.push({ 
      keyEn: 'Grade: ' + (grade?.name || 'N/A'),
      keyAr: 'الصف: ' + (grade?.name || 'غير متوفر')
    });
  }
  
  if (this.selectedClass) {
    const classRoom = this.classes.find(c => c.id === this.selectedClass);
    this.pdfInfoRows.push({ 
      keyEn: 'Class: ' + (classRoom?.name || 'N/A'),
      keyAr: 'الفصل: ' + (classRoom?.name || 'غير متوفر')
    });
  }
  
  if (this.selectedStudent) {
    const student = this.students.find(s => s.id === this.selectedStudent);
    this.pdfInfoRows.push({ 
      keyEn: 'Student: ' + (student?.en_name || 'N/A'),
      keyAr: 'الطالب: ' + (student?.en_name || 'غير متوفر')
    });
  }

  // Add date info
  this.pdfInfoRows.push({
    keyEn: 'Report Date: ' + new Date().toLocaleDateString(),
    keyAr: 'تاريخ التقرير: ' + new Date().toLocaleDateString()
  });

  // Wait for the view to update
  setTimeout(() => {
    const printContents = document.getElementById("printData")?.innerHTML;
    if (!printContents) {
      console.error("Print element not found!");
      this.showPrintView = false;
      return;
    }

    // Create a print-specific stylesheet
    const printStyle = `
      <style>
        @page { size: auto; margin: 0mm; }
        body { 
          margin: 0; 
        }

        @media print {
          body > *:not(#print-container) {
            display: none !important;
          }
          #print-container {
            display: block !important;
            position: static !important;
            top: auto !important;
            left: auto !important;
            width: 100% !important;
            height: auto !important;
            background: white !important;
            box-shadow: none !important;
            margin: 0 !important;
          }
        }
      </style>
    `;

    // Create a container for printing
    const printContainer = document.createElement('div');
    printContainer.id = 'print-container';
    printContainer.innerHTML = printStyle + printContents;

    // Add to body and print
    document.body.appendChild(printContainer);
    window.print();
    
    // Clean up
    setTimeout(() => {
      document.body.removeChild(printContainer);
      this.showPrintView = false;
    }, 100);
  }, 100);
}

inlineAllStyles(source: HTMLElement, target: HTMLElement) {
  const sourceStyles = window.getComputedStyle(source);
  const cssText = Array.from(sourceStyles)
    .map(key => `${key}: ${sourceStyles.getPropertyValue(key)};`)
    .join(' ');
  target.setAttribute('style', cssText);
  
  const children = Array.from(source.children) as HTMLElement[];
  const targetChildren = Array.from(target.children) as HTMLElement[];
  for (let i = 0; i < children.length; i++) {
    this.inlineAllStyles(children[i], targetChildren[i]);
  }
}
IsPrint = false;
}