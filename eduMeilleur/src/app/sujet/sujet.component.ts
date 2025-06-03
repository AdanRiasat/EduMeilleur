import { Component, OnInit } from '@angular/core';
import { SujetService } from '../services/sujet.service';
import { ActivatedRoute } from '@angular/router';
import { DisplaySujet } from '../models/displaySujet';

@Component({
  selector: 'app-sujet',
  standalone: true,
  imports: [],
  templateUrl: './sujet.component.html',
  styleUrl: './sujet.component.css'
})
export class SujetComponent implements OnInit{
  
  sujet: DisplaySujet | null = null

  constructor(public sujetService: SujetService, public route: ActivatedRoute){}

  async ngOnInit() {
    let id: string | null = this.route.snapshot.paramMap.get("id")
    if (id != null){
      this.sujet = await this.sujetService.getSujet(parseFloat(id))
    }
  }

  
}
