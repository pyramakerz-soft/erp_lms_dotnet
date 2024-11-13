import { CommonModule } from '@angular/common';
import { Component, HostListener } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../Models/token-data';
import { AccountService } from '../../Services/account.service';
import { EmployeeService } from '../../Services/Employee/employee.service';
import { ParentService } from '../../Services/Parent/parent.service';
import { StudentService } from '../../Services/Student/student.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {

  dropdownOpen: boolean = false;
  selectedLanguage: string = "English";
  User_Type:string="";
  userName:string="";

  constructor(public account:AccountService ,public empserv:EmployeeService ,public parentServ:ParentService , public studentserv:StudentService, private translate: TranslateService){}

  ngOnInit(){
    this.GetUserInfo();

    const savedLanguage = localStorage.getItem('language') || 'en';
    this.selectedLanguage = savedLanguage === 'ar' ? 'العربية' : 'English';
  }

  toggleDropdown() {
    this.dropdownOpen = !this.dropdownOpen;
  }

  selectLanguage(language: string) {
    this.translate.use(language);
    localStorage.setItem('language', language);
    this.selectedLanguage = language === 'ar' ? 'العربية' : 'English';
    this.updateDirection(language);
    this.dropdownOpen = false;
  }

  updateDirection(language: string) {
    const direction = language === 'ar' ? 'rtl' : 'ltr';
    document.documentElement.setAttribute('dir', direction);
  }

  GetUserInfo(){
    let token = localStorage.getItem("token")
    let User_Data_After_Login = new TokenData("", 0, 0, "", "", "", "", "")
    User_Data_After_Login = this.account.Get_Data_Form_Token()
     this.User_Type = User_Data_After_Login.type 
  
    if(this.User_Type=="parent"){
     this.parentServ.Get_Parent_By_Id(User_Data_After_Login.id).subscribe(
      (d: any) => {
        this.userName=d.user_Name;
      });

    }else if(this.User_Type=="employee"){
      this.empserv.Get_Employee_By_Id(User_Data_After_Login.id).subscribe(
        (d: any) => {
          this.userName=d.user_Name;
        });  
    }else if(this.User_Type=="student"){
      this.studentserv.Get_Student_By_Id(User_Data_After_Login.id).subscribe(
        (d: any) => {
          this.userName=d.user_Name;
        });
  
    }
  }
}
