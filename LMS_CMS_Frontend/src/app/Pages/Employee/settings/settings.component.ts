import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MenuService } from '../../../Services/shared/menu.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {

  constructor(private router: Router ,private menuService: MenuService) {}
  AllowEdit:boolean=false;
  AllowDelete:boolean=false;

  
  ngOnInit() {
    this.menuService.menuItemsForEmployee$.subscribe((items) => {

      const settingsPage = this.menuService.findByPageName('Settings', items);
      this.AllowDelete=settingsPage.allow_Delete;
      this.AllowEdit=settingsPage.allow_Edit;

    });
  }
}
