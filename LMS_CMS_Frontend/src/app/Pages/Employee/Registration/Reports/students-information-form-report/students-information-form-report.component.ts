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
import { GradeService } from '../../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../../Services/Employee/LMS/classroom.service';
import { Grade } from '../../../../../Models/LMS/grade';
import { Classroom } from '../../../../../Models/LMS/classroom';

@Component({
  selector: 'app-students-information-form-report',
  standalone: true,
  imports: [CommonModule, FormsModule, PdfPrintComponent],
  templateUrl: './students-information-form-report.component.html',
  styleUrl: './students-information-form-report.component.css'
})
export class StudentsInformationFormReportComponent {

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
  students: Student[] = []
  academicYears: AcademicYear[] = []
  Grades: Grade[] = []
  class: Classroom[] = []
  isLoading: boolean = false

  SelectedSchoolId: number = 0;
  SelectedYearId: number = 0;
  SelectedGradeId: number = 0;
  SelectedClassId: number = 0;
  showPDF: boolean = false

  school: School = new School()
  showTable: boolean = false
  searchQuery: string = '';
  isSearching: boolean = false; // Track search mode

  DataToPrint: any = null
  CurrentDate : any =new Date()
  ArabicCurrentDate : any =new Date()
  direction: string = "";
  tableData: any[]=[];

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
    private GradeServ: GradeService,
    private ClassroomServ: ClassroomService,
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

  getAllGrades() {
    this.GradeServ.GetBySchoolId(this.SelectedSchoolId, this.DomainName).subscribe((d) => {
      this.Grades = d
    })
  }

  getAllClass() {
    this.ClassroomServ.GetByGradeAndAcYearId(this.SelectedGradeId,this.SelectedYearId, this.DomainName).subscribe((d) => {
      this.class = d
    })
  }

  async ViewReport() {
    await this.GetData()
    this.showTable = true
  }

  Print() {
    const element = document.getElementById("Data");
    if (!element) {
      console.error("Element not found!");
      return;
    }
    this.showPDF = true;
    setTimeout(() => {
      element.classList.remove("hidden");
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
          element.classList.add("hidden");
          this.showPDF = false;
        }, 1000);
  
      }, 200); // allow component to render
    }, 100);
  }

  DownloadAsPDF() {
    this.showPDF = true;
    setTimeout(() => {
      setTimeout(() => this.showPDF = false, 2000);
    }, 500); // give DOM time to render <app-pdf-print>
  }

  formatDate(dateString: string, dir: string): string {
    const date = new Date(dateString);
    const locale = dir === 'rtl' ? 'ar-EG' : 'en-US';
    return date.toLocaleDateString(locale, { weekday: 'long', day: 'numeric', month: 'long', year: 'numeric' });
  }

  async DownloadAsExcel() {
    const headers = ['No', 'Name', 'الاسم', 'Mobile_1', 'Mobile_2', 'Passport', 'Nationality', 'Note', 'Date_Of_Birth', 'Place_Of_Birth', 'Passport_Expired', 'identities_Expired', 'Admission_Date', 'Identity_of_Father', 'Email_Address', 'Bus', 'Religion', 'Pre_School'];
  
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
      this.studentServ.GetByClassIDReport( this.SelectedSchoolId,this.SelectedClassId, this.DomainName)
        .subscribe({
          next: (d) => {
            this.DataToPrint = d; 
            this.school = d.school;
            this.students=d.students
            this.tableData = this.students.map((student: any, index: number) => {
              return {
                No: index + 1, // No
                Name: student.en_name || '',
                الاسم: student.ar_name || '',
                Mobile_1: student.mobile || '',
                Mobile_2: student.phone || '',
                Passport: student.passportNo || '',
                Nationality: student.nationalityEnName || '',
                Note: student.note || '',
                Date_Of_Birth: student.dateOfBirth || '',
                Place_Of_Birth: student.placeOfBirth || '',
                Passport_Expired: student.passportExpiredDate || '',
                identities_Expired: student.nationalIDExpiredDate || '',
                Admission_Date: student.admissionDate || '',
                Identity_of_Father: student.guardianNationalID || '',
                Email_Address: student.email || '',
                Bus: student.isRegisteredToBus ? 'Yes' : 'No',
                Religion: student.religion || '',
                Pre_School: student.previousSchool || ''
              };
            });
            console.log("data",d)
            this.CurrentDate=d.date
            this.CurrentDate = this.formatDate(this.CurrentDate, this.direction);
            this.ArabicCurrentDate = new Date(this.CurrentDate).toLocaleDateString('ar-EG', {
              weekday: 'long',
              year: 'numeric',
              month: 'long',
              day: 'numeric'
            });
            console.log("this.CurrentDate",this.CurrentDate)

            resolve();
          },
          error: (err) => {
            reject(err);
          }
        });
    });
  }
}
