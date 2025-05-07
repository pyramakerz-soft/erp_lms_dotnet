import { Component } from '@angular/core';
import { LessonResource } from '../../../../Models/LMS/lesson-resource';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { TokenData } from '../../../../Models/token-data';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { LessonResourceType } from '../../../../Models/LMS/lesson-resource-type';
import { ActivatedRoute, Router } from '@angular/router';
import { Lesson } from '../../../../Models/LMS/lesson';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { LessonResourceTypeService } from '../../../../Services/Employee/LMS/lesson-resource-type.service';
import { LessonResourceService } from '../../../../Services/Employee/LMS/lesson-resource.service';
import { LessonService } from '../../../../Services/Employee/LMS/lesson.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Classroom } from '../../../../Models/LMS/classroom';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';

@Component({
  selector: 'app-lesson-resource',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './lesson-resource.component.html',
  styleUrl: './lesson-resource.component.css'
})
export class LessonResourceComponent {
  keysArray: string[] = ['id', 'englishTitle', 'arabicTitle', 'lessonEnglishTitle', 'lessonArabicTitle'];
  key: string = 'id';
  value: any = '';
 
  LessonResourceData: LessonResource[] = []; 
  lessonResource: LessonResource = new LessonResource();
  lesson: Lesson = new Lesson();
  editLessonResource: boolean = false;
  validationErrors: { [key in keyof LessonResource]?: string } = {};

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
  
  safeDetailsHtml: SafeHtml | null = null;
  
  LessonResourceTypes: LessonResourceType[] = []
  selectedAttachmentType: 'file' | 'text' | null = null;
  
  SpecificClassroom = false; 
  classes: Classroom[] = [];
  isDropdownOpen = false;

  AllSelectedClassRooms: Classroom[] = []
  
  constructor(
    public account: AccountService, 
    public ApiServ: ApiService,
    public EditDeleteServ: DeleteEditPermissionService,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,  
    public router: Router,
    public lessonResourceService:LessonResourceService,
    public lessonService:LessonService,
    private sanitizer: DomSanitizer,
    private classroomService: ClassroomService,
    public lessonResourceTypeService:LessonResourceTypeService
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.lessonId = Number(this.activeRoute.snapshot.paramMap.get('id')); 

    this.GetLessonResourceByLessonId()
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

  GetLessonResourceByLessonId(){
    this.LessonResourceData = [];
    this.lessonResourceService.GetByLessonId(this.lessonId, this.DomainName).subscribe((data) => {
      this.LessonResourceData = data;
    });
  }

  GetLessonById(){
    this.lessonService.GetByID(this.lessonId, this.DomainName).subscribe((data) => {
      this.lesson = data
      this.safeDetailsHtml = this.sanitizer.bypassSecurityTrustHtml(this.lesson.details);
    });
  }

  GetClasses(){
    this.classes = []
    this.classroomService.Get(this.DomainName).subscribe((data) => {
      this.classes = data 
    });
  }

  GetLessonResourceById(id:number){
    this.lessonResourceService.GetByID(id, this.DomainName).subscribe((data) => {
      this.lessonResource = data; 
      if(this.lessonResource.attachmentLink != null && this.lessonResource.attachmentLink != ''){
        if (this.lessonResource.attachmentLink?.includes('Uploads/')) {
          this.selectedAttachmentType = 'file'
        }else{
          this.selectedAttachmentType = 'text'
        }
      }
 
      this.AllSelectedClassRooms = this.lessonResource.classrooms.map(c => c);
      if(this.AllSelectedClassRooms.length != 0){
        this.SpecificClassroom = true
      }
    });
  }

  GetLessonResourceTypes(){
    this.LessonResourceTypes = []
    this.lessonResourceTypeService.Get(this.DomainName).subscribe((data) => {
      this.LessonResourceTypes = data;
    });
  }

  openModal(lessonResourceId?: number) {
    if (lessonResourceId) {
      this.editLessonResource = true;
      this.GetLessonResourceById(lessonResourceId);
    }  

    this.GetLessonResourceTypes();
    this.GetClasses();

    document.getElementById('Add_Modal')?.classList.remove('hidden');
    document.getElementById('Add_Modal')?.classList.add('flex');
  }

  closeModal() {
    document.getElementById('Add_Modal')?.classList.remove('flex');
    document.getElementById('Add_Modal')?.classList.add('hidden');

    this.lessonResource = new LessonResource(); 
 
    this.selectedAttachmentType = null 
    this.LessonResourceTypes = []  
    this.classes = []  
    this.AllSelectedClassRooms = []  
    this.SpecificClassroom = false; 
    this.isDropdownOpen = false; 

    if (this.editLessonResource) {
      this.editLessonResource = false;
    }
    this.validationErrors = {};
  }

  onSpecificClassroomChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.SpecificClassroom = isChecked
  }

  toggleDropdown(event: MouseEvent) {
    event.stopPropagation(); // Prevent the click event from bubbling up
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  removeFromClasses(classID:number, event: MouseEvent){
    event.stopPropagation();
    this.AllSelectedClassRooms = this.AllSelectedClassRooms.filter(classroom => classroom.id !== classID); 
  }
  
  onClassChange(classroom: Classroom, event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked; 
    if (isChecked) {
      const exists = this.AllSelectedClassRooms.some(c => c.id === classroom.id);
      if (!exists) {
        this.AllSelectedClassRooms.push(classroom);
      }
    } else {
      const index = this.AllSelectedClassRooms.findIndex(c => c.id === classroom.id)
      if (index > -1) {
        this.AllSelectedClassRooms.splice(index, 1); 
      }
    } 
  }
  
  isClassSelected(classId: number): boolean {
    return this.AllSelectedClassRooms.some(c => c.id === classId);
  } 

  capitalizeField(field: keyof LessonResource): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.lessonResource) { 
      if (this.lessonResource.hasOwnProperty(key)) {
        const field = key as keyof LessonResource;
        if (!this.lessonResource[field]) {
          if (field == 'englishTitle' || field == 'arabicTitle' || field == 'lessonResourceTypeID') {
            this.validationErrors[field] = `*${this.capitalizeField( field )} is required`;
            isValid = false;
          }
        } else {
          if (field == 'englishTitle' || field == 'arabicTitle') {
            if (this.lessonResource.englishTitle.length > 100 || this.lessonResource.arabicTitle.length > 100) {
              this.validationErrors[field] = `*${this.capitalizeField( field )} cannot be longer than 100 characters`;
              isValid = false;
            }
          } else {
            this.validationErrors[field] = '';
          }
        }
      }
    } 
    return isValid;
  } 

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    
    if (file) {
      if (file.size > 25 * 1024 * 1024) {
        this.validationErrors['attachmentFile'] = 'The file size exceeds the maximum limit of 25 MB.';
        this.lessonResource.attachmentFile = null;
        return; 
      }
      else {
        this.lessonResource.attachmentFile = file; 
        this.lessonResource.attachmentLink = ''; 
        this.validationErrors['attachmentFile'] = ''; 

        const reader = new FileReader();
        reader.readAsDataURL(file);
      }
    }
    event.target.value = '';
  }

  onInputValueChange(event: { field: keyof LessonResource; value: any }) {
    const { field, value } = event;
    (this.lessonResource as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  handleAttachmentTypeToggle(type: 'file' | 'text') {
    if (this.selectedAttachmentType === type) {
      this.clearAttachmentType(); 
    } else {
      this.selectedAttachmentType = type; 
      this.lessonResource.attachmentLink = '';
      this.lessonResource.attachmentFile = null; 
    }
  }
  
  clearAttachmentType() {
    this.selectedAttachmentType = null;
    this.lessonResource.attachmentLink = '';
    this.lessonResource.attachmentFile = null; 
  }

  Save() {  
    this.lessonResource.lessonID = this.lessonId
 
    if(this.editLessonResource == true){
      this.lessonResource.classes = [];
      this.lessonResource.newClassRooms = [];

      if (this.lessonResource.classrooms && this.lessonResource.classrooms.length > 0) { 
        const existingIds = this.lessonResource.classrooms.map(c => c.id); 
        this.AllSelectedClassRooms.forEach(selected => {
          if (existingIds.includes(selected.id)) { 
            this.lessonResource.classes.push(selected.id);
          } else { 
            this.lessonResource.newClassRooms.push(selected.id);
          }
        });
      } else { 
        this.lessonResource.newClassRooms = this.AllSelectedClassRooms.map(c => c.id);
      }

    }else{
      if(this.AllSelectedClassRooms.length != 0){ 
        // this.AllSelectedClassRooms.forEach( element => {
        //   this.lessonResource.classes.push(element.id)
        // });

        this.lessonResource.classes = this.AllSelectedClassRooms.map(c => c.id);
      } else{
        this.lessonResource.classes = []
      }
    }
 
    if (this.isFormValid()) {
      this.isLoading = true;  
        
      if (this.editLessonResource == false) { 
        this.lessonResourceService.Add(this.lessonResource, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal();
            this.GetLessonResourceByLessonId();
            this.isLoading = false;
          },
          (error) => {
            this.isLoading = false;
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          }
        );
      } else {
        this.lessonResourceService.Edit(this.lessonResource, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal();
            this.GetLessonResourceByLessonId();
            this.isLoading = false;
          },
          (error) => {
            this.isLoading = false;
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

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: LessonResource[] = await firstValueFrom(
        this.lessonResourceService.GetByLessonId(this.lessonId, this.DomainName)
      );
      this.LessonResourceData = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.LessonResourceData = this.LessonResourceData.filter((t) => {
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
      this.LessonResourceData = [];
    }
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Lesson Resource?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.lessonResourceService.Delete(id, this.DomainName).subscribe((data: any) => {
          this.GetLessonResourceByLessonId();
        });
      }
    });
  }
}
