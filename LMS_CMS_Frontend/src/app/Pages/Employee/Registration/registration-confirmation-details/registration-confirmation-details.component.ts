import { Component } from '@angular/core';
import { RegistrationFormSubmissionService } from '../../../../Services/Employee/Registration/registration-form-submission.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { RegistrationFormSubmissionConfirmation } from '../../../../Models/Registration/registration-form-submission-confirmation';
import { CommonModule } from '@angular/common';
import { RegistrationFormService } from '../../../../Services/Employee/Registration/registration-form.service';
import { RegistrationForm } from '../../../../Models/Registration/registration-form';
import { RegistrationFormState } from '../../../../Models/Registration/registration-form-state';
import { RegistrationFormStateService } from '../../../../Services/Employee/Registration/registration-form-state.service';
import { FormsModule } from '@angular/forms';
import { RegisterationFormParentService } from '../../../../Services/Employee/Registration/registeration-form-parent.service';
import { RegisterationFormParent } from '../../../../Models/Registration/registeration-form-parent';
import Swal from 'sweetalert2';
import { TranslateModule } from '@ngx-translate/core';
import { LanguageService } from '../../../../Services/shared/language.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-registration-confirmation-details',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule],
  templateUrl: './registration-confirmation-details.component.html',
  styleUrl: './registration-confirmation-details.component.css'
})
export class RegistrationConfirmationDetailsComponent {
  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  
  AllowEdit: boolean = false;
  AllowEditForOthers: boolean = false;
  path: string = ""

  registrationFormSubmissionConfirmationData: RegistrationFormSubmissionConfirmation[] = []
  RegistrationFormData:RegistrationForm = new RegistrationForm()
  RegisterationFormParentData:RegisterationFormParent = new RegisterationFormParent()

  registrationParentID = 0

  selectedState = 0
  StateData: RegistrationFormState[] = []
  isRtl: boolean = false;
  subscription!: Subscription;
  
  constructor(public account: AccountService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, 
    private menuService: MenuService, public activeRoute: ActivatedRoute, public router:Router, public stateService: RegistrationFormStateService,
    public registrationFormSubmissionService: RegistrationFormSubmissionService, public registrationFormParentService: RegisterationFormParentService
    , public registrationFormService: RegistrationFormService, private languageService: LanguageService){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.registrationParentID = Number(this.activeRoute.snapshot.paramMap.get('Id'))

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.getRegistrationFormSubmissionConfirmationData()
    this.getRegistrationFormData()
    this.getState()
    this.getRegistrationFormParentById()

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
    this.subscription = this.languageService.language$.subscribe(direction => {
      this.isRtl = direction === 'rtl';
    });
    this.isRtl = document.documentElement.dir === 'rtl';
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  } 

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  getRegistrationFormData(){
    this.registrationFormService.GetById(1, this.DomainName).subscribe(
      (data) => {
        this.RegistrationFormData = data
        this.RegistrationFormData.categories.sort((a, b) => (a.orderInForm ? a.orderInForm : 0) - (b.orderInForm ? b.orderInForm : 0))
        this.RegistrationFormData.categories.forEach(element => {
          element.fields.sort((a, b) => a.orderInForm - b.orderInForm)
        });
      }
    )
  }

  getRegistrationFormSubmissionConfirmationData(){
    this.registrationFormSubmissionService.GetByRegistrationParentID(this.registrationParentID, this.DomainName).subscribe(
      (data) => {
        this.registrationFormSubmissionConfirmationData = data;
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

  getRegistrationFormParentById(){
    this.registrationFormParentService.GetById(this.registrationParentID, this.DomainName).subscribe(
      (data) => {
        this.RegisterationFormParentData = data;
        this.selectedState = this.RegisterationFormParentData.registerationFormStateID
      }
    )
  }

  moveToRegistrationConfirmation() {
    this.router.navigateByUrl(`Employee/Registration Confirmation`)
  }

  Submit(){
    this.registrationFormParentService.Edit(this.registrationParentID, this.selectedState, this.DomainName).subscribe(
      (data) => {
        Swal.fire({
          title: 'Saved Successfully',
          text: 'This Registration Confirmation status has been Sent Successfully',
          icon: 'success',
          confirmButtonText: 'OK',
        });
      }, 
      (error) => {
        Swal.fire({
          title: 'Error',
          text: 'Please Try in another time',
          icon: 'error',
          confirmButtonText: 'OK',
        });
      }
    )
  }
}
