import { Component, OnInit } from '@angular/core';
import { DisplaySujet } from '../models/displaySujet';
import { SujetService } from '../services/sujet.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SpinnerService } from '../services/spinner.service';
import { Modal } from 'bootstrap';
import { ModalComponent } from '../modal/modal.component';

@Component({
  selector: 'app-sujets',
  standalone: true,
  imports: [CommonModule,RouterModule, ModalComponent],
  templateUrl: './sujets.component.html',
  styleUrl: './sujets.component.css'
})
export class SujetsComponent implements OnInit{
  sujets: DisplaySujet[] = []
  displaySujets: DisplaySujet[] = []

  constructor(public sujetService: SujetService, public spinner: SpinnerService) {}

  async ngOnInit(){
    await this.getSujets()
    this.displaySujets = this.sujets
  }

  async getSujets(){
    try {
      this.sujets = await this.sujetService.getSujets()
    } catch{
      this.openErrorModal()
    }
    
  }

  sort(type: string){
    if (type == "All"){
      this.displaySujets = this.sujets
      return
    }

    this.displaySujets = []

    for(let s of this.sujets){
      if (s.type == type){
        this.displaySujets.push(s)
      }
    }
  }

  openErrorModal(){
    let modalElement = document.getElementById("error500Modal")
    if (modalElement){
      let modal = new Modal(modalElement)
      modal.show()
      this.spinner.hide()
    }
  }
}
