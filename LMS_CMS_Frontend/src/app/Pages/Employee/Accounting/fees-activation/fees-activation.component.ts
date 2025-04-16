import { Component } from '@angular/core';
import { StudentAcademicYear } from '../../../../Models/LMS/student-academic-year';
import { FeesActivation } from '../../../../Models/Accounting/fees-activation';
import { StudentAcademicYearService } from '../../../../Services/Employee/LMS/student-academic-year.service';
import { Router, ActivatedRoute } from '@angular/router';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { AccountingTreeChartService } from '../../../../Services/Employee/Accounting/accounting-tree-chart.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import Swal from 'sweetalert2';
import { FeesActivationService } from '../../../../Services/Employee/Accounting/fees-activation.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { School } from '../../../../Models/school';
import { Section } from '../../../../Models/LMS/section';
import { Grade } from '../../../../Models/LMS/grade';
import { Classroom } from '../../../../Models/LMS/classroom';
import { Student } from '../../../../Models/student';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { SectionService } from '../../../../Services/Employee/LMS/section.service';
import { GradeService } from '../../../../Services/Employee/LMS/grade.service';
import { ClassroomService } from '../../../../Services/Employee/LMS/classroom.service';
import { TuitionFeesType } from '../../../../Models/Accounting/tuition-fees-type';
import { TuitionFeesTypeService } from '../../../../Services/Employee/Accounting/tuition-fees-type.service';
import { TuitionDiscountTypes } from '../../../../Models/Accounting/tuition-discount-types';
import { TuitionDiscountTypeService } from '../../../../Services/Employee/Accounting/tuition-discount-type.service';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { FeesActivationAddPut } from '../../../../Models/Accounting/fees-activation-add-put';
import { firstValueFrom, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-fees-activation',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './fees-activation.component.html',
  styleUrl: './fees-activation.component.css'
})
export class FeesActivationComponent {

  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableDataStudentOriginal: FeesActivation[] = [];
  TableData: FeesActivation[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['feeActivationID', 'amount', 'discount', "net", "date", "feeTypeName", "feeDiscountTypeName", "studentName", "academicYearName"];

  Fees: FeesActivationAddPut = new FeesActivationAddPut()
  FeesForEdit: FeesActivationAddPut = new FeesActivationAddPut()
  FeesForAdd: FeesActivationAddPut[] = []


  SchoolId: number = 0;
  SectionId: number = 0;
  GradeId: number = 0;
  ClassRoomId: number = 0;
  StudentId: number = 0;

  Schools: School[] = []
  Sections: Section[] = [];
  Grades: Grade[] = [];
  ClassRooms: Classroom[] = [];

  StudentsOriginal: StudentAcademicYear[] = [];
  Students: StudentAcademicYear[] = [];

  FeesTypes: TuitionFeesType[] = []
  FeesDiscountType: TuitionDiscountTypes[] = []
  academicYear: AcademicYear[] = []
  DiscountPercentage: number = 0

  IsSearch: boolean = false

  IsOpenStudentandClassroom: boolean = false

  IsEdit = false;
  editingRowId: any = 0;
  IsOpenTable:boolean=false;

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    public accountServ: AccountingTreeChartService,
    public StudentAcademicYearServ: StudentAcademicYearService,
    public EditDeleteServ: DeleteEditPermissionService,
    public feesActivationServ: FeesActivationService,
    public SchoolServ: SchoolService,
    public SectionServ: SectionService,
    public GradeServ: GradeService,
    public ClassRoomServ: ClassroomService,
    public TuitionFeesTypeServ: TuitionFeesTypeService,
    public FeesDiscountTypeServ: TuitionDiscountTypeService,
    public AcademicYearServ: AcadimicYearService
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

    this.GetAllStudentData();
    this.GetAllSchools();
    this.GetAllFeesData();
    this.GetAllTuitionFeesType();
    this.GetAllDiscountType()
  }

  GetAllStudentData() {
    this.StudentAcademicYearServ.Get(this.DomainName).subscribe((d) => {
      this.Students = d
      this.StudentsOriginal = d
    })
  }

  GetAllFeesData() {
    this.feesActivationServ.Get(this.DomainName).subscribe((d) => {
      this.TableDataStudentOriginal = d;
      // this.TableData = d
    })
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Fees Activation?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.feesActivationServ.Delete(id, this.DomainName).subscribe((D) => {
          this.GetAllFeesData();
          this.GetStudents();
        })
      }
    });
  }

  validateNumber(event: any, field: keyof FeesActivationAddPut): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.Fees[field] === 'string') {
        this.Fees[field] = '' as never;  
      }
    }
  }
  

  validateNumberForDiscount(event: any): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      this.DiscountPercentage = 0
    }
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

  async onSearchEvent(event: { key: string; value: any }) {
    this.IsOpenTable=true
    this.key = event.key;
    this.value = event.value;

    try {
      await Promise.all([
        new Promise<void>((resolve) => {
          this.GetAllFeesData();
          resolve();
        }),
        this.GetStudents()
      ]);

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
      console.error("Error fetching data:", error);
      this.TableData = [];
    }
  }

  GetAllSchools() {
    this.SchoolServ.Get(this.DomainName).subscribe((d) => {
      this.Schools = d
    })
  }

  GetAllSectionsBySchoolID() {
    this.SectionServ.GetBySchoolId(this.SchoolId, this.DomainName).subscribe((d) => {
      this.Sections = d
    })
  }

  GetAllGradeBySectionId() {
    this.GradeServ.GetBySectionId(this.SectionId, this.DomainName).subscribe((d) => {
      this.Grades = d
    })
  }

  GetAllClassRoomByGradeID() {
    this.ClassRoomServ.GetByGradeId(this.GradeId, this.DomainName).subscribe((d) => {
      this.ClassRooms = d
    })
  }


  SchoolIsChanged(event: Event) {
    this.SchoolId = Number((event.target as HTMLSelectElement).value);
    this.SectionId = 0;
    this.GradeId = 0;
    this.ClassRoomId = 0;
    this.StudentId = 0;
    this.Sections = [];
    this.Grades = [];
    this.ClassRooms = [];
    this.Students = [];
    this.GetAllSectionsBySchoolID();
    this.GetStudents();
    this.GetAllAcademicYear();
    this.IsOpenStudentandClassroom = false;
    this.IsOpenTable=false
    this.key=""
    this.value=""
  }

  SectionIsChanged(event: Event) {
    this.SectionId = Number((event.target as HTMLSelectElement).value);
    this.GradeId = 0;
    this.ClassRoomId = 0;
    this.StudentId = 0;
    this.Grades = [];
    this.ClassRooms = [];
    this.Students = [];
    this.GetAllGradeBySectionId();
    this.GetStudents();
  }

  GradeIsChanged(event: Event) {
    this.GradeId = Number((event.target as HTMLSelectElement).value);
    this.ClassRoomId = 0;
    this.StudentId = 0;
    this.ClassRooms = [];
    this.Students = [];
    this.GetAllClassRoomByGradeID();
    this.GetStudents();
    this.IsOpenStudentandClassroom = true
  }

  ClassRoomIsChanged(event: Event) {
    this.ClassRoomId = Number((event.target as HTMLSelectElement).value);
    this.StudentId = 0;
    this.GetStudents();
  }

  StudentChanged(event: Event) {
    this.StudentId = Number((event.target as HTMLSelectElement).value);
    this.GetStudents();
  }

  async GetStudents() {
    this.TableDataStudentOriginal = await firstValueFrom(
      this.feesActivationServ.Get(this.DomainName)
    );
    this.TableData = this.TableDataStudentOriginal || [];

    if (this.StudentId == 0) {
      this.Students = [];
      this.Students = this.StudentsOriginal.filter((item: StudentAcademicYear) => {
        return (
          (this.SchoolId == 0 || item.schoolID == this.SchoolId) &&
          (this.SectionId == 0 || item.sectionId == this.SectionId) &&
          (this.GradeId == 0 || item.gradeID == this.GradeId) &&
          (this.ClassRoomId == 0 || item.classID == this.ClassRoomId)
        );
      });

      this.TableData = [];
      this.TableData = this.TableDataStudentOriginal.filter((item: FeesActivation) => {
        return (
          (this.SchoolId == 0 || item.schoolID == this.SchoolId) &&
          (this.SectionId == 0 || item.sectionId == this.SectionId) &&
          (this.GradeId == 0 || item.gradeID == this.GradeId) &&
          (this.ClassRoomId == 0 || item.classID == this.ClassRoomId)
        );
      });
    }
    else {
      this.TableData = [];
      this.TableData = this.TableDataStudentOriginal.filter(s => s.studentID == this.StudentId);
    }

  }

  Search() {
    this.IsOpenTable=false
    if (this.SchoolId == 0 || this.SectionId == 0 || this.GradeId == 0) {
      if (this.SchoolId == 0) {
        Swal.fire({
          icon: 'warning',
          title: 'Warning!',
          text: 'School Is Required',
          confirmButtonColor: '#FF7519',
        });
      }
      else if (this.SchoolId == 0) {
        Swal.fire({
          icon: 'warning',
          title: 'Warning!',
          text: 'School Is Required',
          confirmButtonColor: '#FF7519',
        });
      }
      else if (this.SectionId == 0) {
        Swal.fire({
          icon: 'warning',
          title: 'Warning!',
          text: 'Section Is Required',
          confirmButtonColor: '#FF7519',
        });
      }
      else if (this.GradeId == 0) {
        Swal.fire({
          icon: 'warning',
          title: 'Warning!',
          text: 'Grade Is Required',
          confirmButtonColor: '#FF7519',
        });
      }
    }
    else {
      this.IsSearch = true
      this.GetAllAcademicYear()
    }
  }

  GetAllTuitionFeesType() {
    this.TuitionFeesTypeServ.Get(this.DomainName).subscribe((d) => {
      this.FeesTypes = d
    })
  }

  GetAllDiscountType() {
    this.FeesDiscountTypeServ.Get(this.DomainName).subscribe((d) => {
      this.FeesDiscountType = d
    })
  }

  GetAllAcademicYear() {
    this.AcademicYearServ.GetBySchoolId(this.SchoolId, this.DomainName).subscribe((d) => {
      this.academicYear = d
    })
  }

  async Activate() {
    this.FeesForAdd = [];
    this.Students.forEach(stu => {
      var fee: FeesActivationAddPut = new FeesActivationAddPut();
      fee.academicYearId = this.Fees.academicYearId;
      fee.amount = this.Fees.amount;
      fee.date = this.Fees.date;
      fee.discount = this.Fees.discount;
      fee.feeDiscountTypeID = this.Fees.feeDiscountTypeID;
      fee.feeTypeID = this.Fees.feeTypeID;
      fee.net = this.Fees.net;
      fee.studentID = stu.studentID;

      this.FeesForAdd.push(fee);
    });
    try {
      await lastValueFrom(this.feesActivationServ.Add(this.FeesForAdd, this.DomainName));
      this.GetStudents();
    } catch (error) {
      console.error("Error while activating fees:", error);
    }
  }

  CalculateDiscountFromPercentage() {
    if (this.DiscountPercentage >= 0) {
      this.Fees.discount = (this.Fees.amount * this.DiscountPercentage) / 100;
      this.CalculateNet();
    }
  }

  CalculatePercentageFromDiscount() {
    this.DiscountPercentage = 0
    if (this.Fees.amount > 0) {
      this.DiscountPercentage = (this.Fees.discount / this.Fees.amount) * 100;
      this.CalculateNet();
    }
  }

  CalculateNet() {
    this.Fees.net = this.Fees.amount - this.Fees.discount;
  }

  CalculateNetForEdit(row: FeesActivation) {
    row.net = row.amount - row.discount;
  }

  Edit(id: number) {
    this.IsEdit = true
    this.editingRowId = id
  }

  Save(row: FeesActivation) {
    this.editingRowId = null;
    var fee: FeesActivationAddPut = new FeesActivationAddPut()
    fee.academicYearId = row.academicYearId;
    fee.amount = row.amount;
    fee.date = row.date;
    fee.discount = row.discount;
    fee.feeDiscountTypeID = row.feeDiscountTypeID;
    fee.feeTypeID = row.feeTypeID;
    fee.net = row.net;
    fee.studentID = row.studentID;
    fee.feeActivationID = row.feeActivationID;
    this.feesActivationServ.Edit(fee, this.DomainName).subscribe((d) => {
      this.GetAllFeesData();
      this.GetStudents();
    })
  }
}
