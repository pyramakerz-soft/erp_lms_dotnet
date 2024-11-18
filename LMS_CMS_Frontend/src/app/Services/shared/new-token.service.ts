import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewTokenService {

  private actionSource = new BehaviorSubject<boolean>(false);

  action$ = this.actionSource.asObservable();

  constructor() {}

  sendAction(state: boolean): void {
    this.actionSource.next(state);
  }
}