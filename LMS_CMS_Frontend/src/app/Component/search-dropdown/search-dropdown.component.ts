import { CommonModule } from '@angular/common';
import { Component, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-dropdown',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './search-dropdown.component.html',
  styleUrl: './search-dropdown.component.css'
})
export class SearchDropdownComponent {
  constructor(private _eref: ElementRef) {}

  @Input() selectedValue: any;
  @Output() selectedValueChange = new EventEmitter<any>();

  @Input() placeholder: string = 'Search...';
  @Input() displayProperty: string = 'name';
  @Input() serviceFunction!: (searchTerm: string, page: number) => any;

  @Input() validationKey: string = '';
  @Input() validationErrors: any = {};
  @Output() blur = new EventEmitter<void>();

  searchTerm: string = '';
  showDropdown: boolean = false;
  items: any[] = [];
  currentPage: number = 1;
  totalPages: number = 1;
  searchTriggered = false;

  // Detect clicks outside this component
  @HostListener('document:click', ['$event'])
  onClickOutside(event: Event): void {
    if (!this._eref.nativeElement.contains(event.target)) {
      this.hideDropdown();
    }
  }

  search(page: number = 1): void {
    if (!this.serviceFunction) return;

    this.searchTriggered = true;
    this.currentPage = page;

    this.serviceFunction(this.searchTerm, page).subscribe((res: any) => {
      this.items = res.items || [];
      this.totalPages = res.totalPages || 1;
      this.showDropdown = true;
      this.searchTriggered = false;
    });
  }

  selectItem(item: any): void {
    this.selectedValue = item.id;
    this.searchTerm = item[this.displayProperty];
    this.selectedValueChange.emit(item.id);
    this.showDropdown = false;
  }

  hideDropdown(): void {
    this.showDropdown = false;
    this.searchTriggered = false;
    this.blur.emit();
  }
  }