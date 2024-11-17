import { Component } from '@angular/core';
import { TokenData } from '../../../Models/token-data';
import { AccountService } from '../../../Services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-student-home',
  standalone: true,
  imports: [],
  templateUrl: './student-home.component.html',
  styleUrl: './student-home.component.css'
})
export class StudentHomeComponent {

  User_Data_After_Login :TokenData =new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  name:string=""
  constructor(private router:Router ,public account:AccountService){  }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.name=this.User_Data_After_Login.user_Name

  }
}
