import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Chat } from '../../models/chat';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-chatbot-sidebar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './chatbot-sidebar.component.html',
  styleUrl: './chatbot-sidebar.component.css'
})
export class ChatbotSidebarComponent {
  @Input() chats: Chat[] = []
  @Input() currentChat: Chat | null = null
  @Input() dropdownOpen: number | null = null
  @Input() isMobileSidebar: boolean = false
  @Output() newChat = new EventEmitter<void>()
  @Output() getMessages = new EventEmitter<Chat>()
  @Output() toggleDropdown = new EventEmitter<number>()
  @Output() openDeleteModal = new EventEmitter<number>()
}
