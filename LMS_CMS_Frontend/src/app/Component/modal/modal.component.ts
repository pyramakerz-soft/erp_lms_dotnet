import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent {
  @Input() title: string = ''; // Title of the modal
  @Input() isEditMode: boolean = false; // Whether the modal is in edit mode
  @Input() buttonText: string = 'Create'; // Dynamic button text
  @Output() save = new EventEmitter<void>(); // Emit when the save button is clicked
  @Output() close = new EventEmitter<void>(); // Emit when the modal is closed

  // Close the modal
  closeModal() {
    this.close.emit();
  }

  // Save action
  onSave() {
    this.save.emit();
  }
}