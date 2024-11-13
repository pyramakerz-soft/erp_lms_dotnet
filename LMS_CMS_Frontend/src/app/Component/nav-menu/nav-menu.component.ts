import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, HostListener, Renderer2 } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../Models/token-data';
import { AccountService } from '../../Services/account.service';
import { EmployeeService } from '../../Services/Employee/employee.service';
import { ParentService } from '../../Services/Parent/parent.service';
import { StudentService } from '../../Services/Student/student.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {

  dropdownOpen: boolean = false;
  selectedLanguage: string = "English";
  User_Type:string="";
  userName:string="";
  isPopupOpen = false;
  allTokens: { key: string; value: string }[] = [];
  User_Data_After_Login = new TokenData("", 0, 0, "", "", "", "", "")

  constructor(private cdr: ChangeDetectorRef ,private router: Router, public account: AccountService, public empserv: EmployeeService, public parentServ: ParentService, public studentserv: StudentService, private renderer: Renderer2, private translate: TranslateService) {}

  ngOnInit(){
    this.GetUserInfo();
    const savedLanguage = localStorage.getItem('language') || 'en';
    this.selectedLanguage = savedLanguage === 'ar' ? 'العربية' : 'English';
    this.getAllTokens();
  }

  getAllTokens(): void {
    for (let i = 0; i < localStorage.length; i++) {
      const key = localStorage.key(i);
      const value = localStorage.getItem(key || '');

      if (key && key.includes('token') && key != "current_token") {
        if (value) {
          this.User_Data_After_Login = jwtDecode(value)
          const existingToken = this.allTokens.find(token => token.key === this.User_Data_After_Login.user_Name);

          if (!existingToken) {
            // Only add the token if the userName is not already in the array
            this.allTokens.push({ key: this.User_Data_After_Login.user_Name, value: value || '' });
          }
        }

      }
    }
  }


  gotologin() {
    localStorage.removeItem("current_token");
    this.router.navigateByUrl('')
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
    this.dropdownOpen = false;
  }

  GetUserInfo() {
    let token = localStorage.getItem("current_token")
    let User_Data_After_Login = new TokenData("", 0, 0, "", "", "", "", "")
    User_Data_After_Login = this.account.Get_Data_Form_Token()
    this.User_Type = User_Data_After_Login.type
    this.userName = User_Data_After_Login.user_Name
    if (this.User_Type == "parent") {
      this.parentServ.Get_Parent_By_Id(User_Data_After_Login.id).subscribe(
        (d: any) => {
          this.userName = d.user_Name;
        });

    } else if (this.User_Type == "employee") {
      this.empserv.Get_Employee_By_Id(User_Data_After_Login.id).subscribe(
        (d: any) => {
          this.userName = d.user_Name;
        });
    } else if (this.User_Type == "student") {
      this.studentserv.Get_Student_By_Id(User_Data_After_Login.id).subscribe(
        (d: any) => {
          this.userName = d.user_Name;
        });

    }
  }
  togglePopup(): void {
    this.isPopupOpen = !this.isPopupOpen;

    if (this.isPopupOpen) {
      this.renderer.addClass(document.body, 'overflow-hidden');
    } else {
      this.renderer.removeClass(document.body, 'overflow-hidden');
    }
  }

  ngOnDestroy(): void {
    this.renderer.removeClass(document.body, 'overflow-hidden');
  }

  // @HostListener('document:click', ['$event'])
  // onClickOutside(event: Event) {
  //   const targetElement = event.target as HTMLElement;
  //   const clickedOutside = !targetElement.closest('.profile-photo');
  //   if (clickedOutside) {
  //     this.isPopupOpen = false;
  //   }
  // }

  ChangeAccount(key: string): void {
    // Find the token object by key
    const tokenObject = this.allTokens.find(s => s.key === key);

    // If the token is found, remove the current token and set the new one
    if (tokenObject) {
      localStorage.removeItem("current_token");
      localStorage.setItem("current_token", tokenObject.value);
      this.User_Data_After_Login = jwtDecode(tokenObject.value)
      this.userName=this.User_Data_After_Login.user_Name
      this.router.navigateByUrl("")
    }
  }

}
