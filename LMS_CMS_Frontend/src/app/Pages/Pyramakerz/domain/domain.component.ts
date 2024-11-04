import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DomainService } from '../../../Services/domain.service';
import { Domain } from '../../../Models/domain';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DomainAdd } from '../../../Models/domain-add';

@Component({
  selector: 'app-domain',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './domain.component.html',
  styleUrl: './domain.component.css'
})
export class DomainComponent {
  
  domains:Domain[]=[];
  DomainAdd:DomainAdd=new DomainAdd("","","");
  DomainEdit:Domain=new Domain(0,"","","",[]);

  isDisplayAddDomain:boolean=false;
  isDisplayEditDomain:boolean=false;

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

  deleteDomain(id:number){
    this.domainServ.Delete_Domain_By_Id(id).subscribe(
      (d: any) => {
        console.log(d);
        this.getAllDomains();
      });
  }

  OpenADD(){
    this.isDisplayAddDomain=true;
  }

  CloseAdd(){
    this.isDisplayAddDomain=false;

  }
  Save(){
    this.domainServ.AddDomain(this.DomainAdd).subscribe(
      (d: any) => {
        console.log(d);
        this.getAllDomains();
        this.isDisplayAddDomain=false;

      });
  }
  OpenUpdate(domain:Domain){
    this.DomainEdit=domain;
    this.isDisplayEditDomain=true;
  }

  CloseEdit(){
    this.isDisplayEditDomain=false;
  }

  update(){
    this.domainServ.Update_Domain(this.DomainEdit).subscribe(
      (d: any) => {
        console.log(d);
        this.getAllDomains();
        this.isDisplayEditDomain=false;

      });
  }
}
