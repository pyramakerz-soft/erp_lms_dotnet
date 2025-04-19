import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PdfPrintComponent } from '../../../../../Component/pdf-print/pdf-print.component';
import { AcademicYear } from '../../../../../Models/LMS/academic-year';
import { School } from '../../../../../Models/school';
import { Student } from '../../../../../Models/student';
import { TokenData } from '../../../../../Models/token-data';
import { AccountService } from '../../../../../Services/account.service';
import { ApiService } from '../../../../../Services/api.service';
import { AcadimicYearService } from '../../../../../Services/Employee/LMS/academic-year.service';
import { SchoolService } from '../../../../../Services/Employee/school.service';
import { DeleteEditPermissionService } from '../../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../../Services/shared/menu.service';
import { ReportsService } from '../../../../../Services/shared/reports.service';
import { StudentService } from '../../../../../Services/student.service';

@Component({
  selector: 'app-proof-registration-report',
  standalone: true,
  imports: [CommonModule, FormsModule, PdfPrintComponent],
  templateUrl: './proof-registration-report.component.html',
  styleUrl: './proof-registration-report.component.css'
})
export class ProofRegistrationReportComponent {

  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  File: any;
  DomainName: string = '';
  UserID: number = 0;
  path: string = '';

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  schools: School[] = []
  academicYears: AcademicYear[] = []
  Students: Student[] = []
  isLoading: boolean = false

  SelectedSchoolId: number = 0;
  SelectedStudentId: number = 0;
  SelectedYearId: number = 0;
  showPDF: boolean = false

  school: School = new School()
  showTable: boolean = false
  SelectedStudent: Student = new Student()
  searchQuery: string = '';
  isSearching: boolean = false; // Track search mode
  filteredStudents: Student[] = [];

  DataToPrint: any = null
  CurrentDate : any =new Date()
  ArabicCurrentDate : any =new Date()
  direction: string = "";
  @ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;

  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    private SchoolServ: SchoolService,
    private academicYearServ: AcadimicYearService,
    private studentServ: StudentService,
    public reportsService: ReportsService
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });
    this.direction = document.dir || 'ltr';
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });
    this.getAllSchools()
    this.getAllYears()
  }

  getAllSchools() {
    this.SchoolServ.Get(this.DomainName).subscribe((d) => {
      this.schools = d
    })
  }

  getAllYears() {
    this.academicYearServ.GetBySchoolId(this.SelectedSchoolId, this.DomainName).subscribe((d) => {
      this.academicYears = d
    })
  }

  toggleSearchMode() {
    this.isSearching = !this.isSearching;
    if (!this.isSearching) {
      // Reset to the full list when exiting search mode
      this.filteredStudents = this.Students;
    }
  }

  getAllStudents() {
    this.studentServ.GetByAcademicYearID(this.SelectedYearId ,this.DomainName).subscribe((d) => {
      this.Students = d;
      this.filteredStudents = d; 
    });
  }

  searchStudents() {
    if (this.searchQuery) {
      // this.SelectedStudent=this.Students
      this.filteredStudents = this.Students.filter(student =>
        student.user_Name.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.filteredStudents = this.Students;
    }
  }

  GetStudentById() {
    this.studentServ.GetByID(this.SelectedStudentId, this.DomainName).subscribe((d) => {
      this.SelectedStudent = d
    })
  }

  async ViewReport() {
    await this.GetData()
    this.showTable = true
    this.GetStudentById()
  }

  Print() {
    this.showPDF = true;
    setTimeout(() => {
      const printContents = document.getElementById("Data")?.innerHTML;
      if (!printContents) {
        console.error("Element not found!");
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
        this.showPDF = false;
      }, 100);
    }, 500);
  }

  DownloadAsPDF() {
    this.showPDF = true;
    setTimeout(() => {
      this.pdfComponentRef.downloadPDF(); // Call manual download
      setTimeout(() => this.showPDF = false, 2000);
    }, 500);
  }

  formatDate(dateString: string, dir: string): string {
    const date = new Date(dateString);
    const locale = dir === 'rtl' ? 'ar-EG' : 'en-US';
    return date.toLocaleDateString(locale, { weekday: 'long', day: 'numeric', month: 'long', year: 'numeric' });
  }

  async DownloadAsExcel() {
    // Transform DataToPrint into Excel tables
    const tables = this.DataToPrint.map((section: { header: any; data: any[]; }) => ({
      title: section.header,
      headers: ['Field', 'Value'],
      data: section.data.map((item: { key: any; value: any; }) => [item.key, item.value])
    }));
  
    await this.reportsService.generateExcelReport({
      mainHeader: {
        en: this.school.reportHeaderOneEn,
        ar: this.school.reportHeaderOneAr
      },
      subHeaders: [
        {
          en: this.school.reportHeaderTwoEn,
          ar: this.school.reportHeaderTwoAr
        }
      ],
      infoRows: [
        { key: 'Date', value: this.CurrentDate },
        { key: 'Student', value: this.SelectedStudent.user_Name },
        { key: 'School', value:  this.school.name }
      ],
      reportImage: this.school.reportImage,
      filename: "Student Information Report.xlsx",
      tables: tables // ✅ dynamic table sections from your actual data
    });
  }
  

  GetData(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.studentServ.GetStudentProofRegistration(this.SelectedYearId, this.SelectedStudentId, this.SelectedSchoolId, this.DomainName)
        .subscribe({
          next: (d) => {
            this.DataToPrint = d; 
            this.school = d.school;
            this.CurrentDate=d.date
            this.CurrentDate = this.formatDate(this.CurrentDate, this.direction);
            this.ArabicCurrentDate = new Date(this.CurrentDate).toLocaleDateString('ar-EG', {
              weekday: 'long',
              year: 'numeric',
              month: 'long',
              day: 'numeric'
            });

            resolve();
          },
          error: (err) => {
            reject(err);
          }
        });
    });
  }
}
