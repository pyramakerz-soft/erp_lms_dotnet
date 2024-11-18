import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewTokenService {

  private actionSource = new BehaviorSubject<boolean>(false);

  // Expose an observable for components to subscribe to
  action$ = this.actionSource.asObservable();

  constructor() {}

  // Method to update the boolean state
  sendAction(state: boolean): void {
    this.actionSource.next(state);
  }
}