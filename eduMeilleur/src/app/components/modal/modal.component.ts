import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Modal } from 'bootstrap';

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

    let modalElement = document.getElementById(this.modalId)
    if (modalElement){
      let modal = Modal.getInstance(modalElement) || new Modal(modalElement);
      modal.hide()
    }
  }
}
