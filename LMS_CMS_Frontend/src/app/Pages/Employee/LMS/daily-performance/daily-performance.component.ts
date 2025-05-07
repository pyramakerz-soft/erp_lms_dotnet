import { Component } from '@angular/core';
import { DailyPerformance } from '../../../../Models/LMS/daily-performance';
import { ActivatedRoute, Router } from '@angular/router';
import { Classroom } from '../../../../Models/LMS/classroom';
import { Grade } from '../../../../Models/LMS/grade';
import { Medal } from '../../../../Models/LMS/medal';
import { School } from '../../../../Models/school';
import { Student } from '../../../../Models/student';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { MedalService } from '../../../../Services/Employee/LMS/medal.service';
import { StudentMedalService } from '../../../../Services/Employee/LMS/student-medal.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { StudentService } from '../../../../Services/student.service';
import { Subject } from '../../../../Models/LMS/subject';
import { SubjectService } from '../../../../Services/Employee/LMS/subject.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PerformanceType } from '../../../../Models/LMS/performance-type';
import { PerformanceTypeService } from '../../../../Services/Employee/LMS/performance-type.service';
import { DailyPerformanceService } from '../../../../Services/Employee/LMS/daily-performance.service';
import Swal from 'sweetalert2';
import { StudentPerformance } from '../../../../Models/LMS/student-performance';
import { StudentMedal } from '../../../../Models/LMS/student-medal';

@Component({
  selector: 'app-daily-performance',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './daily-performance.component.html',
  styleUrl: './daily-performance.component.css'
})
export class DailyPerformanceComponent {
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
  Grades: Grade[] = []
  class: Classroom[] = []
  medals: Medal[] = []
  subjects: Subject[] = []
  PerformanceTypes: PerformanceType[] = []
  isLoading: boolean = false

  SelectedSchoolId: number = 0;
  SelectedYearId: number = 0;
  SelectedGradeId: number = 0;
  SelectedClassId: number = 0;
  SelectedSubjectId: number = 0;

  TableData: DailyPerformance[] = [];
  isModalVisible: boolean = false;
  mode: string = '';

  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'englishName', 'arabicName'];
  SelectedMedalId: number = 0;
  IsView: boolean = false
  selectedTypeIds: number[] = []; // Array to store selected type IDs
  dropdownOpen = false;
  PerformanceTypesSelected: PerformanceType[] = [];
  selectedRating: number = 0;
  RatedStudent: DailyPerformance[] = []
  Data: DailyPerformance = new DailyPerformance()
  selectedStudentIds: number[] = []
  allSelected: boolean = false
  payload: DailyPerformance[] = []
  StudentMedals: StudentMedal[] = []
  studentMedal: StudentMedal = new StudentMedal()
  IsValid: boolean = true
  IsStudentPerformance : boolean = true

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
    public studentMedalServ: StudentMedalService,
    public MedalServ: MedalService,
    public subjectServ: SubjectService,
    public PerformanceTypeServ: PerformanceTypeService,
    public StudentPerformanceServ: DailyPerformanceService
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
  }

  getAllSchools() {
    this.schools = []
    this.SchoolServ.Get(this.DomainName).subscribe((d) => {
      this.schools = d
    })
  }

  View() {
    this.IsView = true;
    this.getAllPerformanceType();
    this.students = [];

    this.studentServ.GetBySchoolGradeClassID(
      this.SelectedSchoolId,
      this.SelectedGradeId,
      this.SelectedClassId,
      this.DomainName
    ).subscribe((d: any) => {
      this.students = d.students;
      this.RatedStudent = [];

      for (let student of this.students) {
        this.RatedStudent.push({
          id: 0,
          studentID: student.id,
          subjectID: this.SelectedSubjectId,
          comment: "",
          insertedByUserId: 0,
          studentPerformance: this.PerformanceTypesSelected.map(type => ({
            id: 0,
            performanceTypeID: type.id,
            dailyPerformanceID: 0,
            stars: 0,
            insertedByUserId: 0
          }))
        });
      }
      console.log(this.RatedStudent);
    });
  }

  getAllSubject() {
    this.subjects = []
    this.SelectedSubjectId = 0
    this.subjectServ.GetByGradeId(this.SelectedGradeId, this.DomainName).subscribe((d) => {
      this.subjects = d
    })
  }

  getAllGradesBySchoolId() {
    this.Grades = []
    this.IsView = false
    this.SelectedGradeId = 0
    this.SelectedClassId = 0
    this.GradeServ.GetBySchoolId(this.SelectedSchoolId, this.DomainName).subscribe((d) => {
      this.Grades = d
    })
  }

  saveSelection() {
    console.log('Selected Type IDs:', this.selectedTypeIds);
  }

  getAllClassByGradeId() {
    this.class = []
    this.SelectedClassId = 0
    this.IsView = false
    this.ClassroomServ.GetByGradeId(this.SelectedGradeId, this.DomainName).subscribe((d) => {
      this.class = d
    })
  }

  toggleDropdown(): void {
    this.dropdownOpen = !this.dropdownOpen;
  }

  selectType(Type: PerformanceType): void {
    this.IsStudentPerformance = true
    if (!this.PerformanceTypesSelected.some((e) => e.id === Type.id)) {
      this.PerformanceTypesSelected.push(Type);
      for (let student of this.RatedStudent) {
        student.studentPerformance.push({
          id: 0,
          performanceTypeID: Type.id,
          dailyPerformanceID: 0,
          stars: 0,
          insertedByUserId: 0
        });
      }
    }
    this.dropdownOpen = false;
  }

  removeSelected(id: number): void {
    this.PerformanceTypesSelected = this.PerformanceTypesSelected.filter((e) => e.id !== id);
    this.RatedStudent = this.RatedStudent.filter((e) => e.studentPerformance.filter((s) => s.performanceTypeID !== id));
  }

  getAllPerformanceType() {
    this.PerformanceTypes = []
    this.PerformanceTypeServ.Get(this.DomainName).subscribe((d) => {
      this.PerformanceTypes = d
    })
  }

  setStar(star: number) {
    this.selectedRating = star;
  }

  setRating(studentId: number, performanceTypeId: number, stars: number) {
    const updatedIds = new Set<number>();
    const self = this.RatedStudent.find(e => e.studentID === studentId);
    if (self) {
      const performance = self.studentPerformance.find(p => p.performanceTypeID === performanceTypeId);
      if (performance) {
        performance.stars = stars;
        updatedIds.add(studentId);
      }
    }
    if (this.selectedStudentIds.length > 0) {
      this.selectedStudentIds.forEach(id => {
        if (updatedIds.has(id)) return;
        const daily = this.RatedStudent.find(e => e.studentID === id);
        if (daily) {
          const performance = daily.studentPerformance.find(p => p.performanceTypeID === performanceTypeId);
          if (performance) {
            performance.stars = stars;
          }
        }
      });
    }
  }

  getStars(studentId: number, typeId: number): number {
    const daily = this.RatedStudent.find(e => e.studentID === studentId);
    const perf = daily?.studentPerformance.find(p => p.performanceTypeID === typeId);
    return perf?.stars ?? 0;
  }

  toggleSelectAll(event: Event): void {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.allSelected = isChecked;
    this.selectedStudentIds = isChecked ? this.students.map(s => s.id) : [];
  }

  submitRatings() {
    console.log(this.RatedStudent)
      if(this.PerformanceTypesSelected.length==0){
       this.IsStudentPerformance=false
      } 
      else{
        this.StudentPerformanceServ.Add(this.RatedStudent, this.DomainName).subscribe((d) => {
          if (this.SelectedMedalId && this.selectedStudentIds.length) {
            this.selectedStudentIds.forEach(element => {
              this.studentMedal = new StudentMedal()
              this.studentMedal.studentID = element
              this.studentMedal.medalID = this.SelectedMedalId
              this.studentMedalServ.Add(this.studentMedal, this.DomainName).subscribe((d) => { })
            });
          }
          Swal.fire({
            icon: 'success',
            title: 'Done',
            text: 'Saved Succeessfully',
            confirmButtonColor: '#FF7519',
          }).then(() => {
            window.location.reload();
          });
        })
      }
  }

  isStudentSelected(id: number): boolean {
    return this.selectedStudentIds.includes(id);
  }

  GetStudentName(ID: number): string {
    return this.students.find(s => s.id === ID)?.user_Name || 'Unknown';
  }

  toggleStudentSelection(event: Event, id: number): void {
    const isChecked = (event.target as HTMLInputElement).checked;
    if (isChecked) {
      this.selectedStudentIds.push(id);
    } else {
      this.selectedStudentIds = this.selectedStudentIds.filter(x => x !== id);
    }
    this.allSelected = this.selectedStudentIds.length === this.students.length;
  }

  Applay() {
    for (let studentId of this.selectedStudentIds) {
      const daily = this.RatedStudent.find(e => e.studentID === studentId);
      if (!daily) continue;
      for (let type of this.PerformanceTypesSelected) {
        const performance = daily.studentPerformance.find(p => p.performanceTypeID === type.id);
        if (performance) {
          performance.stars = this.selectedRating;
        }
      }
    }
  }

  selectMedal(id: number) {
    this.SelectedMedalId = id;
    this.IsValid = true
  }

  GetAllMedals() {
    this.medals = []
    this.MedalServ.Get(this.DomainName).subscribe((d) => {
      this.medals = d
    })
  }

  Create() {
    this.mode = 'Create';
    this.GetAllMedals()
    this.openModal();
  }


  closeModal() {
    this.isModalVisible = false;
  }

  openModal() {
    this.isModalVisible = true;
  }

  Save() {
    if (!this.SelectedMedalId) {
      this.IsValid = false
    }
    else {
      this.closeModal()
    }
  }
}
