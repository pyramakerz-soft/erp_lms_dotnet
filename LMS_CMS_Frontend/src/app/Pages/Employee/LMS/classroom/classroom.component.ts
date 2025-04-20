import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Classroom } from '../../../../Models/LMS/classroom';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { BuildingService } from '../../../../Services/Employee/LMS/building.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { ActivatedRoute } from '@angular/router';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { School } from '../../../../Models/school';
import Swal from 'sweetalert2';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { Section } from '../../../../Models/LMS/section';
import { Floor } from '../../../../Models/LMS/floor';
import { Building } from '../../../../Models/LMS/building';
import { SectionService } from '../../../../Services/Employee/LMS/section.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { FloorService } from '../../../../Services/Employee/LMS/floor.service';
import { CopyClassroom } from '../../../../Models/LMS/copy-classroom';
import { Grade } from '../../../../Models/LMS/grade';
import { firstValueFrom } from 'rxjs';
import { Employee } from '../../../../Models/Employee/employee';
import { EmployeeService } from '../../../../Services/Employee/employee.service';

@Component({
  selector: 'app-classroom',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './classroom.component.html',
  styleUrl: './classroom.component.css'
})
export class ClassroomComponent {
  keysArray: string[] = ['id', 'name','academicYearName','floorName','gradeName','number'];
  key: string= "id";
  value: any = "";

  classroomData:Classroom[] = []
  classroom:Classroom = new Classroom()
  editClassroom:boolean = false
  validationErrors: { [key in keyof Classroom]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  path: string = ""

  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  Schools: School[] = []
  SchoolsForFilteration: School[] = []
  selectedSchool: number | null = null;
  SelectedSchoolIdForFilteration: number = 0;

  AcademicYears:AcademicYear[] = []
  AcademicYearsForFilteration:AcademicYear[] = []
  Sections: Section[] = []
  Grades:Grade[] = []
  Floors:Floor[] = []
  Employees:Employee[] = []
  Buildings:Building[] = []
  selectedSection: number | null = null
  selectedBuilding: number | null = null

  isLoading=false
  isLoadingSaveClassroom=false

  copyClassroom:CopyClassroom = new CopyClassroom()

  activeAcademicYearID = 0

  constructor(public account: AccountService, public buildingService: BuildingService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, 
      private menuService: MenuService, public activeRoute: ActivatedRoute, public schoolService: SchoolService, public classroomService: ClassroomService, public employeeServ : EmployeeService ,
      public sectionService:SectionService, public gradeService:GradeService, public acadimicYearService:AcadimicYearService, public floorService: FloorService){}
      
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.getClassroomData() 
    this.getEmployeeData()
    this.getSchoolData()

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
  } 

  openModal(classroomId?: number) {
    if (classroomId) {
      this.editClassroom = true;
      this.getClassroomById(classroomId); 
    }
     
    this.getSchoolData()

    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.classroom= new Classroom()
    this.Schools = []
    this.AcademicYears = []
    this.Sections = []
    this.Grades = []
    this.Floors = []
    this.Buildings = []
    this.selectedSection = null
    this.selectedBuilding = null
    this.selectedSchool = null

    if(this.editClassroom){
      this.editClassroom = false
    }
    this.validationErrors = {}; 
  }

  openCopyModal() {
    this.getSchoolData()

    document.getElementById("Copy_Modal")?.classList.remove("hidden");
    document.getElementById("Copy_Modal")?.classList.add("flex");
  }

  closeCopyModal() {
    document.getElementById("Copy_Modal")?.classList.remove("flex");
    document.getElementById("Copy_Modal")?.classList.add("hidden");

    this.Schools = []
    this.copyClassroom = new CopyClassroom()
  }

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Classroom[] = await firstValueFrom(this.classroomService.Get(this.DomainName));  
      this.classroomData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.classroomData = this.classroomData.filter(t => {
          const fieldValue = t[this.key as keyof typeof t];
          if (typeof fieldValue === 'string') {
            return fieldValue.toLowerCase().includes(this.value.toLowerCase());
          }
          if (typeof fieldValue === 'number') {
            return fieldValue.toString().includes(numericValue.toString())
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.classroomData = [];
    }
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  getClassroomData(){
    this.classroomData=[]
    this.classroomService.GetByActiveAcYear(this.DomainName).subscribe(
      (data) => {
        this.classroomData = data;
        if(this.classroomData.length != 0){
          this.activeAcademicYearID = this.classroomData[0].academicYearID
          this.getSchoolIDForActiveAcademicYear()
        }
      }
    )
  }
  
  getClassroomDataByYearID(){
    this.classroomData=[]
    this.classroomService.GetByAcYearId(this.activeAcademicYearID, this.DomainName).subscribe(
      (data) => {
        this.classroomData = data;
        if(this.classroomData.length != 0){
          this.activeAcademicYearID = this.classroomData[0].academicYearID
        }
      }
    )
  }
  
  getSchoolIDForActiveAcademicYear(){ 
    this.acadimicYearService.GetByID(this.activeAcademicYearID, this.DomainName).subscribe(
      (data) => {
        this.SelectedSchoolIdForFilteration = data.schoolID; 
        this.getAllYearsForFilteration()
      }
    )
  }

  getClassroomById(id:number){
    this.classroomService.GetByID(id, this.DomainName).subscribe(
      (data) => {
        this.classroom = data;
        this.selectedSchool = this.classroom.schoolID
        if (this.selectedSchool) {
          this.getAcademicYears(); 
          this.getSections(); 
          this.getBuildings(); 
        }
        this.selectedSection = this.classroom.sectionID
        this.selectedBuilding = this.classroom.buildingID 

        this.getFloor()
        this.getGrade()
      }
    )
  }

  getSchoolData(){
    this.schoolService.Get(this.DomainName).subscribe(
      (data) => {
        this.Schools = data;
        this.SchoolsForFilteration = data;
      }
    )
  }

  getAllYearsForFilteration() {
    this.acadimicYearService.GetBySchoolId(this.SelectedSchoolIdForFilteration, this.DomainName).subscribe((d) => {
      this.AcademicYearsForFilteration = d
    })
  }

  getEmployeeData(){
    this.employeeServ.GetWithTypeId(1,this.DomainName).subscribe(
      (data) => {
        this.Employees = data;
      }
    )
  }

  capitalizeField(field: keyof Classroom): string {
      return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.classroom) {
      if (this.classroom.hasOwnProperty(key)) {
        const field = key as keyof Classroom;
        if (!this.classroom[field]) {
          if(field == "name" || field == "number" || field == "gradeID" || field == "floorID" || field == "academicYearID"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.classroom.name.length > 100){
              this.validationErrors[field] = `*${this.capitalizeField(field)} cannot be longer than 100 characters`
              isValid = false;
            }
          } else{
            this.validationErrors[field] = '';
          }
        }
      }
    }
    return isValid;
  }

  onInputValueChange(event: { field: keyof Classroom, value: any }) {
    const { field, value } = event;
    
    (this.classroom as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }
 
  validateNumber(event: any, field: keyof Classroom): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.classroom[field] === 'string') {
        this.classroom[field] = '' as never;  
      }
    }
  }

  SaveClassroom(){
    if(this.isFormValid()){
      this.isLoadingSaveClassroom=true
      if(this.editClassroom == false){
        this.classroomService.Add(this.classroom, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.isLoadingSaveClassroom=false
            this.getClassroomData()
          },
          error => {
            this.isLoadingSaveClassroom=false
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          }
        );
      } else{
        this.classroomService.Edit(this.classroom, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.getClassroomData()
            this.isLoadingSaveClassroom=false
          },
          error => {
            this.isLoadingSaveClassroom=false
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          }
        );
      }  
    }
  } 

  deleteClassroom(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this Classroom?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.classroomService.Delete(id, this.DomainName).subscribe(
          (data: any) => {
            this.classroomData=[]
            this.getClassroomData()
          }
        );
      }
    });
  }

  onSchoolChange(event: Event) {
    this.AcademicYears = []
    this.Sections = []
    this.Grades = []
    this.Floors = []
    this.Buildings = []
    this.selectedSection = null
    this.selectedBuilding = null

    this.classroom.academicYearID = 0
    this.classroom.gradeID = 0
    this.classroom.floorID = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.selectedSchool = Number(selectedValue)
    if (this.selectedSchool) {
      this.getAcademicYears(); 
      this.getSections(); 
      this.getBuildings(); 
    }
  }

  onBuildingChange(event: Event){
    this.Floors = []
    this.classroom.floorID = 0

    this.getFloor()
  }

  getFloor(){
    this.floorService.GetByBuildingId(Number(this.selectedBuilding), this.DomainName).subscribe(
      (data) => {
        this.Floors = data
      }
    )
  }

  onSectionChange(event: Event){
    this.Grades = []
    this.classroom.gradeID = 0

    this.getGrade()
  }

  getGrade(){
    this.gradeService.Get(this.DomainName).subscribe(
      (data) => {
        this.Grades = data.filter((grade) => this.checkSection(grade))
      }
    )
  }

  getAcademicYears(){
    this.acadimicYearService.Get(this.DomainName).subscribe(
      (data) => {
        this.AcademicYears = data.filter((academic_year) => this.checkSchool(academic_year))
      }
    )
  }

  getSections(){
    this.sectionService.Get(this.DomainName).subscribe(
      (data) => {
        this.Sections = data.filter((section) => this.checkSchool(section))
      }
    )
  }

  getBuildings(){
    this.buildingService.Get(this.DomainName).subscribe(
      (data) => {
        this.Buildings = data.filter((building) => this.checkSchool(building))
      }
    )
  }

  checkSchool(element:any) {
    return element.schoolID == this.selectedSchool
  }
  
  checkSection(element:any) {
    return element.sectionI  == this.selectedSection
  }

  SaveCopy(){
    this.isLoading=true
    if(this.copyClassroom.fromAcademicYearID == this.copyClassroom.toAcademicYearID){
      this.closeCopyModal()
      this.getClassroomData()
      this.isLoading=false
    } else{
      this.classroomService.CopyClassroom(this.copyClassroom, this.DomainName).subscribe(
        (result: any) => {
          this.closeCopyModal()
          this.getClassroomData()
          this.isLoading=false
        },
        error => {
          this.isLoading=false
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Try Again Later!',
            confirmButtonText: 'Okay',
            customClass: { confirmButton: 'secondaryBg' },
          });
        }
      );
    }
  }
}
