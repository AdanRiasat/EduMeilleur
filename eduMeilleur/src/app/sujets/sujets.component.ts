import { Component, OnInit } from '@angular/core';
import { DisplaySujet } from '../models/displaySujet';
import { SujetService } from '../services/sujet.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sujets',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './sujets.component.html',
  styleUrl: './sujets.component.css'
})
export class SujetsComponent implements OnInit{
  sujets: DisplaySujet[] = []
  displaySujets: DisplaySujet[] = []

  constructor(public sujetService: SujetService) {}

  async ngOnInit(){
    await this.getSujets()
    this.displaySujets = this.sujets
  }

  async getSujets(){
     this.sujets = await this.sujetService.getSujets()
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
}
