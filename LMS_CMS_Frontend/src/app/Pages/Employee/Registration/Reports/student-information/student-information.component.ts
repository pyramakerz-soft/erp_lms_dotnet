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

@Component({
  selector: 'app-student-information',
  standalone: true,
  imports: [CommonModule, FormsModule],
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
    this.getAllStudents()
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
    this.studentServ.GetAll(this.DomainName).subscribe((d) => {
      this.Students = d;
      this.filteredStudents = d; // Set filtered students to all initially
    });
  }

  searchStudents() {
    if (this.searchQuery) {
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
      console.log(d)
    })
  }

  ViewReport() {
    this.showTable = true
    this.GetStudentById()
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

  async DownloadAsExcel() {
    await this.reportsService.generateExcelReport({
      mainHeader: {
        en: this.school.reportHeaderOneEn,
        ar: this.school.reportHeaderOneAr
      },
      subHeaders: [
        { en: this.school.reportHeaderTwoEn, ar: this.school.reportHeaderTwoAr },
        { en: "Generated by Admin", ar: "تم التوليد بواسطة المشرف" }
      ],
      infoRows: [
        { key: 'Session', value: '2024/2025' }
      ],
      reportImage: this.school.reportImage,
      filename: "List of students' names in class.xlsx",
      tables: [
        {
          title: "Students List",
          headers: ['ID', 'Name', 'Mobile', 'Nationality', 'Gender'],
          data: []
        }
      ]
    });
  }
}
