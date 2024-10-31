import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AccountService } from '../../Services/account.service';

@Component({
  selector: 'app-side-menu',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './side-menu.component.html',
  styleUrl: './side-menu.component.css'
})
export class SideMenuComponent {
  @Input() menuItems: { label: string; route?: string;  subItems?: { label: string; route: string }[]}[] = [];

  constructor(public accountService:AccountService){}

  SignOut(){
    this.accountService.SignOut()
  }
}
