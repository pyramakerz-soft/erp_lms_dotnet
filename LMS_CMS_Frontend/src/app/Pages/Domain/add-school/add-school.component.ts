import { Component } from '@angular/core';
import { SchoolAdd } from '../../../Models/school-add';
import { TokenData } from '../../../Models/token-data';
import { AccountService } from '../../../Services/account.service';
import { SchoolService } from '../../../Services/Domain/school.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RoleAdd } from '../../../Models/role-add';
import { SchoolRoleService } from '../../../Services/Domain/school-role.service';

@Component({
  selector: 'app-add-school',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './add-school.component.html',
  styleUrl: './add-school.component.css'
})
export class AddSchoolComponent {

  newSchool:SchoolAdd=new SchoolAdd("",0);
  User_Data_After_Login :TokenData= new TokenData("", 0, 0, "", "", "", "", "")
  roles: string[] = [];
  School_id:number=0;
  newRole:RoleAdd=new RoleAdd(0,"");
  constructor(private router:Router, public schoolServ:SchoolService ,public account:AccountService ,public schoolRoleServ:SchoolRoleService){  }


  ngOnInit(){
    // this.getDomainInfo();
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.newSchool.domainId=this.User_Data_After_Login.id
  }
  addRole() {
    this.roles.push(''); // Push an empty string to create a new input
  }
  async addSchool(){

   await this.schoolServ.AddSchool(this.newSchool).subscribe(
      (d: any) => {
        console.log(d);
        this.School_id=d;
        this.roles.forEach(element => {
          this.newRole.role_Name=element;
          this.newRole.school_id=this.School_id;
          console.log(this.newRole)
          this.schoolRoleServ.AddSchoolRole(this.newRole).subscribe(
            (d: any) => {
              console.log(d);
              this.router.navigateByUrl("Domain/Home")
            });
    
        });
        
      });
    


  }
  deleteRole(index: number) {
    this.roles = this.roles.filter((_, i) => i !== index);
  }

}
