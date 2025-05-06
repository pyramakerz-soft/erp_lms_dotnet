import { Component } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { LessonActivity } from '../../../../Models/LMS/lesson-activity';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { LessonActivityService } from '../../../../Services/Employee/LMS/lesson-activity.service';
import { LessonService } from '../../../../Services/Employee/LMS/lesson.service';
import { Lesson } from '../../../../Models/LMS/lesson';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';

@Component({
  selector: 'app-lesson-activity',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './lesson-activity.component.html',
  styleUrl: './lesson-activity.component.css'
})
export class LessonActivityComponent {
  keysArray: string[] = ['id', 'englishTitle', 'arabicTitle', 'lessonEnglishTitle', 'lessonArabicTitle'];
  key: string = 'id';
  value: any = '';
 
  LessonActivityData: LessonActivity[] = []; 
  lessonActivity: LessonActivity = new LessonActivity();
  lesson: Lesson = new Lesson();
  editLessonActivity: boolean = false;
  validationErrors: { [key in keyof LessonActivity]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  path: string = '';

  DomainName: string = '';
  UserID: number = 0;
  lessonId: number = 0;
  User_Data_After_Login: TokenData = new TokenData('',0,0,0,0,'','','','','');
  isLoading = false;

  constructor(
    public account: AccountService, 
    public ApiServ: ApiService,
    public EditDeleteServ: DeleteEditPermissionService,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,  
    public router: Router,
    public lessonActivityService:LessonActivityService,
    public lessonService:LessonService
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.lessonId = Number(this.activeRoute.snapshot.paramMap.get('id'));
    this.DomainName = String(
      this.activeRoute.snapshot.paramMap.get('domainName')
    );

    this.GetLessonActivityByLessonId()
    this.GetLessonById()

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });
  }

  GetLessonActivityByLessonId(){
    this.LessonActivityData = [];
    this.lessonActivityService.GetByLessonId(this.lessonId, this.DomainName).subscribe((data) => {
      this.LessonActivityData = data;
    });
  }

  GetLessonById(){
    this.lessonService.GetByID(this.lessonId, this.DomainName).subscribe((data) => {
      this.lesson = data;
    });
  }

  GetLessonActivityById(id:number){
    this.lessonActivityService.GetByID(id, this.DomainName).subscribe((data) => {
      this.lessonActivity = data;
    });
  }

  openModal(lessonActivityId?: number) {
    if (lessonActivityId) {
      this.editLessonActivity = true;
      this.GetLessonActivityById(lessonActivityId);
    }  

    document.getElementById('Add_Modal')?.classList.remove('hidden');
    document.getElementById('Add_Modal')?.classList.add('flex');
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Lesson Activity?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.lessonActivityService.Delete(id, this.DomainName).subscribe((data: any) => {
          this.GetLessonActivityByLessonId();
        });
      }
    });
  }
}
