import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../Services/account.service';
import { Login } from '../../../Models/login';
import { jwtDecode } from 'jwt-decode';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../Models/token-data';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  userInfo:Login = new Login("", "", "", "");
  User_Data_After_Login = new TokenData("", 0, 0, "", "", "", "", "")

  // Initialize the image states
  employeeImage = 'Images/employee.png';
  studentImage = 'Images/student.png';
  parentImage = 'Images/parent.png';

  // Hover state
  isEmployeeHovered = false;
  isStudentHovered = false;
  isParentHovered = false;

  userNameError: string = ""; 
  passwordError: string = ""; 
  somthingError: string = ""

  constructor(private router:Router, public accountService:AccountService){  }

  ngOnInit(){
    this.selectType("student")
  }

  onUserNameChange() {
    this.userNameError = "" 
    this.somthingError = "" 
  }

  onPasswordChange() {
    this.passwordError = "" 
    this.somthingError = "" 
  }

  isFormValid(){
    let isValid = true

    if (this.userInfo.user_Name.trim() === "" && this.userInfo.password.trim() === "") {
      isValid = false;
      this.userNameError = '*Username cannot be empty';
      this.passwordError = '*Password cannot be empty';
    } else if (this.userInfo.user_Name.trim() === "") {
      isValid = false;
      this.userNameError = '*Username cannot be empty';
    } else if (this.userInfo.password.trim() === "") {
      isValid = false;
      this.passwordError = '*Password cannot be empty';
    } 
    return isValid
  }

  SignIN(){
    console.log(this.isFormValid())
    if(this.isFormValid()){
      this.accountService.Login(this.userInfo).subscribe(
        (d: any) => {
          this.accountService.isAuthenticated = true;
          localStorage.setItem("token", JSON.parse(d).token);
          
          this.User_Data_After_Login = this.accountService.Get_Data_Form_Token()
  
          if(this.User_Data_After_Login.type == "parent"){
            this.router.navigateByUrl("/Parent")
          } else if(this.User_Data_After_Login.type == "student"){
            this.router.navigateByUrl("/Student")
          } else if(this.User_Data_After_Login.type == "employee"){
            this.router.navigateByUrl("/Employee")
          }
        },(error)=>{
          if(error.status == 404){
            this.somthingError = "Username, Password or Type maybe wrong"
          }
        }
      );
    }
  }

  selectType(type: string) {
    this.userInfo.type = type;
    if(this.userInfo.type == "employee") {
      this.isEmployeeHovered=true;
      this.isStudentHovered=false;
      this.isParentHovered=false;
    }
   else if(this.userInfo.type == "student"){
     this.isEmployeeHovered=false;
     this.isStudentHovered=true;
     this.isParentHovered=false;
    }
    else if(this.userInfo.type == "parent"){
      this.isEmployeeHovered=false;
      this.isStudentHovered=false;
      this.isParentHovered=true;
    } 

  }
}
