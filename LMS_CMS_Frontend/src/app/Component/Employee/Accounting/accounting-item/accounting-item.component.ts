import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountingTreeChart } from '../../../../Models/Accounting/accounting-tree-chart';

@Component({
  selector: 'app-accounting-item',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './accounting-item.component.html',
  styleUrl: './accounting-item.component.css'
})
export class AccountingItemComponent {
  @Input() accountingData: any;
  @Output() Selected: EventEmitter<number> = new EventEmitter<number>();
  
  toggleChildren(accounting: any) {
    accounting.isOpen = !accounting.isOpen;
  }

  getData(accounting: AccountingTreeChart){
    if(accounting.id)
    this.Selected.emit(accounting.id);
  }
}
