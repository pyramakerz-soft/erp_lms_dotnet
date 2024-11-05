import { Component } from '@angular/core';
import { Domain } from '../../../Models/domain';
import { Router } from '@angular/router';
import { DomainService } from '../../../Services/domain.service';
import { TokenData } from '../../../Models/token-data';
import { AccountService } from '../../../Services/account.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  domain:Domain=new Domain(0,"","","",[]);
   User_Data_After_Login :TokenData= new TokenData("", 0, 0, "", "", "", "", "")


  constructor(private router:Router, public domainServ:DomainService ,public account:AccountService){  }

  ngOnInit(){
    // this.getDomainInfo();
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.getDomainInfo(this.User_Data_After_Login.id)
  }

  getDomainInfo(id:number){
    this.domainServ.Get_Domain_By_Id(id).subscribe(
      (d: any) => {
        console.log(d);
        this.domain=d;
        console.log(this.domain);
      });
  }

  add(){
    this.router.navigateByUrl("Domain/AddSchool")
  }

}
