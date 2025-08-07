import { Injectable } from '@angular/core';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';


@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  constructor(private spinner: NgxSpinnerService) { }

  show(){
    this.spinner.show(undefined, {
      type: 'ball-clip-rotate',
      size: 'medium',
      bdColor: 'rgba(0,0,16,0.8)',
      color: '#fff',
      fullScreen: true
    })
  }

  hide() {
    this.spinner.hide()
  }
}
