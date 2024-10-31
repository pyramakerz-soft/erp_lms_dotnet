import { Component } from '@angular/core';
import { EmployeeService } from '../../../Services/Employee/employee.service';
import { AccountService } from '../../../Services/account.service';
import { TokenData } from '../../../Models/token-data';

@Component({
  selector: 'app-employee-home',
  standalone: true,
  imports: [],
  templateUrl: './employee-home.component.html',
  styleUrl: './employee-home.component.css'
})
export class EmployeeHomeComponent {
}
