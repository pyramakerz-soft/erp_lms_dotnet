import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../../Models/login';
import { TokenData } from '../../../Models/token-data';
import { AccountService } from '../../../Services/account.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ParentAdd } from '../../../Models/parent-add';
import { ParentService } from '../../../Services/parent.service';
import { ApiService } from '../../../Services/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {

  userInfo: Login = new Login("", "", "", "");
  User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  employeeImage = 'Images/employee.png';
  studentImage = 'Images/student.png';
  parentImage = 'Images/parent.png';

  isEmployeeHovered = false;
  isStudentHovered = false;
  isParentHovered = false;

  DomainName: string = '';

  userNameError: string = "";
  passwordError: string = "";
  ConfirmPasswordError: string = "";
  somthingError: string = "";

  token1 = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  token2 = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  allTokens: { id: number, key: string; KeyInLocal: string; value: string; UserType: string }[] = [];
  User_Data_After_Login2 = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  ConfirmPassword: string = ""

  validationErrors: { [key in keyof ParentAdd]?: string } = {};

  parentInfo: ParentAdd = new ParentAdd()
  isLoading = false; // Initialize loading state

  constructor(private router: Router, public accountService: AccountService, public ParentServ: ParentService , public ApiServ: ApiService) { }
  ngOnInit() {
    this.DomainName = this.ApiServ.GetHeader();
    this.selectType("parent")
  }

  onUserNameChange() {
    this.userNameError = ""
    this.somthingError = ""
  }

  onPasswordChange() {
    this.passwordError = ""
    this.somthingError = ""
  }

  onConfirmPasswordChange() {
    this.ConfirmPasswordError = ""
    this.somthingError = ""
  }

  SignUp() {
    if (this.isFormValid()) {
      this.isLoading = true; // Start loading
      this.ParentServ.AddParent(this.parentInfo, this.DomainName).subscribe(() => {
        this.isLoading = false; // Stop loading
        this.router.navigateByUrl(""); // Navigate after success
      }, (error) => {
        this.isLoading = false; // Stop loading on error
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Try Again Later!',
          confirmButtonText: 'Okay',
          customClass: {
            confirmButton: 'secondaryBg'
          }
        });
      });
    }
  }

  login() {
    this.router.navigateByUrl("")
  }

  selectType(type: string) {
    this.userInfo.type = type;
    if (this.userInfo.type == "employee") {
      this.router.navigateByUrl("")
    }
    else if (this.userInfo.type == "student") {
      this.router.navigateByUrl("")
    }
    else if (this.userInfo.type == "parent") {
      this.isEmployeeHovered = false;
      this.isStudentHovered = false;
      this.isParentHovered = true;
    }

  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.parentInfo) {
      if (this.parentInfo.hasOwnProperty(key)) {
        const field = key as keyof ParentAdd;
        if (!this.parentInfo[field]) {
          if (field == "user_Name" || field == "en_name" || field == "ar_name" || field == "password" || field == "email") {
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        }
      }
    }
    const emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
    if (this.parentInfo.email && !emailPattern.test(this.parentInfo.email)) {
      this.validationErrors['email'] = 'Email is not valid';
      isValid = false;
    }
    if (this.parentInfo.password.length < 6) {
      this.validationErrors['password'] = 'Password must be between 6 and 100 characters ';
      isValid = false;
    }
    if (this.parentInfo.password != this.ConfirmPassword) {
      this.validationErrors['password'] = 'Password And Confirm Password not The Same';
      isValid = false;
    }
    return isValid;
  }

  capitalizeField(field: keyof ParentAdd): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  onInputValueChange(event: { field: keyof ParentAdd, value: any }) {
    const { field, value } = event;
    (this.parentInfo as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    } else {
      this.validationErrors[field] = `*${this.capitalizeField(field)} is required`;
    }
  }
}
