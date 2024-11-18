import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { PagesWithRoleId } from '../../Models/pages-with-role-id';
import { MenuService } from '../../Services/shared/menu.service';

@Component({
  selector: 'app-side-menu-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './side-menu-item.component.html',
  styleUrl: './side-menu-item.component.css'
})
export class SideMenuItemComponent {
  @Input() item!: any;
  @Input() menuItems?: { label: string; route?: string;  subItems?: { label: string; route: string }[]}[] = [];
  @Input() menuItemsForEmployee?: PagesWithRoleId[];

  constructor(private router: Router ,private menuService: MenuService) {}

  ngOnInit() {
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      this.menuItemsForEmployee = items;
    });
  }

  navigateToLogin(routName:string): void {
    // this.router.navigateByUrl('Employee/')
    this.router.navigateByUrl(`Employee/${routName}`)
  }
}
