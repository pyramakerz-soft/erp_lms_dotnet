import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  private languageSubject = new BehaviorSubject<'ltr' | 'rtl'>('ltr');
  language$ = this.languageSubject.asObservable();

  setLanguage(language: 'ltr' | 'rtl') {
    this.languageSubject.next(language);
  }
}
