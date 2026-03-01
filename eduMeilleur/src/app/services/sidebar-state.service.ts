import { Injectable, signal } from '@angular/core';

export type SidebarContent = 'base' | 'subject' | 'chatbot';

@Injectable({
  providedIn: 'root'
})
export class SidebarStateService {
  private activeSidebarContent = signal<SidebarContent>('base')
  activeSidebarContent$ = this.activeSidebarContent.asReadonly()

  setActiveSidebar(content: SidebarContent) {
    this.activeSidebarContent.set(content)
  }

  getActiveSidebar() {
    return this.activeSidebarContent();
  }

  constructor() { }
}
