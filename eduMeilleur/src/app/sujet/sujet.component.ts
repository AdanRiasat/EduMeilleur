import { Component, OnInit } from '@angular/core';
import { SujetService } from '../services/sujet.service';
import { ActivatedRoute } from '@angular/router';
import { DisplaySujet } from '../models/displaySujet';
import { Notes } from '../models/notes';
import { CommonModule } from '@angular/common';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { marked } from 'marked';

@Component({
  selector: 'app-sujet',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sujet.component.html',
  styleUrl: './sujet.component.css'
})
export class SujetComponent implements OnInit{
  
  sujet: DisplaySujet | null = null
  id: string | null = null

  allNotes: Notes[] = []
  currentNotes: Notes | null = null

  constructor(public sujetService: SujetService, public route: ActivatedRoute, public sanitizer: DomSanitizer){}

  async ngOnInit() {
    this.id = this.route.snapshot.paramMap.get("id")
    if (this.id != null){
      this.sujet = await this.sujetService.getSujet(parseFloat(this.id))
      await this.getAllNotes(parseFloat(this.id))
    }

    

  }

  async getAllNotes(id: number){
    this.allNotes = await this.sujetService.getAllNotes(id)
    console.log(this.allNotes);
  }

  async getCurrentNotes(id: number){
    this.currentNotes = await this.sujetService.getNotes(id)
  }

  formatMessage(message: string): SafeHtml {
      const rawHtml: string = marked.parse(message) as string
      return this.sanitizer.bypassSecurityTrustHtml(rawHtml);
    }

  
}
