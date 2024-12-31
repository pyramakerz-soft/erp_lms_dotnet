import { Component } from '@angular/core';
import { SubjectService } from '../../../../Services/Employee/LMS/subject.service';
import { Subject } from '../../../../Models/LMS/subject';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-subject-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './subject-view.component.html',
  styleUrl: './subject-view.component.css'
})
export class SubjectViewComponent {
  subject:Subject = new Subject()
  subjectId = 0;
  DomainName = "";

  constructor(public subjectService: SubjectService, public activeRoute:ActivatedRoute, public router:Router){}

  async ngOnInit(){
    this.subjectId = await Number(this.activeRoute.snapshot.paramMap.get('SubId'))
    this.DomainName = await String(this.activeRoute.snapshot.paramMap.get('domainName'))

    this.GetSubjectById()
  }

  GetSubjectById() {
    this.subjectService.GetByID(this.subjectId, this.DomainName).subscribe((data) => {
      this.subject = data;
    })
  }

  moveToSubjects(){
    this.router.navigateByUrl('Employee/Subject');
  }
}
