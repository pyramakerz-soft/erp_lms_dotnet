import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { TokenData } from '../../Models/token-data';
import { NewTokenService } from './new-token.service';

@Injectable({
  providedIn: 'root'
})
export class LogOutService {

  allTokens: { id: number, key: string; KeyInLocal: string; value: string }[] = [];
  User_Data_After_Login = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  constructor(private router: Router ,private communicationService: NewTokenService) { }

  getAllTokens(): void {
    this.allTokens=[]
    let count = 0;
    for (let i = 0; i < localStorage.length; i++) {
      const key = localStorage.key(i);
      const value = localStorage.getItem(key || '');
      if (key && key.includes('token') && key != "current_token"&& key != "token") {
        if (value) {
          this.User_Data_After_Login = jwtDecode(value)
          this.allTokens.push({ id: count, key: this.User_Data_After_Login.user_Name, KeyInLocal: key, value: value || '' });
          count++;
        }
      }
    }
  }

  logOut() {
    
    this.getAllTokens();
    const count = parseInt(localStorage.getItem("count") ?? "0", 10);
    let currentTokenn = localStorage.getItem("current_token") ?? "";

    const currentIndex = this.allTokens.findIndex(token => token.value === currentTokenn);
    if (currentIndex === -1) {
      return;
    }

    const currentToken = this.allTokens[currentIndex];
    localStorage.removeItem(currentToken.KeyInLocal);

    this.allTokens.splice(currentIndex, 1);

    if (this.allTokens.length > 0) {
      const newToken = this.allTokens[currentIndex] || this.allTokens[currentIndex - 1];

      localStorage.setItem("current_token", newToken.value);
    } else {
      localStorage.removeItem("current_token");
    }
    this.getAllTokens();
    localStorage.setItem("count", this.allTokens.length.toString());
    this.communicationService.sendAction(true);
    this.router.navigateByUrl("") 
  }
}
