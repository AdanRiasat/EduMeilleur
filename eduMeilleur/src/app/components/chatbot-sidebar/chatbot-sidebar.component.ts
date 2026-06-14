import { Component, EventEmitter, input, Input, OnInit, Output } from '@angular/core';
import { Chat } from '../../models/chat';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-chatbot-sidebar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chatbot-sidebar.component.html',
  styleUrl: './chatbot-sidebar.component.css'
})
export class ChatbotSidebarComponent implements OnInit{
  @Input() chats: Chat[] = []
  @Input() currentChat: Chat | null = null
  @Input() dropdownOpen: number | null = null
  @Input() isMobileSidebar: boolean = false
  @Input() selectedModel: string = ""
  @Output() selectedModelChange = new EventEmitter<string>()
  @Output() newChat = new EventEmitter<void>()
  @Output() getMessages = new EventEmitter<Chat>()
  @Output() toggleDropdown = new EventEmitter<number>()
  @Output() openDeleteModal = new EventEmitter<number>()

  private readonly STORAGE_KEY = "modelName"
  private readonly DEFAULT_MODEL = "openai/gpt-oss-120b:free"

  ngOnInit() {
    let modelNameData = localStorage.getItem(this.STORAGE_KEY)
    if (!modelNameData) modelNameData = this.DEFAULT_MODEL

    this.selectedModel = modelNameData
    this.selectedModelChange.emit(modelNameData)
    
  }

  onModelChange(value: string) {
    localStorage.setItem(this.STORAGE_KEY, value)
    this.selectedModelChange.emit(value)
  }
}
