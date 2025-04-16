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
import { jwtDecode } from 'jwt-decode';

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

  IsConfimPassEmpty = false

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
    this.IsConfimPassEmpty = false
  }

  SignUp() {
    if (this.isFormValid()) {
      this.isLoading = true; // Start loading
      this.ParentServ.AddParent(this.parentInfo, this.DomainName).subscribe(() => {
        this.isLoading = false; // Stop loading 
        let tologin:Login = new Login(this.parentInfo.user_Name, "", this.parentInfo.password, "parent");
        this.accountService.Login(tologin).subscribe(
          (d: any) => {
            localStorage.removeItem("GoToLogin");
            localStorage.setItem("GoToLogin", "false");
            localStorage.removeItem("current_token");
            let count = localStorage.getItem("count")
            this.getAllTokens();
  
            this.accountService.isAuthenticated = true;
            const token = JSON.parse(d).token;
            let add = true;
            let Counter=0;
            for (let i = 0; i < localStorage.length; i++) {
              const key = localStorage.key(i);
              const value = localStorage.getItem(key || '');
              if (key && value && key.includes('token') && key != "current_token"&&key != "token") {
                let decodedToken1: TokenData = jwtDecode(token);
                let decodedToken2: TokenData = jwtDecode(value);
                if (decodedToken1.user_Name === decodedToken2.user_Name && decodedToken1.type === decodedToken2.type)
                  add = false;
              }
            }
            
            localStorage.setItem("current_token", token);
  
            if (add == true) {
              if (count === null) { 
                localStorage.setItem("token 1", token);
  
              } else {
                let countNum = parseInt(count) + 1; 
                let T = localStorage.getItem("token " + countNum)
                if (T != null) {
                  let i = countNum + 1;
                  let Continue = true;
                  while (Continue) {
                    let T2 = localStorage.getItem("token " + i);
                    if (T2 == null) {
                      localStorage.setItem("token " + i, token);
                      Continue = false;
                    }
                    i++;
                  }
                }
                else {
                  localStorage.setItem("token " + countNum, token);
                }
              }
            }
            else if(add == false) {
              this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
              const currentIndex = this.allTokens.findIndex(token => token.UserType === this.User_Data_After_Login.type && token.key===this.User_Data_After_Login.user_Name);
              const currentToken = this.allTokens[currentIndex];
              localStorage.setItem(currentToken.KeyInLocal, token);
            }
            
            for (let i = 0; i < localStorage.length; i++) {
              const key = localStorage.key(i);
              const value = localStorage.getItem(key || '');
              if (key && value && key.includes('token') && key != "current_token"&&key != "token") {
              Counter++;
              }
            }
            localStorage.removeItem("count");
            localStorage.setItem("count", Counter.toString());
            this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
          
            this.router.navigateByUrl("/Parent") 
          }
        );
      }, (error) => {
        this.isLoading = false; // Stop loading on error
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: error.error,
          confirmButtonText: 'Okay',
          customClass: {
            confirmButton: 'secondaryBg'
          }
        });
      });
    }
  }

  getAllTokens(): void {
    let count = 0;
    this.allTokens=[];
    for (let i = 0; i < localStorage.length; i++) {
      const key = localStorage.key(i);
      const value = localStorage.getItem(key || '');

      if (key && key.includes('token') && key != "current_token"&& key != "token") {
        if (value) {
          this.User_Data_After_Login2 = jwtDecode(value)

          this.allTokens.push({ id: count, key: this.User_Data_After_Login2.user_Name, KeyInLocal: key, value: value || '' ,UserType:this.User_Data_After_Login2.type});
          count++;
        }
      }
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
    if(this.ConfirmPassword != ""){
      if (this.parentInfo.password != this.ConfirmPassword) {
        this.validationErrors['password'] = 'Password And Confirm Password not The Same';
        isValid = false;
      }
    } else{
      this.IsConfimPassEmpty = true
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
