import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-table',
  imports:[CommonModule],
  standalone:true,
  templateUrl: './reuse-table.component.html',
  styleUrls: ['./reuse-table.component.css']
})

export class TableComponent {

  @Input() headers: string[] = []; // Table headers
  @Input() data: any[] = []; // Table data
  @Input() keys: string[] = []; // Keys to display in the table
  @Output() delete = new EventEmitter<any>(); // Emit delete event
  @Output() edit = new EventEmitter<any>(); // Emit edit event

  // Handle delete action
  onDelete(row: any) {
    this.delete.emit(row);
  }

  // Handle edit action
  onEdit(row: any) {
    this.edit.emit(row);
  }
}
