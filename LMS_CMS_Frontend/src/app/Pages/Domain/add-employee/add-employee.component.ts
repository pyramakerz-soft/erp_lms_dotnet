import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../../../Services/Employee/employee.service';
import { Employee } from '../../../Models/employee';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-employee',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-employee.component.html',
  styleUrl: './add-employee.component.css'
})
export class AddEmployeeComponent {
  schoolId:number = 0
  employee:Employee = new Employee(0, 0, "", "", "", [])
  
  constructor(private router:Router, private route: ActivatedRoute, private employeeService: EmployeeService){  }
  
  ngOnInit(){
    const schoolIdParam = this.route.snapshot.paramMap.get('SchoolId');
    this.schoolId = schoolIdParam ? +schoolIdParam : 0; 
  }

  addEmployee() {
    this.employee.school_id = this.schoolId
    this.employeeService.Add_Employee(this.employee).subscribe(
      (d) => {
        console.log(d)
        this.router.navigateByUrl(`Domain/Employees/${this.schoolId}`)
      },
      (error) => {
        console.log(error)
      }
    )
  }
}
