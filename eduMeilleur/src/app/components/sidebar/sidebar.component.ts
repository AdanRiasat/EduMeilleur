import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ModalService } from '../../services/modal.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  @Input() isOpen: boolean = false;
  @Output() close = new EventEmitter<void>()

  constructor(public modalService: ModalService, public router: Router) {}

  closeSidebar() {
    this.close.emit()
  }

  hasSubjectId(): boolean {
    return !!this.router.routerState.snapshot.root.firstChild?.paramMap.get('id')
  }

}
