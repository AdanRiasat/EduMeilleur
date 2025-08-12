import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { DisplaySujet } from '../models/displaySujet';
import { SujetService } from '../services/sujet.service';
import { RouterModule } from '@angular/router';
import { Modal } from 'bootstrap';
import { ModalComponent } from '../modal/modal.component';

@Component({
  selector: 'app-image-slider',
  standalone: true,
  imports: [CommonModule, RouterModule, ModalComponent],
  templateUrl: './image-slider.component.html',
  styleUrl: './image-slider.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA] 
})
export class ImageSliderComponent implements OnInit{
   sujets: DisplaySujet[] = []
  
  constructor(public sujetService: SujetService){}

  async ngOnInit() {
    this.sujets = await this.sujetService.getSujets()
    if (this.sujets.length == 0){
      this.openErrorModal()
    }
  }

   openErrorModal(){
    let modalElement = document.getElementById('error500Modal')
    if (modalElement){
      let modal = new Modal(modalElement)
      modal.show()
    }
  }
}