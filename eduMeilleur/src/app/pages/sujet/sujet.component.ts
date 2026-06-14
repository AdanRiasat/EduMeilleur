import { Component, HostListener, OnInit, signal } from '@angular/core';
import { SujetService } from '../../services/sujet.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DisplaySujet } from '../../models/displaySujet';
import { Item } from '../../models/Item';
import { CommonModule } from '@angular/common';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { MarkdownService } from '../../services/markdown.service';
import { SubjectSidebarComponent } from '../../components/subject-sidebar/subject-sidebar.component';

@Component({
  selector: 'app-sujet',
  standalone: true,
  imports: [CommonModule, SubjectSidebarComponent],
  templateUrl: './sujet.component.html',
  styleUrl: './sujet.component.css',
})
export class SujetComponent implements OnInit {
  // TODO might need ro refactor logic into service
  sujet: DisplaySujet | null = null;
  chapters: string[] = [];
  id: number = 0;

  allItems: Item[] = [];
  currentType: string = '';

  isLoadingContent = signal<boolean>(true);
  isExpanded: boolean = false;

  constructor(
    public sujetService: SujetService,
    public route: ActivatedRoute,
    public sanitizer: DomSanitizer,
    public markdown: MarkdownService,
  ) {}

  async ngOnInit() {
    let sujetIdStringData: string | null = this.route.snapshot.paramMap.get('id');
    if (!sujetIdStringData) {
      // TODO reroute
      return;
    }

    if (window.innerWidth <= 876) return;
    console.log('web interface');

    this.sujetService.selectedContent.set('');

    try {
      await this.loadItems(sujetIdStringData);
    } catch (e) {
      console.log(e);
    }

    this.isLoadingContent.set(false);
  }

  async loadItems(idData: string) {
    this.id = parseFloat(idData);
    this.sujet = await this.sujetService.getSujet(this.id);
    this.chapters = this.sujet.chapters;
    await this.getAllItems('Notes');
    await this.getCurrentItem(this.allItems[0].id);
  }

  async getAllItems(type: string) {
    this.allItems = await this.sujetService.getAllItems(this.id, type);
    this.currentType = type;
  }

  async getCurrentItem(id: number, type?: string) {
    this.isLoadingContent.set(true);

    if (!type) type = this.currentType;

    try {
      await this.sujetService.getItem(id, type);
      this.sujetService.formatMessage(this.sujetService.currentItem()!.content);
    } catch (e) {
      this.sujetService.selectedContent.set('');
      console.log(e);
    }

    this.isLoadingContent.set(false);
    this.currentType = type;
  }

  async refreshItems(id: number, type: string) {
    await this.getAllItems(type);
    await this.getCurrentItem(id, type);
  }

  onCtaClick(event: MouseEvent) {
    if (window.innerWidth <= 867 && !this.isExpanded) {
      this.isExpanded = true;
      event.stopPropagation();
      return;
    }

    this.isExpanded = false;
    const item = this.sujetService.currentItem()!;
    this.refreshItems(item.relatedItems[0].id, item.relatedType);
  }

  @HostListener('document:click')
  onDocumentClick() {
    if (this.isExpanded) this.isExpanded = false;
  }
}
