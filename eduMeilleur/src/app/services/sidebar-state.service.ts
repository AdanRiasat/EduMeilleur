import { inject, Injectable, signal } from '@angular/core';
import { GlobalService } from './global.service';

export type SidebarContent = 'base' | 'subject' | 'chatbot';

@Injectable({
  providedIn: 'root'
})
export class SidebarStateService {
  globalService = inject(GlobalService)
  private activeSidebarContent = signal<SidebarContent>('base')
  activeSidebarContent$ = this.activeSidebarContent.asReadonly()

  isMobileSidebar: boolean = this.globalService.isMobileInterface

  setActiveSidebar(content: SidebarContent) {
    this.activeSidebarContent.set(content)
  }

  getActiveSidebar() {
    return this.activeSidebarContent();
  }

  constructor() { }
}
