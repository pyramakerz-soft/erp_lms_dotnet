import { Component } from '@angular/core';
import { RegisterationFormParent } from '../../../../Models/Registration/registeration-form-parent';
import { TokenData } from '../../../../Models/token-data';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { RegisterationFormParentService } from '../../../../Services/Employee/Registration/registeration-form-parent.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { Classroom } from '../../../../Models/LMS/classroom';
import { Grade } from '../../../../Models/LMS/grade';
import { School } from '../../../../Models/school';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { SearchComponent } from '../../../../Component/search/search.component';
import { firstValueFrom } from 'rxjs';
import { TranslateModule } from '@ngx-translate/core';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-classrooms-accommodation',
  standalone: true,
  imports: [CommonModule, FormsModule, SearchComponent, TranslateModule],
  templateUrl: './classrooms-accommodation.component.html',
  styleUrl: './classrooms-accommodation.component.css',
})
export class ClassroomsAccommodationComponent {
  User_Data_After_Login: TokenData = new TokenData(
    '',
    0,
    0,
    0,
    0,
    '',
    '',
    '',
    '',
    ''
  );

  DomainName: string = '';
  UserID: number = 0;
  path: string = '';

  ClassroomId: number = 0;
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  classrooms: Classroom[] = [];

  Data: RegisterationFormParent[] = [];
  OriginalData: RegisterationFormParent[] = [];

  isModalVisible: boolean = false;
  RpId: number = 0;

  Grades: Grade[] = [];
  Schools: School[] = [];
  Years: AcademicYear[] = [];

  SelectedSchoolId: number = 0;
  SelectedYearId: number = 0;
  SelectedGradeId: number = 0;
  IsSearch: boolean = false;

  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'studentEnName', 'studentArName', 'gradeName'];
  isLoading = false;

  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public registerationFormParentService: RegisterationFormParentService,
    public classroomServ: ClassroomService,
    public SchoolServ: SchoolService,
    public GradeServ: GradeService,
    public YearServ: AcadimicYearService
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

    this.getAllGrades();
    this.getAllSchools();
    this.getAllYears();
    this.GetAllData();
  }
  GetAllData() {
    this.Data = []
    this.OriginalData = []
    this.registerationFormParentService
      .GetAll(this.DomainName)
      .subscribe((data) => {
        this.Data = [];
        this.Data = data;
        this.OriginalData = data;
      });
  }

  Save() {
    this.isLoading = true
    this.classroomServ
      .AddStudentToClass(this.RpId, this.ClassroomId, this.DomainName)
      .subscribe((d) => {
        this.GetAllData();
        this.closeModal();
        this.isLoading = false
      }, (error) => {
        this.isLoading = false
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Try Again Later!',
          confirmButtonText: 'Okay',
          customClass: { confirmButton: 'secondaryBg' },
        });
      });
  }
  Create(id: number) {
    this.openModal();
    this.RpId = id;
    this.GetClassrooms(id);
  }

  GetClassrooms(id: number) {
    this.classroomServ
      .GetByRegistrationFormParentID(id, this.DomainName)
      .subscribe((data) => {
        this.classrooms = data;
      });
  }
  openModal() {
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  getAllSchools() {
    this.SchoolServ.Get(this.DomainName).subscribe((data) => {
      this.Schools = data;
    });
  }
  getAllGrades() {
    this.GradeServ.Get(this.DomainName).subscribe((data) => {
      this.Grades = data;
    });
  }
  getAllYears() {
    this.YearServ.Get(this.DomainName).subscribe((data) => {
      this.Years = data;
    });
  }

  Search() {
    this.IsSearch = true;
    this.Data = []
    this.Data = this.OriginalData.filter((item: any) => {
      const schoolMatch = this.SelectedSchoolId == 0 || item.schoolID == this.SelectedSchoolId;
      const yearMatch = this.SelectedYearId == 0 || item.yearID == this.SelectedYearId;
      const gradeMatch = this.SelectedGradeId == 0 || item.gradeID == this.SelectedGradeId;
      return schoolMatch && yearMatch && gradeMatch;
    });

  }

  ResetFilter() {
    this.IsSearch = false;
    this.SelectedGradeId = 0;
    this.SelectedSchoolId = 0;
    this.SelectedYearId = 0;
    this.Data = this.OriginalData
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: RegisterationFormParent[] = await firstValueFrom(
        this.registerationFormParentService
          .GetAll(this.DomainName)
      );
      this.Data = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.Data = this.Data.filter((t) => {
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
      this.Data = [];
    }
  }
}
