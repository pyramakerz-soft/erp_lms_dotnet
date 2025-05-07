import { Component } from '@angular/core';
import { LessonLive } from '../../../../Models/LMS/lesson-live';
import { LessonLiveService } from '../../../../Services/Employee/LMS/lesson-live.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { DaysService } from '../../../../Services/Octa/days.service';
import { SubjectService } from '../../../../Services/Employee/LMS/subject.service';
import { Subject } from '../../../../Models/LMS/subject';
import { Day } from '../../../../Models/day';
import { Classroom } from '../../../../Models/LMS/classroom';

@Component({
  selector: 'app-lesson-live',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './lesson-live.component.html',
  styleUrl: './lesson-live.component.css'
})
export class LessonLiveComponent {
 User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: LessonLive[] = [];
  subject: Subject[] = [];
  days: Day[] = [];
  classrooms: Classroom[] = [];

  SelectedGradeId: number = 0;
  SelectedClassId: number = 0;
  SelectedSubjectId: number = 0;
  SelectedDayId: number = 0;

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'liveLink' , 'weekDayName' ,'classroomName' ,'subjectEnglishName'];

  live: LessonLive = new LessonLive();

  validationErrors: { [key in keyof LessonLive]?: string } = {};
  isLoading = false;

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public LessonLiveServ: LessonLiveService ,
    public ClassroomServ :ClassroomService ,
    public weekdaysServ : DaysService ,
    public SubjectServ : SubjectService 
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

    this.GetAllData();
    this.getAllClass();
    this.getAllDays();
  }

  GetAllData() {
    this.TableData = [];
    this.LessonLiveServ.Get(this.DomainName).subscribe((d) => {
      this.TableData = d;
      console.log(this.TableData)
    });
  }

  Create() {
    this.mode = 'Create';
    this.live = new LessonLive();
    this.validationErrors = {};
    this.openModal();
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Lesson Activity Type?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.LessonLiveServ.Delete(id, this.DomainName).subscribe((d) => {
          this.GetAllData();
        });
      }
    });
  }

  Edit(row: LessonLive) {
    this.mode = 'Edit';
    this.LessonLiveServ.GetByID(row.id, this.DomainName).subscribe((d) => {
      this.live = d;
      this.selectClass()
    });
    this.openModal();
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

  CreateOREdit() {
    if (this.isFormValid()) {
      this.isLoading = true;
      if (this.mode == 'Create') {
        this.LessonLiveServ.Add(
          this.live,
          this.DomainName
        ).subscribe(
          (d) => {
            this.GetAllData();
            this.isLoading = false;
            this.closeModal();
          },
          (error) => {
            this.isLoading = false; // Hide spinner
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' }
            });
          }
        );
      }
      if (this.mode == 'Edit') {
        this.LessonLiveServ.Edit(
          this.live,
          this.DomainName
        ).subscribe(
          (d) => {
            this.GetAllData();
            this.isLoading = false;
            this.closeModal();
          },
          (error) => {
            this.isLoading = false; // Hide spinner
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' }
            });
          }
        );
      }
    }
    this.GetAllData();
  }

  getAllSubject() {
    this.subject = []
    this.SelectedSubjectId = 0
    this.SubjectServ.GetByGradeId(this.SelectedGradeId, this.DomainName).subscribe((d) => {
      this.subject = d
    })
  }

  getAllDays() {
    this.days = []
    this.live.weekDayID = 0
    this.weekdaysServ.Get( this.DomainName).subscribe((d) => {
      this.days = d
    })
  }

  selectClass() {
    const classs: Classroom | undefined = this.classrooms.find(
      s => s.id === Number(this.live.classroomID)
    );
    if (classs) {
      this.SelectedGradeId = classs.gradeID;
      this.getAllSubject();
    } 
  }

  getAllClass() {
    this.SelectedClassId = 0
    this.classrooms =[]
    this.SelectedSubjectId=0
    this.subject =[]
    this.ClassroomServ.Get(this.DomainName).subscribe((d) => {
      this.classrooms = d
    })
  }

  closeModal() {
    this.isModalVisible = false;
  }

  openModal() {
    this.validationErrors = {};
    this.isModalVisible = true;
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.live) {
      if (this.live.hasOwnProperty(key)) {
        const field = key as keyof LessonLive;
        if (!this.live[field]) {
          if (
            field == 'period' ||
            field == 'liveLink'||
            field == 'weekDayID'||
            field == 'classroomID'||
            field == 'subjectID'
          ) {
            this.validationErrors[field] = `*${this.capitalizeField(
              field
            )} is required`;
            isValid = false;
          }
        }
      }
    }
    return isValid;
  }

  capitalizeField(field: keyof LessonLive): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof LessonLive; value: any }) {
    const { field, value } = event;
    (this.live as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: LessonLive[] = await firstValueFrom(
        this.LessonLiveServ.Get(this.DomainName)
      );
      this.TableData = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.TableData = this.TableData.filter((t) => {
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
      this.TableData = [];
    }
  }

  validateNumber(event: any, field: keyof LessonLive): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.live[field] === 'string') {
        this.live[field] = '' as never;  
      }
    }
  }
}
