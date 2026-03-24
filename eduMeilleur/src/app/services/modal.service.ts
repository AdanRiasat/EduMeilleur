import { Injectable } from '@angular/core';
import { Modal } from 'bootstrap';
import { SpinnerService } from './spinner.service';

@Injectable({
  providedIn: 'root',
})
export class ModalService {
  constructor(public spinner: SpinnerService) {}

  openModal(elementId: string) {
    document.querySelectorAll('.modal-backdrop').forEach(el => el.remove());
    document.body.classList.remove('modal-open');
    
    let modalElement = document.getElementById(elementId);
    if (modalElement) {
      let modal = Modal.getInstance(modalElement) || new Modal(modalElement);
      modal.show();
    }

   
    this.spinner.hide();
  }
}
