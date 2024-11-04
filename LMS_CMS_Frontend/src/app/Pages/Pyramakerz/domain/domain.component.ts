import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DomainService } from '../../../Services/domain.service';
import { Domain } from '../../../Models/domain';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-domain',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './domain.component.html',
  styleUrl: './domain.component.css'
})
export class DomainComponent {
  
  domains:Domain[]=[];
  constructor(private router:Router, public domainServ:DomainService){  }

  ngOnInit(){
    this.getAllDomains();
  }

  getAllDomains(){
    this.domainServ.Get_All_Domain().subscribe(
      (d: any) => {
        console.log(d);
        this.domains=d;
        console.log(this.domains[0].schools);
      });
  }

}
