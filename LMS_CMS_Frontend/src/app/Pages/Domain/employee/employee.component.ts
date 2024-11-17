import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { EmployeeService } from '../../../Services/Employee/employee.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../../../Models/employee';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent {
  schoolId:number = 0
  Employee_With_Permission:Employee[] = []

  constructor(private router:Router, public employeeService: EmployeeService, private route: ActivatedRoute) { }

  ngOnInit(){
    const schoolIdParam = this.route.snapshot.paramMap.get('SchoolId');
    this.schoolId = schoolIdParam ? +schoolIdParam : 0; 
    this.getEmployees()
  }

  getEmployees(){
    this.employeeService.Get_Employee_By_School_Id(this.schoolId).subscribe(
      (d: any) => {
        this.Employee_With_Permission = d
      }, (error) => {
      }
    )
  }

  addEmployee(){
    this.router.navigateByUrl(`Domain/AddEmployee/${this.schoolId}`)
  }
}
