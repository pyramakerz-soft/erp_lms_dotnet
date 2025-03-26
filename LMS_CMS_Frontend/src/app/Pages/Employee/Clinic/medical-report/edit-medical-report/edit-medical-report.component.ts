import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-medical-report',
  templateUrl: './edit-medical-report.component.html',
  styleUrls: ['./edit-medical-report.component.css'],
})
export class EditMedicalReportComponent implements OnInit {
  reportData: any;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      if (params['data']) {
        this.reportData = JSON.parse(params['data']);
      }
    });
  }

  closePage() {
    this.router.navigate(['/'], { queryParams: { tab: 'MH By Doctor' } });
  }
}
