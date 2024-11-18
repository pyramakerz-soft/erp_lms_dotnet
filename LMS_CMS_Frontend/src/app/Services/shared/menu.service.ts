import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private menuItemsForEmployeeSource = new BehaviorSubject<any[]>([]);
  menuItemsForEmployee$ = this.menuItemsForEmployeeSource.asObservable();

  constructor() { }
  updateMenuItemsForEmployee(menuItems: any): void {
    this.menuItemsForEmployeeSource.next(menuItems);
  }

  findByPageName(pageName: string, items: any[] = []): any {
    for (const item of items) {
      if (item.en_name === pageName) {
        return item;
      }
      if (item.children && item.children.length > 0) {
        const found = this.findByPageName(pageName, item.children);
        if (found) {
          return found;
        }
      }
    }
    return null; // Return null if not found
  }
}
