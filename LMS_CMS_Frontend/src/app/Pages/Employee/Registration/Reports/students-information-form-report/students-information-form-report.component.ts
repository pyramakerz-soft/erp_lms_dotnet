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
    let element = document.getElementById("Data");
    if (!element) {
      console.error("Element not found!");
      return;
    }

    element.classList.remove("hidden");

    setTimeout(() => {
      this.reportsService.PrintPDF("List of students' names in class")

      setTimeout(() => {
        element.classList.add("hidden");
      }, 1000);
    }, 200);
  }

  DownloadAsPDF() {
    
    this.showPDF = true;
    setTimeout(() => this.showPDF = false, 1);
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
        { key: 'School', value:  this.school.name }
      ],
      reportImage: this.school.reportImage,
      filename: "Student Information Report.xlsx",
      tables: tables // ✅ dynamic table sections from your actual data
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
                id: index + 1, // No
                en_name: student.en_name || '',
                ar_name: student.ar_name || '',
                mobile1: student.mobile || '',
                mobile2: student.phone || '',
                passportNo: student.passportNo || '',
                nationalityName: student.nationalityEnName || '',
                note: student.note || '',
                dateOfBirth: student.dateOfBirth || '',
                placeOfBirth: student.placeOfBirth || '',
                passportExpiredDate: student.passportExpiredDate || '',
                nationalIDExpiredDate: student.nationalIDExpiredDate || '',
                admissionDate: student.admissionDate || '',
                guardianNationalID: student.guardianNationalID || '',
                email: student.email || '',
                bus: student.isRegisteredToBus ? 'Yes' : 'No',
                religion: student.religion || '',
                previousSchool: student.previousSchool || ''
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
