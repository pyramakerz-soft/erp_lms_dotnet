import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
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
import { SectionService } from '../../../../../Services/Employee/LMS/section.service';

@Component({
  selector: 'app-statistics-on-the-number-of-students-inthe-school',
  standalone: true,
  imports: [CommonModule, FormsModule, PdfPrintComponent],
  templateUrl:
    './statistics-on-the-number-of-students-inthe-school.component.html',
  styleUrl: './statistics-on-the-number-of-students-inthe-school.component.css',
})
export class StatisticsOnTheNumberOfStudentsIntheSchoolComponent {
  User_Data_After_Login: TokenData = new TokenData(
    '',
    0,
    0,
    0,
    0,
    '',
    '',
    '',
    '',
    ''
  );

  File: any;
  DomainName: string = '';
  UserID: number = 0;
  path: string = '';

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  schools: School[] = [];
  academicYears: AcademicYear[] = [];
  Students: Student[] = [];
  isLoading: boolean = false;

  SelectedSchoolId: number = 0;
  SelectedStudentId: number = 0;
  SelectedYearId: number = 0;
  showPDF: boolean = false;

  school: School = new School();
  showTable: boolean = false;
  SelectedStudent: Student = new Student();
  searchQuery: string = '';
  isSearching: boolean = false; // Track search mode
  filteredStudents: Student[] = [];

  DataToPrint: any = null;
  CurrentDate: any = new Date();
  direction: string = '';
  tableData: any[] = [];

  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    private SchoolServ: SchoolService,
    private academicYearServ: AcadimicYearService,
    private SectionServ: SectionService,
    public reportsService: ReportsService
  ) {}

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
    this.getAllSchools();
    this.getAllYears();
  }

  getAllSchools() {
    this.SchoolServ.Get(this.DomainName).subscribe((d) => {
      this.schools = d;
    });
  }

  getAllYears() {
    this.academicYearServ
      .GetBySchoolId(this.SelectedSchoolId, this.DomainName)
      .subscribe((d) => {
        this.academicYears = d;
      });
  }

  async ViewReport() {
    await this.GetData();
    this.showTable = true;
  }

  Print() {
    const element = document.getElementById('Data');
    if (!element) {
      console.error('Element not found!');
      return;
    }
    this.showPDF = true;
    setTimeout(() => {
      element.classList.remove('hidden');
      setTimeout(() => {
        const printContents = element.innerHTML;
        const printWindow = window.open('', '_blank', 'width=800,height=600');
        if (!printWindow) return;
        printWindow.document.open();
        printWindow.document.write(`
          <html>
            <head>
              <title>Print</title>
              <style>
                * { font-family: sans-serif; }
                table { border-collapse: collapse; width: 100%; }
                th, td { border: 1px solid #ccc; padding: 8px; text-align: left; }
                @media print {
                  body { margin: 1cm; }
                }
              </style>
            </head>
            <body onload="window.print(); window.close();">
              ${printContents}
            </body>
          </html>
        `);
        printWindow.document.close();
        setTimeout(() => {
          element.classList.add('hidden');
          this.showPDF = false;
        }, 1000);
      }, 200); // allow component to render
    }, 100);
  }

  DownloadAsPDF() {
    this.showPDF = true;
    setTimeout(() => {
      setTimeout(() => (this.showPDF = false), 2000);
    }, 500); // give DOM time to render <app-pdf-print>
  }

  formatDate(dateString: string, dir: string): string {
    const date = new Date(dateString);
    const locale = dir === 'rtl' ? 'ar-EG' : 'en-US';
    return date.toLocaleDateString(locale, {
      weekday: 'long',
      day: 'numeric',
      month: 'long',
      year: 'numeric',
    });
  }

  async DownloadAsExcel() {
    const headers = ['Grade', 'Num_Of_Classrooms', 'Non_Saudi_Count', 'Saudi_Count', 'Num_Of_Student', 'Students_Assigned_To_Noor_Count'];
  
    const dataRows = this.tableData.map(row =>
      headers.map(header => row[header] ?? '')
    );
  
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
        { key: 'School', value: this.school.name }
      ],
      reportImage: this.school.reportImage,
      filename: "Student Information Report.xlsx",
      tables: [
        {
          title: "Students List",
          headers,
          data: dataRows
        }
      ]
    });
  }

  GetData(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.SectionServ.GetClassAndStudentCountsInGradesGroupedBySectionByYearID(
        this.SelectedSchoolId,
        this.SelectedYearId,
        this.DomainName
      ).subscribe({
        next: (d) => {
          this.tableData = []; // Clear existing data
          this.school = d.school;
          console.log(d);
          this.CurrentDate = this.formatDate(d.date, this.direction);

          d.sections.forEach((section: any) => {
            section.gradeWithStudentClassCount.forEach((grade: any) => {
              this.tableData.push({
                Grade: section.name,
                Num_Of_Classrooms: grade.classCount,
                Non_Saudi_Count: grade.nonSaudiCount,
                Saudi_Count: grade.saudiCount,
                Num_Of_Student: grade.studentCount,
                Students_Assigned_To_Noor_Count: grade.studentsAssignedToNoorCount,
              });
            });
            this.tableData.push({
              Grade: 'Total For ' + section.name,
              Num_Of_Classrooms: section.totalCounts.classCount ,
              Non_Saudi_Count: section.totalCounts.nonSaudiCount, 
              Saudi_Count: section.totalCounts.saudiCount,
              Num_Of_Student: section.studentCount,
              Students_Assigned_To_Noor_Count: section.studentsAssignedToNoorCount,
            });
          });
          console.log(this.tableData); // ✅ Result
          resolve();
        },
        error: (err) => {
          reject(err);
        },
      });
    });
  }
}
