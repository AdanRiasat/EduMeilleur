import { Component, OnInit } from '@angular/core';
import { SujetService } from '../services/sujet.service';
import { ActivatedRoute } from '@angular/router';
import { DisplaySujet } from '../models/displaySujet';
import { Item } from '../models/Item';
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
  id: number = 0 //this is subjectId

  allItems: Item[] = []
  currentItem: Item | null = null
  currentType: string = ""

  constructor(public sujetService: SujetService, public route: ActivatedRoute, public sanitizer: DomSanitizer){}

  async ngOnInit() {
    let sujetIdStringData: string | null = this.route.snapshot.paramMap.get("id")
    if (sujetIdStringData != null){
      this.id = parseFloat(sujetIdStringData)
      this.sujet = await this.sujetService.getSujet(this.id)
      await this.getAllItems("Notes")
    }
  }

  async getAllItems(type: string){
    this.allItems = await this.sujetService.getAllItems(this.id, type)
    console.log(this.allItems);
    this.currentType = type
  }

  async getCurrentItem(id: number, type: string){
    this.currentItem = await this.sujetService.getItem(id, type)
  }

  formatMessage(message: string): SafeHtml {
      const rawHtml: string = marked.parse(message) as string
      return this.sanitizer.bypassSecurityTrustHtml(rawHtml);
  }
}
