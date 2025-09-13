import { Injectable } from '@angular/core';
import { Modal } from 'bootstrap';
import { SpinnerService } from './spinner.service';

@Injectable({
  providedIn: 'root',
})
export class ModalService {
  constructor(public spinner: SpinnerService) {}

  OpenModal(elementId: string) {
    let modalElement = document.getElementById(elementId);
    if (modalElement) {
      let modal = Modal.getInstance(modalElement) || new Modal(modalElement);
      modal.show();
    }
    this.spinner.hide();
  }
}
