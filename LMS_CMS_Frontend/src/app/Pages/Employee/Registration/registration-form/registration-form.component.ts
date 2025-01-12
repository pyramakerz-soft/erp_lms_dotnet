import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegistrationFormService } from '../../../../Services/Employee/Registration/registration-form.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { RegistrationForm } from '../../../../Models/Registration/registration-form';

@Component({
  selector: 'app-registration-form',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './registration-form.component.html',
  styleUrl: './registration-form.component.css'
})
export class RegistrationFormComponent {
  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  RegistrationFormData:RegistrationForm = new RegistrationForm()
  
  currentCategory = 2;

  constructor(public account: AccountService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService,
    public activeRoute: ActivatedRoute, public registrationFormService: RegistrationFormService, public router:Router){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.getRegistrationFormData()
  }

  getRegistrationFormData(){
    this.registrationFormService.GetById(1, this.DomainName).subscribe(
      (data) => {
        this.RegistrationFormData = data
        console.log(this.RegistrationFormData)
      }
    )
  }
}
