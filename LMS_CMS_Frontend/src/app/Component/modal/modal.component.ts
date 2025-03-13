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
  @Input() title: string = '';
  @Input() isEditMode: boolean = false;
  @Input() buttonText: string = 'Create';
  @Output() save = new EventEmitter<void>();
  @Output() close = new EventEmitter<void>();

  closeModal() {
    this.close.emit();
  }

  onSave() {
    this.save.emit();
  }
}