import { CommonModule } from '@angular/common';
import { Component, Input, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PagesWithRoleId } from '../../Models/pages-with-role-id';
import { SideMenuItemComponent } from '../side-menu-item/side-menu-item.component';
import { NewTokenService } from '../../Services/shared/new-token.service';

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

  @ViewChild('searchInput') searchInput: any;
  
  IsMenuOpen = false
  IsSearchFocus = false

  constructor(private communicationService: NewTokenService) {}

  toggleMenu() {
    console.log(this.menuItems)
    this.IsMenuOpen = !this.IsMenuOpen
    this.IsSearchFocus = false
  }

  toggleSearch() {
    this.IsMenuOpen = !this.IsMenuOpen
    this.IsSearchFocus = !this.IsSearchFocus;
  }

  focusSearchInput() {
    if (this.searchInput) {
      this.searchInput.nativeElement.focus();
    }
  }

  ngAfterViewChecked() {
    if (this.IsSearchFocus) {
      this.focusSearchInput();
    }
  }
}
