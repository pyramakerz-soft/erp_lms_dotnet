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
  medals :Medal[]=[]
  subjects :Subject[]=[]
  PerformanceTypes :PerformanceType[]=[]
  isLoading: boolean = false

  SelectedSchoolId: number = 0;
  SelectedYearId: number = 0;
  SelectedGradeId: number = 0;
  SelectedClassId: number = 0;
  SelectedSubjectId: number = 0;

  TableData: DailyPerformance[]=[];
  isModalVisible: boolean = false;
  mode: string = '';

  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'englishName' ,'arabicName'];
  SelectedMedalId: number | null = null;
  IsView:boolean=false
  selectedTypeIds: number[] = []; // Array to store selected type IDs

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
    public studentMedalServ : StudentMedalService ,
    public MedalServ : MedalService ,
    public subjectServ :SubjectService ,
    public PerformanceTypeServ :PerformanceTypeService
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
    this.schools=[]
    this.SchoolServ.Get(this.DomainName).subscribe((d) => {
      this.schools = d
    })
  }

  View() {
    this.IsView=true
    this.getAllPerformanceType()
    this.students=[]
    this.studentServ.GetBySchoolGradeClassID(this.SelectedSchoolId,this.SelectedGradeId,this.SelectedClassId, this.DomainName).subscribe((d: any) => {
      console.log(d)
      this.students=d.students
    })
  }

  getAllSubject(){
    this.subjects=[]
    this.SelectedSubjectId=0
    this.subjectServ.GetByGradeId(this.SelectedGradeId,this.DomainName).subscribe((d)=>{
      this.subjects=d
    })
  }

  getAllGradesBySchoolId() {
    this.Grades = []
    this.IsView=false
    this.SelectedGradeId=0
    this.SelectedClassId=0
    this.GradeServ.GetBySchoolId(this.SelectedSchoolId, this.DomainName).subscribe((d) => {
      this.Grades = d
    })
  }

  saveSelection() {
    console.log('Selected Type IDs:', this.selectedTypeIds);
  }

  getAllClassByGradeId() {
    this.class = []
    this.SelectedClassId=0
    this.IsView=false
    this.ClassroomServ.GetByGradeId(this.SelectedGradeId, this.DomainName).subscribe((d) => {
      this.class = d
    })
  }

  getAllPerformanceType(){
    this.PerformanceTypes=[]
    this.PerformanceTypeServ.Get(this.DomainName).subscribe((d)=>{
      this.PerformanceTypes=d
    })
  }
}
