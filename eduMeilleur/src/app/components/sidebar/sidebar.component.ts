import { Component, effect, EventEmitter, Input, OnInit, Output, signal } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterLink } from '@angular/router';
import { ModalService } from '../../services/modal.service';
import { CommonModule } from '@angular/common';
import { SubjectSidebarComponent } from '../subject-sidebar/subject-sidebar.component';
import { SidebarStateService } from '../../services/sidebar-state.service';
import { DisplaySujet } from '../../models/displaySujet';
import { Item } from '../../models/Item';
import { SujetService } from '../../services/sujet.service';
import { Subject, takeUntil, filter } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, CommonModule, SubjectSidebarComponent],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent implements OnInit{
  @Input() isOpen: boolean = false;
  @Output() close = new EventEmitter<void>()

  readonly DEFAULT_TYPE: string = "Notes"

  subject: DisplaySujet | null = null;
  subjectId: number = -1;
  chapters: string[] = [];
  allItems: Item[] = [];
  currentItem: Item | null = null;
  currentType: string = ""

  hasSubjectId = signal<boolean>(false)

  activeSidebarContent = 'base'

  private destroy$ = new Subject<void>();

  constructor(public modalService: ModalService, public router: Router, public sidebarStateService: SidebarStateService, private subjectService: SujetService, private route: ActivatedRoute) {
    effect(() => {
      this.activeSidebarContent = this.sidebarStateService.getActiveSidebar();
    });
  }

  async ngOnInit() {
    if (window.innerWidth > 876) return

    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      takeUntil(this.destroy$)
    ).subscribe(async () => {
      this.getSubjectId();
      if (this.hasSubjectId()) {
        await this.loadSubjectData(this.subjectId);
        this.openSubjectSidebar()
      } else {
        this.resetSubjectData();
      }
    });

      this.getSubjectId();
      if (this.hasSubjectId()) {
        await this.loadSubjectData(this.subjectId);
        this.openSubjectSidebar()
      } else {
        this.resetSubjectData();
      }
    
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  closeSidebar() {
    this.close.emit()
  }

  async loadSubjectData(id: number) {
    try {
      this.subject = await this.subjectService.getSujet(this.subjectId)
      this.chapters = this.subject.chapters
      
      await this.getAllItems(this.DEFAULT_TYPE)
      await this.getCurrentItem(id)
    } catch (error) {
      console.error('what the sigma', error);
    }
  }

  async getAllItems(type: string){
    this.allItems = await this.subjectService.getAllItems(this.subjectId, type)
    this.currentType = type
  }

  async getCurrentItem(id: number){
    this.currentItem = await this.subjectService.getItem(id, this.currentType)
    this.subjectService.formatMessage(this.currentItem!.content)
  }

  getSubjectId() {
    let id = this.router.routerState.snapshot.root.firstChild?.paramMap.get('id')
    console.log(id);
    
    if (id){
      this.subjectId = parseInt(id)
      this.hasSubjectId.set(true)
    } else {
      this.subjectId = -1 
      this.hasSubjectId.set(false)
    }
  }

  openSubjectSidebar() {
    this.sidebarStateService.setActiveSidebar('subject')
  }

  openBaseSidebar() {
    this.sidebarStateService.setActiveSidebar('base')
  }

  openChatbotSidebar() {
    this.sidebarStateService.setActiveSidebar('chatbot')
  }

  resetSubjectData() {
    this.subject = null;
    this.chapters = [];
    this.allItems = [];
    this.currentItem = null;
    this.subjectId = -1;
    this.openBaseSidebar();
  }

}
