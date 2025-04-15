
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HygieneFormService } from '../../../../../Services/Employee/Clinic/hygiene-form.service';
import { HygieneForm } from '../../../../../Models/Clinic/HygieneForm';
import { School } from '../../../../../Models/school';
import { Grade } from '../../../../../Models/LMS/grade';
import { Classroom } from '../../../../../Models/LMS/classroom';
import { Student } from '../../../../../Models/student';
import { HygieneTypes } from '../../../../../Models/Clinic/hygiene-types';
import { HygieneFormTableComponent } from "../hygiene-form-table/hygiene-form-table.component";
import { ApiService } from '../../../../../Services/api.service';
import { DatePipe } from '@angular/common'; 

@Component({
  selector: 'app-view-hygiene-form',
  templateUrl: './veiw-hygiene-form.component.html',
  styleUrl: './veiw-hygiene-form.component.css',
  imports: [HygieneFormTableComponent , DatePipe],  standalone: true,

})
export class ViewHygieneFormComponent implements OnInit {
moveToHygieneForm() {
this.router.navigateByUrl('Employee/Hygiene Form Medical Report');
}

  hygieneForm: HygieneForm | null = null;
  school: School | null = null;
  grade: Grade | null = null;
  classroom: Classroom | null = null;
  students: Student[] = [];
  hygieneTypes: HygieneTypes[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private hygieneFormService: HygieneFormService,
    private apiService: ApiService,
    
  ) {}
  

ngOnInit(): void {
  const id = this.route.snapshot.paramMap.get('id');
  if (id) {
    this.loadHygieneForm(Number(id));
  }
}

async loadHygieneForm(id: number) {
  try {
    const domainName = this.apiService.GetHeader();
    this.hygieneFormService.GetById(id, domainName).subscribe({
      next: (hygieneForm) => {
        this.hygieneForm = hygieneForm;
        this.students = hygieneForm.studentHygieneTypes ?? []; 
        console.log(this.students)
      },
      error: (error) => {
        console.error('Error loading hygiene form:', error);
      }
    });
  } catch (error) {
    console.error('Error in loadHygieneForm:', error);
  }
}

}