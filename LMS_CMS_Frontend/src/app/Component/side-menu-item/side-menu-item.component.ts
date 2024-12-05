import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { PagesWithRoleId } from '../../Models/pages-with-role-id';
import { MenuService } from '../../Services/shared/menu.service';
import { NewTokenService } from '../../Services/shared/new-token.service';
import { Subscription } from 'rxjs';

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
   subscription: Subscription | undefined;

  constructor(private router: Router ,private menuService: MenuService ,private communicationService: NewTokenService) {}

  async ngOnInit() {
    this.subscription = this.communicationService.action$.subscribe((state) => {
      this.menuService.menuItemsForEmployee$.subscribe((items) => {
        this.menuItemsForEmployee = items;
      },(error)=>{
        this.menuItemsForEmployee = [];
  
      });
    });
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      this.menuItemsForEmployee = items;
    },(error)=>{
      this.menuItemsForEmployee = [];

    });
  
  }

  navigateToRoute(routName:string): void {
    this.router.navigateByUrl(`Employee/${routName}`)
  }
}
