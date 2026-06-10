import { Component, EventEmitter, Input, input, Output } from '@angular/core';
import { DisplaySujet } from '../../models/displaySujet';
import { CommonModule } from '@angular/common';
import { Item } from '../../models/Item';

@Component({
  selector: 'app-subject-sidebar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './subject-sidebar.component.html',
  styleUrl: './subject-sidebar.component.css',
})
export class SubjectSidebarComponent {
  @Input() subject: DisplaySujet | null = null;
  @Input() chapters: string[] = [];
  @Input() allItems: Item[] = [];
  @Input() currentItemId: number | null = null;
  @Input() currentType: string = '';
  @Output() getAllItems = new EventEmitter<string>();
  @Output() getCurrentItem = new EventEmitter<number>();
}
