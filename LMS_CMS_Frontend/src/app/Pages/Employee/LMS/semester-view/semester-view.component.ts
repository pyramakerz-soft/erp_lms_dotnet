import { Component } from '@angular/core';
import { ApiService } from '../../../../Services/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Semester } from '../../../../Models/LMS/semester';
import { SemesterService } from '../../../../Services/Employee/LMS/semester.service';

@Component({
  selector: 'app-semester-view',
  standalone: true,
  imports: [],
  templateUrl: './semester-view.component.html',
  styleUrl: './semester-view.component.css'
})
export class SemesterViewComponent {
  DomainName: string = "";
  semesterId: number = 0

  semester:Semester = new Semester()

  constructor(public ApiServ: ApiService, public activeRoute: ActivatedRoute, public router:Router, public semesterService:SemesterService){}

  ngOnInit(){
    this.DomainName = this.ApiServ.GetHeader();
    this.semesterId = Number(this.activeRoute.snapshot.paramMap.get('Id'))

    this.GetSemesterById(this.semesterId)
  }

  moveToSemester(){
    this.router.navigateByUrl('Employee/Semester/' + this.DomainName + '/' + this.semester.academicYearID)
  }

  GetSemesterById(Id: number) {
    this.semesterService.GetByID(Id, this.DomainName).subscribe((data) => {
      this.semester = data;
    });
  }
}
