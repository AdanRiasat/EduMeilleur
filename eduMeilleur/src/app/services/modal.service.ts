import { Injectable, signal } from '@angular/core';
import { Modal } from 'bootstrap';
import { SpinnerService } from './spinner.service';

@Injectable({
  providedIn: 'root',
})
export class ModalService {
  constructor(public spinner: SpinnerService) {}

  retryAction: (() => void) | null = null;

  isAnyModalOpen(): boolean {
    return document.querySelector('.modal.show') !== null;
  }

  openErrorModal(retryAction?: () => void) {
    this.retryAction = retryAction ?? null;
    this.openModal('error500Modal');
  }

  executeRetry() {
    this.retryAction?.();
    this.retryAction = null;
  }

  openModal(elementId: string) {
    document.querySelectorAll('.modal-backdrop').forEach((el) => el.remove());
    document.body.classList.remove('modal-open');

    this.spinner.show();
    let modalElement = document.getElementById(elementId);
    if (modalElement) {
      let modal = Modal.getInstance(modalElement) || new Modal(modalElement);
      modal.show();
    }
    this.spinner.hide();
  }
}
