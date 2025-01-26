import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, HostListener, Renderer2 } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../Models/token-data';
import { AccountService } from '../../Services/account.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';
import { NewTokenService } from '../../Services/shared/new-token.service';
import { LogOutService } from '../../Services/shared/log-out.service';
import { Subscription } from 'rxjs';
import { LanguageService } from '../../Services/shared/language.service';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {

  dropdownOpen: boolean = false;
  selectedLanguage: string = "English";
  User_Type: string = "";
  userName: string = "";
  isPopupOpen = false;
  allTokens: { id: number, key: string; KeyInLocal: string; value: string; UserType: string }[] = [];
  User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  subscription: Subscription | undefined;

  constructor(private router: Router, public account: AccountService, public languageService: LanguageService,
    private translate: TranslateService, private communicationService: NewTokenService, private logOutService: LogOutService) { }

  ngOnInit() {
    this.GetUserInfo();
    const savedLanguage = localStorage.getItem('language') || 'en';
    this.selectedLanguage = savedLanguage === 'ar' ? 'العربية' : 'English';
    this.getAllTokens();
    this.subscription = this.communicationService.action$.subscribe((state) => {
      this.GetUserInfo();
    });
  }

  getAllTokens(): void {
    let count = 0;
    this.allTokens = [];
    for (let i = 0; i < localStorage.length; i++) {
      const key = localStorage.key(i);
      const value = localStorage.getItem(key || '');

      if (key && key.includes('token') && key != "current_token" && key != "token") {
        if (value) {
          this.User_Data_After_Login = jwtDecode(value)
          if (this.User_Data_After_Login.user_Name)
            this.allTokens.push({ id: count, key: this.User_Data_After_Login.user_Name, KeyInLocal: key, value: value || '', UserType: this.User_Data_After_Login.type });
          count++;
        }

      }
    }
  }


  gotologin() {
    localStorage.setItem("GoToLogin", "true");
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

    const direction = language === 'ar' ? 'rtl' : 'ltr';
    this.languageService.setLanguage(direction);
  }
  
  updateDirection(language: string) {
    const direction = language === 'ar' ? 'rtl' : 'ltr';
    document.documentElement.setAttribute('dir', direction); 
    this.dropdownOpen = false;
  }

  GetUserInfo() {
    let token = localStorage.getItem("current_token")
    let User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
    User_Data_After_Login = this.account.Get_Data_Form_Token()
    this.User_Type = User_Data_After_Login.type
    this.userName = User_Data_After_Login.user_Name 
  }

  togglePopup(): void {
    this.getAllTokens();
    this.isPopupOpen = !this.isPopupOpen;
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    const dropdown = document.querySelector('.dropdown-container') as HTMLElement;

    if (dropdown && !dropdown.contains(target)) {
      this.isPopupOpen = false;
    }
  }

  // Cleanup event listener
  ngOnDestroy() {
    document.removeEventListener('click', this.onDocumentClick);
  } 

  ChangeAccount(id: number): void {
    const tokenObject = this.allTokens.find(s => s.id === id);
    const token = localStorage.getItem("current_token")
    this.togglePopup();
    if (tokenObject && token != tokenObject.value) {
      localStorage.removeItem("current_token");
      localStorage.setItem("current_token", tokenObject.value);
      this.User_Data_After_Login = jwtDecode(tokenObject.value)
      this.userName = this.User_Data_After_Login.user_Name
      this.communicationService.sendAction(true);
      this.router.navigateByUrl("")
    }
  }

  logOutAll() {
    for (let i = 0; i < localStorage.length; i++) {
      const key = localStorage.key(i);
      const value = localStorage.getItem(key || '');

      if (key && value && key.includes('token')) {
        localStorage.removeItem(key);
      }
    }
    localStorage.removeItem("current_token");
    localStorage.removeItem("count");
    this.router.navigateByUrl("")

  }

  async logOut() {
    // const count = parseInt(localStorage.getItem("count") ?? "0", 10);
    // let currentTokenn = localStorage.getItem("current_token") ?? "";

    // const currentIndex = this.allTokens.findIndex(token => token.value === currentTokenn);
    // console.log(currentIndex)

    // if (currentIndex === -1) {
    //   return;
    // }

    // const currentToken = this.allTokens[currentIndex];
    // localStorage.removeItem(currentToken.KeyInLocal);

    // this.allTokens.splice(currentIndex, 1);

    // if (this.allTokens.length > 0) {
    //   const newToken = this.allTokens[currentIndex] || this.allTokens[currentIndex - 1];

    //   localStorage.setItem("current_token", newToken.value);
    // } else {
    //   localStorage.removeItem("current_token");
    // }

    // localStorage.setItem("count", this.allTokens.length.toString());
    this.isPopupOpen = false
    await this.logOutService.logOut();
    this.GetUserInfo();
    this.getAllTokens();
    this.router.navigateByUrl("");
  }
}
