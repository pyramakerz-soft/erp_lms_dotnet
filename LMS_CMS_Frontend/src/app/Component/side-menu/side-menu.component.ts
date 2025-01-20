import { CommonModule } from '@angular/common';
import { Component, ElementRef, Input, QueryList, SimpleChanges, ViewChild, ViewChildren } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PagesWithRoleId } from '../../Models/pages-with-role-id';
import { SideMenuItemComponent } from '../side-menu-item/side-menu-item.component';
import { NewTokenService } from '../../Services/shared/new-token.service';
import { Subscription } from 'rxjs';
import { LanguageService } from '../../Services/shared/language.service';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-side-menu',
  standalone: true,
  imports: [CommonModule, RouterLink, SideMenuItemComponent, TranslateModule],
  templateUrl: './side-menu.component.html',
  styleUrl: './side-menu.component.css'
})
export class SideMenuComponent {
  @Input() menuItems?: { label: string; route?: string;  subItems?: { label: string; route: string }[]}[] = [];
  @Input() menuItemsForEmployee?: PagesWithRoleId[];

  @ViewChild('searchInput') searchInput: any;
  @ViewChildren('details') detailsList!: QueryList<ElementRef>;

  IsMenuOpen = false
  IsSearchFocus = false

  isRtl: boolean = false;
  subscription!: Subscription;

  constructor(private languageService: LanguageService) {} 

  ngOnInit(): void {
    this.subscription = this.languageService.language$.subscribe(direction => {
      this.isRtl = direction === 'rtl';
    });
    this.isRtl = document.documentElement.dir === 'rtl';
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  } 

  toggleMenu() {
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

  isAnyDetailsOpen(): boolean {
    return this.detailsList?.some((details) => details.nativeElement.open);
  }
}
