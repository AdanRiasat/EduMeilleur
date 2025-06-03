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

  constructor(public sujetService: SujetService) {}

  async ngOnInit(){
    this.sujets = await this.sujetService.getSujets()
  }
}
