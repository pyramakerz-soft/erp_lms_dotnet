import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../Services/account.service';
import { Login } from '../../../Models/login';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pyramakerz-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './pyramakerz-login.component.html',
  styleUrl: './pyramakerz-login.component.css'
})
export class PyramakerzLoginComponent {
  userInfo:Login = new Login("", "", "", "");

  constructor(private router:Router, public accountService:AccountService){  }


  SignIN(){}
}
