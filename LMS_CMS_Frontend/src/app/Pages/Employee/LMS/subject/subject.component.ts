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
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-subject',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './subject.component.html',
  styleUrl: './subject.component.css'
})
export class SubjectComponent {
  keysArray: string[] = ['id', 'en_name','ar_name','gradeName','orderInCertificate','creditHours','subjectCode','passByDegree','totalMark','subjectCategoryName','numberOfSessionPerWeek' ];
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

  MoveToSubjectView(SubId:number){
    this.router.navigateByUrl('Employee/Subject/' + this.DomainName + '/' + SubId);
  }
  
  openModal(subjectId?: number) {
    if (subjectId) {
      this.editSubject = true;
      this.openDialog(subjectId, this.editSubject); 
    } else{
      this.openDialog(); 
    }
     
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
  
  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Subject[] = await firstValueFrom(this.subjectService.Get(this.DomainName));  
      this.subjectData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.subjectData = this.subjectData.filter(t => {
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
      this.subjectData = [];
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
