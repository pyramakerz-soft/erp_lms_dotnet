import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router, Routes } from '@angular/router';
import { PagesWithRoleId } from '../../Models/pages-with-role-id';
import { MenuService } from '../../Services/shared/menu.service';
import { NewTokenService } from '../../Services/shared/new-token.service';
import { Subscription } from 'rxjs';
import { LanguageService } from '../../Services/shared/language.service';

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
  subscription!: Subscription;
  isRtl: boolean = false;

  constructor(private router: Router ,private menuService: MenuService ,private communicationService: NewTokenService, private languageService: LanguageService) {}

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
  
    this.subscription = this.languageService.language$.subscribe(direction => {
      this.isRtl = direction === 'rtl';
    });
    this.isRtl = document.documentElement.dir === 'rtl'; 
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  } 

  navigateToRoute(routName:string): void {
    const routes: Routes = this.router.config;

    const routeExists = this.isRouteExist(routName, routes);

    if (routeExists) {
      this.router.navigateByUrl(`Employee/${routName}`)
    }
  }

  isRouteExist(routeName: string, routes: Routes): boolean {
    for (const route of routes) {
      if (route.path === routeName) {
        return true;
      }

      if (route.children) {
        const childRouteExists = this.isRouteExist(routeName, route.children);
        if (childRouteExists) {
          return true;
        }
      }
    }
    return false;
  } 
  
}
