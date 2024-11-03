import { Injectable } from '@angular/core';
import { EmployeePermission } from '../../Models/employee-permission';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GetDataFromLayoutService {

  private sharedData = new EmployeePermission(0, "", "", []);
  private sharedDataSubject = new BehaviorSubject<EmployeePermission>(this.sharedData);

  // Observable for components to subscribe to
  sharedData$ = this.sharedDataSubject.asObservable();

  setData(data: EmployeePermission) {
    this.sharedData = data;
    this.sharedDataSubject.next(this.sharedData); // Emit new data
  }

  getData() {
    console.log(this.sharedData);
    return this.sharedData;
  }
}