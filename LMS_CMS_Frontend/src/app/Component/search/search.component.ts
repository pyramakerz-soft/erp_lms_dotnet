import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BusType } from '../../Models/Bus/bus-type';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {

  key: keyof BusType = "id";
  value: any = "";
  IsSearchOpen: boolean = false;
  @Output() searchEvent = new EventEmitter<{ key: keyof BusType, value: any }>();
  @Input() keysArray: string[] = [];
  constructor() { }

  ngOnInit() { }

  SearchToggle() {
    this.IsSearchOpen = !this.IsSearchOpen;
  }

  SearchByKeyValue() {
    this.searchEvent.emit({ key: this.key, value: this.value });
  }
}
