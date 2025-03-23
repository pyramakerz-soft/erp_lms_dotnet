// reuse-table.component.ts
import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DatePipe } from '@angular/common'; // Import DatePipe

@Component({
  selector: 'app-table',
  imports: [CommonModule , DatePipe],
  standalone: true,
  templateUrl: './reuse-table.component.html',
  styleUrls: ['./reuse-table.component.css']
})
export class TableComponent {

  @Input() headers: string[] = []; // Table headers
  @Input() data: any[] = []; // Table data
  @Input() keys: string[] = []; // Keys to display in the table
  @Input() showViewAction: boolean = false; // Control visibility of the eye icon

  @Output() delete = new EventEmitter<any>(); // Emit delete event
  @Output() edit = new EventEmitter<any>(); // Emit edit event
  @Output() view = new EventEmitter<any>(); // Emit view event

  // Handle delete action
  onDelete(row: any) {
    this.delete.emit(row);
  }

  // Handle edit action
  onEdit(row: any) {
    this.edit.emit(row);
  }

  // Handle view action
  onView(row: any) {
    this.view.emit(row);
  }
}