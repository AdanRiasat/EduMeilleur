import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AiService } from '../services/ai.service';
import { ChatMessage } from '../models/chatMessage';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { marked } from 'marked';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Chat } from '../models/chat';

@Component({
  selector: 'app-ai',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ai.component.html',
  styleUrl: './ai.component.css'
})
export class AiComponent implements OnInit{

  userMessage: string = ""
  messages: ChatMessage[] = []
  chats: Chat[] = []
  currentChat: Chat | null = null

  isLoading: boolean = false
  loadingId: number = -1

  constructor(public aiService: AiService, public sanitizer: DomSanitizer) {}

  @ViewChild('scrollContainer') private scrollContainer!: ElementRef;
  
  async ngOnInit(){
    await this.getChats()
  }

  scrollToBottom() {
  try {
    setTimeout(() => {
      this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
    }, 50);
  } catch (err) {}
}

  async getChats(){
    this.chats = await this.aiService.getChats()
  }

  async getMessages(chat: Chat){
    this.currentChat = chat
    this.messages = await this.aiService.getMessages(chat.id)
    this.scrollToBottom()
  }

  async sendMessage() {
    let text = this.userMessage.trim()
    console.log(text);

    let userMsg: ChatMessage = {
      text: text,
      isUser: true,
      timeStamp: new Date(),
    }
    this.messages.push(userMsg)

    this.userMessage = ""
    this.isLoading = true
    

    if (this.currentChat == null){
      this.currentChat = await this.aiService.postChat(text)
      await this.getChats()
    }
    
    if (this.currentChat != null){
      this.loadingId = this.currentChat.id
      let botReply = await this.aiService.sendMessage(text, this.currentChat)
      this.messages.push(botReply)
      this.scrollToBottom()
    }

    this.isLoading = false
    this.loadingId = -1

  }

  newChat(){
    this.currentChat = null
    this.messages = []
  }

  formatMessage(message: string): SafeHtml {
    let rawHtml: string = marked.parse(message) as string
    return this.sanitizer.bypassSecurityTrustHtml(rawHtml);
  }
}

