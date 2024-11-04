import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../Services/account.service';
import { Login } from '../../../Models/login';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-domain-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './domain-login.component.html',
  styleUrl: './domain-login.component.css'
})
export class DomainLoginComponent {
  
  userInfo:Login = new Login("", "", "", "");

  constructor(private router:Router, public accountService:AccountService){  }


  SignIN(){}
}
