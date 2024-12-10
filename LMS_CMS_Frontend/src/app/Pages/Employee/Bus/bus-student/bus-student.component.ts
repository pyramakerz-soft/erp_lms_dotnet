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
import { Semester } from '../../../../Models/semester';
import { SemesterService } from '../../../../Services/Employee/semester.service';
import { Grade } from '../../../../Models/grade';
import { Class } from '../../../../Models/class';
import { FormsModule } from '@angular/forms';
import { StudentService } from '../../../../Services/student.service';
import { Student } from '../../../../Models/student';

@Component({
  selector: 'app-bus-student',
  standalone: true,
  imports: [CommonModule, FormsModule],
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

  filteredGrades:Grade[] = [];
  filteredClasses:Class[] = [];
  Students:Student[] = [];

  validationErrors: { [key in keyof BusStudent]?: string } = {};

  path:string = ""

  constructor(public busService:BusService, public busStudentService:BusStudentService, public account:AccountService, public activeRoute:ActivatedRoute ,public EditDeleteServ:DeleteEditPermissionService,
    public menuService :MenuService,public ApiServ:ApiService, public schoolService:SchoolService, public busCategoryService:BusCategoryService
    , public semesterService:SemesterService, public studentService:StudentService){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID=this.User_Data_After_Login.id;
    this.busId = Number(this.activeRoute.snapshot.paramMap.get('busId'))
    this.DomainName = String(this.activeRoute.snapshot.paramMap.get('domainName'))
    
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
      this.busStudent = data;
      console.log(this.busStudent)
      this.selectedSchool = this.busStudent.schoolID
      this.selectedGrade = this.busStudent.gradeID
      this.selectedClass = this.busStudent.classID

      const selectedSchool = this.SchoolGroupByGradeGroupByClass.find((element) => element.id == this.selectedSchool)
      this.filteredGrades = selectedSchool ? selectedSchool.grades : [];
      const selectedGrade = this.filteredGrades.find((element) => element.id == this.selectedGrade)
      this.filteredClasses = selectedGrade ? selectedGrade.classes : [];

      // To get the students
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
      title: 'Are you sure you want to delete student?',
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

    if(this.editBusStudent){
      this.editBusStudent = false
    }

    this.selectedSchool = null;
    this.selectedGrade = null;
    this.selectedClass = null;

    this.filteredGrades = [];
    this.filteredClasses = [];
    this.Students = [];
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
    this.filteredGrades = selectedSchool ? selectedSchool.grades : [];
    this.selectedGrade = null;
    this.selectedClass = null;
    this.filteredClasses = [];
  }

  onGradeChange() {
    const selectedSchool = this.SchoolGroupByGradeGroupByClass.find((element) => element.id == this.selectedSchool)
    this.filteredGrades = selectedSchool ? selectedSchool.grades : [];
    const selectedGrade = this.filteredGrades.find((element) => element.id == this.selectedGrade)
    this.filteredClasses = selectedGrade ? selectedGrade.classes : [];
    this.selectedClass = null;
  }

  onClassChange(){
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
    console.log(this.busStudent)
    this.busStudent.busID = this.busId

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
}
