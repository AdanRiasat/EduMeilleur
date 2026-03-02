import { Component, OnInit } from '@angular/core';
import { SujetService } from '../../services/sujet.service';
import { ActivatedRoute } from '@angular/router';
import { DisplaySujet } from '../../models/displaySujet';
import { Item } from '../../models/Item';
import { CommonModule } from '@angular/common';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { MarkdownService } from '../../services/markdown.service';
import { SubjectSidebarComponent } from '../../components/subject-sidebar/subject-sidebar.component';
import { SidebarStateService } from '../../services/sidebar-state.service';

@Component({
  selector: 'app-sujet',
  standalone: true,
  imports: [CommonModule, SubjectSidebarComponent],
  templateUrl: './sujet.component.html',
  styleUrl: './sujet.component.css'
})
export class SujetComponent implements OnInit{ // TODO might need ro refactor logic into service
  sujet: DisplaySujet | null = null
  chapters: string[] = []
  id: number = 0 //this is subjectId

  allItems: Item[] = []
  currentItem: Item | null = null
  currentType: string = ""

  constructor(public sujetService: SujetService, public route: ActivatedRoute, public sanitizer: DomSanitizer, public markdown: MarkdownService, private sidebarStateService: SidebarStateService){}

  async ngOnInit() {
    let sujetIdStringData: string | null = this.route.snapshot.paramMap.get("id")
    if (!sujetIdStringData){
      // TODO reroute
      return
    }

    if (window.innerWidth <= 876) return
    console.log('web interface');
    await this.loadItems(sujetIdStringData)
  }

  async loadItems(idData: string){
    this.id = parseFloat(idData)
    this.sujet = await this.sujetService.getSujet(this.id)
    this.chapters = this.sujet.chapters
    await this.getAllItems("Notes")
    await this.getCurrentItem(this.allItems[0].id)
  }

  async getAllItems(type: string){
    this.allItems = await this.sujetService.getAllItems(this.id, type)
    this.currentType = type
  }

  async getCurrentItem(id: number){
    this.currentItem = await this.sujetService.getItem(id, this.currentType)
    this.sujetService.formatMessage(this.currentItem!.content)
  }
}


