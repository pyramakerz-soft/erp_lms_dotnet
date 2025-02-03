import { Component } from '@angular/core';
import { StudentAcademicYear } from '../../../../Models/LMS/student-academic-year';
import { FeesActivation } from '../../../../Models/Accounting/fees-activation';
import { StudentAcademicYearService } from '../../../../Services/Employee/LMS/student-academic-year.service';
import { Router, ActivatedRoute } from '@angular/router';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { AccountingTreeChartService } from '../../../../Services/Employee/Accounting/accounting-tree-chart.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import Swal from 'sweetalert2';
import { FeesActivationService } from '../../../../Services/Employee/Accounting/fees-activation.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { School } from '../../../../Models/school';
import { Section } from '../../../../Models/LMS/section';
import { Grade } from '../../../../Models/LMS/grade';
import { Classroom } from '../../../../Models/LMS/classroom';
import { Student } from '../../../../Models/student';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { SectionService } from '../../../../Services/Employee/LMS/section.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { TuitionFeesType } from '../../../../Models/Accounting/tuition-fees-type';
import { TuitionFeesTypeService } from '../../../../Services/Employee/Accounting/tuition-fees-type.service';
import { TuitionDiscountTypes } from '../../../../Models/Accounting/tuition-discount-types';
import { TuitionDiscountTypeService } from '../../../../Services/Employee/Accounting/tuition-discount-type.service';

@Component({
  selector: 'app-fees-activation',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './fees-activation.component.html',
  styleUrl: './fees-activation.component.css'
})
export class FeesActivationComponent {

  User_Data_After_Login: TokenData = new TokenData('',0,0,0,0,'','','','','');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableDataStudent: StudentAcademicYear[] = [];
  TableDataStudentOriginal: StudentAcademicYear[] = [];

  TableData: FeesActivation[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name' ,'accountNumberName'];

  Fees:FeesActivation=new FeesActivation()

  SchoolId: number = 0;
  SectionId: number = 0;
  GradeId: number = 0;
  ClassRoomId: number = 0;
  StudentId: number = 0;
  
  Schools:School[]=[]
  Sections: Section[] = [];
  Grades: Grade[] = [];
  ClassRooms: Classroom[] = [];
  Students: StudentAcademicYear[] = [];

  FeesTypes:TuitionFeesType[]=[]
  FeesDiscountType :TuitionDiscountTypes[]=[]

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    public accountServ:AccountingTreeChartService ,
    public StudentAcademicYearServ :StudentAcademicYearService ,
    public EditDeleteServ: DeleteEditPermissionService,
    public feesActivationServ : FeesActivationService ,
    public SchoolServ :SchoolService ,
    public SectionServ:SectionService ,
    public GradeServ : GradeService ,
    public ClassRoomServ : ClassroomService ,
    public TuitionFeesTypeServ :TuitionFeesTypeService ,
    public FeesDiscountTypeServ :TuitionDiscountTypeService
  ) {}

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

    this.GetAllStudentData();
    this.GetAllSchools();
    this.GetAllFeesData()
  }

  GetAllStudentData(){
     this.StudentAcademicYearServ.Get(this.DomainName).subscribe((d)=>{
       this.TableDataStudent=d;
       this.TableDataStudentOriginal=d;
       this.Students=d
     })
  }

  GetAllFeesData(){
    this.feesActivationServ.Get(this.DomainName).subscribe((d)=>{
      this.TableData=d
    })
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Fees Activation?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.feesActivationServ.Delete(id,this.DomainName).subscribe((D)=>{
          
        })
      }
    });
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(
      InsertedByID,
      this.UserID,
      this.AllowDeleteForOthers
    );
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(
      InsertedByID,
      this.UserID,
      this.AllowEditForOthers
    );
    return IsAllow;
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
  }

  GetAllSchools(){
    this.SchoolServ.Get(this.DomainName).subscribe((d)=>{
      this.Schools=d
    })
  }

  GetAllSectionsBySchoolID(){
    this.SectionServ.GetBySchoolId(this.SchoolId,this.DomainName).subscribe((d)=>{
      this.Sections=d
    })
  }

  GetAllGradeBySectionId(){
    this.GradeServ.GetBySectionId(this.SectionId, this.DomainName).subscribe((d)=>{
      console.log(d)
      this.Grades=d
    })
  }

  GetAllClassRoomByGradeID(){
    this.ClassRoomServ.GetByGradeId(this.GradeId,this.DomainName).subscribe((d)=>{
      this.ClassRooms=d
    })
  }

  
  SchoolIsChanged(event: Event) {
    this.SchoolId = Number((event.target as HTMLSelectElement).value);
    this.SectionId = 0;
    this.GradeId = 0;
    this.ClassRoomId = 0;
    this.StudentId = 0;
    this.Sections = [];
    this.Grades = [];
    this.ClassRooms = [];
    this.Students = [];
    this.GetAllSectionsBySchoolID();
    this.GetStudents();
  }
  
  SectionIsChanged(event: Event) {
    this.SectionId = Number((event.target as HTMLSelectElement).value);
    this.GradeId = 0;
    this.ClassRoomId = 0;
    this.StudentId = 0;
    this.Grades = [];
    this.ClassRooms = [];
    this.Students = [];
    this.GetAllGradeBySectionId();
    this.GetStudents();
  }
  
  GradeIsChanged(event: Event) {
    this.GradeId = Number((event.target as HTMLSelectElement).value);
    this.ClassRoomId = 0;
    this.StudentId = 0;
    this.ClassRooms = [];
    this.Students = [];
    this.GetAllClassRoomByGradeID();
    this.GetStudents();
  }
  
  ClassRoomIsChanged(event: Event) {
    this.ClassRoomId = Number((event.target as HTMLSelectElement).value);
    this.StudentId = 0;
    this.GetStudents();
  }
  
  GetStudents() {
    console.log(this.Students)
    this.Students = [];
    this.Students = this.TableDataStudentOriginal.filter((item: StudentAcademicYear) => {
      return (
        (this.SchoolId == 0 || item.schoolID == this.SchoolId) &&
        (this.SectionId == 0 || item.sectionId == this.SectionId) &&
        (this.GradeId == 0 || item.gradeID == this.GradeId) &&
        (this.ClassRoomId == 0 || item.classID == this.ClassRoomId)
      );
    });
  }

  Search(){

  }

  GetAllTuitionFeesType(){
    this.TuitionFeesTypeServ.Get(this.DomainName).subscribe((d)=>{
      this.FeesTypes=d
    })
  }

  GetAllDiscountType(){
    this.FeesDiscountTypeServ.Get(this.DomainName).subscribe((d)=>{
      this.FeesDiscountType=d
    })
  }

  GetAllAcademicYear(){
    
  }
  
}
