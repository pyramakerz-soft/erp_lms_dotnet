import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PagesWithRoleId } from '../../Models/pages-with-role-id';
import { SideMenuItemComponent } from '../side-menu-item/side-menu-item.component';

@Component({
  selector: 'app-side-menu',
  standalone: true,
  imports: [CommonModule, RouterLink, SideMenuItemComponent],
  templateUrl: './side-menu.component.html',
  styleUrl: './side-menu.component.css'
})
export class SideMenuComponent {
  @Input() menuItems?: { label: string; route?: string;  subItems?: { label: string; route: string }[]}[] = [];
  @Input() menuItemsForEmployee?: PagesWithRoleId[];

  IsMenuOpen = false
  
  toggleMenu() {
    this.IsMenuOpen = !this.IsMenuOpen
  }
}
