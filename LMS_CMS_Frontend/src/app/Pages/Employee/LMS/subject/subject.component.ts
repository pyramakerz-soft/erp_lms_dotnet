import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Subject } from '../../../../Models/LMS/subject';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { SubjectService } from '../../../../Services/Employee/LMS/subject.service';
import Swal from 'sweetalert2';
import { SubjectCategory } from '../../../../Models/LMS/subject-category';
import { SubjectCategoryService } from '../../../../Services/Employee/LMS/subject-category.service';
import { School } from '../../../../Models/school';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { Section } from '../../../../Models/LMS/section';
import { SectionService } from '../../../../Services/Employee/LMS/section.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { Grade } from '../../../../Models/LMS/grade';
import { AddEditSubjectComponent } from '../../../../Component/Employee/LMS/add-edit-subject/add-edit-subject.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-subject',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './subject.component.html',
  styleUrl: './subject.component.css'
})
export class SubjectComponent {
  keysArray: string[] = ['id', 'name','date'];
  key: string= "id";
  value: any = "";

  subjectData:Subject[] = []
  subjectCategories:SubjectCategory[] = []
  subject:Subject = new Subject()
  editSubject:boolean = false
  validationErrors: { [key in keyof Subject]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  path: string = ""

  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  selectedSchool: number | null = null;
  Schools: School[] = []
  
  selectedSection: number | null = null;
  Sections: Section[] = []
  
  Grades: Grade[] = []
  
  constructor(public account: AccountService, public router:Router, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, 
    public activeRoute: ActivatedRoute, private menuService: MenuService, public subjectService: SubjectService, public subjectCategoryService: SubjectCategoryService,
    public schoolService: SchoolService, public sectionService:SectionService, public gradeService:GradeService, public dialog: MatDialog) {}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.getSubjectData()

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

  getSubjectData(){
    this.subjectService.Get(this.DomainName).subscribe(
      (data) => {
        this.subjectData = data;
      }
    )
  }

  // getSubjectCategoryData(){
  //   this.subjectCategoryService.Get(this.DomainName).subscribe(
  //     (data) => {
  //       this.subjectCategories = data;
  //     }
  //   )
  // }

  // GetSubjectById(subjectId: number) {
  //   this.subjectService.GetByID(subjectId, this.DomainName).subscribe((data) => {
  //     this.subject = data;
  //   });
  // }

  MoveToSubjectView(SubId:number){
    this.router.navigateByUrl('Employee/Subject/' + this.DomainName + '/' + SubId);
  }

  // getSchools(){
  //   this.schoolService.Get(this.DomainName).subscribe(
  //     (data) => {
  //       this.Schools = data;
  //     }
  //   )
  // }

  // getSections(){
  //   this.sectionService.Get(this.DomainName).subscribe(
  //     (data) => {
  //       this.Sections = data.filter((section) => this.checkSchool(section))
  //     }
  //   )
  // }

  // getGrades(){
  //   this.gradeService.Get(this.DomainName).subscribe(
  //     (data) => {
  //       this.Grades = data.filter((grade) => this.checkSection(grade))
  //     }
  //   )
  // }

  // onSchoolChange(event: Event) {
  //   this.Sections = []
  //   this.Grades = []
  //   this.selectedSection = null
  //   const selectedValue = (event.target as HTMLSelectElement).value;
  //   this.selectedSchool = Number(selectedValue)
  //   if (this.selectedSchool) {
  //     this.getSections(); 
  //   }
  // }
 
  // onSectionChange(event: Event) {
  //   this.Grades = []
  //   const selectedValue = (event.target as HTMLSelectElement).value;
  //   this.selectedSection = Number(selectedValue)
  //   if (this.selectedSection) {
  //     this.getGrades(); 
  //   }
  // }

  // checkSchool(section:Section) {
  //   return section.schoolID == this.selectedSchool
  // }
 
  // checkSection(grade:Grade) {
  //   return grade.sectionID == this.selectedSection
  // }

  openModal(subjectId?: number) {
    if (subjectId) {
      this.editSubject = true;
      this.openDialog(subjectId, this.editSubject); 
    } else{
      this.openDialog(); 
    }
    
    // this.getSubjectCategoryData() 
    // this.getSchools()

    // document.getElementById("Add_Modal")?.classList.remove("hidden");
    // document.getElementById("Add_Modal")?.classList.add("flex");
  }

  openDialog(subjectId?: number, editSubject?: boolean): void {
    const dialogRef = this.dialog.open(AddEditSubjectComponent, {
      data: editSubject
        ? {
          subjectId: subjectId,
          editSubject: editSubject
        }
        : {
          editSubject: false
        },
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getSubjectData()
    });
  }

  // closeModal() {
  //   document.getElementById("Add_Modal")?.classList.remove("flex");
  //   document.getElementById("Add_Modal")?.classList.add("hidden");

  //   this.subject= new Subject()
  //   this.subjectCategories = []
  //   this.Schools = []
  //   this.Sections = []
  //   this.Grades = []
  //   this.selectedSchool = null
  //   this.selectedSection = null

  //   if(this.editSubject){
  //     this.editSubject = false
  //   }
  //   this.validationErrors = {}; 
  // }
  
  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    // try {
    //   const data: Bus[] = await firstValueFrom(this.busService.Get(this.DomainName));  
    //   this.busData = data || [];
  
    //   if (this.value !== "") {
    //     const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
    //     this.busData = this.busData.filter(t => {
    //       const fieldValue = t[this.key as keyof typeof t];
    //       if (typeof fieldValue === 'string') {
    //         return fieldValue.toLowerCase().includes(this.value.toLowerCase());
    //       }
    //       if (typeof fieldValue === 'number') {
    //         return fieldValue === numericValue;
    //       }
    //       return fieldValue == this.value;
    //     });
    //   }
    // } catch (error) {
    //   this.busData = [];
    //   console.log('Error fetching data:', error);
    // }
  }

  // capitalizeField(field: keyof Subject): string {
  //     return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  // }

  // isFormValid(): boolean {
  //   let isValid = true;
  //   for (const key in this.subject) {
  //     if (this.subject.hasOwnProperty(key)) {
  //       const field = key as keyof Subject;
  //       if (!this.subject[field]) {
  //         if(field == "ar_name" || field == "en_name" || field == "creditHours" || field == "gradeID" || field == "numberOfSessionPerWeek" || field == "orderInCertificate"
  //            || field == "passByDegree"  || field == "totalMark"  || field == "subjectCategoryID"  || field == "subjectCode"
  //         ){
  //           this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
  //           isValid = false;
  //         } else if(field == "iconFile"){
  //           if(!this.editSubject){
  //             this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
  //             isValid = false;
  //           }
  //         }
  //       } else {
  //         if(field == "en_name" || field == "ar_name"){
  //           if(this.subject.en_name.length > 100 || this.subject.ar_name.length > 100){
  //             this.validationErrors[field] = `*${this.capitalizeField(field)} cannot be longer than 100 characters`
  //             isValid = false;
  //           }
  //         } else{
  //           this.validationErrors[field] = '';
  //         }
  //       }
  //     }
  //   }
  //   return isValid;
  // }

  // validateNumber(event: any): void {
  //   const value = event.target.value;
  //   if (isNaN(value) || value === '') {
  //       event.target.value = '';
  //   }
  // }

  // onIsHideChange(event: Event) {
  //   const isChecked = (event.target as HTMLInputElement).checked;
  //   this.subject.hideFromGradeReport = isChecked
  // }

  // onInputValueChange(event: { field: keyof Subject, value: any }) {
  //   const { field, value } = event;
  //   (this.subject as any)[field] = value;
  //   if (value) {
  //     this.validationErrors[field] = '';
  //   }
  // }

  // onImageFileSelected(event: any) {
  //   const file: File = event.target.files[0];
    
  //   if (file) {
  //     if (file.size > 25 * 1024 * 1024) {
  //       this.validationErrors['iconFile'] = 'The file size exceeds the maximum limit of 25 MB.';
  //       this.subject.iconFile = null;
  //       return; 
  //     }
  //     if (file.type === 'image/jpeg' || file.type === 'image/png') {
  //       this.subject.iconFile = file; 
  //       this.validationErrors['iconFile'] = ''; 

  //       const reader = new FileReader();
  //       reader.readAsDataURL(file);
  //     } else {
  //       this.validationErrors['iconFile'] = 'Invalid file type. Only JPEG, JPG and PNG are allowed.';
  //       this.subject.iconFile = null;
  //       return; 
  //     }
  //   }
  // }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  // SaveSubject(){
  //   if(this.isFormValid()){
  //     if(this.editSubject == false){
  //       (this.subjectService.Add(this.subject, this.DomainName)).subscribe(
  //         (result: any) => {
  //           this.closeModal()
  //           this.getSubjectData()
  //         },
  //         error => {
  //           console.log(error)
  //         }
  //       );
  //     } else{
  //       this.subjectService.Edit(this.subject, this.DomainName).subscribe(
  //         (result: any) => {
  //           this.closeModal()
  //           this.getSubjectData()
  //         },
  //         error => {
  //           console.log(error)
  //         }
  //       );
  //     }  
  //   }
  // } 

  deleteSubject(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this Subject?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.subjectService.Delete(id, this.DomainName).subscribe(
          (data: any) => {
            this.subjectData=[]
            this.getSubjectData()
          }
        );
      }
    });
  }
}
