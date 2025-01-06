import { Component } from '@angular/core';
import { Bus } from '../../../../Models/Bus/bus';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { BusService } from '../../../../Services/Employee/Bus/bus.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BusStudentService } from '../../../../Services/Employee/Bus/bus-student.service';
import { BusStudent } from '../../../../Models/Bus/bus-student';
import Swal from 'sweetalert2';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { ApiService } from '../../../../Services/api.service';
import { School } from '../../../../Models/school';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { BusCategoryService } from '../../../../Services/Employee/Bus/bus-category.service';
import { BusType } from '../../../../Models/Bus/bus-type';
import { Semester } from '../../../../Models/LMS/semester';
import { FormsModule } from '@angular/forms';
import { StudentService } from '../../../../Services/student.service';
import { Student } from '../../../../Models/student';
import { SearchComponent } from '../../../../Component/search/search.component';
import { firstValueFrom } from 'rxjs';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { SemesterService } from '../../../../Services/Employee/LMS/semester.service';
import { Classroom } from '../../../../Models/LMS/classroom';
import { Grade } from '../../../../Models/LMS/grade';
import { Section } from '../../../../Models/LMS/section';
import { SectionService } from '../../../../Services/Employee/LMS/section.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';

@Component({
  selector: 'app-bus-student',
  standalone: true,
  imports: [CommonModule, FormsModule, SearchComponent],
  templateUrl: './bus-student.component.html',
  styleUrl: './bus-student.component.css'
})
export class BusStudentComponent {
  User_Data_After_Login :TokenData =new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  bus :Bus  = new Bus()
  busStudent :BusStudent  = new BusStudent()
  busId: number = 0
  busStudentData :BusStudent[] = []
  editBusStudent = false
  exception = false
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  UserID:number=0; 
  DomainName: string = "";
  SchoolGroupByGradeGroupByClass:School[] = []
  BusCategories:BusType[] = []
  Semesters:Semester[] = []

  selectedSchool: number | null = null;
  selectedGrade: number | null = null;
  selectedClass: number | null = null;
  selectedSection: number | null = null;

  filteredGrades:Grade[] = [];
  filteredClasses:Classroom[] = [];
  filteredSections:Section[] = [];
  Students:Student[] = [];

  validationErrors: { [key in keyof BusStudent]?: string } = {};

  path:string = ""

  keysArray: string[] = ['schoolName', 'gradeName', 'className', 'studentName', 'busCategoryName', 'semseterName'];
  key: string= "schoolName";
  value: any = "";

  busDataTransfer :Bus[] = []
  busStudentTransfer :BusStudent = new BusStudent()
  isTransfer:boolean = false

  AcademicYearData:AcademicYear[]=[]

  id:number =0;

  constructor(public busService:BusService, public busStudentService:BusStudentService, public account:AccountService, public activeRoute:ActivatedRoute ,public EditDeleteServ:DeleteEditPermissionService,
    public menuService :MenuService,public ApiServ:ApiService, public schoolService:SchoolService, public busCategoryService:BusCategoryService, public sectionService:SectionService, 
    public gradeService:GradeService, public classroomService:ClassroomService, public semesterService:SemesterService, public studentService:StudentService ,public AcademicServ:AcadimicYearService){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID=this.User_Data_After_Login.id;
    this.busId = Number(this.activeRoute.snapshot.paramMap.get('busId'))
    this.DomainName = String(this.activeRoute.snapshot.paramMap.get('domainName'))
    this.GetAllAcademicYears();
    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });
    
    this.GetBusById(this.busId);
    this.GetStudentsByBusId(this.busId);

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers=settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers=settingsPage.allow_Edit_For_Others
      }
    });

    this.GetSchoolsGroupByGradeGroupByClass()
    this.GetBusCategories()
    this.GetSemesters()
  }

  GetBusById(busId:number){
    this.busService.GetbyBusId(busId,this.DomainName).subscribe((data) => {
      this.bus = data;
    });
  }
  
  GetbyId(busStuId:number){
    this.busStudentService.GetbyId(busStuId,this.DomainName).subscribe((data) => {
      this.id = data.studentID
      this.busStudent = data; 
      this.selectedClass = this.busStudent.classID
      this.onClassChange()
    });
  }
  
  GetStudentsByBusId(busId:number){
    this.busStudentData= []
    this.busStudentService.GetbyBusId(busId,this.DomainName).subscribe((data) => {
      this.busStudentData = data;
    });
  }
  
  GetSchoolsGroupByGradeGroupByClass(){
    this.schoolService.Get(this.DomainName).subscribe((data) => {
      this.SchoolGroupByGradeGroupByClass = data;
    });
  }
 
  GetBusCategories(){
    this.busCategoryService.Get(this.DomainName).subscribe((data) => {
      this.BusCategories = data;
    });
  }
 
  GetSemesters(){
    this.semesterService.Get(this.DomainName).subscribe((data) => {
      this.Semesters = data;
    });
  }

  deleteBusStudent(busStudentId: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this student?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.busStudentService.DeleteBusStudent(busStudentId,this.DomainName).subscribe(
          (data: any) => {
            this.GetStudentsByBusId(this.busId);
          }
        );
      }
    });
  }

  OpenModal(busStudentId?: number) {
    if (busStudentId) {
      this.editBusStudent = true;
      this.GetbyId(busStudentId)
    }
    
    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");
    this.busStudent = new BusStudent()

    this.exception = false

    if(this.editBusStudent){
      this.editBusStudent = false
    }

    this.selectedSchool = null;
    this.selectedGrade = null;
    this.selectedClass = null;
    this.selectedSection = null;

    this.filteredGrades = [];
    this.filteredSections = [];
    this.filteredClasses = [];
    this.Students = [];
    this.validationErrors = {};
  }

  onIsExceptionChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.exception = isChecked
    this.busStudent.isException = this.exception
    if(this.busStudent.isException == false){
      this.busStudent.exceptionFromDate = null
      this.busStudent.exceptionToDate = null
      this.validationErrors["exceptionFromDate"] = '';
      this.validationErrors["exceptionToDate"] = '';
    }
  }
  
  IsAllowDelete(InsertedByID:number){
    const IsAllow=this.EditDeleteServ.IsAllowDelete(InsertedByID,this.UserID,this.AllowDeleteForOthers);
    return IsAllow;
  }
  
  IsAllowEdit(InsertedByID:number){
    const IsAllow=this.EditDeleteServ.IsAllowEdit(InsertedByID,this.UserID,this.AllowEditForOthers);
    return IsAllow;
  }

  onSchoolChange() {
    const selectedSchool = this.SchoolGroupByGradeGroupByClass.find((element) => element.id == this.selectedSchool)
    this.sectionService.Get(this.DomainName).subscribe(
      (data) => {
        this.filteredSections = data.filter((section) => this.checkSchool(section))
      }
    )

    this.selectedGrade = null;
    this.selectedClass = null;
    this.selectedSection = null;
    this.filteredClasses = [];
    this.filteredGrades = [];
    this.Students = [];
    this.busStudent.studentID = 0
  }

  checkSchool(element:any) {
    return element.schoolID == this.selectedSchool
  }

  onSectionChange() {
    this.gradeService.Get(this.DomainName).subscribe(
      (data) => {
        this.filteredGrades = data.filter((grade) => this.checkSection(grade))
      }
    )

    this.selectedGrade = null;
    this.selectedClass = null;
    this.filteredClasses = [];
    this.Students = [];
    this.busStudent.studentID = 0
  }

  checkSection(element:any) {
    return element.sectionID == this.selectedSection
  }

  onGradeChange() {
    this.classroomService.Get(this.DomainName).subscribe(
      (data) => {
        this.filteredClasses = data.filter((cls) => this.checkGrade(cls))
      }
    )

    this.selectedClass = null;
    this.Students = [];
    this.busStudent.studentID = 0
  }

  checkGrade(element:any) {
    return element.gradeID == this.selectedGrade
  }

  onClassChange(){
    this.Students = [];
    this.busStudent.studentID = 0
    if(this.selectedClass){
      this.studentService.GetByClassID(this.selectedClass, this.DomainName).subscribe((data) => {
        this.Students = data
      });
    }
  }

  capitalizeField(field: keyof BusStudent): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.busStudent) {
      if (this.busStudent.hasOwnProperty(key)) {
        const field = key as keyof BusStudent;
        if (!this.busStudent[field]) {
          if(field == "busCategoryID" || field == 'semseterID'|| field == 'studentID'){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          this.validationErrors[field] = '';
        }

        if(field == 'isException'){
          if(this.busStudent.isException == true){
            if(this.busStudent.exceptionFromDate == null){
              this.validationErrors["exceptionFromDate"] = `*From Date is required`
              isValid = false;
            }
            if(this.busStudent.exceptionToDate == null){
              this.validationErrors["exceptionToDate"] = `*To Date is required`
              isValid = false;
            }
          }
        }
      }
    }
    return isValid;
  }

  onInputValueChange(event: { field: keyof BusStudent, value: any }) {
    const { field, value } = event;
    if (field == "busCategoryID" || field == "semseterID"|| field == "studentID"|| field == "exceptionFromDate"|| field == "exceptionToDate") {
      (this.busStudent as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
    }
  }

  SaveBusStudent(){
    this.busStudent.busID = this.busId
    this.busStudent.studentID = this.id

    if(this.busStudent.isException == false){
      this.busStudent.exceptionFromDate = null
      this.busStudent.exceptionToDate = null
    }
    
    if (this.isFormValid()) {
      if(this.editBusStudent == false){
        this.busStudentService.Add(this.busStudent, this.DomainName).subscribe((data) => {
          this.closeModal()
          this.GetStudentsByBusId(this.busId);
        });
      } else{ 
        this.busStudentService.Edit(this.busStudent, this.DomainName).subscribe((data) => {
          this.closeModal()
          this.GetStudentsByBusId(this.busId);
        });
      }
    }
  }

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: BusStudent[] = await firstValueFrom(this.busStudentService.GetbyBusId(this.busId,this.DomainName));  
      this.busStudentData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.busStudentData = this.busStudentData.filter(t => {
          const fieldValue = t[this.key as keyof typeof t];
          if (typeof fieldValue === 'string') {
            return fieldValue.toLowerCase().includes(this.value.toLowerCase());
          }
          if (typeof fieldValue === 'number') {
            return fieldValue === numericValue;
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.busStudentData = [];
      console.log('Error fetching data:', error);
    }
  }

  getBusesForTransfer(){
    this.busService.Get(this.DomainName).subscribe(
      (data: any) => {
        this.busDataTransfer = data;
      }
    );
  }

  openTransferBusStudentModal(busStudentId:number){
    document.getElementById("Transfer_Modal")?.classList.remove("hidden");
    document.getElementById("Transfer_Modal")?.classList.add("flex");

    this.getBusesForTransfer()

    this.busStudentService.GetbyId(busStudentId,this.DomainName).subscribe((data) => {
      this.busStudentTransfer = data
    });
  }
  
  closeTransferBusStudentModal(){
    document.getElementById("Transfer_Modal")?.classList.remove("flex");
    document.getElementById("Transfer_Modal")?.classList.add("hidden");

    this.busDataTransfer = []
    this.busStudentTransfer = new BusStudent()
    this.isTransfer = false
  }

  onTransferChange(){
    this.isTransfer = true
  }

  SaveTransfer(){
    this.busStudentService.Edit(this.busStudentTransfer, this.DomainName).subscribe((data) => {
      this.closeTransferBusStudentModal()
      this.GetStudentsByBusId(this.busId)
    });
  }

  GetAllAcademicYears(){
    this.AcademicServ.Get(this.DomainName).subscribe((data) => {
        this.AcademicYearData=data
    });
  }

  getDataByAcademicYear(event: Event){
    const selectedValue: string = ((event.target as HTMLSelectElement).value);
    if(selectedValue=="default")
    this.GetStudentsByBusId(this.busId);
    else{
      this.busStudentData=this.busStudentData.filter(t=>t.studentAcademicYear==selectedValue)
    }
  }
}
