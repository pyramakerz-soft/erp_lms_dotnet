import { Component } from '@angular/core';
import { SearchComponent } from '../../../../Component/search/search.component';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { RegisterationFormParent } from '../../../../Models/Registration/registeration-form-parent';
import { RegisterationFormParentService } from '../../../../Services/Employee/Registration/registeration-form-parent.service';
import { School } from '../../../../Models/school';
import { AcademicYear } from '../../../../Models/LMS/academic-year';
import { AcadimicYearService } from '../../../../Services/Employee/LMS/academic-year.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { RegistrationFormStateService } from '../../../../Services/Employee/Registration/registration-form-state.service';
import { RegistrationFormState } from '../../../../Models/Registration/registration-form-state';
import { firstValueFrom } from 'rxjs';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-registration-confirmation',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent, TranslateModule],
  templateUrl: './registration-confirmation.component.html',
  styleUrl: './registration-confirmation.component.css'
})
export class RegistrationConfirmationComponent {
  keysArray: string[] = ['id', 'studentEnName', 'studentArName','phone','gradeName','academicYearName','schoolName','email'];
  key: string= "id";
  value: any = "";

  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  
  AllowEdit: boolean = false;
  AllowEditForOthers: boolean = false;
  path: string = ""

  registerationFormParentData: RegisterationFormParent[] = []
  registerationFormParentDataByYear: RegisterationFormParent[] = []
  registerationFormParentDataBySchool: RegisterationFormParent[] = []
  registerationFormParentDataByState: RegisterationFormParent[] = []
  SchoolData: School[] = []
  AcademicYearData: AcademicYear[] = []
  StateData: RegistrationFormState[] = []

  selectedYear = 0
  selectedSchool = 0
  selectedState = 0

  constructor(public account: AccountService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, 
        private menuService: MenuService, public activeRoute: ActivatedRoute, public router:Router, 
        public registerationFormParentServicea:RegisterationFormParentService, public yearService: AcadimicYearService, 
        public schoolService: SchoolService, public stateService: RegistrationFormStateService){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.getRegisterationFormParentData()
    this.getSchools()
    this.getYears()
    this.getState()

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: RegisterationFormParent[] = await firstValueFrom(
        this.registerationFormParentServicea.Get(this.DomainName)
      );
      this.registerationFormParentData = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.registerationFormParentData = this.registerationFormParentData.filter((t) => {
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
      this.registerationFormParentData = [];
    }
  }

  // getRegisterationFormParentData(){
  //   this.registerationFormParentServicea.Get(this.DomainName).subscribe(
  //     async (data) => {
  //       this.registerationFormParentData = await data;
  //     }
  //   )
  // }

  getRegisterationFormParentData(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.registerationFormParentServicea.Get(this.DomainName).subscribe(
        (data) => {
          console.log(data)
          this.registerationFormParentData = data;
          resolve();  // Resolve the promise when data is received
        },
        (error) => {
          reject(error);  // Reject the promise if there's an error
        }
      );
    });
  }

  
  getSchools(){
    this.schoolService.Get(this.DomainName).subscribe(
      (data) => {
        this.SchoolData = data;
      }
    )
  }

  getYears(){
    this.yearService.Get(this.DomainName).subscribe(
      (data) => {
        this.AcademicYearData = data;
      }
    )
  }

  getState(){
    this.stateService.Get(this.DomainName).subscribe(
      (data) => {
        this.StateData = data;
      }
    )
  }
  
  async Search() {

    this.registerationFormParentDataByYear = []
    this.registerationFormParentDataBySchool = []
    this.registerationFormParentDataByState = []

    await this.getRegisterationFormParentData()

    if (this.selectedSchool !== 0) {
      try {
        this.registerationFormParentDataBySchool = await firstValueFrom(
          this.registerationFormParentServicea.GetBySchoolId(this.selectedSchool, this.DomainName)
        );
      } catch (error) {
        console.error("Error fetching school data:", error);
      }
    }
  
    if (this.selectedYear !== 0) {
      try {
        
        this.registerationFormParentDataByYear = await firstValueFrom(
          this.registerationFormParentServicea.GetByYearId(this.selectedYear, this.DomainName)
        );
      } catch (error) {
        console.error("Error fetching year data:", error);
      }
    }
    
    if (this.selectedState !== 0) {
      try {
        
        this.registerationFormParentDataByState = await firstValueFrom(
          this.registerationFormParentServicea.GetByStateId(this.selectedState, this.DomainName)
        );
      } catch (error) {
        console.error("Error fetching year data:", error);
      }
    }
    
    let filteredData = [...this.registerationFormParentData];

    if (this.selectedSchool !== 0) {
      filteredData = filteredData.filter(item =>
        this.registerationFormParentDataBySchool.some(schoolItem => schoolItem.id === item.id)
      );
    }

    if (this.selectedYear !== 0) {
      filteredData = filteredData.filter(item =>
        this.registerationFormParentDataByYear.some(yearItem => yearItem.id === item.id)
      );

    }

    if (this.selectedState !== 0) {
      filteredData = filteredData.filter(item =>
        this.registerationFormParentDataByState.some(stateItem => stateItem.id === item.id)
      );
    }

    if((this.selectedSchool != 0 && this.registerationFormParentDataBySchool.length == 0) ||
    (this.selectedYear != 0 && this.registerationFormParentDataByYear.length == 0) ||
    (this.selectedState != 0 && this.registerationFormParentDataByState.length == 0)){
      filteredData = []
    }
    
    this.registerationFormParentData = filteredData;
  }

  ResetFilter(){
    this.selectedYear = 0
    this.selectedSchool = 0
    this.selectedState = 0

    this.registerationFormParentDataByYear = []
    this.registerationFormParentDataBySchool = []
    this.registerationFormParentDataByState = []

    this.getRegisterationFormParentData()
  }

  moveToRegistrationConfirmationTestDetails(Id:number){
    this.router.navigateByUrl('Employee/Registration Confirmation Test/' + Id);
  } 
  
  moveToRegistrationFormSubmission(Id:number){
    this.router.navigateByUrl('Employee/Registration Confirmation/' + Id);
  } 
}
