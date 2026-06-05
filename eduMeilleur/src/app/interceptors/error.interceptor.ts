import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { ToastService } from '../services/toast.service';
import { ModalService } from '../services/modal.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastService = inject(ToastService);
  const modalService = inject(ModalService);

  const handleError = (error: HttpErrorResponse) => {
    setTimeout(() => {
      if (!modalService.isAnyModalOpen()) {
        if (!navigator.onLine) {
          toastService.error('The request failed due to a lack of internet connection.');
        } else if (error.status === 0) {
          toastService.error('Unable to reach the server, please try again later.');
        } else if (error.status === 500) {
          toastService.error('Something went wrong on our end, please try again later.');
        }
      }
    }, 500);

    return throwError(() => error);
  };

  return next(req).pipe(catchError(handleError));
};
