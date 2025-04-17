import { Component } from '@angular/core';
import { Student } from '../../../../../Models/student';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { School } from '../../../../../Models/school';
import { AcademicYear } from '../../../../../Models/LMS/academic-year';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../../Services/account.service';
import { ApiService } from '../../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../../Services/shared/menu.service';
import { TokenData } from '../../../../../Models/token-data';
import { SchoolService } from '../../../../../Services/Employee/school.service';
import { AcadimicYearService } from '../../../../../Services/Employee/LMS/academic-year.service';
import { StudentService } from '../../../../../Services/student.service';
import { ReportsService } from '../../../../../Services/shared/reports.service';
import { PdfPrintComponent } from '../../../../../Component/pdf-print/pdf-print.component';

@Component({
  selector: 'app-student-information',
  standalone: true,
  imports: [CommonModule, FormsModule, PdfPrintComponent],
  templateUrl: './student-information.component.html',
  styleUrl: './student-information.component.css'
})
export class StudentInformationComponent {

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
  direction: string = "";

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
      this.filteredStudents = d; // Set filtered students to all initially
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
      this.studentServ.GetByYear(this.SelectedYearId, this.SelectedStudentId, this.SelectedSchoolId, this.DomainName)
        .subscribe({
          next: (d) => {
            this.DataToPrint = []; // Clear existing data
            this.school = d.school;
            this.CurrentDate=d.date
            this.CurrentDate = this.formatDate(this.CurrentDate, this.direction);
            const generalInfo = {
              header: "General Information",
              data: [
                { key: "Student Full Name", value: d.student?.en_name || 'N/A' },
                { key: "Arabic Name", value: d.student?.ar_name || 'N/A' },
                { key: "Admission Date", value: d.student?.insertedAt || 'N/A' },
                { key: "Mobile", value: d.student?.mobile || 'N/A' },
                { key: "Alternative Mobile", value: d.student?.phone || 'N/A' },
                { key: "Date of Birth", value: d.student?.dateOfBirth || 'N/A' },
                { key: "Gender", value: d.student?.genderName || 'N/A' },
                { key: "Nationality", value: d.student?.nationalityEnName || 'N/A' },
                { key: "Student' Passport Number", value: d.student?.passportNo || 'N/A' },
                { key: "Student's Id Number", value: d.student?.idNumber || 'N/A' },
                { key: "Religion", value: d.student?.religion || 'N/A' },
                { key: "Place To Birth", value: d.student?.placeOfBirth || 'N/A' },
                { key: "Pre-School", value: d.student?.previousSchool || 'N/A' },
              ]
            };
            this.DataToPrint.push(generalInfo);

            const classInfo = {
              header: "Class Information",
              data: [
                { key: "Class", value: d.class?.name || 'N/A' }
              ]
            };
            this.DataToPrint.push(classInfo);

            const GuardianInformation = {
              header: "Guardian Information",
              data: [
                { key: "Guardian's Name", value: d.student?.guardianName || 'N/A' },
                { key: "Relationship", value: d.student?.guardianRelation || 'N/A' },
                { key: "Passport", value: d.student?.guardianPassportNo || 'N/A' },
                { key: "Identity", value: d.student?.guardianNationalID || 'N/A' },
                { key: "Qualification", value: d.student?.guardianQualification || 'N/A' },
                { key: "Profession", value: d.student?.guardianProfession || 'N/A' },
                { key: "WorkPlace", value: d.student?.guardianWorkPlace || 'N/A' },
                { key: "E-mail Address", value: d.student?.guardianEmail || 'N/A' },
                { key: "Identity Expiration", value: d.student?.guardianNationalIDExpiredDate || 'N/A' },
                { key: "Passport Expiration", value: d.student?.guardianPassportExpireDate || 'N/A' },
              ]
            };
            this.DataToPrint.push(GuardianInformation);

            const MotherInformation = {
              header: "Mother Information",
              data: [
                { key: "Mother's Name", value: d.student?.motherName || 'N/A' },
                { key: "Passport", value: d.student?.motherPassportNo || 'N/A' },
                { key: "Identity", value: d.student?.motherNationalID || 'N/A' },
                { key: "Passport Expiration", value: d.student?.motherPassportExpireDate || 'N/A' },
                { key: "Qualification", value: d.student?.motherQualification || 'N/A' },
                { key: "Profession", value: d.student?.motherProfession || 'N/A' },
                { key: "WorkPlace", value: d.student?.motherWorkPlace || 'N/A' },
                { key: "E-mail Address", value: d.student?.motherEmail || 'N/A' },
                { key: "Experiences", value: d.student?.motherExperiences || 'N/A' },
              ]
            };
            this.DataToPrint.push(MotherInformation);

            const EmergencyContactPerson = {
              header: "Emergency Contact Person",
              data: [
                { key: "Name", value: d.student?.emergencyContactName || 'N/A' },
                { key: "RelationShip", value: d.student?.emergencyContactRelation || 'N/A' },
                { key: "Mobile", value: d.student?.emergencyContactMobile || 'N/A' },
              ]
            };
            this.DataToPrint.push(EmergencyContactPerson);

            const AddressInformation = {
              header: "Address Information",
              data: [
                { key: "Address", value: d.student?.address || 'N/A' },
              ]
            };
            this.DataToPrint.push(AddressInformation);

            const PersonResponsibleToPickUpAndReceiveTheStudent = {
              header: "Person Responsible To Pick Up And Receive The Student",
              data: [
                { key: "Pick_name", value: d.student?.pickUpContactName || 'N/A' },
                { key: "Pick_Relation", value: d.student?.pickUpContactRelation || 'N/A' },
                { key: "Pick_mobile", value: d.student?.pickUpContactMobile || 'N/A' },
              ]
            };
            this.DataToPrint.push(PersonResponsibleToPickUpAndReceiveTheStudent);
            resolve();
          },
          error: (err) => {
            reject(err);
          }
        });
    });
  }
}
