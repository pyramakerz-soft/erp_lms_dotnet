import { Component, ElementRef, ViewChild } from '@angular/core';
import { Lesson } from '../../../../Models/LMS/lesson';
import { LessonService } from '../../../../Services/Employee/LMS/lesson.service';
import { Router, ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { SearchComponent } from '../../../../Component/search/search.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { SubjectService } from '../../../../Services/Employee/LMS/subject.service';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { SemesterService } from '../../../../Services/Employee/LMS/semester.service';
import { Subject } from '../../../../Models/LMS/subject';
import { Semester } from '../../../../Models/LMS/semester';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { School } from '../../../../Models/school';
import { Grade } from '../../../../Models/LMS/grade';
import { SemesterWorkingDays } from '../../../../Models/LMS/semester-working-days';
import { SemesterWorkingWeekService } from '../../../../Services/Employee/LMS/semester-working-week.service';  
import { QuillModule } from 'ngx-quill';
import { Tag } from '../../../../Models/LMS/tag';
import { SemesterWorkingWeek } from '../../../../Models/LMS/semester-working-week';

@Component({
  selector: 'app-lesson',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent, QuillModule],
  templateUrl: './lesson.component.html',
  styleUrl: './lesson.component.css'
})
export class LessonComponent {
  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: Lesson[] = [];

  DomainName: string = '';
  UserID: number = 0;
  
  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'englishTitle' ,'arabicTitle', 'order', 'subjectEnglishName', 'subjectArabicName'];

  lesson: Lesson = new Lesson();

  validationErrors: { [key in keyof Lesson]?: string } = {};
  isLoading = false;
  editLesson = false;
  showTable = false;

  SchoolId: number = 0
  SchoolModalId: number = 0
  AcademicYearId: number = 0
  AcademicYearModalId: number = 0
  GradeId: number = 0
  GradeModalId: number = 0
  SubjectId: number = 0
  SemesterId: number = 0
  SemesterModalId: number = 0
  Schools: School[] = []
  SchoolsModal: School[] = []
  AcademicYears: AcademicYear[] = []
  AcademicYearsModal: AcademicYear[] = []
  Grades: Grade[] = []
  GradesModal: Grade[] = []
  Subjects: Subject[] = []
  SubjectsModal: Subject[] = [] 
  Semesters: Semester[] = []
  SemestersModal: Semester[] = []
  WeeksModal: SemesterWorkingDays[] = []

  @ViewChild('quillEditor') quillEditor!: ElementRef;
  editorModules = {
    toolbar: [
      ['bold', 'italic', 'underline', 'strike'],
      ['blockquote', 'code-block'],
      [{ 'header': 1 }, { 'header': 2 }],
      [{ 'list': 'ordered'}, { 'list': 'bullet' }],
      [{ 'script': 'sub'}, { 'script': 'super' }],
      [{ 'indent': '-1'}, { 'indent': '+1' }],
      [{ 'direction': 'rtl' }],
      [{ 'size': ['small', false, 'large', 'huge'] }],
      [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
      [{ 'color': [] }, { 'background': [] }],
      [{ 'font': [] }],
      [{ 'align': [] }],
      ['link', 'image', 'video'],
      ['clean']
    ]
  };

  inputValue: string = '';
  Tags: string[] = [];
  InsertedTags: string[] = [];
  ExistingTags: number[] = [];
  
  SelectedLessonImportFrom = 0
  SelectedWeekImportTo = 0
  LessonsImportedFrom: Lesson[] = [];
  SubjectModalId: number = 0
  
  SchoolModalImportToId: number = 0 
  AcademicYearModalImportToId: number = 0
  AcademicYearsImportToModal: AcademicYear[] = []
  GradeModalImportToId: number = 0
  GradesImportToModal: Grade[] = []
  SemesterModalImportToId: number = 0
  SemestersImportToModal: Semester[] = []
  SubjectModalImportToId: number = 0
  SubjectsImportToModal: Subject[] = []
  WeeksImportToModal: SemesterWorkingWeek[] = [] 

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public lessonService: LessonService,
    public SchoolServ: SchoolService,
    public GradeServ: GradeService,
    public SubjectServ: SubjectService,
    public SemesterServ: SemesterService,
    public SemesterWorkingWeekServ: SemesterWorkingWeekService,
    public acadimicYearService: AcadimicYearService
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

    this.getSchool()
  } 

  getSchool() {
    this.Schools = []
    this.SchoolServ.Get(this.DomainName).subscribe(
      data => {
        this.Schools = data
      }
    )
  }

  getSchoolModal() {
    this.SchoolsModal = []
    this.SchoolServ.Get(this.DomainName).subscribe(
      data => {
        this.SchoolsModal = data
      }
    )
  }

  onSchoolChange(event: Event) {
    this.AcademicYearId = 0
    this.GradeId = 0
    this.SemesterId = 0
    this.SubjectId = 0
    this.AcademicYears = []
    this.Grades = []
    this.Subjects = []
    this.Semesters = []
    this.showTable = false

    this.TableData = []  

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.SchoolId = Number(selectedValue)
    if (this.SchoolId) {
      this.GetYearData();
      this.GetGradeData();
    }
  }

  onSchoolModalChange(event: Event) {
    this.AcademicYearModalId = 0
    this.GradeModalId = 0
    this.SemesterModalId = 0
    this.lesson.subjectID = 0
    this.AcademicYearsModal = []
    this.GradesModal = []
    this.SubjectsModal = []
    this.SemestersModal = [] 
    this.WeeksModal = [] 
    this.lesson.semesterWorkingWeekID = 0

    this.LessonsImportedFrom = [] 
    this.SubjectModalId = 0
    this.SelectedLessonImportFrom = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.SchoolModalId = Number(selectedValue)
    if (this.SchoolModalId) {
      this.GetYearModalData();
      this.GetGradeModalData();
    }
  }

  onSchoolModalImportToChange(event: Event) {
    this.AcademicYearModalImportToId = 0
    this.GradeModalImportToId = 0
    this.SemesterModalImportToId = 0
    this.SubjectModalImportToId = 0
    this.SelectedWeekImportTo = 0
    this.AcademicYearsImportToModal = []
    this.GradesImportToModal = []
    this.SubjectsImportToModal = []
    this.SemestersImportToModal = [] 
    this.WeeksImportToModal = [] 
      
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.SchoolModalImportToId = Number(selectedValue)
    if (this.SchoolModalImportToId) {
      this.GetYearModalImportToData();
      this.GetGradeModalImportToData();
    }
  }

  onGradeChange(event: Event) {  
    this.SubjectId = 0 
    this.Subjects = [] 
    this.showTable = false

    this.TableData = []  

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.GradeId = Number(selectedValue)
    if (this.GradeId) { 
      this.GetSubjectData();
    }
  }

  onGradeModalChange(event: Event) {  
    this.lesson.subjectID = 0
    this.SubjectsModal = []  

    this.LessonsImportedFrom = [] 
    this.SubjectModalId = 0
    this.SelectedLessonImportFrom = 0
  
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.GradeModalId = Number(selectedValue)
    if (this.GradeModalId) { 
      this.GetSubjectModalData();
    }
  }

  onGradeModalImportToChange(event: Event) {  
    this.SubjectModalImportToId = 0
    this.SubjectsImportToModal = []  
  
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.GradeModalImportToId = Number(selectedValue)
    if (this.GradeModalImportToId) { 
      this.GetSubjectModalImportToData();
    }
  }

  onYearChange(event: Event) {
    this.SemesterId = 0
    this.Semesters = []
    this.showTable = false

    this.TableData = [] 

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.AcademicYearId = Number(selectedValue)
    if (this.AcademicYearId) { 
      this.GetSemesterData();
    }
  }

  onYearModalChange(event: Event) {
    this.SemesterModalId = 0
    this.SemestersModal = [] 
    this.WeeksModal = [] 
    this.lesson.semesterWorkingWeekID = 0
    this.LessonsImportedFrom = [] 
    this.SelectedLessonImportFrom = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.AcademicYearModalId = Number(selectedValue)
    if (this.AcademicYearModalId) { 
      this.GetSemesterModalData();
    }
  }

  onYearModalImportToChange(event: Event) {
    this.SemesterModalImportToId = 0
    this.SemestersImportToModal = [] 
    this.WeeksImportToModal = [] 
    this.SelectedWeekImportTo = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.AcademicYearModalImportToId = Number(selectedValue)
    if (this.AcademicYearModalImportToId) { 
      this.GetSemesterModalImportToData();
    }
  }

  onSemesterAndSubjectModalChange() {
    this.SelectedLessonImportFrom = 0
    this.LessonsImportedFrom = []  
 
    if (this.SubjectModalId && this.SemesterModalId) { 
      this.LessonsImportedFrom = [];
      this.lessonService.GetBySubjectIDAndSemester(this.SemesterModalId, this.SubjectModalId, this.DomainName).subscribe((data) => {
        this.LessonsImportedFrom = data;
      });
    }
  }

  onSemesterModalChange(event: Event) {
    this.WeeksModal = [] 
    this.lesson.semesterWorkingWeekID = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.SemesterModalId = Number(selectedValue)
    if (this.SemesterModalId) { 
      this.GetWeekModalData();
    }
  }

  onSemesterModalImportToChange(event: Event) {
    this.WeeksImportToModal = [] 
    this.SelectedWeekImportTo = 0

    const selectedValue = (event.target as HTMLSelectElement).value;
    this.SemesterModalImportToId = Number(selectedValue)
    if (this.SemesterModalImportToId) { 
      this.GetWeekModalImportToData();
    }
  }

  onSubjectOrSemesterChange() { 
    this.showTable = false 
    this.TableData = [] 
  }   

  GetYearData() {
    this.AcademicYears = []
    this.acadimicYearService.GetBySchoolId(this.SchoolId, this.DomainName).subscribe((d) => {
      this.AcademicYears = d
    })
  }

  GetYearModalData() {
    this.AcademicYearsModal = []
    this.acadimicYearService.GetBySchoolId(this.SchoolModalId, this.DomainName).subscribe((d) => {
      this.AcademicYearsModal = d
    })
  }

  GetYearModalImportToData() {
    this.AcademicYearsImportToModal = []
    this.acadimicYearService.GetBySchoolId(this.SchoolModalImportToId, this.DomainName).subscribe((d) => {
      this.AcademicYearsImportToModal = d
    })
  }

  GetGradeModalData() {
    this.GradesModal = []
    this.GradeServ.GetBySchoolId(this.SchoolModalId, this.DomainName).subscribe((d) => {
      this.GradesModal = d
    })
  }

  GetGradeModalImportToData() {
    this.GradesImportToModal = []
    this.GradeServ.GetBySchoolId(this.SchoolModalImportToId, this.DomainName).subscribe((d) => {
      this.GradesImportToModal = d
    })
  }

  GetGradeData() {
    this.Grades = []
    this.GradeServ.GetBySchoolId(this.SchoolId, this.DomainName).subscribe((d) => {
      this.Grades = d
    })
  }
  
  GetSubjectModalData() {
    this.SubjectsModal = []
    this.SubjectServ.GetByGradeId(this.GradeModalId, this.DomainName).subscribe((d) => {
      this.SubjectsModal = d
    })
  }
  
  GetSubjectData() {
    this.Subjects = []
    this.SubjectServ.GetByGradeId(this.GradeId, this.DomainName).subscribe((d) => {
      this.Subjects = d
    })
  }
  
  GetSubjectModalImportToData() {
    this.SubjectsImportToModal = []
    this.SubjectServ.GetByGradeId(this.GradeModalImportToId, this.DomainName).subscribe((d) => {
      this.SubjectsImportToModal = d
    })
  }
  
  GetSemesterModalData() {
    this.SemestersModal = []
    this.SemesterServ.GetByAcademicYearId(this.AcademicYearModalId, this.DomainName).subscribe((d) => {
      this.SemestersModal = d
    })
  }
  
  GetSemesterData() {
    this.Semesters = []
    this.SemesterServ.GetByAcademicYearId(this.AcademicYearId, this.DomainName).subscribe((d) => {
      this.Semesters = d
    })
  }
  
  GetSemesterModalImportToData() {
    this.SemestersImportToModal = []
    this.SemesterServ.GetByAcademicYearId(this.AcademicYearModalImportToId, this.DomainName).subscribe((d) => {
      this.SemestersImportToModal = d
    })
  }
  
  GetWeekModalData() {
    this.WeeksModal = []
    this.SemesterWorkingWeekServ.GetBySemesterID(this.SemesterModalId, this.DomainName).subscribe((d) => {
      this.WeeksModal = d
    })
  }
  
  GetWeekModalImportToData() {
    this.WeeksImportToModal = []
    this.SemesterWorkingWeekServ.GetBySemesterID(this.SemesterModalImportToId, this.DomainName).subscribe((d) => {
      this.WeeksImportToModal = d
    })
  }
  
  GetAllData() {
    this.TableData = [];
    this.lessonService.GetBySubjectIDAndSemester(this.SemesterId, this.SubjectId, this.DomainName).subscribe((data) => {
      this.TableData = data;
    });
  }
  
  GetLessonById(Id: number) {
    this.lessonService.GetByID(Id, this.DomainName).subscribe((data) => {
      this.lesson = data;  
      this.lesson.tags.forEach((element: Tag) => {
        this.Tags.push(element.name)
        this.ExistingTags.push(element.id)
      });

      this.SchoolModalId = this.lesson.schoolID
      this.GradeModalId = this.lesson.gradeID
      this.AcademicYearModalId = this.lesson.academicYearID
      this.SemesterModalId = this.lesson.semesterID 

      this.GetGradeModalData()
      this.GetSubjectModalData()
      this.GetYearModalData()
      this.GetSemesterModalData()
      this.GetWeekModalData()
    });
  }

  Apply(){
    this.showTable = true
    this.GetAllData()
  }

  openModal(lessonId?: number) {
    if (lessonId) {
      this.editLesson = true;
      this.GetLessonById(lessonId);
    } 

    this.getSchoolModal()

    document.getElementById('Add_Modal')?.classList.remove('hidden');
    document.getElementById('Add_Modal')?.classList.add('flex');
  }

  closeModal() {
    document.getElementById('Add_Modal')?.classList.remove('flex');
    document.getElementById('Add_Modal')?.classList.add('hidden');

    this.lesson = new Lesson(); 

    this.AcademicYearModalId = 0
    this.SchoolModalId = 0
    this.GradeModalId = 0
    this.SemesterModalId = 0 
    this.AcademicYearsModal = []
    this.SchoolsModal = []
    this.GradesModal = []
    this.SubjectsModal = []
    this.SemestersModal = [] 
    this.WeeksModal = [] 
    this.inputValue = '';
    this.Tags = [];
    this.ExistingTags = [];
    this.InsertedTags = [];

    if (this.editLesson) {
      this.editLesson = false;
    }
    this.validationErrors = {};
  }

  ImportModal() { 
    this.getSchoolModal()

    document.getElementById('Import_Modal')?.classList.remove('hidden');
    document.getElementById('Import_Modal')?.classList.add('flex');
  }

  closeImportModal() {
    document.getElementById('Import_Modal')?.classList.remove('flex');
    document.getElementById('Import_Modal')?.classList.add('hidden'); 

    this.SelectedLessonImportFrom = 0
    this.SelectedWeekImportTo = 0
    this.SubjectModalId = 0
    this.LessonsImportedFrom = []
    this.AcademicYearModalId = 0
    this.SchoolModalId = 0
    this.GradeModalId = 0
    this.SemesterModalId = 0 
    this.AcademicYearsModal = []
    this.SchoolsModal = []
    this.GradesModal = []
    this.SubjectsModal = []
    this.SemestersModal = [] 
    this.WeeksModal = []   
    this.AcademicYearsImportToModal = []  
    this.SchoolModalImportToId = 0 
    this.AcademicYearModalImportToId = 0 
    this.GradeModalImportToId = 0 
    this.GradesImportToModal = []
    this.SemesterModalImportToId = 0 
    this.SemestersImportToModal = []
    this.SubjectModalImportToId = 0 
    this.SubjectsImportToModal = []
    this.WeeksImportToModal = [] 
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
 
  addTag() {
    if (this.inputValue.trim() !== '') {
      this.Tags.push(this.inputValue.trim());
      this.InsertedTags.push(this.inputValue.trim());
      this.inputValue = '';
    }
  }

  removeTag(index: number) {
    this.Tags.splice(index, 1);
    this.ExistingTags.splice(index, 1);
  }

  MoveToLessonActivity(lessonId:number) {
    this.router.navigateByUrl('Employee/Lesson Activity/'+ lessonId);
  }

  MoveToLessonResource(lessonId:number) {
    this.router.navigateByUrl('Employee/Lesson Resource/'+ lessonId);
  }

  capitalizeField(field: keyof Lesson): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.lesson) { 
      if (this.lesson.hasOwnProperty(key)) {
        const field = key as keyof Lesson;
        if (!this.lesson[field]) {
          if (field == 'englishTitle' || field == 'arabicTitle' || field == 'semesterWorkingWeekID' || field == 'details' || field == 'order' || field == 'subjectID') {
            this.validationErrors[field] = `*${this.capitalizeField( field )} is required`;
            isValid = false;
          }
        } else {
          if (field == 'englishTitle' || field == 'arabicTitle') {
            if (this.lesson.englishTitle.length > 100 || this.lesson.arabicTitle.length > 100) {
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

  onInputValueChange(event: { field: keyof Lesson; value: any }) {
    const { field, value } = event;
    (this.lesson as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  validateNumber(event: any, field: keyof Lesson): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.lesson[field] === 'string') {
        this.lesson[field] = '' as never;  
      }
    }
  }

  Save() {  
    if (this.isFormValid()) {
      this.isLoading = true; 

      this.lesson.tagNames = [] 
      this.lesson.tagIDs = [] 
      if(this.InsertedTags.length != 0){ 
        this.InsertedTags.forEach( element => {
          this.lesson.tagNames.push(element)
        });
      }
      if(this.ExistingTags.length != 0){ 
        this.ExistingTags.forEach( element => {
          this.lesson.tagIDs.push(element)
        });
      }
       
      if (this.editLesson == false) { 
        this.lessonService.Add(this.lesson, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal();
            this.GetAllData();
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
        this.lessonService.Edit(this.lesson, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal();
            this.GetAllData();
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

  Import() {  
    this.isLoading = true; 
    let lesson = new Lesson()
    lesson.subjectID = this.SubjectModalImportToId
    lesson.toSemesterWorkingWeekID = this.SelectedWeekImportTo
    lesson.fromLessonID = this.SelectedLessonImportFrom
    this.lessonService.ImportLessonFrom(lesson, this.DomainName).subscribe(
      (result: any) => {
        this.closeImportModal();
        this.GetAllData();
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
    )
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Lesson?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.lessonService.Delete(id, this.DomainName).subscribe((data: any) => {
          this.TableData = [];
          this.GetAllData();
        });
      }
    });
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Lesson[] = await firstValueFrom(
        this.lessonService.Get(this.DomainName)
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

}
