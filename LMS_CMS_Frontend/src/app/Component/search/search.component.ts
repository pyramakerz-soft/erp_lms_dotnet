import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BusType } from '../../Models/Bus/bus-type';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';


@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
private searchSubject = new Subject<string>();

  @Input() key: string = "id" ;
  value: any = "";
  IsSearchOpen: boolean = false;
  @Output() searchEvent = new EventEmitter<{ key: string, value: any }>();
  @Input() keysArray: string[] = [];
  constructor() { }

ngOnInit() {
    this.searchSubject.pipe(
        debounceTime(300),
        distinctUntilChanged()
    ).subscribe(() => {
        this.SearchByKeyValue();
    });
}

  SearchToggle() {
    this.IsSearchOpen = !this.IsSearchOpen;
  }

  SearchByKeyValue() {
    this.searchEvent.emit({ key: this.key, value: this.value });
  }

  formatKey(key: string): string {
    return key.replace(/([A-Z])/g, ' $1').replace(/^./, (str) => str.toUpperCase());
  }

  onInputValueChange() {
    this.value=""
    this.SearchByKeyValue()
  }
  
}
