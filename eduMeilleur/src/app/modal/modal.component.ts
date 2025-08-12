import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.css'
})
export class ModalComponent {
  @Input() modalId!: string;
  @Input() title!: string;
  @Input() body!: string;
  @Input() cancelText = 'Cancel';
  @Input() confirmText = 'Confirm';
  
  @Output() confirmed = new EventEmitter<void>();

  confirm() {
    this.confirmed.emit();
  }
}
